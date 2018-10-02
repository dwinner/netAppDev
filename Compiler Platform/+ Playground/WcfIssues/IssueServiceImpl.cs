using System;

namespace WcfIssues
{
    public class IssueServiceImpl : IIssueService
    {
        public string OneWayIssue(string data)
        {
            Console.WriteLine(data);
            return string.Empty;
        }
    }
}
