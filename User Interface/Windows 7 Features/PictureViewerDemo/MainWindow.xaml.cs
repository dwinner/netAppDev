using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.ApplicationServices;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;


namespace PictureViewerDemo
{


   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      private ImageList _list = null;

      private ThumbnailToolbarButton _PreviousButton;
      private ThumbnailToolbarButton _NextButton;

      # region CTors
      public MainWindow()
      {
         InitializeComponent();

         //Register next Restart Recovery key (It ensures that application if application closes other than OnPatch/ OnReboot / NormalEnd
         // it pass the /recovery argument.
         ApplicationRestartRecoveryManager.RegisterForApplicationRestart(
             new RestartSettings("/recover", RestartRestrictions.NotOnPatch | RestartRestrictions.NotOnReboot));

         //Register Recovery data.
         RecoveryData data = new RecoveryData(new RecoveryCallback(RecoverDataCallback), null);
         RecoverySettings seting = new RecoverySettings(data, 0);
         ApplicationRestartRecoveryManager.RegisterForApplicationRecovery(seting);

         // Lets Check wheher recovery needed
         string[] args = System.Environment.GetCommandLineArgs();
         if (args.Length > 1 && args[1].Equals("/recovery", StringComparison.InvariantCultureIgnoreCase))
         {
            StartRecovery("/recovery");
         }
         this.SaveButton.IsEnabled = false;
      }
      # endregion

      # region Public Properties
      public List<LibraryElement> Images
      {
         get { return this._list; }
      }
      //public Dictionary<string, string> AllPackages
      //{
      //    get { return this._allPackages; }
      //}
      #endregion

      # region Application Restart Recovery Section
      private void StartRecovery(string p)
      {
         string recoveryfile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Recoverydata.xml");
         if (!File.Exists(recoveryfile))
         {
            TaskDialog.Show("RecoveryFile Not found", "Recovery Element missing", "Application Recovery Error");
            return;
         }
         //
         XDocument doc = XDocument.Load(recoveryfile);
         if (doc.Descendants("FileName").FirstOrDefault() != null)
         {
            string filename = doc.Element("FileName").Value;
            if (File.Exists(filename))
            {
               this.FileNameLabel.Content = doc.Element("FileName").Value;
               this._list = new ImageList(filename);
            }

         }
         if (doc.Descendants("Comment").FirstOrDefault() != null)
         {
            this.txtComments.Text = doc.Descendants("Comment").FirstOrDefault().Value;
         }
         this.DataContext = this;
         File.Delete(recoveryfile);

      }
      private int RecoverDataCallback(object state)
      {
         Ping();
         string recoveryfile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Recoverydata.xml");
         XDocument doc = new XDocument(
                         new XDeclaration("1.0", "UTF-8", "yes"),
                         new XElement("root",
                         new XElement("FileName", this._list.FilePath),
                         new XElement("Comment", this.txtComments.Text),
                         new XElement("Selected", this.Imgdisplay.Source)));
         doc.Save(recoveryfile, SaveOptions.DisableFormatting);
         ApplicationRestartRecoveryManager.ApplicationRecoveryFinished(true);
         return 0;
      }
      private void Ping()
      {
         if (ApplicationRestartRecoveryManager.ApplicationRecoveryInProgress())
         {
            Debug.Write("Operation Cancelled");
            Environment.Exit(2);
         }
      }

      #endregion

      # region EventHandlers
      private void BrowseButton_Click(object sender, RoutedEventArgs e)
      {
         CommonOpenFileDialog fdlg = new CommonOpenFileDialog("Choose an Album or an Image File");
         fdlg.Filters.Add(new CommonFileDialogFilter("Picture Albums", "*.pkg"));
         fdlg.Filters.Add(new CommonFileDialogFilter("Image Files", "*.jpg,*.gif,*.png,*.bmp"));
         fdlg.EnsurePathExists = true;
         fdlg.InitialDirectory = KnownFolders.Documents.Path;
         fdlg.IsFolderPicker = false;

         if (fdlg.ShowDialog() == CommonFileDialogResult.OK)
         {
            this.FileNameLabel.Content = fdlg.FileName;
            string selectedpath = fdlg.FileName;
            if (fdlg.FileName.EndsWith("pkg")) //ensure we selected an Album
            {
               this._list = new ImageList(selectedpath);
            }
            else
            {
               selectedpath = System.IO.Path.GetDirectoryName(selectedpath);
               this._list = new ImageList(selectedpath);
               this._list.StoreLibraryFromFolder(selectedpath);
            }
            this.DataContext = this;
         }
         //OpenFileDialog dlg = new OpenFileDialog();

         //dlg.InitialDirectory = "c:\\";
         //dlg.Filter = "Picture Album (*.pkg)|*.pkg|Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
         //dlg.RestoreDirectory = true;

         //if (dlg.ShowDialog().Value)
         //{
         //    string selectedFileName = dlg.FileName;
         //    this.FileNameLabel.Content = selectedFileName;
         //    if (dlg.FileName.EndsWith("pkg"))
         //    {
         //        this._list = new ImageList(selectedFileName);
         //        this.DataContext = this;
         //    }
         //    else
         //    {
         //        DirectoryInfo di = new DirectoryInfo(System.IO.Path.GetDirectoryName(dlg.FileName));
         //        this._list = new ImageList(di.FullName);
         //        this._list.StoreLibraryFromFolder(di.FullName);
         //        this.DataContext = this;
         //    }
         this.SaveButton.IsEnabled = true;


      }


      private void SaveButton_Click(object sender, RoutedEventArgs e)
      {

         if (this._list._newStore)
         {
            CommonSaveFileDialog sfdlg = new CommonSaveFileDialog();
            sfdlg.Filters.Add(new CommonFileDialogFilter("Picture Album (*.pkg)", "*.pkg"));
            sfdlg.Filters.Add(new CommonFileDialogFilter("All Files (*.*)", "*.*"));
            sfdlg.DefaultExtension = "pkg";
            sfdlg.InitialDirectory = KnownFolders.Documents.Path;
            if (sfdlg.ShowDialog() == CommonFileDialogResult.OK)
               this._list.FilePath = sfdlg.FileName;
         }

         this._list.UpdateLibrary();
         TaskDialog.Show("Note: It stores only metadata related to the Image, Renaming the original image will affect it.",
                         "Saved Successfully to Disk", "Operation Successful");
      }

      private void SearchButton_Click(object sender, RoutedEventArgs e)
      {

         OpenFileDialog dialog = new OpenFileDialog();

         if (dialog.ShowDialog().Value)
         {
            this._list = new ImageList(System.IO.Path.GetDirectoryName(dialog.FileName));
            this.DataContext = this;
         }
      }


      private void Window_Loaded(object sender, RoutedEventArgs e)
      {
         System.Drawing.Rectangle clippingRect = new System.Drawing.Rectangle(new System.Drawing.Point(400, 30), new System.Drawing.Size(Convert.ToInt32(Imgdisplay.ActualWidth), Convert.ToInt32(Imgdisplay.ActualHeight)));

         WindowInteropHelper interopHelper = new WindowInteropHelper(this);
         IntPtr handle = interopHelper.Handle;
         TaskbarManager.Instance.TabbedThumbnail.SetThumbnailClip(handle, clippingRect);
         TaskbarManager.Instance.SetOverlayIcon(new System.Drawing.Icon(@"images\overlay.ico"), "Picture Viewer");
         this._PreviousButton = new ThumbnailToolbarButton(new System.Drawing.Icon(@"images\prevArrow.ico"), "Previous Image");
         this._NextButton = new ThumbnailToolbarButton(new System.Drawing.Icon(@"images\nextArrow.ico"), "Next Image");
         this._PreviousButton.Click += this.ButtonPrevious_Click;
         this._NextButton.Click += this.ButtonNext_Click;
         TaskbarManager.Instance.ThumbnailToolbars.AddButtons(handle, this._PreviousButton, this._NextButton);
         this._PreviousButton.Enabled = this.ImageList.SelectedIndex > 0;
         this._NextButton.Enabled = this.ImageList.SelectedIndex < ImageList.Items.Count - 1;
      }

      protected void ButtonPrevious_Click(object sender, EventArgs e)
      {
         int prevIndex = this.ImageList.SelectedIndex - 1;
         if (prevIndex > -1)
         {
            this.ImageList.SelectedIndex = prevIndex;
         }

      }
      protected void ButtonNext_Click(object sender, EventArgs e)
      {
         int nextIndex = this.ImageList.SelectedIndex + 1;
         if (nextIndex < this.ImageList.Items.Count)
         {
            this.ImageList.SelectedIndex = nextIndex;
         }

      }

      private void ImageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         //Diable the buttons
         this._PreviousButton.Enabled = this.ImageList.SelectedIndex > 0;
         this._NextButton.Enabled = this.ImageList.SelectedIndex < ImageList.Items.Count - 1;

      }
      # endregion
   }

   #region Additional Types
   public class ImageList : List<LibraryElement>
   {
      private Uri _LibraryLocation = null;
      public bool _newStore = true;
      public string FilePath { get; set; }
      public delegate void ProgressEventDelegate(int percent);
      public event ProgressEventDelegate ProgressChange;

      public virtual void OnProgressChange(int percent)
      {
         if (this.ProgressChange != null)
            this.ProgressChange(percent);
      }
      public ImageList(string filepath)
      {
         if (File.Exists(filepath))
            _newStore = false;

         this.FilePath = filepath;
         this._LibraryLocation = new Uri(filepath);
         FileAttributes attr = File.GetAttributes(this.FilePath);
         if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
            this.LoadLibrary();
         else
         {
            DirectoryInfo di = new DirectoryInfo(filepath);
            this.SearchPictures(di);
         }
      }

      public Uri Url
      {
         get
         {
            return this._LibraryLocation;
         }
      }
      private bool LoadLibrary()
      {

         XDocument document = XDocument.Load(this.FilePath);
         var elements = document.Descendants("LI");
         foreach (XElement elem in elements)
         {
            string path = elem.Attribute("p").Value;

            if (File.Exists(path))
            {
               LibraryElement lelem = new LibraryElement(path);
               if (elem.Attribute("c") != null)
                  lelem.Comment = elem.Attribute("c").Value;
               if (elem.Attribute("n") != null)
                  lelem.Name = elem.Attribute("n").Value;
               this.Add(lelem);
            }
         }
         return true;

      }

      private void SearchPictures(DirectoryInfo info)
      {
         FileInfo[] files = info.GetFiles("*.jpg");
         int count = files.Count();
         int i = 0;
         foreach (FileInfo file in files)
         {
            i++;
            LibraryElement elem = new LibraryElement(file.FullName);
            this.Add(elem);
            this.OnProgressChange(i / count);
         }
         DirectoryInfo[] dirs = info.GetDirectories();
         foreach (DirectoryInfo dinfo in dirs)
            this.SearchPictures(dinfo);
      }
      public bool StoreLibraryFromFolder(string dirpath)
      {
         XDocument document = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"));
         XElement libItems = new XElement("LIS");
         if (Directory.Exists(dirpath))
         {
            List<string> flist = new List<string>();
            flist.AddRange(Directory.GetFiles(dirpath, "*.jpg"));
            flist.AddRange(Directory.GetFiles(dirpath, "*.bmp"));

            foreach (string path in flist)
            {
               LibraryElement lelem = new LibraryElement(path);
               lelem.Comment = "";
               lelem.Image = new BitmapImage(new Uri(path));
               lelem.Name = System.IO.Path.GetFileName(path);
               this.Add(lelem);

               XElement elem = new XElement("LI"); //Individual Library Image
               elem.SetAttributeValue("p", path);
               elem.SetAttributeValue("c", "");
               elem.SetAttributeValue("n", System.IO.Path.GetFileName(path));
               libItems.Add(elem);
            }

         }
         document.Add(libItems);
         return true;
      }
      public bool UpdateLibrary()
      {
         XDocument document = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"));
         XElement libItems = new XElement("LIS");
         foreach (LibraryElement elem in this)
         {
            XElement xelem = new XElement("LI"); //Individual Library Image
            xelem.SetAttributeValue("p", elem.Path);
            xelem.SetAttributeValue("c", elem.Comment);
            xelem.SetAttributeValue("n", elem.Name);
            libItems.Add(xelem);
         }
         document.Add(libItems);
         document.Save(this.FilePath);
         return true;
      }

   }

   public class LibraryElement
   {
      private ImageSource _image;
      private string _name;


      public LibraryElement(string path)
      {
         BitmapImage image = new BitmapImage(new Uri(path));
         _image = image;
         this.Path = path;
      }

      public override string ToString()
      {
         return _name;
      }

      public ImageSource Image { get { return _image; } set { this._image = value; } }
      public string Name { get { return _name; } set { this._name = value; } }
      public string Path { get; set; }
      public string Comment
      {
         get;
         set;
      }
   }
   # endregion
}
