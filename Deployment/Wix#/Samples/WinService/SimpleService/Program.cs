using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;

namespace SimpleService
{
    class Program
    {
        static string usage =
                    "Simple Service. Version " + Assembly.GetExecutingAssembly().GetName().Version + "\n" +
                    "Usage: " + Path.GetFileName(Assembly.GetExecutingAssembly().Location) + " </i|/u>|/app\n" +
                    "i - install service\n" +
                    "u - uninstall service\n" +
                    "app - run as application";

        static int Main(string[] args)
        {
            bool createdNew;
            var appSingleInstanceMutex = new Mutex(true, "WixSharp.SimpleService", out createdNew);

            if (!createdNew)
            {
                Console.WriteLine("Another instance of the application is already running.");
                return -1;
            }

            try
            {
                var module = new AppModule();

                if (Utils.IsService())
                {
                    ServiceBase.Run(module);
                }
                else
                {
                    if (args.Contains("/i") || args.Contains("-i"))
                    {
                        Utils.Install(true);
                    }
                    else if (args.Contains("/u") || args.Contains("-u"))
                    {
                        Utils.Install(false);
                    }
                    else if (args.Contains("/?") || args.Contains("-?"))
                    {
                        Console.WriteLine(usage);
                    }
                    else if (args.Contains("/app"))
                    {
                        Console.WriteLine("Started in Application Mode...");

                        module.Start(args.Skip(1).ToArray());

                        Console.WriteLine("Press any key to exit the application...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Invalid command line parameter.\n" + usage);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return -1;
            }
            return 0;
        }
    }
}