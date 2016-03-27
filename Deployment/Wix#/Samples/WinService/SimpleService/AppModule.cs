using System;
using System.IO;
using System.Linq;
using System.ServiceProcess;

namespace SimpleService
{
    public partial class AppModule : ServiceBase
    {
        public AppModule()
        {
            InitializeComponent();
            Environment.CurrentDirectory = Path.GetDirectoryName(this.GetType().Assembly.Location);
        }

        //will be called only if the module is run in Application mode
        public void Start(string[] args)
        {
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ServiceName = "WixSharp.SimpleService";
        }
        #endregion Component Designer generated code
    }
}