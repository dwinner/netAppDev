using ProgramVerificationSystems.SelfTester.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace ProgramVerificationSystems.SelfTester.UI
{
   /// <summary>
   ///    Конвертер привязки данных для соответствия статусов и цветов
   /// </summary>
   public class RunningStatusToColorConverter : IValueConverter
   {
      private readonly IDictionary<Brush, AnalysisStatus> _brushStringMap = new Dictionary<Brush, AnalysisStatus>
      {
         {Brushes.White, AnalysisStatus.NotStarted},
         {Brushes.LightGray, AnalysisStatus.VsVersionNotInstalled},
         {Brushes.DimGray, AnalysisStatus.PluginNotInstalled},
         {Brushes.DarkGray, AnalysisStatus.VsVersionNotSupported},
         {Brushes.LightBlue, AnalysisStatus.InPending},
         {Brushes.Green, AnalysisStatus.OkFinished},
         {Brushes.Red, AnalysisStatus.DiffFinished},
         {Brushes.RoyalBlue, AnalysisStatus.InProgress},
         {Brushes.Orange, AnalysisStatus.PluginCrashed},
         {Brushes.Violet, AnalysisStatus.NoSuchEtalon},
         {Brushes.Crimson, AnalysisStatus.Opening},
         {Brushes.Magenta, AnalysisStatus.Opened},
         {Brushes.BlueViolet, AnalysisStatus.InUpgrading},
         {Brushes.BurlyWood, AnalysisStatus.СomparingLogs}
      };

      private readonly Brush _defaultNotFoundedBrush = Brushes.White;

      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (value == null || value.GetType() != typeof(AnalysisStatus))
            throw new InvalidOperationException(
               string.Format("value {0} must be casted to {1}", value, typeof(AnalysisStatus)));

         var statusValue = (AnalysisStatus)value;

         if (targetType == typeof(Brush))
         {
            var foundedBrush = _defaultNotFoundedBrush;
            foreach (var brushStatusPair in _brushStringMap.Where(pair => statusValue == pair.Value))
            {
               foundedBrush = brushStatusPair.Key;
               break;
            }

            return foundedBrush;
         }

         if (targetType == typeof(bool))
            return statusValue != AnalysisStatus.VsVersionNotSupported;

         throw new InvalidOperationException(string.Format("Not supported type {0}", targetType));
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException(string.Format("ConvertBack is not implemented for {0}", GetType()));
      }
   }

   /// <summary>
   ///    Конвертер привязки данных для соответствия статусов их описаниям
   /// </summary>
   public class RunningStatusToStringConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (value == null || value.GetType() != typeof(AnalysisStatus))
            throw new InvalidOperationException(
               string.Format("value {0} must be casted to {1}", value, typeof(AnalysisStatus)));

         var statusValue = (AnalysisStatus)value;
         return statusValue.GetAnalysisStatusDescription();
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException(string.Format("ConvertBack is not implemented for {0}", GetType()));
      }
   }
}