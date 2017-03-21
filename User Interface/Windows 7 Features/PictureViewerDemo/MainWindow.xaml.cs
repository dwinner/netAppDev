using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.ApplicationServices;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace PictureViewerDemo
{
	public partial class MainWindow
	{
		private ImageList _list;
		private ThumbnailToolBarButton _nextButton;

		private ThumbnailToolBarButton _previousButton;

		#region CTors

		public MainWindow()
		{
			InitializeComponent();

			//Register next Restart Recovery key (It ensures that application if application closes other than OnPatch/ OnReboot / NormalEnd
			// it pass the /recovery argument.
			ApplicationRestartRecoveryManager.RegisterForApplicationRestart(
				new RestartSettings("/recover", RestartRestrictions.NotOnPatch | RestartRestrictions.NotOnReboot));

			//Register Recovery data.
			var data = new RecoveryData(RecoverDataCallback, null);
			var seting = new RecoverySettings(data, 0);
			ApplicationRestartRecoveryManager.RegisterForApplicationRecovery(seting);

			// Lets Check wheher recovery needed
			var args = Environment.GetCommandLineArgs();
			if (args.Length > 1 && args[1].Equals("/recovery", StringComparison.InvariantCultureIgnoreCase))
				StartRecovery("/recovery");
			SaveButton.IsEnabled = false;
		}

		#endregion

		#region Public Properties

		public List<LibraryElement> Images
		{
			get { return _list; }
		}

		//public Dictionary<string, string> AllPackages
		//{
		//    get { return this._allPackages; }
		//}

		#endregion

		#region Application Restart Recovery Section

		private void StartRecovery(string p)
		{
			var recoveryfile = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Recoverydata.xml");
			if (!File.Exists(recoveryfile))
			{
				TaskDialog.Show("RecoveryFile Not found", "Recovery Element missing", "Application Recovery Error");
				return;
			}
			//
			var doc = XDocument.Load(recoveryfile);
			if (doc.Descendants("FileName").FirstOrDefault() != null)
			{
				var filename = doc.Element("FileName").Value;
				if (File.Exists(filename))
				{
					FileNameLabel.Content = doc.Element("FileName").Value;
					_list = new ImageList(filename);
				}
			}
			if (doc.Descendants("Comment").FirstOrDefault() != null)
				txtComments.Text = doc.Descendants("Comment").FirstOrDefault().Value;
			DataContext = this;
			File.Delete(recoveryfile);
		}

		private int RecoverDataCallback(object state)
		{
			Ping();
			var recoveryfile = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Recoverydata.xml");
			var doc = new XDocument(
				new XDeclaration("1.0", "UTF-8", "yes"),
				new XElement("root",
					new XElement("FileName", _list.FilePath),
					new XElement("Comment", txtComments.Text),
					new XElement("Selected", Imgdisplay.Source)));
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

		#region EventHandlers

		private void BrowseButton_Click(object sender, RoutedEventArgs e)
		{
			var fdlg = new CommonOpenFileDialog("Choose an Album or an Image File");
			fdlg.Filters.Add(new CommonFileDialogFilter("Picture Albums", "*.pkg"));
			fdlg.Filters.Add(new CommonFileDialogFilter("Image Files", "*.jpg,*.gif,*.png,*.bmp"));
			fdlg.EnsurePathExists = true;
			fdlg.InitialDirectory = KnownFolders.Documents.Path;
			fdlg.IsFolderPicker = false;

			if (fdlg.ShowDialog() == CommonFileDialogResult.Ok)
			{
				FileNameLabel.Content = fdlg.FileName;
				var selectedpath = fdlg.FileName;
				if (fdlg.FileName.EndsWith("pkg")) //ensure we selected an Album
				{
					_list = new ImageList(selectedpath);
				}
				else
				{
					selectedpath = Path.GetDirectoryName(selectedpath);
					_list = new ImageList(selectedpath);
					_list.StoreLibraryFromFolder(selectedpath);
				}
				DataContext = this;
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
			SaveButton.IsEnabled = true;
		}


		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			if (_list != null && _list._newStore)
			{
				var sfdlg = new CommonSaveFileDialog();
				sfdlg.Filters.Add(new CommonFileDialogFilter("Picture Album (*.pkg)", "*.pkg"));
				sfdlg.Filters.Add(new CommonFileDialogFilter("All Files (*.*)", "*.*"));
				sfdlg.DefaultExtension = "pkg";
				sfdlg.InitialDirectory = KnownFolders.Documents.Path;
				if (sfdlg.ShowDialog() == CommonFileDialogResult.Ok)
					_list.FilePath = sfdlg.FileName;
			}

			_list.UpdateLibrary();
			TaskDialog.Show("Note: It stores only metadata related to the Image, Renaming the original image will affect it.",
				"Saved Successfully to Disk", "Operation Successful");
		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new OpenFileDialog();

			if (dialog.ShowDialog().Value)
			{
				_list = new ImageList(Path.GetDirectoryName(dialog.FileName));
				DataContext = this;
			}
		}


		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var clippingRect = new Rectangle(new Point(400, 30),
				new Size(Convert.ToInt32(Imgdisplay.ActualWidth), Convert.ToInt32(Imgdisplay.ActualHeight)));

			var interopHelper = new WindowInteropHelper(this);
			var handle = interopHelper.Handle;
			TaskbarManager.Instance.TabbedThumbnail.SetThumbnailClip(handle, clippingRect);
			TaskbarManager.Instance.SetOverlayIcon(new Icon(@"images\overlay.ico"), "Picture Viewer");
			_previousButton = new ThumbnailToolBarButton(new Icon(@"images\prevArrow.ico"), "Previous Image");

			_nextButton = new ThumbnailToolBarButton(new Icon(@"images\nextArrow.ico"), "Next Image");
			_previousButton.Click += ButtonPrevious_Click;
			_nextButton.Click += ButtonNext_Click;
			TaskbarManager.Instance.ThumbnailToolBars.AddButtons(handle, _previousButton, _nextButton);			
			_previousButton.Enabled = ImageList.SelectedIndex > 0;
			_nextButton.Enabled = ImageList.SelectedIndex < ImageList.Items.Count - 1;
		}

		protected void ButtonPrevious_Click(object sender, EventArgs e)
		{
			var prevIndex = ImageList.SelectedIndex - 1;
			if (prevIndex > -1)
				ImageList.SelectedIndex = prevIndex;
		}

		protected void ButtonNext_Click(object sender, EventArgs e)
		{
			var nextIndex = ImageList.SelectedIndex + 1;
			if (nextIndex < ImageList.Items.Count)
				ImageList.SelectedIndex = nextIndex;
		}

		private void ImageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//Diable the buttons
			_previousButton.Enabled = ImageList.SelectedIndex > 0;
			_nextButton.Enabled = ImageList.SelectedIndex < ImageList.Items.Count - 1;
		}

		#endregion
	}

	#region Additional Types

	public class ImageList : List<LibraryElement>
	{
		public delegate void ProgressEventDelegate(int percent);

		public bool _newStore = true;

		public ImageList(string filepath)
		{
			if (File.Exists(filepath))
				_newStore = false;

			FilePath = filepath;
			Url = new Uri(filepath);
			var attr = File.GetAttributes(FilePath);
			if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
			{
				LoadLibrary();
			}
			else
			{
				var di = new DirectoryInfo(filepath);
				SearchPictures(di);
			}
		}

		public string FilePath { get; set; }

		public Uri Url { get; }

		public event ProgressEventDelegate ProgressChange;

		public virtual void OnProgressChange(int percent)
		{
			if (ProgressChange != null)
				ProgressChange(percent);
		}

		private bool LoadLibrary()
		{
			var document = XDocument.Load(FilePath);
			var elements = document.Descendants("LI");
			foreach (var elem in elements)
			{
				var path = elem.Attribute("p").Value;

				if (File.Exists(path))
				{
					var lelem = new LibraryElement(path);
					if (elem.Attribute("c") != null)
						lelem.Comment = elem.Attribute("c").Value;
					if (elem.Attribute("n") != null)
						lelem.Name = elem.Attribute("n").Value;
					Add(lelem);
				}
			}
			return true;
		}

		private void SearchPictures(DirectoryInfo info)
		{
			var files = info.GetFiles("*.jpg");
			var count = files.Count();
			var i = 0;
			foreach (var file in files)
			{
				i++;
				var elem = new LibraryElement(file.FullName);
				Add(elem);
				OnProgressChange(i / count);
			}
			var dirs = info.GetDirectories();
			foreach (var dinfo in dirs)
				SearchPictures(dinfo);
		}

		public bool StoreLibraryFromFolder(string dirpath)
		{
			var document = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"));
			var libItems = new XElement("LIS");
			if (Directory.Exists(dirpath))
			{
				var flist = new List<string>();
				flist.AddRange(Directory.GetFiles(dirpath, "*.jpg"));
				flist.AddRange(Directory.GetFiles(dirpath, "*.bmp"));

				foreach (var path in flist)
				{
					var lelem = new LibraryElement(path);
					lelem.Comment = "";
					lelem.Image = new BitmapImage(new Uri(path));
					lelem.Name = Path.GetFileName(path);
					Add(lelem);

					var elem = new XElement("LI"); //Individual Library Image
					elem.SetAttributeValue("p", path);
					elem.SetAttributeValue("c", "");
					elem.SetAttributeValue("n", Path.GetFileName(path));
					libItems.Add(elem);
				}
			}
			document.Add(libItems);
			return true;
		}

		public bool UpdateLibrary()
		{
			var document = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"));
			var libItems = new XElement("LIS");
			foreach (var elem in this)
			{
				var xelem = new XElement("LI"); //Individual Library Image
				xelem.SetAttributeValue("p", elem.Path);
				xelem.SetAttributeValue("c", elem.Comment);
				xelem.SetAttributeValue("n", elem.Name);
				libItems.Add(xelem);
			}
			document.Add(libItems);
			document.Save(FilePath);
			return true;
		}
	}

	public class LibraryElement
	{
		public LibraryElement(string path)
		{
			var image = new BitmapImage(new Uri(path));
			Image = image;
			Path = path;
		}

		public ImageSource Image { get; set; }

		public string Name { get; set; }

		public string Path { get; set; }

		public string Comment { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}

	#endregion
}