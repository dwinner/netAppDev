namespace AutolotWindowsServiceHost
{
   partial class ProjectInstaller
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
         this.autolotServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
         this.autolotServiceInstaller = new System.ServiceProcess.ServiceInstaller();
         // 
         // autolotServiceProcessInstaller
         // 
         this.autolotServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.NetworkService;
         this.autolotServiceProcessInstaller.Password = null;
         this.autolotServiceProcessInstaller.Username = null;
         // 
         // autolotServiceInstaller
         // 
         this.autolotServiceInstaller.Description = "Auto lot database network service";
         this.autolotServiceInstaller.DisplayName = "AutolotService";
         this.autolotServiceInstaller.ServiceName = "Service1";
         // 
         // ProjectInstaller
         // 
         this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.autolotServiceProcessInstaller,
            this.autolotServiceInstaller});

      }

      #endregion

      private System.ServiceProcess.ServiceProcessInstaller autolotServiceProcessInstaller;
      private System.ServiceProcess.ServiceInstaller autolotServiceInstaller;
   }
}