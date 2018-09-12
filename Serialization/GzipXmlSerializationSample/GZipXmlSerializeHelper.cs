using System;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;

namespace GzipXmlSerializationSample
{
   public sealed class GZipXmlSerializeHelper<T> : ICapableSerialize<T>
   {
      private const int DefaultBufferSize = 0x4000;
      private readonly MemoryStream _memoryStream = new MemoryStream();
      private readonly string _zipFileName;
      private GZipStream _gzipStream;
      private bool _isDisposed;

      public GZipXmlSerializeHelper(string zipFileName)
      {
         _zipFileName = zipFileName;
      }

      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      public void Serialize(T obj)
      {
         if (_isDisposed) throw new ObjectDisposedException(GetType().FullName);
         var serializer = new XmlSerializer(obj.GetType());
         serializer.Serialize(_memoryStream, obj);
         _memoryStream.Position = 0;

         _gzipStream = new GZipStream(File.Create(_zipFileName), CompressionMode.Compress);
         var buffer = new byte[DefaultBufferSize];
         int numRead;
         while ((numRead = _memoryStream.Read(buffer, 0, buffer.Length)) != 0)
         {
            _gzipStream.Write(buffer, 0, numRead);
         }
      }

      public T Deserialize()
      {
         if (_isDisposed) throw new ObjectDisposedException(GetType().FullName);
         _gzipStream = new GZipStream(new FileInfo(_zipFileName).OpenRead(), CompressionMode.Decompress);
         var buffer = new byte[DefaultBufferSize];

         int numRead;
         while ((numRead = _gzipStream.Read(buffer, 0, buffer.Length)) != 0)
         {
            _memoryStream.Capacity += numRead;
            _memoryStream.Write(buffer, 0, numRead);
         }

         _memoryStream.Position = 0;
         var deserializer = new XmlSerializer(typeof(T));
         var result = (T)deserializer.Deserialize(_memoryStream);

         return result;
      }

      ~GZipXmlSerializeHelper()
      {
         Dispose(false);
      }

      private void Dispose(bool disposing)
      {
         if (!_isDisposed && disposing)
         {
            _memoryStream.Dispose();
            _gzipStream.Dispose();
         }

         _isDisposed = true;
      }
   }
}