using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BinaryFileReader
{
   public partial class Form1 : Form
   {
      private readonly OpenFileDialog _chooseOpenFileDialog = new OpenFileDialog();
      private string _chosenFile;

      public Form1()
      {
         InitializeComponent();

         menuFileOpen.Click += OnFileOpen;
         _chooseOpenFileDialog.FileOk += OnOpenFileDialogOk;
      }

      private void OnFileOpen(object sender, EventArgs e)
      {
         _chooseOpenFileDialog.ShowDialog();
      }

      private void OnOpenFileDialogOk(object sender, CancelEventArgs e)
      {
         _chosenFile = _chooseOpenFileDialog.FileName;
         Text = Path.GetFileName(_chosenFile);
         DisplayFile();
      }

      private void DisplayFile()
      {
         const int nCols = 16;
         const int maxBytes = 65536;

         var inStream = new FileStream(_chosenFile, FileMode.Open, FileAccess.Read);
         long nBytesToRead = inStream.Length;
         if (nBytesToRead > maxBytes / 4)
         {
            nBytesToRead = maxBytes / 4;
         }

         var nLines = (int)(nBytesToRead / nCols) + 1;
         var lines = new string[nLines];
         int nBytesRead = 0;
         for (int i = 0; i < nLines; i++)
         {
            var nextLine = new StringBuilder { Capacity = 4 * nCols };
            for (int j = 0; j < nCols; j++)
            {
               int nextByte = inStream.ReadByte();
               nBytesRead++;
               if (nextByte < 0 || nBytesRead > maxBytes)
               {
                  break;
               }

               var nextChar = (char)nextByte;
               if (nextChar < 16)
               {
                  nextLine.AppendFormat(" x0{0}", string.Format("{0,1:X}", (int)nextChar));
               }
               else if (char.IsLetterOrDigit(nextChar) || char.IsPunctuation(nextChar))
               {
                  nextLine.AppendFormat("  {0} ", nextChar);
               }
               else
               {
                  nextLine.AppendFormat(" x{0}", string.Format("{0,2:X}", (int)nextChar));
               }
            }

            lines[i] = nextLine.ToString();
         }

         inStream.Close();
         textBoxContents.Lines = lines;
      }
   }
}