using System;
using System.Reactive.Linq;
using System.Windows;
using Helpers;

namespace ObserveUIEvents;

public partial class MainWindow
{
   public MainWindow()
   {
      InitializeComponent();
      var clicks =
         Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
            h => theButton.Click += h,
            h => theButton.Click -= h);

      //IObservable<EventPattern<object>> clicks = Observable.FromEventPattern(theButton, "Click");

      // the message will be written to VS output window
      clicks.SubscribeConsole();

      // the message will be written in the TextBox
      clicks.Subscribe(eventPattern => output.Text += "button clicked" + Environment.NewLine);
   }
}