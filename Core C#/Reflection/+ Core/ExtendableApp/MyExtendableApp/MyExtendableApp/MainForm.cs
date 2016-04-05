using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CommonSnappableTypes;

namespace MyExtendableApp
{
   public partial class MainForm : Form
   {
      public MainForm()
      {
         InitializeComponent();
      }      

      private void snapInModuleToolStripMenuItem_Click(object sender, EventArgs e)
      {
         // Позволить пользователю выбрать подлежащую загрузке сборку.
         OpenFileDialog dlg = new OpenFileDialog();
         if (dlg.ShowDialog() == DialogResult.OK)
         {
            if (dlg.FileName.Contains("CommonSnappableTypes"))
            {
               // CommonSnappableTypes не содержит оснасток.
               MessageBox.Show("CommonSnappableTypes has no snap-ins!");
            }
            else if (!LoadExternalModule(dlg.FileName))
            {
               // Не удается обнаружить класс, реализующий IAppFunctionality
               MessageBox.Show("Nothing implements IAppFunctionality!");
            }
         }
      }

      private bool LoadExternalModule(string pathToAssembly)
      {
         bool foundSnapIn = false;
         Assembly theSnapInAsm = null;

         try
         {
            // Динамическая загрузка выбранной сборки.
            theSnapInAsm = Assembly.LoadFrom(pathToAssembly);
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
            return foundSnapIn;
         }

         // Получение информации обо всех совместимых с IAppFunctionality классах в сборке.
         var theClassTypes = from t in theSnapInAsm.GetTypes()
                             where t.IsClass && (t.GetInterface("IAppFunctionality") != null)
                             select t;
         // Создание объекта и вызов метода DoIt().
         foreach (Type t in theClassTypes)
         {
            foundSnapIn = true;
            // Использование позднего связывания для создания типа.
            IAppFunctionality itfApp =
               (IAppFunctionality) theSnapInAsm.CreateInstance(t.FullName, true);
            itfApp.DoIt();
            snapInListBox.Items.Add(t.FullName);
            // Отображение информации о компании.
            DisplayCompanyData(t);
         }
         return foundSnapIn;
      }

      private void DisplayCompanyData(Type t)
      {
         // Получение данных из атрибута [CompanyInfo].
         var compInfo = from ci in t.GetCustomAttributes(false)
                        where (ci.GetType() == typeof(CompanyInfoAttribute))
                        select ci;
         // Отображение этих данных.
         foreach (CompanyInfoAttribute c in compInfo)
         {
            MessageBox.Show(c.CompanyUrl,
               string.Format("More info about {0} can be found at", c.CompanyName));
         }
      }
   }
}
