using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace Wrox.Metro.Notification
{
   public class TileManager
   {
      public static void UpdateTile()
      {
         TileTemplateType tileTemplate = TileTemplateType.TileWideImageAndText01;
         XmlDocument tileXml = TileUpdateManager.GetTemplateContent(tileTemplate);
         XmlNodeList tileImageAttributes = tileXml.GetElementsByTagName("image");
         ((XmlElement)tileImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/breakfast400.jpg");
         ((XmlElement)tileImageAttributes[0]).SetAttribute("alt", "Breakfast");

         var textElements = tileXml.GetElementsByTagName("text");
         ((XmlElement)textElements[0]).InnerText = "MENU card";

         TileNotification notification = new TileNotification(tileXml);
         notification.ExpirationTime = DateTimeOffset.Now.AddMinutes(60);

         TileUpdater tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();

         tileUpdater.Update(notification);
      }
   }
}
