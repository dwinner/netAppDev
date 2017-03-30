using System;
using System.IO;

namespace SatelliteMovingApp.Lib.Utils
{
   /// <summary>
   ///    Вспомогательный класс для чтения подключаемых необработанных (raw) файлов
   /// </summary>
   public static class RawReaderUtils
   {
      /// <summary>
      ///    Преобразование входного потока в строку
      /// </summary>
      /// <param name="anInputStream">Объект <code>Stream</code> для чтения</param>
      /// <returns>Объект <code>String</code>, принявший данные входного потока</returns>
      public static string InputStreamToString(Stream anInputStream)
      {
         throw new NotImplementedException();
      }

      /*
      public static String inputStreamToString(InputStream inputStream) throws IOException
      {
         BufferedReader bufferedReader = null;
         try
         {
            StringBuilder strBuilder = new StringBuilder(inputStream.available());
            bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
            String line;
            while ((line = bufferedReader.readLine()) != null)
            {
               strBuilder.append(line).append("\n");
            }
            return strBuilder.toString();
         }
         finally
         {
            if (bufferedReader != null)
            {
               bufferedReader.close();
            }
            inputStream.close();
         }
      } 
      */
   }
}