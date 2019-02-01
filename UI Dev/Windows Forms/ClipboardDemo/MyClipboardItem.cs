using System;
using System.Windows.Forms;

namespace ClipboardDemo
{
   [Serializable]
   internal class MyClipboardItem
   {
      //we're naming it this, but we'll actually store a list of these
      private const string FormatName = "HowToCSharp.ch16.ClipboardDemo.MyClipboardItem";
      public static readonly DataFormats.Format Format;

      static MyClipboardItem()
      {
         Format = DataFormats.GetFormat(FormatName);
      }

      public MyClipboardItem(string name, string sex, string age)
      {
         Name = name;
         Sex = sex;
         Age = age;
      }

      public string Name { get; set; }
      public string Sex { get; set; }
      public string Age { get; set; }
   }
}