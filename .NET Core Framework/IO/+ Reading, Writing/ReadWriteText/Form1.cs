using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ReadWriteText
{
   public partial class Form1 : Form
   {
      private readonly OpenFileDialog _chooseOpenFileDialog = new OpenFileDialog();
      private string _chosenFile;

      public Form1()
      {
         InitializeComponent();

         menuFileOpen.Click += (sender, e) => _chooseOpenFileDialog.ShowDialog();

         _chooseOpenFileDialog.FileOk += (o, args) =>
         {
            _chosenFile = _chooseOpenFileDialog.FileName;
            Text = Path.GetFileName(_chosenFile);
            DisplayFile();
         };

         menuFileSave.Click += (o, eventArgs) =>
         {
            var writer = new StreamWriter(_chosenFile, false, Encoding.Unicode);
            foreach (string line in textBoxContents.Lines)
            {
               writer.WriteLine(line);
            }

            writer.Close();
         };
      }

      private void DisplayFile()
      {
         StringCollection linesCollection = ReadFileIntoStringCollection();
         var linesArray = new string[linesCollection.Count];
         linesCollection.CopyTo(linesArray, 0);
         textBoxContents.Lines = linesArray;
      }

      private StringCollection ReadFileIntoStringCollection()
      {
         const int maxBytes = 0x10000;
         var streamReader = new StreamReader(_chosenFile);
         var result = new StringCollection();
         int nBytesRead = 0;
         string nextLine;
         while ((nextLine = streamReader.ReadLine()) != null)
         {
            nBytesRead += nextLine.Length;
            if (nBytesRead > maxBytes)
            {
               break;
            }

            result.Add(nextLine);
         }
         streamReader.Close();

         return result;
      }
   }
}