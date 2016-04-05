namespace _02_EventBasedAsync
{
   partial class MainForm
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
         this.uriTextBox = new System.Windows.Forms.TextBox();
         this.downloadedTextBox = new System.Windows.Forms.TextBox();
         this.goButton = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // uriTextBox
         // 
         this.uriTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.uriTextBox.Location = new System.Drawing.Point(13, 13);
         this.uriTextBox.Name = "uriTextBox";
         this.uriTextBox.Size = new System.Drawing.Size(213, 20);
         this.uriTextBox.TabIndex = 0;
         // 
         // downloadedTextBox
         // 
         this.downloadedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.downloadedTextBox.Location = new System.Drawing.Point(13, 39);
         this.downloadedTextBox.Multiline = true;
         this.downloadedTextBox.Name = "downloadedTextBox";
         this.downloadedTextBox.ReadOnly = true;
         this.downloadedTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.downloadedTextBox.Size = new System.Drawing.Size(359, 210);
         this.downloadedTextBox.TabIndex = 1;
         // 
         // goButton
         // 
         this.goButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.goButton.Location = new System.Drawing.Point(297, 9);
         this.goButton.Name = "goButton";
         this.goButton.Size = new System.Drawing.Size(75, 23);
         this.goButton.TabIndex = 2;
         this.goButton.Text = "Go";
         this.goButton.UseVisualStyleBackColor = true;
         this.goButton.Click += new System.EventHandler(this.goButton_Click);
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(401, 261);
         this.Controls.Add(this.goButton);
         this.Controls.Add(this.downloadedTextBox);
         this.Controls.Add(this.uriTextBox);
         this.Name = "MainForm";
         this.Text = "Main form";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox uriTextBox;
      private System.Windows.Forms.TextBox downloadedTextBox;
      private System.Windows.Forms.Button goButton;
   }
}

