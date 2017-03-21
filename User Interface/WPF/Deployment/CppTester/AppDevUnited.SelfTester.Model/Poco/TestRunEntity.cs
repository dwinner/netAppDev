using System;
using AppDevUnited.SelfTester.Model.Utils;

namespace AppDevUnited.SelfTester.Model.Poco
{
   /// <summary>
   /// Сущность тестового запуска
   /// </summary>
   public sealed class TestRunEntity : IComparable<TestRunEntity>, IEquatable<TestRunEntity>
   {
      /// <summary>
      /// Абсолютный путь к директории тестового запуска
      /// </summary>
      public string TestRunDirectory { get; set; }

      /// <summary>
      /// Имя машины
      /// </summary>
      public string MachineName { private get; set; }

      /// <summary>
      /// Дата и время запуска
      /// </summary>
      public DateTime RunningDateTime { private get; set; }

      public int CompareTo(TestRunEntity other)
      {
         return RunningDateTime.CompareTo(other.RunningDateTime);
      }

      public static implicit operator TestRunEntity(string testRunFolder)
      {
         return TestedSolutionsManager.ObtainTestRunEntity(testRunFolder);
      }

      public override string ToString()
      {
         return string.Format("{0:G} ({1})", RunningDateTime, MachineName);
      }

      public bool Equals(TestRunEntity other)
      {
         return !ReferenceEquals(null, other) && (ReferenceEquals(this, other) ||
                                                  string.Equals(TestRunDirectory, other.TestRunDirectory) &&
                                                  string.Equals(MachineName, other.MachineName) &&
                                                  RunningDateTime.Equals(other.RunningDateTime));
      }

      public override bool Equals(object obj)
      {
         return !ReferenceEquals(null, obj) &&
                (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((TestRunEntity)obj));
      }

      public override int GetHashCode()
      {
         unchecked
         {
            var hashCode = (TestRunDirectory != null ? TestRunDirectory.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (MachineName != null ? MachineName.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ RunningDateTime.GetHashCode();
            return hashCode;
         }
      }

      public static bool operator ==(TestRunEntity left, TestRunEntity right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(TestRunEntity left, TestRunEntity right)
      {
         return !Equals(left, right);
      }
   }
}