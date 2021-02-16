using System.Runtime.Serialization;

namespace DataContract.SerialSample
{
   [DataContract(Name = "candidate", Namespace = "http://oreilly.com/nutshell")]
   public class Person
   {
      [DataMember(Name = "firstName")]
      public string Name { get; set; }

      [DataMember(Name = "claimedAge")]
      public int Age { get; set; }

      public override string ToString() => $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
   }
}