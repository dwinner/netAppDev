/**
 * Типовое приложение WPF
 */

using System;
using System.Windows;
using System.Windows.Input;

namespace EightBall
{   
   public partial class EightBallWindow
   {
      public EightBallWindow()
      {
         InitializeComponent();
      }

      private void AnswerButton_Click(object sender, RoutedEventArgs e)
      {
         Cursor = Cursors.Wait;
         System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

         var generator = new AnswerGenerator();
         AnswerTextBox.Text = generator.GetRandomAnswer(QuestionTextBox.Text);
         Cursor = null;
      }
   }
}