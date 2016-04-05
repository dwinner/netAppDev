using System;
using System.IO;

namespace DirectoryApp
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Fun with Directory(Info) *****\n");
         ShowWindowsDirectoryInfo();
         DisplayImageFiles();
         ModifyAppDirectory();
         FunWithDirectoryType();
         Console.ReadLine();
      }

      #region Отображение информации о C:\Windows
      static void ShowWindowsDirectoryInfo()
      {
         DirectoryInfo dir = new DirectoryInfo(@"C:\Windows");
         Console.WriteLine("***** Directory Info *****");
         Console.WriteLine("FullName: {0}", dir.FullName);
         Console.WriteLine("Name: {0}", dir.Name);
         Console.WriteLine("Parent: {0}", dir.Parent);
         Console.WriteLine("Creation: {0}", dir.CreationTime);
         Console.WriteLine("Attributes: {0}", dir.Attributes);
         Console.WriteLine("Root: {0}", dir.Root);
         Console.WriteLine("**************************\n");
      }
      #endregion

      #region Отобразить информацию о JPG файлах
      static void DisplayImageFiles()
      {
         DirectoryInfo dir = new DirectoryInfo(@"C:\Windows\Web\Wallpaper");

         // Получить все файлы с расширением *.jpg
         FileInfo[] imageFiles = dir.GetFiles("*.jpg", SearchOption.AllDirectories);

         // Сколько мы нашли?
         Console.WriteLine("Found {0} *.jpg files\n", imageFiles.Length);

         // Напечатаем их все.
         foreach (FileInfo f in imageFiles)
         {
            Console.WriteLine("***************************");
            Console.WriteLine("File name: {0}", f.Name);
            Console.WriteLine("File size: {0}", f.Length);
            Console.WriteLine("Creation: {0}", f.CreationTime);
            Console.WriteLine("Attributes: {0}", f.Attributes);
            Console.WriteLine("***************************\n");
         }
      }
      #endregion

      #region Модификация структуры каталога
      static void ModifyAppDirectory()
      {
         DirectoryInfo dir = new DirectoryInfo(".");

         // Создать \MyFolder относительно текущего каталога
         dir.CreateSubdirectory("MyFolder");

         // Создать и вернуть информацию о созданном
         DirectoryInfo myDataFolder = dir.CreateSubdirectory(@"MyFolder2\Data");

         // Печатаем путь к ..\MyFolder2\Data.
         Console.WriteLine("New Folder is: {0}", myDataFolder);
      }
      #endregion

      #region Используем класс Directory
      static void FunWithDirectoryType()
      {
         // Печатаем все диски на компьютере
         string[] drives = Directory.GetLogicalDrives();
         Console.WriteLine("Here are your drives:");
         foreach (string s in drives)
            Console.WriteLine("--> {0} ", s);

         // Удалим то, что создали
         Console.WriteLine("Press Enter to delete directories");
         Console.ReadLine();
         try
         {
            Directory.Delete(@"C:\MyFolder");

            // Если мы хотим удалить подкаталоги, указываем true во 2-м параметре
            Directory.Delete(@"C:\MyFolder2", true);
         }
         catch (IOException e)
         {
            Console.WriteLine(e.Message);
         }
      }
      #endregion

   }
}
