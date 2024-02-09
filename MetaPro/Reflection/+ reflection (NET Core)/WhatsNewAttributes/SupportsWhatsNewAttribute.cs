using System;

namespace WhatsNewAttributes
{
   [AttributeUsage(AttributeTargets.Assembly)]
   public class SupportsWhatsNewAttribute : Attribute
   {
   }
}