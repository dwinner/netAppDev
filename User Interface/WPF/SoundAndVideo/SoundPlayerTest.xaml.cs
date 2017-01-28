using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Media;

namespace SoundAndVideo
{
   public partial class SoundPlayerTest
   {
      private readonly MediaPlayer _mediaPlayer = new MediaPlayer();

      public SoundPlayerTest()
      {
         InitializeComponent();
      }

      private void OnPlayAudio(object sender, RoutedEventArgs e)
      {
         var soundPlayer = new SoundPlayer {Stream = Properties.Resources.chimes};
         try
         {
            soundPlayer.Load();
            soundPlayer.PlaySync();
         }
         catch (FileNotFoundException)
         {
            // An error will occur here if the file can't be found.
         }
         catch (FormatException)
         {
            // A FormatException will occur here if the file doesn't
            // contain valid WAV audio.
         }
      }

      private void OnPlayAudioAsync(object sender, RoutedEventArgs e)
      {
         var soundPlayer = new SoundPlayer
         {
            SoundLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.wav")
         };
         try
         {
            soundPlayer.Load();
            soundPlayer.Play();
         }
         catch (FileNotFoundException)
         {
            // An error will occur here if the file can't be found.
         }
         catch (FormatException)
         {
            // A FormatException will occur here if the file doesn't
            // contain valid WAV audio.
         }
      }

      private void OnPlayWithMediaPlayer(object sender, RoutedEventArgs e)
      {
         _mediaPlayer.Open(new Uri("test.mp3", UriKind.Relative));
         _mediaPlayer.Play();
      }

      private void OnClosed(object sender, EventArgs e)
      {
         _mediaPlayer.Close();
      }
   }
}