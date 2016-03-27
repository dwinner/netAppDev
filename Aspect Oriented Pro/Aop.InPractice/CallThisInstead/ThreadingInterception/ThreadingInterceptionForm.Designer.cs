namespace ThreadingInterception
{
   partial class ThreadingInterceptionForm
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.UpdateButton = new System.Windows.Forms.Button();
         this.TweetListBox = new System.Windows.Forms.ListBox();
         this.SuspendLayout();
         // 
         // UpdateButton
         // 
         this.UpdateButton.Location = new System.Drawing.Point(13, 13);
         this.UpdateButton.Name = "UpdateButton";
         this.UpdateButton.Size = new System.Drawing.Size(75, 23);
         this.UpdateButton.TabIndex = 0;
         this.UpdateButton.Text = "Update Tweets";
         this.UpdateButton.UseVisualStyleBackColor = true;
         this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
         // 
         // TweetListBox
         // 
         this.TweetListBox.FormattingEnabled = true;
         this.TweetListBox.Location = new System.Drawing.Point(12, 42);
         this.TweetListBox.Name = "TweetListBox";
         this.TweetListBox.Size = new System.Drawing.Size(120, 95);
         this.TweetListBox.TabIndex = 1;
         // 
         // ThreadingInterceptionForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(284, 261);
         this.Controls.Add(this.TweetListBox);
         this.Controls.Add(this.UpdateButton);
         this.Name = "ThreadingInterceptionForm";
         this.Text = "Threading interception";
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button UpdateButton;
      private System.Windows.Forms.ListBox TweetListBox;
   }
}

