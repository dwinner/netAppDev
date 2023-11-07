namespace Sorting.Algs.PrioQ;

// TODO: Had better use record struct instead of class

/// <summary>
///    The <tt>Transaction</tt> class is an immutable data type to encapsulate a
///    commercial transaction with a customer name, date, and amount
/// </summary>
public sealed class Transaction :
   IComparable<Transaction>,
   IComparable,
   IEquatable<Transaction>
{
   public Transaction(string who, DateTime when, double amount)
   {
      Who = who;
      When = when;
      Amount = amount;
   }

   /// <summary>
   ///    Creates a new transaction by parsing a string of the form NAME DATE AMOUNT
   /// </summary>
   /// <param name="transaction">Transaction's string to parse</param>
   public Transaction(string transaction)
   {
      var trnParts = transaction.Split(new[] { ' ' },
         StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

      // TODO: check for validity
      Who = trnParts[0];
      When = DateTime.Parse(trnParts[1]);
      Amount = double.Parse(trnParts[2]);
   }

   public static IEqualityComparer<Transaction> DefaultEqualityComparer { get; } = new TransactionEqualityComparer();

   /// <summary>
   ///    Customer
   /// </summary>
   public string Who { get; }

   /// <summary>
   ///    Date
   /// </summary>
   public DateTime When { get; }

   /// <summary>
   ///    Amount
   /// </summary>
   public double Amount { get; }

   public static IComparer<Transaction> DefaultComparer { get; } = new TransactionComparer();

   public static IComparer<Transaction> WhoComparer { get; } = new WhoComparerImpl();

   public static IComparer<Transaction> WhenComparer { get; } = new WhenComparerImpl();

   public static IComparer<Transaction> AmountComparer { get; } = new AmountComparerImpl();

   public int CompareTo(object? obj)
   {
      if (ReferenceEquals(null, obj))
      {
         return 1;
      }

      if (ReferenceEquals(this, obj))
      {
         return 0;
      }

      return obj is Transaction other
         ? CompareTo(other)
         : throw new ArgumentException($"Object must be of type {nameof(Transaction)}");
   }

   public int CompareTo(Transaction? other) => DefaultComparer.Compare(this, other);

   public bool Equals(Transaction? other) =>
      !ReferenceEquals(null, other)
      && (ReferenceEquals(this, other)
          ||
          (string.Equals(Who, other.Who, StringComparison.CurrentCultureIgnoreCase)
           && When.Equals(other.When)
           && Amount.Equals(other.Amount)));

   public override string ToString() => $"{nameof(Who)}: {Who}, {nameof(When)}: {When}, {nameof(Amount)}: {Amount}";

   public static bool operator <(Transaction? left, Transaction? right) =>
      DefaultComparer.Compare(left, right) < 0;

   public static bool operator >=(Transaction? left, Transaction? right) => !(left < right);

   public static bool operator >(Transaction? left, Transaction? right) =>
      DefaultComparer.Compare(left, right) > 0;

   public static bool operator <=(Transaction? left, Transaction? right) => !(left > right);

   public override bool Equals(object? obj) =>
      ReferenceEquals(this, obj) || (obj is Transaction other && Equals(other));

   public override int GetHashCode()
   {
      var hashCode = new HashCode();
      hashCode.Add(Who, StringComparer.CurrentCultureIgnoreCase);
      hashCode.Add(When);
      hashCode.Add(Amount);

      return hashCode.ToHashCode();
   }

   public static bool operator ==(Transaction? left, Transaction? right) => Equals(left, right);

   public static bool operator !=(Transaction? left, Transaction? right) => !Equals(left, right);

   private sealed class TransactionEqualityComparer : IEqualityComparer<Transaction>
   {
      public bool Equals(Transaction? x, Transaction? y) => x?.Equals(y) ?? false;

      public int GetHashCode(Transaction obj) => obj.GetHashCode();
   }

   private sealed class TransactionComparer : IComparer<Transaction>
   {
      public int Compare(Transaction? x, Transaction? y)
      {
         if (ReferenceEquals(x, y))
         {
            return 0;
         }

         if (ReferenceEquals(null, y))
         {
            return 1;
         }

         if (ReferenceEquals(null, x))
         {
            return -1;
         }

         var whoComparison = string.Compare(x.Who, y.Who, StringComparison.CurrentCultureIgnoreCase);
         if (whoComparison != 0)
         {
            return whoComparison;
         }

         var whenComparison = x.When.CompareTo(y.When);

         return whenComparison != 0
            ? whenComparison
            : x.Amount.CompareTo(y.Amount);
      }
   }

   private sealed class AmountComparerImpl : IComparer<Transaction>
   {
      public int Compare(Transaction? x, Transaction? y)
      {
         if (ReferenceEquals(x, y))
         {
            return 0;
         }

         if (ReferenceEquals(null, y))
         {
            return 1;
         }

         if (ReferenceEquals(null, x))
         {
            return -1;
         }

         return x.Amount.CompareTo(y.Amount);
      }
   }

   private sealed class WhenComparerImpl : IComparer<Transaction>
   {
      public int Compare(Transaction? x, Transaction? y) =>
         ReferenceEquals(x, y)
            ? 0
            : ReferenceEquals(null, y)
               ? 1
               : ReferenceEquals(null, x)
                  ? -1
                  : x.When.CompareTo(y.When);
   }

   private sealed class WhoComparerImpl : IComparer<Transaction>
   {
      public int Compare(Transaction? x, Transaction? y) =>
         ReferenceEquals(x, y)
            ? 0
            : ReferenceEquals(null, y)
               ? 1
               : ReferenceEquals(null, x)
                  ? -1
                  : string.Compare(x.Who, y.Who, StringComparison.CurrentCultureIgnoreCase);
   }
}