using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Wrox.Metro.Common;

namespace Wrox.Metro.DataModel
{
   public class MenuCard : BindableBase
   {
      private string title;
      public string Title
      {
         get { return title; }
         set
         {
            SetProperty(ref title, value);
            SetDirty();
         }
      }

      private string description;
      public string Description
      {
         get { return description; }
         set
         {
            SetProperty(ref description, value);
            SetDirty();
         }
      }

      private ImageSource image;
      public ImageSource Image
      {
         get { return image; }
         set { SetProperty(ref image, value); }
      }

      private string imagePath;
      public string ImagePath
      {
         get { return imagePath; }
         set { imagePath = value; }
      }

      public void SetDirty()
      {
         IsDirty = true;
      }
      public void ClearDirty()
      {
         IsDirty = false;
      }
      public bool IsDirty { get; private set; }

      public void SetImage(Uri baseUri, String path)
      {
         var bitmapImage = new BitmapImage(new Uri(baseUri, path));
         if (bitmapImage.PixelHeight > bitmapImage.PixelWidth)
         {
            bitmapImage.DecodePixelHeight = 450;
         }
         else
         {
            bitmapImage.DecodePixelWidth = 450;
         }
         Image = bitmapImage;
         if (imagePath == null)
         {
            ImagePath = string.Format("{0}.{1}", Guid.NewGuid().ToString(), Path.GetExtension(path));
         }
      }

      private readonly ICollection<MenuItem> menuItems = new ObservableCollection<MenuItem>();
      public ICollection<MenuItem> MenuItems
      {
         get { return menuItems; }
      }

      public void RestoreReferences()
      {
         foreach (var menuItem in MenuItems)
         {
            menuItem.MenuCard = this;
         }
      }

      public override string ToString()
      {
         return Title;
      }

      public string GetTextContent()
      {
         StringBuilder sb = new StringBuilder();
         sb.AppendLine(this.Title);
         sb.AppendLine();
         sb.AppendLine(this.Description);
         sb.AppendLine();
         foreach (var menuItem in MenuItems)
         {
            sb.AppendFormat("{0}\t\t{1}", menuItem.Text, menuItem.Price);
            sb.AppendLine();
         }
         return sb.ToString();
      }

      public string GetHtmlContent()
      {
         return
           new XElement("table",
             new XElement("thead",
               new XElement("td", "Text"),
               new XElement("td", "Price"),
               MenuItems.Select(mi =>
                 new XElement("tr",
                 new XElement("td", mi.Text),
                 new XElement("td", mi.Price.ToString("C")))))).ToString();

      }
   }
}
