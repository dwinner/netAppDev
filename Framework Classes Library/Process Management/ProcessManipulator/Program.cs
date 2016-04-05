using System;
using System.Diagnostics;
using System.Linq;

namespace ProcessManipulator
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Fun with Processes *****\n");
         ListAllRunningProcesses();

         // Запрос PID
         Console.WriteLine("***** Enter PID of process to investigate *****");
         Console.WriteLine("PID: ");
         string pId = Console.ReadLine();
         int theProcId = int.Parse(pId ?? "0");
         EnumThreadsForPid(theProcId);
         EnumModsForPid(theProcId);
         StartAndKillProcess();

         Console.ReadKey(true);
      }

      /// <summary>
      /// Список запущенных процессов.
      /// </summary>
      public static void ListAllRunningProcesses()
      {
         // Получение списка всех процессов, которые выполняются
         // на текущей машине, упорядоченных по PID.
         var runningProcs = from proc in Process.GetProcesses(".")
                            orderby proc.Id
                            select proc;
         // Отображение идентификатора и имени каждого процесса.
         foreach (var p in runningProcs)
         {
            string info = string.Format("-> PID: {0}\tName: {1}",
               p.Id, p.ProcessName);
            Console.WriteLine(info);
         }
         Console.WriteLine("******************************\n");
      }

      /// <summary>
      /// Получение процесса по его идентификатору
      /// </summary>
      /// <param name="pId">Идентификатор процесса</param>
      static void GetSpecificProcess(int pId)
      {
         Process theProcess = null;
         try
         {
            theProcess = Process.GetProcessById(pId);
         }
         catch (ArgumentException argEx)
         {
            Console.WriteLine(argEx.Message);
         }
      }

      /// <summary>
      /// Список потоков конкретного процесса.
      /// </summary>
      /// <param name="pId">Идентификатор процесса</param>
      static void EnumThreadsForPid(int pId)
      {
         Process theProcess = null;
         try
         {
            theProcess = Process.GetProcessById(pId);
         }
         catch (ArgumentException argEx)
         {
            Console.WriteLine(argEx.Message);
            return;
         }
         // Отображение статистических данных по каждому потоку в указанном процессе.
         Console.WriteLine("Here are the threads used by: {0}", theProcess);
         ProcessThreadCollection theThreads = theProcess.Threads;
         foreach (ProcessThread theThread in theThreads)
         {
            string info =
               string.Format("-> Thread Id: {0}\tStart Time: {1}\tPriority: {2}",
                             theThread.Id,
                             theThread.StartTime.ToShortDateString(),
                             theThread.PriorityLevel);
            Console.WriteLine(info);
         }
         Console.WriteLine("*******************************************\n");
      }

      /// <summary>
      /// Список модулей конкретного процесса.
      /// </summary>
      /// <param name="pId">Идентификатор процесса</param>
      static void EnumModsForPid(int pId)
      {
         Process process = null;
         try
         {
            process = Process.GetProcessById(pId);
            Console.WriteLine("Here are the loaded modules for {0}", process.ProcessName);
            ProcessModuleCollection modules = process.Modules;
            foreach (ProcessModule processModule in modules)
            {
               string info = string.Format("-> Mod Name: {0}", processModule.ModuleName);
               Console.WriteLine(info);
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
            return;
         }
      }

      /// <summary>
      /// Запуск и останов процессов программным образом.
      /// </summary>
      static void StartAndKillProcess()
      {
         Process ieProcess = null;
         // Запустить Internet Explorer и перейти на страницу facebook.com,
         // отображаем окно в развернутом виде.
         try
         {
            var startInfo = new ProcessStartInfo("IExplore", "www.facebook.com")
               {
                  WindowStyle = ProcessWindowStyle.Maximized
               };

            ieProcess = Process.Start(startInfo); // Process.Start("IExplore.exe", "www.facebook.com");            
            Console.Write("--> Hit enter to kill {0}...", ieProcess.ProcessName);
            Console.ReadLine();

            // Уничтожить процесс ieexplore.exe
            ieProcess.Kill();
         }
         catch (InvalidOperationException ex)
         {
            Console.WriteLine(ex.Message);
         }
      }
   }
}
