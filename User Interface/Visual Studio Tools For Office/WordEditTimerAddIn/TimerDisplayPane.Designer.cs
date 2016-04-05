namespace WordEditTimerAddIn
{
   partial class TimerDisplayPane
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

      #region Код, автоматически созданный конструктором компонентов

      /// <summary> 
      /// Обязательный метод для поддержки конструктора - не изменяйте 
      /// содержимое данного метода при помощи редактора кода.
      /// </summary>
      private void InitializeComponent()
      {
         this.RefreshButton = new System.Windows.Forms.Button();
         this.DocEditLabel = new System.Windows.Forms.Label();
         this.TimerListBox = new System.Windows.Forms.ListBox();
         this.SuspendLayout();
         // 
         // RefreshButton
         // 
         this.RefreshButton.AutoSize = true;
         this.RefreshButton.Dock = System.Windows.Forms.DockStyle.Top;
         this.RefreshButton.Location = new System.Drawing.Point(0, 0);
         this.RefreshButton.Name = "RefreshButton";
         this.RefreshButton.Size = new System.Drawing.Size(150, 23);
         this.RefreshButton.TabIndex = 0;
         this.RefreshButton.Text = "Refresh";
         this.RefreshButton.UseVisualStyleBackColor = true;
         this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
         // 
         // DocEditLabel
         // 
         this.DocEditLabel.AutoSize = true;
         this.DocEditLabel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.DocEditLabel.Location = new System.Drawing.Point(0, 23);
         this.DocEditLabel.Name = "DocEditLabel";
         this.DocEditLabel.Size = new System.Drawing.Size(109, 13);
         this.DocEditLabel.TabIndex = 1;
         this.DocEditLabel.Text = "Document edit timers:";
         this.DocEditLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // TimerListBox
         // 
         this.TimerListBox.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.TimerListBox.FormattingEnabled = true;
         this.TimerListBox.Location = new System.Drawing.Point(0, 42);
         this.TimerListBox.Name = "TimerListBox";
         this.TimerListBox.Size = new System.Drawing.Size(150, 108);
         this.TimerListBox.TabIndex = 2;
         // 
         // TimerDisplayPane
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.TimerListBox);
         this.Controls.Add(this.DocEditLabel);
         this.Controls.Add(this.RefreshButton);
         this.Name = "TimerDisplayPane";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button RefreshButton;
      private System.Windows.Forms.Label DocEditLabel;
      private System.Windows.Forms.ListBox TimerListBox;
   }
}
