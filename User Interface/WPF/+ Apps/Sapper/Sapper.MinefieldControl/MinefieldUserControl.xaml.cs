using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Sapper.MinefieldControl
{
   public partial class MinefieldUserControl
   {
      private const uint DefaultMinefieldSize = 10;
      private const uint DefaultMinefieldCellMargin = 5;

      public static readonly DependencyProperty MinefieldSizeProperty = DependencyProperty.Register(
         nameof(MinefieldSize), typeof(uint), typeof(MinefieldUserControl), new PropertyMetadata(DefaultMinefieldSize));

      public static readonly DependencyProperty MinefieldCellMarginProperty = DependencyProperty.Register(
         nameof(MinefieldCellMargin), typeof(uint), typeof(MinefieldUserControl),
         new PropertyMetadata(DefaultMinefieldCellMargin));

      public MinefieldUserControl()
      {
         InitializeComponent();

         Canvas minefieldCanvas=new Canvas();

         var actualWidth = minefieldCanvas.Width;
         var actualHeight = minefieldCanvas.Height;

         var commonMinefieldWidth = Math.Floor(actualWidth / MinefieldSize) - 2 * MinefieldCellMargin;
         var commonMinefieldHeight = Math.Floor(actualHeight / MinefieldSize) - 2 * MinefieldCellMargin;

         for (var rowIndex = 0; rowIndex < MinefieldSize; rowIndex++)
         {
            var fieldRectangle = new Rectangle
            {
               Width = commonMinefieldWidth,
               Height = commonMinefieldHeight,
               Margin = new Thickness(MinefieldCellMargin)
            };
            minefieldCanvas.Children.Add(fieldRectangle);
            Canvas.SetTop(fieldRectangle, 0);
            Canvas.SetLeft(fieldRectangle, rowIndex * commonMinefieldWidth);
         }

         Minefield.Content = minefieldCanvas;
      }      

      public uint MinefieldCellMargin
      {
         get => (uint) GetValue(MinefieldCellMarginProperty);
         set => SetValue(MinefieldCellMarginProperty, value);
      }

      public uint MinefieldSize
      {
         get => (uint) GetValue(MinefieldSizeProperty);
         set => SetValue(MinefieldSizeProperty, value);
      }      
   }
}