using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DataBinding
{
   public sealed class TileView : ViewBase
   {
      private const string TileViewResourceId = "TileView";
      private const string TileViewItemResourceId = "TileViewItem";

      public DataTemplate ItemTemplate { get; set; }

      public Brush SelectedBackground { get; set; } = Brushes.Transparent;

      public Brush SelectedBorderBrush { get; } = Brushes.Black;

      protected override object DefaultStyleKey
         => new ComponentResourceKey(GetType(), TileViewResourceId);

      protected override object ItemContainerDefaultStyleKey
         => new ComponentResourceKey(GetType(), TileViewItemResourceId);
   }
}