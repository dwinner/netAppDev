using System.Threading.Tasks;
using System.Windows.Forms;

namespace _07_PuppetTask
{
   public partial class PuppetForm : Form
   {
      public PuppetForm()
      {
         InitializeComponent();
         GetUserPermission();
      }

      /// <summary>Какой-то диалог, запрашивающий разрешение на что-то</summary>
      /// <example>
      /// <code>
      ///   if (await GetUserPermission()) { ... }
      /// </code>
      /// </example>
      /// <returns></returns>
      private static Task<bool> GetUserPermission()
      {
         // Создать объект TaskCompletionSource, чтобы можно было вернуть задачу-марионетку
         TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

         // Создать диалог
         // PermissionDialog dialog = new PermissionDialog();

         // Когда поьзователь закроет диалог, сделать задачу завершившейся с помощью SetResult
         // dialog.Closed += delegate { taskCompletionSource.SetResult(dialog.PermissionGranted); };

         // Показать диалог на экране
         // dialog.Show();

         // Вернуть еще не завершившуюся задачу-марионетку
         // return taskCompletionSource.Task;
         return null;   // TODO: Delete
      }
   }
}
