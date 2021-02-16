/**
 * Культуры в действии
 */

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace CultureDemo
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
         SetupCultures();
      }

      private void SetupCultures()
      {
         Dictionary<string, CultureData> cultureDatas =
            CultureInfo.GetCultures(CultureTypes.AllCultures)
               .OrderBy(info => info.Name)
               .Select(info => new CultureData { CultureInfo = info, SubCultures = new List<CultureData>() })
               .ToDictionary(data => data.CultureInfo.Name);

         var rootCultures = new List<CultureData>();
         foreach (var cultureData in cultureDatas.Values)
         {
            if (cultureData.CultureInfo.Parent.LCID == 127) // Идентификатор инвариантной культуры
            {
               rootCultures.Add(cultureData);
            }
            else
            {
               CultureData parentCultureData;
               if (cultureDatas.TryGetValue(cultureData.CultureInfo.Parent.Name, out parentCultureData))
               {
                  parentCultureData.SubCultures.Add(cultureData);
               }
               else
               {
                  throw new ParentCultureException("unexpected error - parent culture not found");
               }
            }
         }

         DataContext = rootCultures.OrderBy(data => data.CultureInfo.EnglishName);
      }

      private void CultureTreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
      {
         var cultureData = e.NewValue as CultureData;
         if (cultureData != null)
         {
            ItemGrid.DataContext = cultureData;
         }
      }
   }
}
