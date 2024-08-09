using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace subjectAsObservable;

public class Order
{
   private readonly Subject<Order> _paidSubject = new();
   public IObservable<Order> Paid => _paidSubject.AsObservable();

   public required DateTime? PaidDate { get; set; }

   public void MarkPaid(DateTime paidDate)
   {
      PaidDate = paidDate;
      _paidSubject.OnNext(this); // Raise PAID event
   }
}