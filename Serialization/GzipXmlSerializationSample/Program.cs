using System.Diagnostics;

namespace GzipXmlSerializationSample
{
   internal static class Program
   {
      private static void Main()
      {
         const string zipFileName = "person.zip";

         // Сериализация   
         Person personToSaved;
         using (ICapableSerialize<Person> serializeHelper = new GZipXmlSerializeHelper<Person>(zipFileName))
         {
            personToSaved = new Person("Dennis", "Winner");
            serializeHelper.Serialize(personToSaved);
         }

         // Десериализация
         Person restoredPerson;
         using (ICapableSerialize<Person> serializeHelper = new GZipXmlSerializeHelper<Person>(zipFileName))
         {
            restoredPerson = serializeHelper.Deserialize();
         }

         Debug.Assert(personToSaved.Equals(restoredPerson));
      }
   }
}