using System.Text;
using BenchmarkDotNet.Attributes;

namespace StringConcat_Benchmark;

public class StringConcatPerf
{
   private string[] _source;

   [Params(10, 100)]
   public int NumStrings { get; set; }

   [Params(1, 10)]
   public int StringLength { get; set; }

   [GlobalSetup]
   public void SetupSource()
   {
      _source = new string[NumStrings];
      for (var i = 0; i < _source.Length; i++)
      {
         _source[i] = new string('a', StringLength);
      }
   }

   [Benchmark(Baseline = true)]
   public string StaticConcatLiteral()
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
      var result = _source[0] +
                   _source[1] +
                   _source[2] +
                   _source[3] +
                   _source[4] +
                   _source[5] +
                   _source[6] +
                   _source[7] +
                   _source[8] +
                   _source[9];

      return result;
   }

   [Benchmark]
   public string Format()
   {
      var result = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
         _source[0], _source[1], _source[2], _source[3], _source[4],
         _source[5], _source[6], _source[7], _source[8], _source[9]);
      return result;
   }

   [Benchmark]
   public string Concat() => string.Concat(_source);

   [Benchmark]
   public string Join() => string.Join(string.Empty, _source);

   [Benchmark]
   public string InitializedStringBuilder()
   {
      var sb = new StringBuilder(_source.Length * _source[0].Length);
      for (var i = 0; i < _source.Length; i++)
      {
         sb.Append(_source[i]);
      }

      var result = sb.ToString();
      return result;
   }

   [Benchmark]
   public string UninitializedStringBuilder()
   {
      var sb = new StringBuilder();
      for (var i = 0; i < _source.Length; i++)
      {
         sb.Append(_source[i]);
      }

      var result = sb.ToString();
      return result;
   }
}