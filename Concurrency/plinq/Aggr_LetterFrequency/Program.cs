SequenceFreq();
FuncFreq();
ParallelFreq();

return;

// 1. Sequence version
void SequenceFreq()
{
   const string text = "Let’s suppose this is a really long string";
   var letterFrequencies = new int[26];
   foreach (var index in text.Select(chr => char.ToUpper(chr) - 'A')
               .Where(index => index is >= 0 and < 26))
   {
      letterFrequencies[index]++;
   }

   Array.ForEach(letterFrequencies, item => Console.Write($"{item} "));
}

// 2. functional version
void FuncFreq()
{
   const string text = "Let’s suppose this is a really long string";
   var result =
      text.Aggregate(
         new int[26], // Create the "accumulator"
         (letterFrequencies, chr) => // Aggregate a letter into the accumulator
         {
            var index = char.ToUpper(chr) - 'A';
            if (index is >= 0 and < 26)
            {
               letterFrequencies[index]++;
            }

            return letterFrequencies;
         });

   Array.ForEach(result, item => Console.Write($"{item} "));
}

// 3. Parallel version
void ParallelFreq()
{
   const string text = "Let’s suppose this is a really long string";
   var result = text.AsParallel().Aggregate(
      () => new int[26], // Create a new local accumulator
      (localFrequencies, chr) => // Aggregate into the local accumulator
      {
         var index = char.ToUpper(chr) - 'A';
         if (index is >= 0 and < 26)
         {
            localFrequencies[index]++;
         }

         return localFrequencies;
      },
      // Aggregate local->main accumulator
      (mainFreq, localFreq) =>
         mainFreq.Zip(localFreq, (f1, f2) => f1 + f2).ToArray(),
      finalResult => finalResult // Perform any final transformation
   ); // on the end result.

   Array.ForEach(result, item => Console.Write($"{item} "));
}