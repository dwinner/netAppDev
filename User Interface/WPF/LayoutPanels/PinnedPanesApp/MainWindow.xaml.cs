using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PinnedPanesApp
{
   public partial class MainWindow
   {
      // Dummy columns for layers 0 and 1:
      private readonly ColumnDefinition _column1CloneForLayer0;
      private readonly ColumnDefinition _column2CloneForLayer0;
      private readonly ColumnDefinition _column2CloneForLayer1;

      public MainWindow()
      {
         InitializeComponent();

         // Initialize the dummy columns used when docking:
         _column1CloneForLayer0 = new ColumnDefinition {SharedSizeGroup = "column1"};
         _column2CloneForLayer0 = new ColumnDefinition {SharedSizeGroup = "column2"};
         _column2CloneForLayer1 = new ColumnDefinition {SharedSizeGroup = "column2"};
      }

      // Toggle between docked and undocked states (Pane 1)
      private void Pane1Pin_Click(object sender, RoutedEventArgs e)
      {
         if (Pane1Button.Visibility == Visibility.Collapsed)
            UndockPane(1);
         else
            DockPane(1);
      }

      // Toggle between docked and undocked states (Pane 2)
      private void Pane2Pin_Click(object sender, RoutedEventArgs e)
      {
         if (Pane2Button.Visibility == Visibility.Collapsed)
            UndockPane(2);
         else
            DockPane(2);
      }

      // Show Pane 1 when hovering over its button
      private void Pane1Button_MouseEnter(object sender, RoutedEventArgs e)
      {
         Layer1.Visibility = Visibility.Visible;

         // Adjust Z order to ensure the pane is on top:
         ParentGrid.Children.Remove(Layer1);
         ParentGrid.Children.Add(Layer1);

         // Ensure the other pane is hidden if it is undocked
         if (Pane2Button.Visibility == Visibility.Visible)
            Layer2.Visibility = Visibility.Collapsed;
      }

      // Show Pane 2 when hovering over its button
      private void Pane2Button_MouseEnter(object sender, RoutedEventArgs e)
      {
         Layer2.Visibility = Visibility.Visible;

         // Adjust Z order to ensure the pane is on top:
         ParentGrid.Children.Remove(Layer2);
         ParentGrid.Children.Add(Layer2);

         // Ensure the other pane is hidden if it is undocked
         if (Pane1Button.Visibility == Visibility.Visible)
            Layer1.Visibility = Visibility.Collapsed;
      }

      // Hide any undocked panes when the mouse enters Layer 0
      private void Layer0_MouseEnter(object sender, RoutedEventArgs e)
      {
         if (Pane1Button.Visibility == Visibility.Visible)
            Layer1.Visibility = Visibility.Collapsed;
         if (Pane2Button.Visibility == Visibility.Visible)
            Layer2.Visibility = Visibility.Collapsed;
      }

      // Hide the other pane if undocked when the mouse enters Pane 1
      private void pane1_MouseEnter(object sender, RoutedEventArgs e)
      {
         // Ensure the other pane is hidden if it is undocked
         if (Pane2Button.Visibility == Visibility.Visible)
            Layer2.Visibility = Visibility.Collapsed;
      }

      // Hide the other pane if undocked when the mouse enters Pane 2
      private void Pane2_MouseEnter(object sender, RoutedEventArgs e)
      {
         // Ensure the other pane is hidden if it is undocked
         if (Pane1Button.Visibility == Visibility.Visible)
            Layer1.Visibility = Visibility.Collapsed;
      }

      // Docks a pane, which hides the corresponding pane button
      private void DockPane(int paneNumber)
      {
         if (paneNumber == 1)
         {
            Pane1Button.Visibility = Visibility.Collapsed;
            Pane1PinImage.Source = new BitmapImage(new Uri("pin.gif", UriKind.Relative));

            // Add the cloned column to layer 0:
            Layer0.ColumnDefinitions.Add(_column1CloneForLayer0);
            // Add the cloned column to layer 1, but only if pane 2 is docked:
            if (Pane2Button.Visibility == Visibility.Collapsed) Layer1.ColumnDefinitions.Add(_column2CloneForLayer1);
         }
         else if (paneNumber == 2)
         {
            Pane2Button.Visibility = Visibility.Collapsed;
            Pane2PinImage.Source = new BitmapImage(new Uri("pin.gif", UriKind.Relative));

            // Add the cloned column to layer 0:
            Layer0.ColumnDefinitions.Add(_column2CloneForLayer0);
            // Add the cloned column to layer 1, but only if pane 1 is docked:
            if (Pane1Button.Visibility == Visibility.Collapsed) Layer1.ColumnDefinitions.Add(_column2CloneForLayer1);
         }
      }

      // Undocks a pane, which reveals the corresponding pane button
      private void UndockPane(int paneNumber)
      {
         if (paneNumber == 1)
         {
            Layer1.Visibility = Visibility.Collapsed;
            Pane1Button.Visibility = Visibility.Visible;
            Pane1PinImage.Source = new BitmapImage(new Uri("pinHorizontal.gif", UriKind.Relative));

            // Remove the cloned columns from layers 0 and 1:
            Layer0.ColumnDefinitions.Remove(_column1CloneForLayer0);
            // This won't always be present, but Remove silently ignores bad columns:
            Layer1.ColumnDefinitions.Remove(_column2CloneForLayer1);
         }
         else if (paneNumber == 2)
         {
            Layer2.Visibility = Visibility.Collapsed;
            Pane2Button.Visibility = Visibility.Visible;
            Pane2PinImage.Source = new BitmapImage(new Uri("pinHorizontal.gif", UriKind.Relative));

            // Remove the cloned columns from layers 0 and 1:
            Layer0.ColumnDefinitions.Remove(_column2CloneForLayer0);
            // This won't always be present, but Remove silently ignores bad columns:
            Layer1.ColumnDefinitions.Remove(_column2CloneForLayer1);
         }
      }
   }
}