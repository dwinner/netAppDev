namespace QuoteService
{
   partial class QuoteServiceInstaller
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
         this.quoteServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
         this.qServiceInstaller = new System.ServiceProcess.ServiceInstaller();
         // 
         // quoteServiceProcessInstaller
         // 
         this.quoteServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
         this.quoteServiceProcessInstaller.Password = null;
         this.quoteServiceProcessInstaller.Username = null;
         // 
         // qServiceInstaller
         // 
         this.qServiceInstaller.Description = "Professional C# Sample Service";
         this.qServiceInstaller.DisplayName = "Quote Service";
         this.qServiceInstaller.ServiceName = "QuoteService";
         // 
         // QuoteServiceInstaller
         // 
         this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.quoteServiceProcessInstaller,
            this.qServiceInstaller});

      }

      #endregion

      private System.ServiceProcess.ServiceProcessInstaller quoteServiceProcessInstaller;
      private System.ServiceProcess.ServiceInstaller qServiceInstaller;
   }
}