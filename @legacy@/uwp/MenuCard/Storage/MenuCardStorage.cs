﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;
using Wrox.Metro.DataModel;

namespace Wrox.Metro.Storage
{
   public class MenuCardStorage
   {
      public MenuCard Read()
      {
         return null;
      }

      public async Task<bool> IsRoamingFolderEmpty()
      {
         StorageFolder folder = ApplicationData.Current.RoamingFolder;
         IReadOnlyList<StorageFile> files = await folder.GetFilesAsync();
         return files.Count == 0;
      }

      //public async Task DeleteMenuCardAsync(MenuCard menuCard)
      //{
      //  //StorageFolder folder = ApplicationData.Current.RoamingFolder;
      //  //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(
      //  //await file.DeleteAsync(StorageDeleteOption.PermanentDelete);

      //}

      public async Task WriteMenuCardToFileAsync(MenuCard menuCard, StorageFile storageFile)
      {
         //using (Stream stream = await storageFile.OpenStreamForWriteAsync())
         //{
         //  var menuCardData = new MenuCardData(menuCard);

         //  Type[] extraTypes = new Type[]
         //    {
         //      typeof(MenuItemData)
         //    };
         //  var serializer = new XmlSerializer(typeof(MenuCardData), extraTypes);
         //  await Task.Run(() => serializer.Serialize(stream, menuCardData));
         //  await stream.FlushAsync();
         //}

         var menuCardData = new MenuCardData(menuCard);
         var knownTypes = new Type[]
             {
            typeof(MenuItemData)
             };
         var cardStream = new MemoryStream();
         var serializer = new DataContractSerializer(typeof(MenuCardData), knownTypes);
         serializer.WriteObject(cardStream, menuCardData);

         using (Stream fileStream = await storageFile.OpenStreamForWriteAsync())
         {
            cardStream.Seek(0, SeekOrigin.Begin);
            await cardStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
         }
      }

      public async Task WriteMenuCardAsync(MenuCard menuCard)
      {
         StorageFolder folder = ApplicationData.Current.RoamingFolder;

         if (menuCard.IsDirty)
         {
            StorageFile storageFile = await folder.CreateFileAsync(string.Format("MenuCards{0}.xml", menuCard.Title), CreationCollisionOption.ReplaceExisting);
            await WriteMenuCardToFileAsync(menuCard, storageFile);
            menuCard.ClearDirty();
         }
      }

      public async Task WriteMenuCardsAsync(List<MenuCard> menuCards)
      {
         foreach (var menuCard in menuCards)
         {
            await WriteMenuCardAsync(menuCard);

         }
      }

      public async Task<IEnumerable<MenuCard>> ReadMenuCardsAsync()
      {
         List<MenuCard> menuCards = new List<MenuCard>();
         StorageFolder folder = ApplicationData.Current.RoamingFolder;

         StorageFileQueryResult result = folder.CreateFileQuery();
         var queryOptions = new QueryOptions();
         queryOptions.IndexerOption = IndexerOption.DoNotUseIndexer;
         queryOptions.FolderDepth = FolderDepth.Shallow;
         queryOptions.FileTypeFilter.Add(".xml");
         result.ApplyNewQueryOptions(queryOptions);
         IReadOnlyList<StorageFile> files = await result.GetFilesAsync();
         foreach (var file in files)
         {
            using (Stream stream = await file.OpenStreamForReadAsync())
            {
               try
               {
                  var serializer = new DataContractSerializer(typeof(MenuCardData));
                  object data = await Task<object>.Run(() => serializer.ReadObject(stream));

                  //var serializer = new XmlSerializer(typeof(MenuCardData));
                  //object data = await Task<object>.Run(() => serializer.Deserialize(stream));
                  MenuCard menuCard = (data as MenuCardData).ToMenuCard();
                  menuCard.RestoreReferences();
                  menuCards.Add(menuCard);
               }
               catch (Exception)
               {
                  // TODO: log exception
               }
            }
         }

         // read images
         MenuCardImageStorage imageStorage = new MenuCardImageStorage();
         foreach (var menuCard in menuCards)
         {
            if (menuCard.ImagePath != null)
            {
               menuCard.Image = await imageStorage.ReadImageAsync(menuCard.ImagePath);
            }
         }
         return menuCards;
      }


   }
}
