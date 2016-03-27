using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SimpleService
{
    public static class Utils
    {
        static string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// Install/Uninstall service
        /// </summary>
        /// <param name="mode">True to install sevice, othervise false.</param>
        /// <summary>
        /// Installs the module as Windows service.
        /// </summary>
        /// Set <param name="installMode"> to <c>true</c> to install the service, to uninstall set <param name="installMode"> to <c>false</c>.</param>
        static public void Install(bool installMode)
        {
            try
            {
                string installUtil = Path.Combine(Path.GetDirectoryName(typeof(string).Assembly.Location), "InstallUtil.exe");

                AppDomain dom = AppDomain.CreateDomain("execDom");

                if (installMode) //installing
                    dom.ExecuteAssembly(installUtil, new string[] { "/LogFile=", Assembly.GetExecutingAssembly().Location });
                else            //uninstalling
                    dom.ExecuteAssembly(installUtil, new string[] { "/u", "/LogFile=", Assembly.GetExecutingAssembly().Location });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static public bool IsService()
        {
            //Service vs. Application mode. Determines if the module is started by the ServiceManager (service mode) or from the command line (or batch file) with the /app switch
            //GetCommandLineArgs returns first arg as path to the executable itself.
            return Environment.GetCommandLineArgs().Length == 1;
        }
    }
}