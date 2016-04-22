using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace JpegMetadata.Library.Tests
{
   /// <summary>
   /// Тесты на сжатие изображений
   /// </summary>
   public class PictureCompressorTest
   {
      private string _compressFileName;
      private string _originalFileName;
      private List<string> _filesToDelete;
      private const string CompressSuffix = "_compressed.jpg";

      [TestFixtureSetUp]
      public void CompressorSetUp()
      {
         _originalFileName = "pictures/Corp.jpg";
         _compressFileName = _originalFileName + CompressSuffix;
         _filesToDelete = new List<string>();
      }

      /// <summary>
      /// Изменение размеров
      /// </summary>
      [Test]
      public void ResizePictureTest()
      {
         byte[] compressedPicture = ImageProcessorUtils.ResizePicture(_originalFileName, 150);
         using (var fileStream = new FileStream(_compressFileName, FileMode.Create, FileAccess.Write, FileShare.None))
         {
            fileStream.Write(compressedPicture, 0, compressedPicture.Length);
         }
      }

      /// <summary>
      /// Изменение качества изображений
      /// </summary>
      /// <param name="fileName">Имя файла</param>
      [TestCase("pictures/Corp.jpg")]
      [TestCase("pictures/Zebrous.png")]
      public void ChangeCompressionQualityTest(string fileName)
      {
         var byteList = new List<byte[]>(3)
         {
            ImageProcessorUtils.ChangeCompressionQuality(fileName, 0L),
            ImageProcessorUtils.ChangeCompressionQuality(fileName),
            ImageProcessorUtils.ChangeCompressionQuality(fileName, 100)
         };

         for (int i = 0; i < byteList.Count; i++)
         {
            File.WriteAllBytes(fileName + i, byteList[i]);
            _filesToDelete.Add(fileName + i);
         }
      }

      [TestFixtureTearDown]
      public void CompressorDestroyer()
      {
         File.Delete(_compressFileName);
         foreach (var file in _filesToDelete)
         {
            File.Delete(file);
         }
      }
   }
}