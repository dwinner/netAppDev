using Microsoft.WindowsAPICodePack.Controls.WindowsForms;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace WinFormsCommandLink
{
   partial class CmdLinkForm
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
         this.CommandLabel = new System.Windows.Forms.Label();
         this.commandLink = new CommandLink();
         this.SuspendLayout();
         // 
         // CommandLabel
         // 
         this.CommandLabel.AutoSize = true;
         this.CommandLabel.Location = new System.Drawing.Point(13, 13);
         this.CommandLabel.Name = "CommandLabel";
         this.CommandLabel.Size = new System.Drawing.Size(73, 13);
         this.CommandLabel.TabIndex = 0;
         this.CommandLabel.Text = "Command link";
         //
         // CommandLink
         //
         this.commandLink.AutoSize = true;
         this.commandLink.Location = new System.Drawing.Point(130, 130);
         this.commandLink.Name = "CommandLink";
         this.commandLink.Size = new System.Drawing.Size(100, 100);
         this.commandLink.TabIndex = 0;
         this.commandLink.Text = "Give access to this computer";
         this.commandLink.NoteText = "Clicking this command requires admin rights";
         this.commandLink.UseVisualStyleBackColor = true;
         this.commandLink.Click += commandLink_Click;
         // 
         // CmdLinkForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(284, 261);
         this.Controls.Add(this.CommandLabel);
         this.Controls.Add(this.commandLink);
         this.Name = "CmdLinkForm";
         this.Text = "Command link form";
         this.ResumeLayout(false);
         this.PerformLayout();

      }      

      #endregion

      private System.Windows.Forms.Label CommandLabel;
      private CommandLink commandLink;
   }
}

