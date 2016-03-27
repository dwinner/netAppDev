// Способ создания слабого делегата

using System;
using System.Drawing;
using System.Windows.Forms;
using Memory.Library;

namespace _07_WeakDelegateSample
{
   internal static class Program
   {
      [STAThread]
      private static void Main()
      {
         Go();
      }

      private static void Go()
      {
         var form = new Form
         {
            Text = "Weak delegate test",
            FormBorderStyle = FormBorderStyle.FixedSingle
         };

         var testButton = new Button
         {
            Text = "Click me",
            Width = form.Width / 2
         };

         var gcButton = new Button
         {
            Text = "Force GC",
            Left = testButton.Width,
            Width = testButton.Width
         };

         // WeakEventHandler превращает делегат EventHandler в его слабую версию
         testButton.Click += new WeakEventHandler(new DoNotLiveJustForTheEvent().Clicked)
         {
            RemoveDelegateCode = handler => testButton.Click -= handler
         };

         gcButton.Click += (sender, args) =>
         {
            GC.Collect();
            MessageBox.Show("GC complete");
         };

         form.Controls.Add(testButton);
         form.Controls.Add(gcButton);
         form.ClientSize = new Size(testButton.Width * 2, testButton.Height * 2);
         form.ShowDialog();
      }
   }

   internal sealed class DoNotLiveJustForTheEvent
   {
      public void Clicked(object sender, EventArgs e)
      {
         MessageBox.Show("Test got notified of button click.");
      }
   }
}