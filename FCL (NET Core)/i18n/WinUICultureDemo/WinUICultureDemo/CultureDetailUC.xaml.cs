using Microsoft.UI.Xaml;

#nullable enable

namespace WinUICultureDemo
{
   public sealed partial class CultureDetailUc
   {
      public CultureDetailUc() => InitializeComponent();

      public CultureData CultureData
      {
         get => (CultureData)GetValue(CultureDataProperty);
         set => SetValue(CultureDataProperty, value);
      }

      public static readonly DependencyProperty CultureDataProperty =
         DependencyProperty.Register(nameof(CultureData),
            typeof(CultureData),
            typeof(CultureDetailUc),
            new PropertyMetadata(null));
   }
}

#nullable restore