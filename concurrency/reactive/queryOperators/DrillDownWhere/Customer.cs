using System.Collections.ObjectModel;

namespace DrillDownWhere;

internal class Customer
{
   public string Name { get; init; } = "";

   public string Region { get; init; } = "";

   public ObservableCollection<Order> Orders { get; } = [];
}