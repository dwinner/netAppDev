using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace PhotoGallery
{
   public partial class MainWindow
   {
      private readonly object _dummyNode = null;
      private readonly Photos _photos = new Photos();
      private ScaleTransform _scaleTransform;

      private void ShowPhoto(bool? showFixBar)
      {
         var selectedPhoto = PictureBox.SelectedItem as Photo;
         if (selectedPhoto != null)
         {
            var filename = selectedPhoto.FullPath;
            ImageView.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
            Image.Source = new BitmapImage(new Uri(filename));
         }

         switch (showFixBar)
         {
            case true:
               FixBar.Visibility = Visibility.Visible;
               break;
            case false:
               FixBar.Visibility = Visibility.Collapsed;
               break;
         }
      }

      private void AddPhotosInFolder(string folder)
      {
         try
         {
            foreach (var s in Directory.GetFiles(folder, "*.jpg"))
               _photos.Add(new Photo(s));
         }
         catch (UnauthorizedAccessException)
         {
         }
         catch (IOException)
         {
         }
      }

      private void OnShowPhoto(object sender, RoutedEventArgs e)
      {
         ShowPhoto(false);
      }

      private void Refresh()
      {
         try
         {
            Cursor = Cursors.Wait;

            // Go back to the gallery if we're viewing an individual photo:
            ImageView.Visibility = Visibility.Hidden;
            BackButton.Visibility = Visibility.Hidden;

            _photos.Clear();

            if (Equals(TreeView.SelectedItem, FavoritesItem))
            {
               foreach (TreeViewItem item in FavoritesItem.Items)
                  AddPhotosInFolder(item.Tag as string);
               FavoritesMenu.IsEnabled = false;
            }
            else if (!Equals(TreeView.SelectedItem, FoldersItem))
            {
               var selectedPhoto = TreeView.SelectedItem as TreeViewItem;
               if (selectedPhoto != null)
               {
                  var folder = selectedPhoto.Tag as string;
                  AddPhotosInFolder(folder);

                  // Update the favorites menu text depending on whether the folder is already a favorite
                  FavoritesMenu.IsEnabled = true;
                  if (FavoritesItem.Items.Cast<TreeViewItem>().Any(item => item.Header as string == folder))
                  {
                     FavoritesMenu.Header = "Remove Current Folder from Fa_vorites";
                     return;
                  }
               }

               FavoritesMenu.Header = "Add Current Folder to Fa_vorites";
            }
         }
         finally
         {
            Cursor = Cursors.Arrow;
         }
      }

      private void OnPictureBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         if (e.AddedItems.Count == 0)
         {
            DeleteMenu.IsEnabled = false;
            RenameMenu.IsEnabled = false;
            FixMenu.IsEnabled = false;
            PrintMenu.IsEnabled = false;
            EditMenu.IsEnabled = false;
            PreviousButton.IsEnabled = false;
            PreviousButton.Opacity = 0.5;
            NextButton.IsEnabled = false;
            NextButton.Opacity = 0.5;
            CounterclockwiseButton.IsEnabled = false;
            CounterclockwiseButton.Opacity = 0.5;
            ClockwiseButton.IsEnabled = false;
            ClockwiseButton.Opacity = 0.5;
            DeleteButton.IsEnabled = false;
            DeleteButton.Opacity = 0.5;
         }
         else
         {
            DeleteMenu.IsEnabled = true;
            RenameMenu.IsEnabled = true;
            FixMenu.IsEnabled = true;
            PrintMenu.IsEnabled = true;
            EditMenu.IsEnabled = true;
            PreviousButton.IsEnabled = true;
            PreviousButton.Opacity = 1;
            NextButton.IsEnabled = true;
            NextButton.Opacity = 1;
            CounterclockwiseButton.IsEnabled = true;
            CounterclockwiseButton.Opacity = 1;
            ClockwiseButton.IsEnabled = true;
            ClockwiseButton.Opacity = 1;
            DeleteButton.IsEnabled = true;
            DeleteButton.Opacity = 1;
         }
      }

      private void DeleteCurrentPhoto()
      {
         var selectedPhoto = PictureBox.SelectedItem as Photo;
         if (selectedPhoto != null)
         {
            var filename = selectedPhoto.FullPath;
            if (
               MessageBox.Show("Are you sure you want to delete '" + filename + "'?", "Delete Picture",
                  MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
               try
               {
                  File.Delete(filename);
               }
               catch (Exception ex)
               {
                  MessageBox.Show(ex.Message, "Cannot Rename File", MessageBoxButton.OK, MessageBoxImage.Error);
               }
         }
      }

      #region Window Management

      public MainWindow()
      {
         InitializeComponent();
         DataContext = _photos;
         _photos.ItemsUpdated += delegate { Dispatcher.Invoke(DispatcherPriority.Normal, new ThreadStart(Refresh)); };

         // Get the default view
         var view = CollectionViewSource.GetDefaultView(_photos);

         // Do the grouping
         view.GroupDescriptions.Clear();
         view.GroupDescriptions.Add(new PropertyGroupDescription("DateTime", new DateTimeToDateConverter()));
      }

      protected override void OnClosing(CancelEventArgs e)
      {
         base.OnClosing(e);

         if (MessageBox.Show("Are you sure you want to close Photo Gallery?", "Annoying Prompt", MessageBoxButton.YesNo,
                MessageBoxImage.Question)
             == MessageBoxResult.No)
            e.Cancel = true;
      }

      protected override void OnClosed(EventArgs e)
      {
         base.OnClosed(e);

         // Persist the list of favorites
         var storeForAssembly = IsolatedStorageFile.GetUserStoreForAssembly();
         using (var stream = new IsolatedStorageFileStream("myFile", FileMode.Create, storeForAssembly))
         using (var writer = new StreamWriter(stream))
         {
            foreach (var item in FavoritesItem.Items.Cast<TreeViewItem>())
               writer.WriteLine(item.Tag as string);
         }
      }

      protected override void OnInitialized(EventArgs e)
      {
         base.OnInitialized(e);

         // Retrieve the list of favorites
         var f = IsolatedStorageFile.GetUserStoreForAssembly();
         using (var stream = new IsolatedStorageFileStream("myFile", FileMode.OpenOrCreate, f))
         using (var reader = new StreamReader(stream))
         {
            var line = reader.ReadLine();
            while (line != null)
            {
               AddFavorite(line);
               line = reader.ReadLine();
            }
         }

         // At least have the user's Pictures folder as a favorite if nothing else
         if (!FavoritesItem.HasItems)
            AddFavorite(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));

         var treeViewItem = TreeView.Items[0] as TreeViewItem;
         if (treeViewItem != null)
            treeViewItem.IsSelected = true;
      }

      #endregion Window Management

      #region TreeView Management

      private void OnFoldersChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
      {
         Refresh();
      }

      private void OnWindowLoaded(object sender, RoutedEventArgs e)
      {
         foreach (var s in Directory.GetLogicalDrives())
         {
            var item = new TreeViewItem
            {
               Header = s,
               Tag = s
            };
            item.Items.Add(_dummyNode);
            item.Expanded += OnFolderExpanded;
            FoldersItem.Items.Add(item);
         }
      }

      private void OnFolderExpanded(object sender, RoutedEventArgs e)
      {
         var item = (TreeViewItem) sender;
         if (item.Items.Count == 1 && item.Items[0] == _dummyNode)
         {
            item.Items.Clear();
            try
            {
               foreach (var subitemTag in Directory.GetDirectories(item.Tag.ToString()))
               {
                  var subitem = new TreeViewItem
                  {
                     Header = subitemTag.Substring(subitemTag.LastIndexOf("\\", StringComparison.Ordinal) + 1),
                     Tag = subitemTag
                  };
                  subitem.Items.Add(_dummyNode);
                  subitem.Expanded += OnFolderExpanded;
                  item.Items.Add(subitem);
               }
            }
            catch (UnauthorizedAccessException)
            {
            }
         }
      }

      private void AddFavorite(string folder)
      {
         FavoritesItem.Items.Add(new TreeViewItem
         {
            Header = folder,
            Tag = folder
         });
      }

      private void RemoveFavorite(string folder)
      {
         for (var i = 0; i < FavoritesItem.Items.Count; i++)
         {
            var treeViewItem = FavoritesItem.Items[i] as TreeViewItem;
            if (treeViewItem != null && treeViewItem.Header as string == folder)
            {
               FavoritesItem.Items.RemoveAt(i);
               break;
            }
         }
      }

      #endregion TreeView Management

      #region Menu Handlers

      private void OnAddToFavorites(object sender, RoutedEventArgs e)
      {
         var treeViewItem = TreeView.SelectedItem as TreeViewItem;
         if (treeViewItem != null)
         {
            var folder = treeViewItem.Tag as string;
            if (FavoritesMenu.Header as string == "Add Current Folder to Fa_vorites")
            {
               AddFavorite(folder);
               FavoritesMenu.Header = "Remove Current Folder from Fa_vorites";
            }
            else
            {
               RemoveFavorite(folder);
               FavoritesMenu.Header = "Add Current Folder to Fa_vorites";
            }
         }
      }

      private void OnDelete(object sender, RoutedEventArgs e)
      {
         DeleteCurrentPhoto();
      }

      private void OnRename(object sender, RoutedEventArgs e)
      {
         var selectedPhoto = PictureBox.SelectedItem as Photo;
         if (selectedPhoto != null)
         {
            var filename = selectedPhoto.FullPath;
            var dialog = new RenameDialog(Path.GetFileNameWithoutExtension(filename));
            if (dialog.ShowDialog() == true) // Result could be true, false, or null
               try
               {
                  var directoryName = Path.GetDirectoryName(filename);
                  if (directoryName != null)
                     File.Move(filename,
                        string.Format("{0}{1}", Path.Combine(directoryName, dialog.NewFilename),
                           Path.GetExtension(filename)));
               }
               catch (Exception ex)
               {
                  MessageBox.Show(ex.Message, "Cannot Rename File", MessageBoxButton.OK, MessageBoxImage.Error);
               }
         }
      }

      private void OnRefresh(object sender, RoutedEventArgs e)
      {
         Refresh();
      }

      private void OnExit(object sender, RoutedEventArgs e)
      {
         Close();
      }

      private void OnFix(object sender, RoutedEventArgs e)
      {
         ShowPhoto(true);
      }

      private void OnPrint(object sender, RoutedEventArgs e)
      {
         var selectedPhoto = PictureBox.SelectedItem as Photo;
         if (selectedPhoto != null)
         {
            var filename = selectedPhoto.FullPath;
            var image = new Image {Source = new BitmapImage(new Uri(filename, UriKind.RelativeOrAbsolute))};

            var pd = new PrintDialog();
            if (pd.ShowDialog() == true) // Result could be true, false, or null
               pd.PrintVisual(image, string.Format("{0} from Photo Gallery", Path.GetFileName(filename)));
         }
      }

      private void OnEdit(object sender, RoutedEventArgs e)
      {
         var selectedPhoto = PictureBox.SelectedItem as Photo;
         if (selectedPhoto != null)
         {
            var filename = selectedPhoto.FullPath;
            Process.Start("mspaint.exe", filename);
         }
      }

      #endregion Menu Handlers

      #region Bottom Button Handlers

      private void OnDefaultSizeApply(object sender, RoutedEventArgs e)
      {
         ZoomSlider.Value = 3;
      }

      private void OnBack(object sender, RoutedEventArgs e)
      {
         ImageView.Visibility = Visibility.Hidden;
         BackButton.Visibility = Visibility.Hidden;
      }

      private void OnZoomChanged(object sender, RoutedEventArgs e)
      {
         if (_scaleTransform == null)
            _scaleTransform = (ScaleTransform) FindResource("St");

         _scaleTransform.ScaleX = ZoomSlider.Value;
         _scaleTransform.ScaleY = ZoomSlider.Value;
      }

      private void OnZoomPopupLeave(object sender, RoutedEventArgs e)
      {
         ZoomPopup.IsOpen = false;
      }

      private void OnZoomActivated(object sender, RoutedEventArgs e)
      {
         ZoomPopup.IsOpen = true;
      }

      private void OnSlideShowPlay(object sender, RoutedEventArgs e)
      {
         MessageBox.Show("NYI");
      }

      private void OnClockwiseClick(object sender, RoutedEventArgs e)
      {
         if (PictureBox.SelectedItem != null)
         {
            var item = PictureBox.ItemContainerGenerator.ContainerFromItem(PictureBox.SelectedItem) as ListBoxItem;
            if (item != null && !(item.LayoutTransform is RotateTransform))
            {
               item.LayoutTransform = new RotateTransform();
               ((RotateTransform) item.LayoutTransform).Angle += 90;
            }            
         }
      }

      private void OnCounterClockwiseClick(object sender, RoutedEventArgs e)
      {
         if (PictureBox.SelectedItem != null)
         {
            var item = PictureBox.ItemContainerGenerator.ContainerFromItem(PictureBox.SelectedItem) as ListBoxItem;
            if (item != null && !(item.LayoutTransform is RotateTransform))
            {
               item.LayoutTransform = new RotateTransform();
               ((RotateTransform) item.LayoutTransform).Angle -= 90;
            }            
         }
      }

      private void OnPreviousPhoto(object sender, RoutedEventArgs e)
      {
         // Get the default view
         var view = CollectionViewSource.GetDefaultView(_photos);

         // Move backward
         view.MoveCurrentToPrevious();

         // Wrap around to the end
         if (view.IsCurrentBeforeFirst) view.MoveCurrentToLast();

         if (ImageView.Visibility == Visibility.Visible)
            ShowPhoto(null);
      }

      private void OnNextPhoto(object sender, RoutedEventArgs e)
      {
         // Get the default view
         var view = CollectionViewSource.GetDefaultView(_photos);

         // Move forward
         view.MoveCurrentToNext();

         // Wrap around to the beginning
         if (view.IsCurrentAfterLast) view.MoveCurrentToFirst();

         if (ImageView.Visibility == Visibility.Visible)
            ShowPhoto(null);
      }

      private void OnDeletePhoto(object sender, RoutedEventArgs e)
      {
         DeleteCurrentPhoto();
      }

      #endregion Buttom Button Handlers

      #region Fix Bar Handlers

      private void OnFixRotateClockwise(object sender, RoutedEventArgs e)
      {
         var rotateTransform = Image.LayoutTransform as RotateTransform;
         if (rotateTransform != null)
            rotateTransform.Angle += 90;
      }

      private void OnFixRotate(object sender, RoutedEventArgs e)
      {
         var rotateTransform = Image.LayoutTransform as RotateTransform;
         if (rotateTransform != null)
            rotateTransform.Angle -= 90;
      }

      private void OnFixSave(object sender, RoutedEventArgs e)
      {
         MessageBox.Show("NYI");
      }

      #endregion Fix Bar Handlers
   }
}