using System;
using PostSharp.Aspects;
using PostSharp.Reflection;

namespace PsCodeContracts
{
   [Serializable]
   public sealed class StartsWithBeeAttribute :  Attribute, ILocationValidationAspect<string>
   {
      public Exception ValidateValue(string value, string locationName, LocationKind locationKind)
         => value.StartsWith("B") ? null : new ApplicationException("Must start with B!");
   }
}