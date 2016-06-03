using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace ReceiveApprovedHolidayRequestsService
{
    [ServiceContract]
    public interface IReceiveApprovedHolidayRequestService
    {
        [OperationContract]
        void ReceiveApprovedHolidayRequest(ApprovedHolidayData approvedHolidayData);
    }

    public class ReceiveApprovedHolidayRequestService : IReceiveApprovedHolidayRequestService
    {
        public void ReceiveApprovedHolidayRequest(ApprovedHolidayData approvedHolidayData)
        {
            Console.WriteLine("Got Approved Holiday Request");
            Console.WriteLine(string.Format(" by employee {0}, approved by manager {1}, from  {2} to {3}",
                        approvedHolidayData.EmployeeID.ToString(),
                        approvedHolidayData.ApprovedByManagerID.ToString(),
                        approvedHolidayData.HolidayStartDate.ToShortTimeString(),
                        approvedHolidayData.HolidayEndDate.ToShortTimeString()));
        }
    }

    [DataContract]
    public class ApprovedHolidayData
    {
        [DataMember]
        public int EmployeeID { get; set; }
        [DataMember]
        public int ApprovedByManagerID { get; set; }
        [DataMember]
        public DateTime HolidayStartDate { get; set; }
        [DataMember]
        public DateTime HolidayEndDate { get; set; }
    }
}

