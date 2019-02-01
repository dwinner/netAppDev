using System;
using System.Speech.Synthesis;
using System.Windows;

namespace SoundAndVideo
{
   public partial class SpeechSynthesis
   {
      public SpeechSynthesis()
      {
         InitializeComponent();
      }

      private void OnSpeak(object sender, RoutedEventArgs e)
      {
         var synthesizer = new SpeechSynthesizer();
         synthesizer.Speak(WordsTextBox.Text);
      }

      private void OnPrompt(object sender, RoutedEventArgs e)
      {
         var promptBuilder = new PromptBuilder();

         promptBuilder.AppendText("How are you");
         promptBuilder.AppendBreak(TimeSpan.FromSeconds(2));
         promptBuilder.AppendText("How ", PromptEmphasis.Reduced);
         var style = new PromptStyle
         {
            Rate = PromptRate.ExtraSlow,
            Emphasis = PromptEmphasis.Strong
         };
         promptBuilder.StartStyle(style);
         promptBuilder.AppendText("are ");
         promptBuilder.EndStyle();
         promptBuilder.AppendText("you?");

         var synthesizer = new SpeechSynthesizer();
         synthesizer.Speak(promptBuilder);
      }
   }
}