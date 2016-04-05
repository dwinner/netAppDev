// Альтернативный способ сериализации в формате JSON

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using static System.String;

namespace JsonViaNewtonSerialization
{
   internal static class Program
   {
      private static void Main()
      {
         var account = new Account
         {
            Email = "dwinner@inbox.ru",
            Active = true,
            CreatedDate = new DateTime(2013, 1, 20, 0, 0, 0, DateTimeKind.Utc),
            Roles = new List<string> { "User", "Admin" }
         };

         var json = JsonConvert.SerializeObject(account, Formatting.Indented,
            new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
         Console.WriteLine(json);

         var o = JsonConvert.DeserializeObject<Account>(json);
         Console.WriteLine(o);
      }
   }

   public sealed class Account
   {
      [JsonProperty("mail")]
      public string Email { get; set; }

      [JsonProperty("active")]
      public bool Active { get; set; }

      [JsonProperty("created")]
      public DateTime CreatedDate { get; set; }

      [JsonProperty("claims")]
      public IList<string> Roles { get; set; }

      public override string ToString()
      {
         return
            $"Email: {Email}, Active: {Active}, CreatedDate: {CreatedDate}, Roles: {Roles?.Aggregate(Empty, (current, role) => $"{current}{(role + ", ")}")}";
      }
   }
}