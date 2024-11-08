using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DataFlowTextProcess
{
    class Program
    {
        static void Main(string[] args)
        {           
            Task completionTask;
            ITargetBlock<string> startBlock = CreateTextProcessingPipeline(args[0], out completionTask);

            startBlock.Post(args[0]);

            completionTask.Wait();

            Console.WriteLine("Done. Exiting");
        }

        private static readonly HashSet<string> IgnoreWords = new HashSet<string>() { "a", "an", "the", "and", "of", "to" };
        private static readonly Regex WordRegex = new Regex("[a-zA-Z]+", RegexOptions.Compiled);

        private static ITargetBlock<string> CreateTextProcessingPipeline(string inputPath, out Task completionTask)
        {
            int fileCount = Directory.GetFiles(inputPath, "*.txt").Length;

            var getFilenames = new TransformManyBlock<string, string>(path =>
            {
                return Directory.GetFiles(path, "*.txt");
            });

            var getFileContents = new TransformBlock<string, string>(async (filename) =>
            {
                Console.WriteLine("Begin: getFileContents");
                using (var streamReader = new StreamReader(filename))
                {
                    return await streamReader.ReadToEndAsync();
                }
            }, new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = Environment.ProcessorCount });

            var analyzeContents = new TransformBlock<string, Dictionary<string, ulong>>(contents =>
            {
                Console.WriteLine("Begin: analyzeContents");
                var frequencies = new Dictionary<string, ulong>(10000, StringComparer.OrdinalIgnoreCase);
                var matches = WordRegex.Matches(contents);
                foreach (Match match in matches)
                {
                    ulong currentValue;
                    if (!frequencies.TryGetValue(match.Value, out currentValue))
                    {
                        currentValue = 0;
                    }
                    frequencies[match.Value] = currentValue + 1;
                }
                Console.WriteLine("End: analyzeContents");
                return frequencies;
            }, new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = Environment.ProcessorCount });

            var eliminateIgnoredWords = new TransformBlock<Dictionary<string, ulong>, Dictionary<string, ulong>>(input =>
            {
                foreach(var word in IgnoreWords)
                {
                    input.Remove(word);
                }
                return input;
            });

            var batch = new BatchBlock<Dictionary<string, ulong>>(fileCount);

            // This is one point of synchronization -- all processing converges on this     
            var combineFrequencies = new TransformBlock<Dictionary<string, ulong>[], List<KeyValuePair<string, ulong>>>(inputs =>
            {
                Console.WriteLine("Begin: combiningFrequencies");
                var sortedList = new List<KeyValuePair<string, ulong>>();
                var combinedFrequencies = new Dictionary<string, ulong>(10000, StringComparer.OrdinalIgnoreCase);
                foreach (var input in inputs)
                {
                    foreach (var kvp in input)
                    {                        
                        ulong currentFrequency;
                        if (!combinedFrequencies.TryGetValue(kvp.Key, out currentFrequency))
                        {
                            currentFrequency = 0;
                        }
                        combinedFrequencies[kvp.Key] = currentFrequency + kvp.Value;
                    }
                }
                foreach(var kvp in combinedFrequencies)
                {
                    sortedList.Add(kvp);
                }
                sortedList.Sort((a, b) => 
                {
                    return -a.Value.CompareTo(b.Value);
                });
                Console.WriteLine("End: combineFrequencies");
                
                return sortedList;
            }, new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 1 });

            var printTopTen = new ActionBlock<List<KeyValuePair<string, ulong>>>(input =>
            {
                for (int i=0;i<10;i++)
                {
                    Console.WriteLine($"{input[i].Key} - {input[i].Value}");
                }
                getFilenames.Complete();
            });

            // Hook up blocks
            getFilenames.LinkTo(getFileContents);
            getFileContents.LinkTo(analyzeContents);
            analyzeContents.LinkTo(eliminateIgnoredWords);
            eliminateIgnoredWords.LinkTo(batch);
            batch.LinkTo(combineFrequencies);
            combineFrequencies.LinkTo(printTopTen);
            
            completionTask = getFilenames.Completion;

            return getFilenames;
        }
    }
}
