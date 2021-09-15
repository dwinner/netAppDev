using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;

namespace CsCsLang
{
   public class Translation
   {
      private static readonly string[] latUp =
      {
         "Shch", "_MZn", "_TZn", "Ts", "Ch", "Sh", "Yu", "Ya", "Ye", "Yo", "Zh", "X", "Q", "W", "A", "B", "V", "G", "D",
         "Z", "I", "J", "K", "C", "L", "M", "N", "O", "P", "R", "S", "T", "U", "F", "H", "Y", "E"
      };

      private static readonly string[] latLo =
      {
         "shch", "_mzh", "_tzn", "ts", "ch", "sh", "yu", "ya", "ye", "yo", "zh", "x", "q", "w", "a", "b", "v", "g", "d",
         "z", "i", "j", "k", "c", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "h", "y", "e"
      };

      private static readonly string[] rusUp =
      {
         "Щ", "Ъ", "Ь", "Ц", "Ч", "Ш", "Ю", "Я", "Е", "Ё", "Ж", "Кс", "Кю", "Уи", "А", "Б", "В", "Г", "Д", "З", "И",
         "Й", "К", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ы", "Э"
      };

      private static readonly string[] rusLo =
      {
         "щ", "ъ", "ь", "ц", "ч", "ш", "ю", "я", "е", "ё", "ж", "кс", "кю", "уи", "а", "б", "в", "г", "д", "з", "и",
         "й", "к", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ы", "э"
      };

      private static readonly HashSet<string> s_nativeWords = new HashSet<string>();
      private static readonly HashSet<string> s_tempWords = new HashSet<string>();

      private static readonly Dictionary<string, string> s_spellErrors =
         new Dictionary<string, string>();

      private static readonly Dictionary<string, Dictionary<string, string>> s_keywords =
         new Dictionary<string, Dictionary<string, string>>();

      private static readonly Dictionary<string, Dictionary<string, string>> s_dictionaries =
         new Dictionary<string, Dictionary<string, string>>();

      private static readonly Dictionary<string, Dictionary<string, string>> s_errors =
         new Dictionary<string, Dictionary<string, string>>();

      // The default user language. Can be changed in settings.
      private static string s_language = Constants.English;

      public static string Language
      {
         set => s_language = value;
      }

      public static void AddNativeKeyword(string word)
      {
         s_nativeWords.Add(word);
         AddSpellError(word);
      }

      public static void AddTempKeyword(string word)
      {
         s_tempWords.Add(word);
         AddSpellError(word);
      }

      public static void AddSpellError(string word)
      {
         if (word.Length > 2)
         {
            s_spellErrors[word.Substring(0, word.Length - 1)] = word;
            s_spellErrors[word.Substring(1)] = word;
         }
      }

      public static Dictionary<string, string> KeywordsDictionary(string fromLang, string toLang) =>
         GetDictionary(fromLang, toLang, s_keywords);

      public static Dictionary<string, string> TranslationDictionary(string fromLang, string toLang) =>
         GetDictionary(fromLang, toLang, s_dictionaries);

      public static Dictionary<string, string> ErrorDictionary(string lang) => GetDictionary(lang, s_dictionaries);

      private static Dictionary<string, string> GetDictionary(string fromLang, string toLang,
         Dictionary<string, Dictionary<string, string>> dictionaries)
      {
         var key = fromLang + "-->" + toLang;
         return GetDictionary(key, dictionaries);
      }

      private static Dictionary<string, string> GetDictionary(string key,
         Dictionary<string, Dictionary<string, string>> dictionaries)
      {
         Dictionary<string, string> result;
         if (!dictionaries.TryGetValue(key, out result))
         {
            result = new Dictionary<string, string>();
            dictionaries[key] = result;
         }
         return result;
      }

      public static void TryLoadDictionary(string dirname, string fromLang, string toLang)
      {
         if (string.IsNullOrEmpty(dirname) || !Directory.Exists(dirname)) return;
         var filename = Path.Combine(dirname, fromLang + "_" + toLang + ".txt");
         if (File.Exists(filename)) LoadDictionary(filename, fromLang, toLang);
         filename = Path.Combine(dirname, toLang + "_" + fromLang + ".txt");
         if (File.Exists(filename)) LoadDictionary(filename, toLang, fromLang);
      }

      public static void LoadDictionary(string filename, string fromLang, string toLang)
      {
         var dict1 = TranslationDictionary(fromLang, toLang);
         var dict2 = TranslationDictionary(toLang, fromLang);

         var lines = Utils.GetFileLines(filename);
         foreach (var line in lines)
         {
            var tokens = line.Split(" ".ToCharArray(),
               StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length < 2 || tokens[0].StartsWith("#")) continue;
            var word1 = tokens[0].Trim();
            var word2 = tokens[1].Trim();
            dict1[word1] = word2;
            dict2[word2] = word1;
         }
      }

      public static void LoadErrors(string filename)
      {
         if (!File.Exists(filename)) return;
         var dict = GetDictionary(Constants.English, s_errors);
         var lines = Utils.GetFileLines(filename);
         foreach (var line in lines)
         {
            var tokens = line.Split("=".ToCharArray(),
               StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length < 1 || tokens[0].StartsWith("#")) continue;
            if (tokens.Length == 1)
            {
               dict = GetDictionary(tokens[0], s_errors);
               continue;
            }

            dict[tokens[0].Trim()] = tokens[1].Trim();
         }
      }

      public static string TryTranslit(string fromLang, string toLang,
         string str)
      {
         if (fromLang == Constants.Russian) str = Transliterate(rusUp, latUp, rusLo, latLo, str);
         else if (toLang == Constants.Russian) str = Transliterate(latUp, rusUp, latLo, rusLo, str);

         return str;
      }

      private static string Transliterate(string[] up1, string[] up2,
         string[] lo1, string[] lo2,
         string str)
      {
         for (var i = 0; i < up1.Length; i++)
         {
            str = str.Replace(up1[i], up2[i]);
            str = str.Replace(lo1[i], lo2[i]);
         }
         return str;
      }

      public static string GetErrorString(string key)
      {
         string result = null;
         var dict = GetDictionary(s_language, s_errors);
         if (dict.TryGetValue(key, out result)) return result;

         if (s_language != Constants.English)
         {
            dict = GetDictionary(Constants.English, s_errors);
            if (dict.TryGetValue(key, out result)) return result;
         }
         return key;
      }

      public static void Add(NameValueCollection langDictionary, string origName,
         Dictionary<string, string> translations1,
         Dictionary<string, string> translations2)
      {
         AddNativeKeyword(origName);

         var translation = langDictionary[origName];
         if (string.IsNullOrWhiteSpace(translation))
         {
            // The translation is not provided for this function.
            translations1[origName] = origName;
            translations2[origName] = origName;
            return;
         }

         AddNativeKeyword(translation);

         translations1[origName] = translation;
         translations2[translation] = origName;

         if (translation.IndexOfAny(" \t\r\n".ToCharArray()) >= 0)
            throw new ArgumentException("Translation of [" + translation + "] contains white spaces");

         var origFunction = ParserFunction.GetFunction(origName);
         Utils.CheckNotNull(origName, origFunction);
         ParserFunction.RegisterFunction(translation, origFunction);

         // Also add the translation to the list of functions after which there
         // can be a space (besides a parenthesis).
         if (Constants.FunctWithSpace.Contains(origName)) Constants.FunctWithSpace.Add(translation);
         if (Constants.FunctWithSpaceOnce.Contains(origName)) Constants.FunctWithSpaceOnce.Add(translation);
      }

      public static void AddSubstatement(NameValueCollection langDictionary,
         string origName,
         List<string> keywordsArray,
         Dictionary<string, string> translations1,
         Dictionary<string, string> translations2)
      {
         var translation = langDictionary[origName];
         if (string.IsNullOrWhiteSpace(translation))
         {
            // The translation is not provided for this sub statement.
            translations1[origName] = origName;
            translations2[origName] = origName;
            return;
         }

         translations1[origName] = translation;
         translations2[translation] = origName;

         if (translation.IndexOfAny(" \t\r\n".ToCharArray()) >= 0)
            throw new ArgumentException("Translation of [" + translation + "] contains white spaces");

         keywordsArray.Add(translation);
         s_nativeWords.Add(origName);
         s_nativeWords.Add(translation);
      }

      public static void PrintScript(string script)
      {
         var item = new StringBuilder();

         var inQuotes = false;

         for (var i = 0; i < script.Length; i++)
         {
            var ch = script[i];
            inQuotes = ch == Constants.Quote ? !inQuotes : inQuotes;

            if (inQuotes)
            {
               Interpreter.Instance.AppendOutput(ch.ToString());
               continue;
            }
            if (!Constants.TokenSeparation.Contains(ch))
            {
               item.Append(ch);
               continue;
            }
            if (item.Length > 0)
            {
               var token = item.ToString();
               var func = ParserFunction.GetFunction(token);
               var isNative = s_nativeWords.Contains(token);
               if (func != null || isNative)
               {
                  var col = isNative || func.isNative
                     ? ConsoleColor.Green
                     : func.isGlobal
                        ? ConsoleColor.Magenta
                        : ConsoleColor.Gray;
                  Utils.PrintColor(token, col);
               }
               else Interpreter.Instance.AppendOutput(token);
               item.Clear();
            }
            Interpreter.Instance.AppendOutput(ch.ToString());
         }
      }

      public static void TranslateScript(string[] args)
      {
         if (args.Length < 3) return;
         var fromLang = args[0];
         var toLang = args[1];
         var script = Utils.GetFileText(args[2]);

         var result = TranslateScript(script, fromLang, toLang);
         Console.WriteLine(result);
      }

      public static string TranslateScript(string script, string toLang)
      {
         var tempScript = TranslateScript(script, Constants.English, Constants.English);
         if (toLang == Constants.English) return tempScript;

         var result = TranslateScript(tempScript, Constants.English, toLang);
         return result;
      }

      public static string TranslateScript(string script, string fromLang, string toLang)
      {
         var result = new StringBuilder();
         var item = new StringBuilder();

         var keywordsDict = KeywordsDictionary(fromLang, toLang);
         var transDict = TranslationDictionary(fromLang, toLang);
         var inQuotes = false;

         for (var i = 0; i < script.Length; i++)
         {
            var ch = script[i];
            inQuotes = ch == Constants.Quote ? !inQuotes : inQuotes;

            if (inQuotes)
            {
               result.Append(ch);
               continue;
            }
            if (!Constants.TokenSeparation.Contains(ch))
            {
               item.Append(ch);
               continue;
            }
            if (item.Length > 0)
            {
               var token = item.ToString();
               var translation = string.Empty;
               if (toLang == Constants.English)
               {
                  var func = ParserFunction.GetFunction(token);
                  if (func != null) translation = func.Name;
               }
               if (string.IsNullOrEmpty(translation) &&
                   !keywordsDict.TryGetValue(token, out translation) &&
                   !transDict.TryGetValue(token, out translation))
                  translation = token; //TryTranslit (fromLang, toLang, token);
               result.Append(translation);
               item.Clear();
            }
            result.Append(ch);
         }

         return result.ToString();
      }

      public static string TryFindError(string item, ParsingScript script)
      {
         string candidate = null;
         var minSize = Math.Max(2, item.Length - 1);

         for (var i = item.Length - 1; i >= minSize; i--)
         {
            candidate = item.Substring(0, i);
            if (s_nativeWords.Contains(candidate)) return candidate + " " + Constants.StartArg;
            if (s_tempWords.Contains(candidate)) return candidate;
         }

         if (s_spellErrors.TryGetValue(item, out candidate)) return candidate;

         return null;
      }
   }
}