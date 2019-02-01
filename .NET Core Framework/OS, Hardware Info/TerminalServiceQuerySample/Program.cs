using System;
using System.Linq;
using System.Management;

namespace TerminalServiceQuerySample
{
   internal static class Program
   {
      private static int Main(string[] args)
      {
         try
         {
            var computer = string.Empty;
            if (args.Length == 1) computer = string.Format(@"\\{0}\", args[0]);

            var remotePcPath = string.Format("{0}root\\CIMV2", computer);
            var searcher =
               new ManagementObjectSearcher(remotePcPath, "SELECT * FROM Win32_TerminalService");
            var managementObjects = searcher.Get();

            Console.WriteLine("{0} instance{1}", managementObjects.Count, managementObjects.Count == 1 ? string.Empty : "s");
            Console.WriteLine();

            foreach (var queryObj in managementObjects.Cast<ManagementObject>())
            {
               Console.WriteLine("AcceptPause             : {0}", queryObj["AcceptPause"]);
               Console.WriteLine("AcceptStop              : {0}", queryObj["AcceptStop"]);
               Console.WriteLine("Caption                 : {0}", queryObj["Caption"]);
               Console.WriteLine("CheckPoint              : {0}", queryObj["CheckPoint"]);
               Console.WriteLine("CreationClassName       : {0}", queryObj["CreationClassName"]);
               Console.WriteLine("Description             : {0}", queryObj["Description"]);
               Console.WriteLine("DesktopInteract         : {0}", queryObj["DesktopInteract"]);
               Console.WriteLine("DisconnectedSessions    : {0}", queryObj["DisconnectedSessions"]);
               Console.WriteLine("DisplayName             : {0}", queryObj["DisplayName"]);
               Console.WriteLine("ErrorControl            : {0}", queryObj["ErrorControl"]);
               Console.WriteLine("ExitCode                : {0}", queryObj["ExitCode"]);
               Console.WriteLine("InstallDate             : {0}", queryObj["InstallDate"]);
               Console.WriteLine("Name                    : {0}", queryObj["Name"]);
               Console.WriteLine("PathName                : {0}", queryObj["PathName"]);
               Console.WriteLine("ProcessId               : {0}", queryObj["ProcessId"]);
               Console.WriteLine("ServiceSpecificExitCode : {0}", queryObj["ServiceSpecificExitCode"]);
               Console.WriteLine("ServiceType             : {0}", queryObj["ServiceType"]);
               Console.WriteLine("Started                 : {0}", queryObj["Started"]);
               Console.WriteLine("StartMode               : {0}", queryObj["StartMode"]);
               Console.WriteLine("StartName               : {0}", queryObj["StartName"]);
               Console.WriteLine("State                   : {0}", queryObj["State"]);
               Console.WriteLine("Status                  : {0}", queryObj["Status"]);
               Console.WriteLine("SystemCreationClassName : {0}", queryObj["SystemCreationClassName"]);
               Console.WriteLine("SystemName              : {0}", queryObj["SystemName"]);
               Console.WriteLine("TagId                   : {0}", queryObj["TagId"]);
               Console.WriteLine("TotalSessions           : {0}", queryObj["TotalSessions"]);
               Console.WriteLine("WaitHint                : {0}", queryObj["WaitHint"]);
               Console.WriteLine();
            }

            return 0;
         }
         catch (Exception e)
         {
            Console.Error.WriteLine("An error occurred while querying WMI: {0}", e.Message);
            return 1;
         }
      }
   }
}