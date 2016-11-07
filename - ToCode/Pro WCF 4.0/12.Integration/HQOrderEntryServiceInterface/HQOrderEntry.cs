using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace HQOrderEntryServiceInterface
{
    [DataContract]
    public class HQOrderEntry
    {
        [DataMember]
        public String OrderEntryID { get; set; }
        [DataMember]
        public DateTime OrderEntryDate { get; set; }
        [DataMember]
        public Customer OrderCustomer { get; set; }
        [DataMember]
        public List<OrderedProducts> OrderOrderedProducts { get; set; }

    }
    [DataContract]
    public class Customer
    {
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string CustomerAddressLine1 { get; set; }
        [DataMember]
        public string CustomerAddressLine2 { get; set; }
        [DataMember]
        public string CustomerCountryCode { get; set; }
    }
    [DataContract]
    public class OrderedProducts
    {
        [DataMember]
        public string ProductID { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public string ProductName { get; set; }
    }
}
