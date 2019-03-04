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
      protected virtual int PvOwn { get; set; }

      protected internal virtual double PiOwn { get; private set; }

      public virtual object PubvOwn { protected get; set; }

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
      protected override int PvOwn
      {
         get => base.PvOwn;
         set => base.PvOwn = value;
      }

      protected internal override double PiOwn => base.PiOwn;

      public override object PubvOwn
      {
         protected get => base.PubvOwn;
         set => base.PubvOwn = value;
      }

      protected internal override ref int GetSomeRef(ref int a)
      {
         return ref base.GetSomeRef(ref a);
      }

      protected internal override ref readonly Manager GetSomeRefReadonly(out object param1, ref Manager manager)
      {
         return ref base.GetSomeRefReadonly(out param1, ref manager);
      }

      public override Employee GetEmployee(params Manager[] managers)
      {
         return base.GetEmployee(managers);
      }

      public override Employee GetEmployee2(Manager[,,,,] managers)
      {
         return base.GetEmployee2(managers);
      }

      public override void ProcessDynamic(dynamic dynamic)
      {
         base.ProcessDynamic((object) dynamic);
      }

      public override unsafe void ProcessUnsafe(int* pInt)
      {
         base.ProcessUnsafe(pInt);
      }

      public override dynamic GetDynamic()
      {
         return base.GetDynamic();
      }

      public override TOut Generic<TOut, TIn>(TIn inType)
      {
         return base.Generic<TOut, TIn>(inType);
      }

      public override Task GetSomeAsync()
      {
         return base.GetSomeAsync();
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