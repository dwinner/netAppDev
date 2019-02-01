using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace WmiProcessManagement
{
   /// <summary>
   ///    ProcessInfo class.
   /// </summary>
   public class ProcessInfo : IDisposable
   {
      public EventHandler Started;
      public EventHandler Terminated;
      
      // WMI event _watcher
      private readonly ManagementEventWatcher _watcher;

      // The constructor uses the application name like notepad.exe
      // And it starts the _watcher
      public ProcessInfo(string appName)
      {
         // querry every 2 seconds
         const string pol = "2";
         var queryString =
            string.Format(
               "SELECT * " + " FROM __InstanceOperationEvent " + " WITHIN {0} WHERE TargetInstance ISA 'Win32_Process' " +
               " AND TargetInstance.Name = '{1}'", pol, appName);

         // You could replace the dot by a machine name to watch to that machine
         const string scope = @"\\.\root\CIMV2";

         // create the _watcher and start to listen
         _watcher = new ManagementEventWatcher(scope, queryString);
         _watcher.EventArrived += OnEventArrived;
         _watcher.Start();
      }

      public void Dispose()
      {
         _watcher.Stop();
         _watcher.Dispose();
      }

      public static DataTable RunningProcesses()
      {
         /* One way of constructing a query
         string className = "Win32_Process";
         string condition = "";
         string[] selectedProperties = new string[] {"Name", "ProcessId", "Caption", "ExecutablePath"};
         SelectQuery query = new SelectQuery(className, condition, selectedProperties);
         */

         // The second way of constructing a query
         const string queryString = "SELECT Name, ProcessId, Caption, ExecutablePath FROM Win32_Process";

         var query = new SelectQuery(queryString);
         var scope = new ManagementScope(@"\\.\root\CIMV2");

         var searcher = new ManagementObjectSearcher(scope, query);
         var processes = searcher.Get();

         var result = new DataTable();
         result.Columns.Add("Name", typeof(string));
         result.Columns.Add("ProcessId", typeof(int));
         result.Columns.Add("Caption", typeof(string));
         result.Columns.Add("Path", typeof(string));

         foreach (var managementObject in processes.Cast<ManagementObject>())
         {
            var row = result.NewRow();
            row["Name"] = managementObject["Name"].ToString();
            row["ProcessId"] = Convert.ToInt32(managementObject["ProcessId"]);
            if (managementObject["Caption"] != null)
            {
               row["Caption"] = managementObject["Caption"].ToString();
            }

            if (managementObject["ExecutablePath"] != null)
            {
               row["Path"] = managementObject["ExecutablePath"].ToString();
            }

            result.Rows.Add(row);
         }

         return result;
      }

      private void OnEventArrived(object sender, EventArrivedEventArgs e)
      {
         try
         {
            var eventName = e.NewEvent.ClassPath.ClassName;

            if (string.Compare(eventName, "__InstanceCreationEvent", StringComparison.Ordinal) == 0)
            {               
               if (Started != null)
               {
                  Started(this, e);
               }
            }
            else if (string.Compare(eventName, "__InstanceDeletionEvent", StringComparison.Ordinal) == 0)
            {
               // Terminated
               if (Terminated != null)
               {
                  Terminated(this, e);
               }
            }
         }
         catch (Exception ex)
         {
            Debug.WriteLine(ex.Message);
         }
      }
   }
}