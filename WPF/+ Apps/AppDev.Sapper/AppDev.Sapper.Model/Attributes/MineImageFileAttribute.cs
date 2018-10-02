using System;

namespace AppDev.Sapper.Model.Attributes
{
   [AttributeUsage(AttributeTargets.Field)]
   internal sealed class MineImageFileAttribute : Attribute
   {
      /// <summary>
      ///    Image File
      /// </summary>
      public string ImageFile { get; set; }
   }
}