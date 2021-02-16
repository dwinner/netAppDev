using System;
using System.Speech.Recognition;

namespace SoundAndVideo
{
   public partial class SpeechRecognition
   {
      private readonly SpeechRecognizer _recognizer = new SpeechRecognizer();

      public SpeechRecognition()
      {
         InitializeComponent();

         var grammar = new GrammarBuilder();
         grammar.Append(new Choices("red", "blue", "green", "black", "white"));
         grammar.Append(new Choices("on", "off"));

         _recognizer.LoadGrammar(new Grammar(grammar));
         _recognizer.SpeechDetected += OnSpeechDetected;
         _recognizer.SpeechRecognized += OnSpeechRecognized;
         _recognizer.SpeechRecognitionRejected += OnSpeechRejected;
         _recognizer.SpeechHypothesized += OnSpeechHypothesized;
      }

      private void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
      {
         SpeechRecognitionLabel.Content = string.Format("You said: {0}", e.Result.Text);
      }

      private void OnSpeechDetected(object sender, SpeechDetectedEventArgs e)
      {
         SpeechRecognitionLabel.Content = "Speech detected.";
      }

      private void OnSpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
      {
         SpeechRecognitionLabel.Content = "Speech uncertain.";
      }

      private void OnSpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
      {
         SpeechRecognitionLabel.Content = "Speech rejected.";
      }

      protected override void OnClosed(EventArgs e)
      {
         _recognizer.Dispose();
      }
   }
}