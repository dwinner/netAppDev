using System.Diagnostics;
using Grpc.Core;

namespace TranslationSvc.Services;

public sealed class TranslatorService : Translator.TranslatorBase
{
   private readonly Dictionary<string, string> _words = new()
   {
      ["red"] = "красный",
      ["green"] = "зеленый",
      ["blue"] = "синий"
   };

   public override Task<Response> Translate(Request request, ServerCallContext context)
   {
      var word = request.Word;
      Debug.WriteLine($"Requested word: {word}");
      var translation = _words.GetValueOrDefault(word, "Not found");
      return Task.FromResult(new Response
      {
         Word = word,
         Translation = translation
      });
   }
}