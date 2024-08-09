using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reactive.Linq;

namespace DrillDownWhere;

internal static class Program
{
   private static void Main()
   {
      var customers = new ObservableCollection<Customer>();

      var customerChanges = Observable.FromEventPattern(
         (EventHandler<NotifyCollectionChangedEventArgs> ev)
            => new NotifyCollectionChangedEventHandler(ev),
         ev => customers.CollectionChanged += ev,
         ev => customers.CollectionChanged -= ev);

      var watchForNewCustomersFromWashington =
         from evtPattern in customerChanges
         where evtPattern.EventArgs.Action == NotifyCollectionChangedAction.Add
         from cus in evtPattern.EventArgs.NewItems.Cast<Customer>().ToObservable()
         where cus.Region == "WA"
         select cus;

      Console.WriteLine("New customers from Washington and their orders:");

      watchForNewCustomersFromWashington.Subscribe(customer =>
      {
         Console.WriteLine("Customer {0}:", customer.Name);
         foreach (var order in customer.Orders)
         {
            Console.WriteLine("Order {0}: {1}",
               order.OrderId,
               order.OrderDate);
         }
      });

      customers.Add(new Customer
      {
         Name = "Lazy K Kountry Store",
         Region = "WA",
         Orders =
         {
            new Order
            {
               OrderDate = DateTimeOffset.Now,
               OrderId = 1
            }
         }
      });

      Thread.Sleep(1000);
      customers.Add(new Customer
      {
         Name = "Joe's Food Shop",
         Region = "NY",
         Orders =
         {
            new Order
            {
               OrderDate = DateTimeOffset.Now,
               OrderId = 2
            }
         }
      });

      Thread.Sleep(1000);
      customers.Add(new Customer
      {
         Name = "Trail's Head Gourmet Provisioners",
         Region = "WA",
         Orders =
         {
            new Order
            {
               OrderDate = DateTimeOffset.Now,
               OrderId = 3
            }
         }
      });

      Console.ReadKey();
   }
}