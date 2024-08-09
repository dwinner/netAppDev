using subjectAsObservable;

var order = new Order { PaidDate = DateTime.Now };
order.Paid.Subscribe(newOrder => Console.WriteLine($"Paid: {newOrder.PaidDate}")); // Subscribe

order.MarkPaid(DateTime.Now);