using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AppDevUnited.SelfTester.Model.Poco;
using AppDevUnited.SelfTester.Model.Utils;

namespace AppDevUnited.SelfTester.Model.BusinessLogic
{
   /// <summary>
   ///    Тип, заменяющий эталонные логи
   /// </summary>
   public class ReplaceEtalonsExecutor
   {
      private readonly string _etalonFolder;
      private readonly IEnumerable<string> _logsToReplace;

      public ReplaceEtalonsExecutor(string etalonFolder, IEnumerable<string> logsToReplace)
      {
         _etalonFolder = etalonFolder;
         _logsToReplace = logsToReplace;
      }

      /// <summary>
      ///    Обновление эталонных логов
      /// </summary>
      /// <returns>Задача продолжения с результатом обновления в виде пары [Кол-во обновленных эталонов, Кол-во новых эталонов]</returns>
      public Task<Tuple<int, int>> UpdateEtalonsAsync()
      {
         return Task.Run(() => UpdateEtalons());
      }

      /// <summary>
      ///    Обновление эталонных логов
      /// </summary>
      /// <returns>Кортеж [Кол-во обновленных эталонов, Кол-во новых эталонов]</returns>
      private Tuple<int, int> UpdateEtalons()
      {
         var etalons = Directory.GetFiles(_etalonFolder,
            string.Format("*{0}", CoreExtensions.EtalonLogExt), SearchOption.AllDirectories);
         var applyEtalonEntries = GetReplaceEntries(_etalonFolder, etalons, _logsToReplace).ToArray();

         int updated = 0, created = 0;
         foreach (var entry in applyEtalonEntries)
         {
            if (entry.NewEtalon)
            {
               File.WriteAllText(entry.EtalonLog, File.ReadAllText(entry.TestingLog));
               created++;
            }
            else
            {
               File.Copy(entry.TestingLog, entry.EtalonLog, true);
               var diffFile = entry.TestingLog.GetDiffFile();
               if (diffFile != string.Empty)
               {
                  File.Delete(diffFile);
               }

               updated++;
            }
         }

         return Tuple.Create(updated, created);
      }

      /// <summary>
      ///    Получение набора эталонных сущностей, которые нужно заменить
      /// </summary>
      /// <param name="etalonFolder">Путь к директории с эталонами</param>
      /// <param name="etalonFiles">Набор абсолютных путей для эталонных логов</param>
      /// <param name="logsToReplaceEtalons">Набор абсолютных путей для тестовых логов</param>
      /// <returns>Набор эталонных сущностей, которые нужно заменить</returns>
      private static IEnumerable<ApplyEtalonEntry> GetReplaceEntries(string etalonFolder,
         ICollection<string> etalonFiles,
         IEnumerable<string> logsToReplaceEtalons)
      {
         var applyEtalonEntries = new List<ApplyEtalonEntry>(etalonFiles.Count);

         var etalonsToBeReplaced = etalonFiles.ToArray();
         var etalonFileNamesToBeReplaced = etalonsToBeReplaced.Select(Path.GetFileName).ToArray();

         var logsToReplace = logsToReplaceEtalons.ToArray();
         var logFileNamesToReplace = logsToReplace.Select(Path.GetFileName).ToArray();

         for (var i = 0; i < logFileNamesToReplace.Length; i++)
         {
            var foundEtalonIndex = -1;

            for (var j = 0; j < etalonFileNamesToBeReplaced.Length; j++)
            {
               if (
                  string.Compare(logFileNamesToReplace[i], etalonFileNamesToBeReplaced[j],
                     StringComparison.OrdinalIgnoreCase) == 0)
               {
                  foundEtalonIndex = j;
                  break;
               }
            }

            var etalonEntry = foundEtalonIndex == -1
               ? new ApplyEtalonEntry(
                  string.Format("{0}{1}{2}", etalonFolder, Path.DirectorySeparatorChar, logFileNamesToReplace[i]),
                  logsToReplace[i], true)
               : new ApplyEtalonEntry(etalonsToBeReplaced[foundEtalonIndex], logsToReplace[i]);

            applyEtalonEntries.Add(etalonEntry);
         }

         return applyEtalonEntries;
      }
   }
}