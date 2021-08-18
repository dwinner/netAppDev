using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using PluginDemo.Properties;
using PluginInterfaces;

namespace PluginDemo
{
   public partial class MainForm : Form
   {
      private readonly Dictionary<string, IImagePlugin> _plugins =
         new Dictionary<string, IImagePlugin>();

      public MainForm()
      {
         InitializeComponent();

         var assembly = Assembly.GetExecutingAssembly();
         var folder = Path.GetDirectoryName(assembly.Location);
         LoadPlugins(folder);
         CreatePluginMenu();
      }

      private void LoadPlugins(string folder)
      {
         _plugins.Clear();

         // Загрузить все DLL-библиотеки
         foreach (var assemblyFile in Directory.GetFiles(folder, "*.dll"))
            try
            {
               var assembly = Assembly.LoadFrom(assemblyFile);
               // В каждой сборке найти все типы, реализующие интерфейс IImagePlugin
               foreach (var plugin in from type in assembly.GetTypes()
                  where type.GetInterface(nameof(IImagePlugin)) == typeof(IImagePlugin)
                  select Activator.CreateInstance(type) as IImagePlugin)
                  _plugins[plugin.Name] = plugin;
            }
            catch (Exception commonEx) // Todo: Записать в журнал, что это не наш случай
            {
               throw new PluginErrorException(commonEx.Message, commonEx);
            }
      }

      private void CreatePluginMenu()
      {
         // Динамически создать меню на основании информации из модуля

         pluginsToolStripMenuItem.DropDownItems.Clear();

         foreach (var menuItem in _plugins.Select(pair => new ToolStripMenuItem(pair.Key)))
         {
            menuItem.Click += menuItem_Click;
            pluginsToolStripMenuItem.DropDownItems.Add(menuItem);
         }
      }

      private void menuItem_Click(object sender, EventArgs e)
      {
         if (!(sender is ToolStripMenuItem menuItem))
            return;

         var plugin = _plugins[menuItem.Text];
         try
         {
            try
            {
               Cursor = Cursors.WaitCursor;
               pictureBox.Image = plugin.RunPlugin(pictureBox.Image);
            }
            finally
            {
               Cursor = Cursors.Default;
            }
         }
         catch (Exception ex) // Note: Никогда не доверяйте добавляемым модулям
         {
            MessageBox.Show(ex.Message, Resources.PluginError);
         }
      }

      private void buttonLoad_Click(object sender, EventArgs e)
      {
         var ofd = new OpenFileDialog {Filter = Resources.FileDialogFilter};
         if (ofd.ShowDialog() == DialogResult.OK) pictureBox.Image = Image.FromFile(ofd.FileName);
      }
   }
}