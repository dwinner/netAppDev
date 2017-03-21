using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProgramVerificationSystems.SelfTester.Model.Utils
{
   /// <summary>
   ///    Монитор процессов, зависших с критической ошибкой
   ///    <remarks>Для простоты реализации, этот тип работает только с прямыми потомками, не учитывая дерево процессов</remarks>
   /// </summary>
   public sealed class FaultProcessMonitor
   {
      private const string DefaultChildProcessName = "devenv.exe";
      private const int DefaultMonitorIntervalInSeconds = 1;

      private static readonly int HangTimeMinutes = ApplicationConfigReader.Instance.HangTime;
      private static readonly string[] IsolatedNames = { "DW20", "vstest.discoveryengine.x86" };
      private readonly CancellationTokenSource _cancellationTokenSource;
      private readonly string _childProcessName; // Имя дочернего процесса, за которым нужно наблюдать
      private readonly string[] _faultProcessNames; // Процессы, которые вызывают критическое завершение      
      private readonly int _monitorInterval; // Интервал для слежения в секундах
      private readonly uint _parentProcessId; // PID родительского процесса
      private CancellationToken _cancellationToken;

      /// <summary>
      ///    Конструктор
      /// </summary>
      /// <param name="parentProcessId">PID родительского процесса</param>
      /// <param name="faultProcessNames">Процессы, которые вызывают критическое завершение</param>
      /// <param name="childProcessName">Имя дочернего процесса, за которым нужно наблюдать</param>
      /// <param name="monitorInterval">Интервал для слежения в секундах</param>
      public FaultProcessMonitor(uint parentProcessId, string[] faultProcessNames,
         string childProcessName = DefaultChildProcessName, int monitorInterval = DefaultMonitorIntervalInSeconds)
      {
         _parentProcessId = parentProcessId;
         _childProcessName = childProcessName;
         _faultProcessNames = faultProcessNames;
         _monitorInterval = monitorInterval;
         _cancellationTokenSource = new CancellationTokenSource();
         _cancellationToken = _cancellationTokenSource.Token;
      }

      public event EventHandler<FaultedEventArgs> Faulted;
      public event EventHandler<HangoutEventArgs> Hangout;

      public void Start()
      {
         Task.Factory.StartNew(() =>
         {
            while (!_cancellationToken.IsCancellationRequested)
            {
               try
               {
                  KillFaulted();
                  KillHangout();
               }
               catch
               {
                  // ignored
               }

               Thread.Sleep(TimeSpan.FromSeconds(_monitorInterval));
            }
         }, _cancellationToken);
      }

      private void KillHangout()
      {
         var childProcessIds = ProcessUtils.GetChildProcessIds(_parentProcessId, _childProcessName);
         var devenvs = childProcessIds.Select(childProcId => Process.GetProcessById((int)childProcId)).ToList();
         var hangoutDevenvInstances = (from process in devenvs
                                       let devenvStartTime = process.StartTime
                                       let delta = DateTime.Now.Subtract(devenvStartTime)
                                       let workingMinutes = delta.Duration().TotalMinutes
                                       where workingMinutes > HangTimeMinutes
                                       select process).ToList();

         if (hangoutDevenvInstances.Count > 0) // Есть подвисшие процессы
         {
            OnHangout(new HangoutEventArgs(hangoutDevenvInstances));
            hangoutDevenvInstances.ForEach(process => ProcessUtils.MandatoryKill(process));
         }
      }

      private void KillFaulted()
      {
         var childProcessIds = ProcessUtils.GetChildProcessIds(_parentProcessId, _childProcessName);
         var devenvs = childProcessIds.Select(childProcId => Process.GetProcessById((int)childProcId)).ToList();
         var faultDevenvs = (from process in devenvs
                             from faultProcessName in _faultProcessNames
                             where ProcessUtils.ExistsChildProcess((uint)process.Id, faultProcessName)
                             select process).ToList();

         if (faultDevenvs.Count > 0) // Есть процессы с ошибками
         {
            OnFaulted(new FaultedEventArgs(faultDevenvs));
            faultDevenvs.ForEach(process => ProcessUtils.MandatoryKill(process));
         }

         foreach (var isolatedFault in IsolatedNames.SelectMany(Process.GetProcessesByName))
         {
            ProcessUtils.MandatoryKill(isolatedFault);
         }
      }

      public void Stop()
      {
         _cancellationTokenSource.Cancel(false);
      }

      private void OnFaulted(FaultedEventArgs e)
      {
         var handler = Faulted;
         if (handler != null) handler(this, e);
      }

      private void OnHangout(HangoutEventArgs e)
      {
         var handler = Hangout;
         if (handler != null) handler(this, e);
      }
   }
}