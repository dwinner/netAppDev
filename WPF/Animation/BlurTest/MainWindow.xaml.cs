using System;
using System.Windows;

namespace BlurTest
{
    public partial class MainWindow
    {
        private const int BlurRadius = 10;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txt.BlurApply(BlurRadius, new TimeSpan(0, 0, 1), TimeSpan.Zero);
                MessageBox.Show("Test");
            }
            finally
            {
                txt.BlurDisable(new TimeSpan(0, 0, 5), TimeSpan.Zero);
            }
        }
    }
}