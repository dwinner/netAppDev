/**
 * Обновление базы данных из объекта DataSet
 */

using System;
using System.Windows.Forms;

namespace UpdateDatabaseFromDataSet
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
