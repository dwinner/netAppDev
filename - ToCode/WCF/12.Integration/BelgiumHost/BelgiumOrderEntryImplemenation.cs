using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BelgiumHost
{
    class BelgiumOrderEntryImplemenation : LocalOrderEntryInterface.IReceiveOrderEntryLocalBranch
    {
        public int SendLocalOrderEntry(LocalOrderEntryInterface.LocalOrderEntry localOrderEntry)
        {
            Console.WriteLine("Belgium SendLocalOrderEntry");
            Console.WriteLine(localOrderEntry.CustomerName);
            foreach (var item in localOrderEntry.OrderOrderedProducts)
            {
                Console.WriteLine(string.Format("{0} {1} {2}", item.ProductID, item.Quantity.ToString(), item.LocalizedDescription));
            }
            return 1;
        }
    }
}
