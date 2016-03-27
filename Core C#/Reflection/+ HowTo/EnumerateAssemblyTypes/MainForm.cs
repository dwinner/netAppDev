using System;
using System.Windows.Forms;
using System.Reflection;
using EnumerateAssemblyTypes.Properties;

namespace EnumerateAssemblyTypes
{
   public partial class MainForm : Form
   {
      public MainForm()
      {
         InitializeComponent();
      }

      private void buttonBrowse_Click(object sender, EventArgs e)
      {
         OpenFileDialog openFileDialog = new OpenFileDialog { Filter = Resources.FileDialogFilterResource };
         if (openFileDialog.ShowDialog() != DialogResult.OK)
            return;
         labelAssembly.Text = openFileDialog.FileName;
         ReflectAssembly(labelAssembly.Text);
      }

      private void ReflectAssembly(string filename)
      {
         treeView.Nodes.Clear();

         Assembly assembly = Assembly.LoadFrom(filename);
         foreach (Type type in assembly.GetTypes())
         {
            TreeNode typeNode = new TreeNode(string.Format("(T) {0}", type.Name));
            treeView.Nodes.Add(typeNode);

            foreach (MethodInfo methodInfo in type.GetMethods()) // Получить методы
            {
               typeNode.Nodes.Add(
                  new TreeNode(string.Format("(M) {0}", methodInfo.Name)));
            }
            
            foreach (PropertyInfo propertyInfo in type.GetProperties())  // Получить свойства
            {
               typeNode.Nodes.Add(
                  new TreeNode(string.Format("(P) {0}", propertyInfo.Name)));
            }
            
            foreach (FieldInfo fieldInfo in
               type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
               typeNode.Nodes.Add(
                  new TreeNode(string.Format("(F) {0}", fieldInfo.Name)));
            }
            
            foreach (EventInfo eventInfo in type.GetEvents())  // Получить события
            {
               typeNode.Nodes.Add(
                  new TreeNode(string.Format("(E) {0}", eventInfo.Name)));
            }

            /*
             * Note: Вместо всего этого можно было вызвать метод t.GetMembers,
             * возвращающий массив объектов MemberInfo, базового класса
             * для всех вышеперечисленных классов
             */
         }
      }
   }
}
