﻿using ControlsSamples.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ControlsSamples
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void OnNavigate(XamlUICommand sender, ExecuteRequestedEventArgs args)
        {
            Type pageType = args.Parameter switch
            {
                "Text" => typeof(TextPage),
                "Presenters" => typeof(PresentersPage),
                "Ranges" => typeof(RangeControlsPage),
                "Buttons" => typeof(ButtonsPage),
                "Dates" => typeof(DateSelectionPage),
                "Web" => typeof(WebView2Page),
                _ => throw new InvalidOperationException()
            };

            MainFrame.Navigate(pageType);
        }
    }
}
