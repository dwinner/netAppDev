using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using System.ServiceModel;

namespace LocalOrderEntryInterface
{
    [ServiceContract]
    public interface IReceiveOrderEntryLocalBranch
    {
        [OperationContract]
        int SendLocalOrderEntry(LocalOrderEntry localOrderEntry);
    }

    [DataContract]
    public class LocalOrderEntry
    {
        [DataMember]
        public String OrderEntryID { get; set; }
        [DataMember]
        public DateTime OrderEntryDate { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string CustomerAddressLine1 { get; set; }
        [DataMember]
        public string CustomerAddressLine2 { get; set; }
        [DataMember]
        public string CustomerCountryCode { get; set; }
        [DataMember]
        public List<OrderedProducts> OrderOrderedProducts { get; set; }
    }

    
    [DataContract]
    public class OrderedProducts
    {
        [DataMember]
        public string ProductID { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public string LocalizedDescription { get; set; }
    }
}
