using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Console;

namespace JsonNetSample
{
   public static class JsonSampleEntry
   {
      private const string InventoryFileName = "inventory.json";
      //private const string InventoryXmlFileName = "inventory.xml";

      private const string CreateJsonOption = "-j";
      private const string ConvertOption = "-c";
      private const string SerializeOption = "-s";
      private const string DeserializeOption = "-d";
      private const string ReadOption = "-r";

      private static Inventory GetInventoryObject() =>
         new Inventory
         {
            InventoryItems = new[]
            {
               new Product
               {
                  ProductId = 100,
                  ProductName = "Product Thing",
                  SupplierId = 10
               },
               new BookProduct
               {
                  ProductId = 101,
                  ProductName = "How to use your new product thing",
                  SupplierId = 10,
                  Isbn = "1234567890"
               }
            }
         };

      public static void Main(string[] args)
      {
         if (args.Length != 1)
         {
            ShowUsage();
            return;
         }

         switch (args[0])
         {
            case CreateJsonOption:
               CreateJson();
               break;
            case ConvertOption:
               ConvertObject();
               break;
            case SerializeOption:
               SerializeJson();
               break;
            case DeserializeOption:
               DeserializeJson();
               break;
            case ReadOption:
               ReaderSample();
               break;
            default:
               ShowUsage();
               break;
         }
      }

      private static void ShowUsage()
      {
         WriteLine("XmlReaderAndWriterSample options");
         WriteLine("\tOptions");
         WriteLine($"\t{CreateJsonOption}\tCreate JSON");
         WriteLine($"\t{ConvertOption}\tConvert Object");
         WriteLine($"\t{SerializeOption}\tSerialize");
         WriteLine($"\t{DeserializeOption}\tDeserialize");
         WriteLine($"\t{ReadOption}\tUse Reader");
      }

      private static void SerializeJson()
      {
         using (var writer = File.CreateText(InventoryFileName))
         {
            var serializer = JsonSerializer.Create(new JsonSerializerSettings {Formatting = Formatting.Indented});
            serializer.Serialize(writer, GetInventoryObject());
         }
      }

      private static void ConvertObject()
      {
         var inventory = GetInventoryObject();
         var json = JsonConvert.SerializeObject(inventory, Formatting.Indented);
         WriteLine(json);
         WriteLine();
         var newInventory = JsonConvert.DeserializeObject<Inventory>(json);
         foreach (var product in newInventory.InventoryItems)
         {
            WriteLine(product.ProductName);
         }
      }

      private static void DeserializeJson()
      {
         using (var reader = File.OpenText(InventoryFileName))
         using (JsonReader jsonReader = new JsonTextReader(reader))
         {
            var serializer = JsonSerializer.Create();
            var inventory = serializer.Deserialize<Inventory>(jsonReader);
            foreach (var item in inventory.InventoryItems)
            {
               WriteLine(item.ProductName);
            }
         }
      }

      /*private static string ConvertXmlToJson()
      {
         var xmlInventory = XElement.Load(InventoryXmlFileName);
         return JsonConvert.SerializeXNode(xmlInventory);
      }*/

      private static void CreateJson()
      {
         const string titleLabel = "title";
         const string publisherLabel = "publisher";

         var book1 = new JObject
         {
            [titleLabel] = "Professional C# 6 and .NET Core",
            [publisherLabel] = "Wrox Press"
         };

         var book2 = new JObject
         {
            [titleLabel] = "Professional C# 5 and .NET 4.5.1",
            [publisherLabel] = "Wrox Press"
         };

         var books = new JArray {book1, book2};

         var json = new JObject {["books"] = books};
         WriteLine(json);
      }

      private static void ReaderSample()
      {
         using (var textReader = File.OpenText(InventoryFileName))
         using (var jsonReader = new JsonTextReader(textReader) {CloseInput = true})
         {
            while (jsonReader.Read())
            {
               WriteLine(jsonReader.Value);
            }
         }
      }
   }
}