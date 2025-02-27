﻿namespace PrivateState;

public class Customer
{
   private CustomerStatus _status = CustomerStatus.Regular;

   public void Promote()
   {
      _status = CustomerStatus.Preferred;
   }

   public decimal GetDiscount() => _status == CustomerStatus.Preferred ? 0.05m : 0m;
}

public enum CustomerStatus
{
   Regular,
   Preferred
}