namespace WinFormLocalization
{
   partial class BookOfTheDayForm
   {
      /// <summary>
      /// Требуется переменная конструктора.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Освободить все используемые ресурсы.
      /// </summary>
      /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Код, автоматически созданный конструктором форм Windows

      /// <summary>
      /// Обязательный метод для поддержки конструктора - не изменяйте
      /// содержимое данного метода при помощи редактора кода.
      /// </summary>
      private void InitializeComponent()
      {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookOfTheDayForm));
         this.BookOfTheDayLabel = new System.Windows.Forms.Label();
         this.DateTextBox = new System.Windows.Forms.TextBox();
         this.BookTitleTextBox = new System.Windows.Forms.TextBox();
         this.BooksSoldLabel = new System.Windows.Forms.Label();
         this.BooksSoldTextBox = new System.Windows.Forms.TextBox();
         this.FlagPictureBox = new System.Windows.Forms.PictureBox();
         ((System.ComponentModel.ISupportInitialize)(this.FlagPictureBox)).BeginInit();
         this.SuspendLayout();
         // 
         // BookOfTheDayLabel
         // 
         resources.ApplyResources(this.BookOfTheDayLabel, "BookOfTheDayLabel");
         this.BookOfTheDayLabel.Name = "BookOfTheDayLabel";
         // 
         // DateTextBox
         // 
         resources.ApplyResources(this.DateTextBox, "DateTextBox");
         this.DateTextBox.Name = "DateTextBox";
         // 
         // BookTitleTextBox
         // 
         resources.ApplyResources(this.BookTitleTextBox, "BookTitleTextBox");
         this.BookTitleTextBox.Name = "BookTitleTextBox";
         // 
         // BooksSoldLabel
         // 
         resources.ApplyResources(this.BooksSoldLabel, "BooksSoldLabel");
         this.BooksSoldLabel.Name = "BooksSoldLabel";
         // 
         // BooksSoldTextBox
         // 
         resources.ApplyResources(this.BooksSoldTextBox, "BooksSoldTextBox");
         this.BooksSoldTextBox.Name = "BooksSoldTextBox";
         // 
         // FlagPictureBox
         // 
         resources.ApplyResources(this.FlagPictureBox, "FlagPictureBox");
         this.FlagPictureBox.Name = "FlagPictureBox";
         this.FlagPictureBox.TabStop = false;
         // 
         // BookOfTheDayForm
         // 
         resources.ApplyResources(this, "$this");
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.BooksSoldTextBox);
         this.Controls.Add(this.BooksSoldLabel);
         this.Controls.Add(this.BookTitleTextBox);
         this.Controls.Add(this.DateTextBox);
         this.Controls.Add(this.BookOfTheDayLabel);
         this.Controls.Add(this.FlagPictureBox);
         this.Name = "BookOfTheDayForm";
         ((System.ComponentModel.ISupportInitialize)(this.FlagPictureBox)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.PictureBox FlagPictureBox;
      private System.Windows.Forms.Label BookOfTheDayLabel;
      private System.Windows.Forms.TextBox DateTextBox;
      private System.Windows.Forms.TextBox BookTitleTextBox;
      private System.Windows.Forms.Label BooksSoldLabel;
      private System.Windows.Forms.TextBox BooksSoldTextBox;
   }
}

