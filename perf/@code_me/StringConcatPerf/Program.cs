using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;

namespace StringConcatPerf
{
    public class StringConcatTest
    {
        private string[] source;

        [Params(10, 100)]
        public int NumStrings { get; set; }

        [Params(1, 10)]
        public int StringLength { get; set; }

        [GlobalSetup]
        public void SetupSource()
        {
            this.source = new string[this.NumStrings];
            for (int i=0;i<this.source.Length;i++)
            {
                this.source[i] = new string('a', this.StringLength);
            }
        }

        [Benchmark(Baseline = true)]
        public string  StaticConcatLiteral()
        {
            var result =
                "aaaaaaaaaa" +
                "bbbbbbbbbb" +
                "cccccccccc" +
                "dddddddddd" +
                "eeeeeeeeee" +
                "ffffffffff" +
                "gggggggggg" +
                "hhhhhhhhhh" +
                "iiiiiiiiii" +
                "jjjjjjjjjj";
            return result;
        }

        [Benchmark]
        public string StaticConcat()
        {
            var result = source[0] +
                source[1] +
                source[2] +
                source[3] +
                source[4] +
                source[5] +
                source[6] +
                source[7] +
                source[8] +
                source[9];
            return result;
        }
        
        [Benchmark]
        public string Format()
        {
            var result = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}", 
                source[0], source[1], source[2], source[3], source[4], 
                source[5], source[6], source[7], source[8], source[9]);
            return result;
        }

        [Benchmark]
        public string Concat()
        {
            return string.Concat(this.source);
        }

        [Benchmark]
        public string Join()
        {
            return string.Join(string.Empty, this.source);
        }

        [Benchmark]
        public string InitializedStringBuilder()
        {
            var sb = new StringBuilder(this.source.Length * this.source[0].Length);
            for(int i=0;i<source.Length;i++)
            {
                sb.Append(source[i]);
            }
            var result = sb.ToString();
            return result;
        }

        [Benchmark]
        public string UninitializedStringBuilder()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {
                sb.Append(source[i]);
            }
            var result = sb.ToString();
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {            
            var summary = BenchmarkRunner.Run<StringConcatTest>();           
        }
    }
}
