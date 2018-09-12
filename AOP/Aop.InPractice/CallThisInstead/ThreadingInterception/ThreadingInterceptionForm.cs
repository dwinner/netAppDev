using System;
using System.Windows.Forms;

namespace ThreadingInterception
{
   public partial class ThreadingInterceptionForm : Form
   {
      private TwitterService _twitterService;

      public ThreadingInterceptionForm()
      {
         InitializeComponent();
      }

      protected override void OnLoad(EventArgs e) => _twitterService = new TwitterService();

      private void UpdateButton_Click(object sender, EventArgs e)
      {
         GetNewTweet();
      }

      [WorkerThread]
      private void GetNewTweet()
      {
         var tweet = _twitterService.GetTweet();
         UpdateTweetListBox(tweet);
      }

      [UiThread]
      private void UpdateTweetListBox(string tweet) => TweetListBox.Items.Add(tweet);
   }
}