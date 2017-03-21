using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace PhotoGallery
{
   public sealed class Photos : ObservableCollection<Photo>
   {
      private readonly Dictionary<string, FileSystemWatcher> _watchers = new Dictionary<string, FileSystemWatcher>();

      public event EventHandler ItemsUpdated;

      protected override void ClearItems()
      {
         base.ClearItems();
         _watchers.Clear();
      }

      protected override void InsertItem(int index, Photo item)
      {
         base.InsertItem(index, item);
         if (!_watchers.ContainsKey(item.Path))
         {
            var watcher = new FileSystemWatcher(item.Path, "*.jpg") {EnableRaisingEvents = true};
            watcher.Created += OnPhotoCreated;
            watcher.Deleted += OnPhotoDeleted;
            watcher.Renamed += OnPhotoRenamed;
            _watchers.Add(item.Path, watcher);
         }
      }

      private void OnPhotoRenamed(object sender, RenamedEventArgs e)
      {
         var index = -1;
         for (var i = 0; i < Items.Count; i++)
            if (Items[i].FullPath == e.OldFullPath)
            {
               index = i;
               break;
            }

         if (index >= 0)
            Items[index] = new Photo(e.FullPath);

         if (ItemsUpdated != null)
            ItemsUpdated(this, new EventArgs());
      }

      private void OnPhotoDeleted(object sender, FileSystemEventArgs e)
      {
         var index = -1;
         for (var i = 0; i < Items.Count; i++)
            if (Items[i].FullPath == e.FullPath)
            {
               index = i;
               break;
            }

         if (index >= 0)
            Items.RemoveAt(index);

         if (ItemsUpdated != null)
         {
            ItemsUpdated.Invoke(this, EventArgs.Empty);
         }
      }

      private void OnPhotoCreated(object sender, FileSystemEventArgs e)
      {
         Items.Add(new Photo(e.FullPath));

         if (ItemsUpdated != null)
            ItemsUpdated(this, new EventArgs());
      }
   }
}