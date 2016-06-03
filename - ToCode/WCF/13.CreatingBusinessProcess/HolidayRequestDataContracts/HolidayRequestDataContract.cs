using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace HolidayRequestDataContracts
{
    [DataContract]
    public class HolidayRequestDataContract_Input
    {
        [DataMember]
        public int EmployeeID { get; set; }
        [DataMember]
        public DateTime HolidayStartDate { get; set; }
        [DataMember]
        public DateTime HolidayEndDate { get; set; }
    }

    [DataContract]
    public class HolidayRequestDataContract_Output
    {
        [DataMember]
        public int ReferenceID { get; set; }
    }

    [DataContract]
    public class HolidayApprovalDataContract_Input
    {
        [DataMember]
        public int ManagerID { get; set; }
        [DataMember]
        public int ReferenceID { get; set; }
        [DataMember]
        public ApprovedOrDeniedEnum ApprovedOrDenied { get; set; }
    }

    [DataContract]
    public enum ApprovedOrDeniedEnum
    {
        [EnumMember]
        Approved,
        [EnumMember]
        Denied
    }

    [DataContract]
    public class HolidayApprovalDataContract_Output
    {
        [DataMember]
        public int EmployeeID { get; set; }
        [DataMember]
        public DateTime HolidayStartDate { get; set; }
        [DataMember]
        public DateTime HolidayEndDate { get; set; }
    }
}
