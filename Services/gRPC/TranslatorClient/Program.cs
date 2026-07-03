using System.Diagnostics;
using Grpc.Net.Client;
using TranslatorClient;

var words = new List<string>
{
   "red", "yellow", "green"
};

using var channel = GrpcChannel.ForAddress("https://localhost:7279");
var client = new Translator.TranslatorClient(channel);

words.ForEach(async void (word) =>
{
   try
   {
      var request = new Request { Word = word };
      var response = await client.TranslateAsync(request);
      Console.WriteLine($"{response.Word} : {response.Translation}");
   }
   catch (Exception commonEx)
   {
      Debug.WriteLine(commonEx.Message);
   }
});

Console.ReadKey();