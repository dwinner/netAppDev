namespace TrieDemo.Tests;

public class TrieTests
{
   private readonly Trie<Data> _trie = new();
   private string[] _contextualWords;

   [SetUp]
   public void Setup()
   {
      _contextualWords = new[]
      {
         "on",
         "variables",
         "includes",
         "char",
         "byte",
         "int",
         "long",
         "int64",
         "float",
         "double",
         "word",
         "dword",
         "qword",
         "on start",
         "on prestart",
         "on prestop",
         "on busoff",
         "on errorframe",
         "on erroractive",
         "on errorpassive",
         "on stopmeasurement",
         "on key",
         "on message",
         "on multiplexed_message",
         "on mostmessage",
         "on mostamsmessage",
         "on diagrequest",
         "on diagresponse",
         "on signal",
         "on ethernetpacket",
         "on ethernetstatus",
         "on timer",
         "on sysvar",
         "on sysvar_update",
         "on sysvar_change",
         "on envvar"
      };

      Array.ForEach(_contextualWords, word =>
      {
         var item = new Data(word);
         _trie.AddValue(word, item);
      });
   }

   [Test]
   public void TrieTest()
   {
      var values = _trie.FindValues("envvar");
      foreach (var data in values)
      {
         Console.WriteLine(data.Value);
      }
   }
}

internal class Data
{
   public Data(string value) => Value = value;

   public string Value { get; }
}