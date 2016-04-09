namespace CarOrderApp
{
   partial class ImageOrderAutoDialog
   {
      /// <summary>
      ///Требуется переменная конструктора.
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
         this.LogoPictureBox = new System.Windows.Forms.PictureBox();
         ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
         this.SuspendLayout();
         // 
         // LogoPictureBox
         // 
         this.LogoPictureBox.Image = global::CarOrderApp.Properties.Resources.SLR_McLaren_Mercedes_IAA2003_Matl09180156e;
         this.LogoPictureBox.Location = new System.Drawing.Point(159, 10);
         this.LogoPictureBox.Name = "LogoPictureBox";
         this.LogoPictureBox.Size = new System.Drawing.Size(156, 134);
         this.LogoPictureBox.TabIndex = 8;
         this.LogoPictureBox.TabStop = false;         
         // 
         // ImageOrderAutoDialog
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.ClientSize = new System.Drawing.Size(327, 185);
         this.Controls.Add(this.LogoPictureBox);
         this.Name = "ImageOrderAutoDialog";
         this.Controls.SetChildIndex(this.LogoPictureBox, 0);
         ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.PictureBox LogoPictureBox;
   }
}
