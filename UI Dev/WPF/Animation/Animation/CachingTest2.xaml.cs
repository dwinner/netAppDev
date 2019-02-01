using System.Windows;
using System.Windows.Media;

namespace Animation
{
   public partial class CachingTest2
   {
      public CachingTest2()
      {
         InitializeComponent();
      }

      private void OnCacheOnOff(object sender, RoutedEventArgs e)
      {
         if (CacheCheckbox.IsChecked == true)
         {
            var bitmapCache = new BitmapCache { RenderAtScale = 5 };
            CmdButton.CacheMode = bitmapCache;
            SourceImg.CacheMode = new BitmapCache();
         }
         else
         {
            CmdButton.CacheMode = null;
            SourceImg.CacheMode = null;
         }
      }
   }
}