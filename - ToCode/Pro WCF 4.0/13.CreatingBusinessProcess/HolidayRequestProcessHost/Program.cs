using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel.Activities;
using System.ServiceModel.Activities.Description;
using System.ServiceModel.Description;

namespace HolidayRequestProcessHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                WorkflowServiceHost workflowServiceHost;
                workflowServiceHost = new WorkflowServiceHost(new HolidayRequestProcess.HolidayRequestProcessDefinition(), new Uri(@"http://localhost:9874/HolidayRequestProcess"));

                workflowServiceHost.Description.Behaviors.Add(new SqlWorkflowInstanceStoreBehavior("Data Source=.;Initial Catalog=SqlWorkflowInstanceStore;Integrated Security=True"));

                ServiceMetadataBehavior serviceMetadataBehavior;
                serviceMetadataBehavior = new ServiceMetadataBehavior();
                serviceMetadataBehavior.HttpGetEnabled = true;
                workflowServiceHost.Description.Behaviors.Add(serviceMetadataBehavior);

                WorkflowIdleBehavior workflowIdleBehavior = new WorkflowIdleBehavior()
                {
                    TimeToUnload = TimeSpan.FromSeconds(0)
                };
                workflowServiceHost.Description.Behaviors.Add(workflowIdleBehavior);

                workflowServiceHost.Description.Behaviors.Find<ServiceDebugBehavior>().IncludeExceptionDetailInFaults = true;

                workflowServiceHost.Open();

                Console.WriteLine("WorkflowServiceHost started.");

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }

            Console.ReadKey();
        }
    }
}
