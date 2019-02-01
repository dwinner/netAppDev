using System;

namespace AppDevUnited.SelfTester.Model.Poco
{
   /// <summary>
   ///    Сущность для упрощения логики замены старого / создания нового эталона
   /// </summary>
   public class ApplyEtalonEntry : IEquatable<ApplyEtalonEntry>
   {
      /// <summary>
      ///    Конструктор эталонной записи
      /// </summary>
      /// <param name="etalonLog">Абсолютный путь к эталонному логу</param>
      /// <param name="testingLog">Абсолютный путь к тестовому логу</param>
      /// <param name="newEtalon">Является ли тестовый лог новым для эталона</param>
      public ApplyEtalonEntry(string etalonLog, string testingLog, bool newEtalon = false)
      {
         EtalonLog = etalonLog;
         TestingLog = testingLog;
         NewEtalon = newEtalon;
      }

      /// <summary>
      ///    Абсолютный путь к эталонному логу
      /// </summary>
      public string EtalonLog { get; private set; }

      /// <summary>
      ///    Абсолютный путь к тестовому логу
      /// </summary>
      public string TestingLog { get; private set; }

      /// <summary>
      ///    Является ли тестовый лог новым для эталона
      /// </summary>
      public bool NewEtalon { get; private set; }

      public bool Equals(ApplyEtalonEntry other)
      {
         return !ReferenceEquals(null, other) && (ReferenceEquals(this, other) ||
                                                  string.Equals(EtalonLog, other.EtalonLog) &&
                                                  string.Equals(TestingLog, other.TestingLog) &&
                                                  NewEtalon.Equals(other.NewEtalon));
      }

      public override string ToString()
      {
         return string.Format("EtalonLog: {0}, TestingLog: {1}, NewEtalon: {2}", EtalonLog, TestingLog, NewEtalon);
      }

      public override bool Equals(object obj)
      {
         return !ReferenceEquals(null, obj) &&
                (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((ApplyEtalonEntry)obj));
      }

      public override int GetHashCode()
      {
         unchecked
         {
            var hashCode = (EtalonLog != null ? EtalonLog.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (TestingLog != null ? TestingLog.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ NewEtalon.GetHashCode();
            return hashCode;
         }
      }

      public static bool operator ==(ApplyEtalonEntry left, ApplyEtalonEntry right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(ApplyEtalonEntry left, ApplyEtalonEntry right)
      {
         return !Equals(left, right);
      }
   }
}