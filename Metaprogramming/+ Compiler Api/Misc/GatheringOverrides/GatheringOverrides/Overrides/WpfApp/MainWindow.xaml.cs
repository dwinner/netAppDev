using System.Threading.Tasks;

namespace WpfApp
{
   public partial class MainWindow
   {
      private readonly Manager manager = new Manager();

      public MainWindow()
      {
         InitializeComponent();
      }

      /// <summary>
      /// Own
      /// </summary>
      protected virtual int Own { get; set; }

      protected internal virtual ref int GetSomeRef(ref int a)
      {
         return ref a;
      }

      protected internal virtual ref readonly Manager GetSomeRefReadonly(out object param1, ref Manager manager)
      {
         param1 = null;
         return ref this.manager;
      }

      public virtual Employee GetEmployee(params Manager[] managers)
      {
         foreach (var manager in managers)
         {
            if (manager.FirstName == "Den")
            {
               return new Employee { FirstName = "Den" };
            }
         }

         return new Employee();
      }

      public virtual Employee GetEmployee2(Manager[,,,,] managers)
      {
         foreach (var manager in managers)
         {
            if (manager.FirstName == "Den")
            {
               return new Employee { FirstName = "Den" };
            }
         }

         return new Employee();
      }

      public virtual void ProcessDynamic(dynamic @dynamic)
      {
         System.Console.WriteLine(@dynamic);
      }

      public virtual unsafe void ProcessUnsafe(int* pInt)
      {
         string sig = pInt->ToString();
         System.Console.WriteLine(sig);
      }

      public virtual dynamic GetDynamic()
      {
         return new object();
      }

      public virtual TOut Generic<TOut, TIn>(TIn inType) where TOut : new()
      {
         return new TOut();
      }
      
      public virtual async Task GetSomeAsync()
      {
         System.Console.WriteLine();
         await Task.Delay(System.TimeSpan.FromSeconds(2));
         System.Console.WriteLine();
      }
   }   

   class DerivedWindow : MainWindow
   {
      public override unsafe void ProcessUnsafe(int* pInt)
      {
         base.ProcessUnsafe(pInt);
      }      
   }

   public class Employee
   {
      public string FirstName { get; set; }

      public string SecondName { get; set; }
   }

   public struct Manager
   {
      public string FirstName { get; set; }

      public string SecondName { get; set; }
   }
}