using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntMapping
{
    public class Sampler
    {
        public string GetSample()
        {
            StringBuilder input = new StringBuilder();
            input.Append("Invoice <- Order ;");
            input.AppendLine();
            input.Append("InvoiceDate = Order.OrderDate + 30 ;");
            input.AppendLine();
            input.Append("InvoiceNo = GetNextInvoiceNo() ;");
            input.AppendLine();
            input.Append("Freight = Order.TotalCBM * 1.5 + Order.TotalWeight * 2.2;");
            input.AppendLine();
            input.Append("ShipVia = IF(Order.IsLocal, \"Express\", \"Mail\") ;");
            input.AppendLine();
            return input.ToString();
        }
    }
}
