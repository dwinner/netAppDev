using System.Runtime.Serialization;
using System.ServiceModel;

namespace WcfIssues
{
    [ServiceContract]
    public interface IIssueService
    {
        [OperationContract(IsOneWay = true)]
        string OneWayIssue(string data);
    }
}
