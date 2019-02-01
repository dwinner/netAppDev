using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using AppDevUnited.SelfTester.Model;
using AppDevUnited.SelfTester.Model.BusinessLogic;
using AppDevUnited.SelfTester.Model.Poco;
using AppDevUnited.SelfTester.Model.Utils;

namespace AppDevUnited.SelfTester.UI
{
   /// <summary>
   ///    Главное окно SelfTester'а
   /// </summary>
   public partial class MainWindow
   {
      public MainWindow()
      {
         RenderOptions.ProcessRenderMode = RenderMode.SoftwareOnly;

         var freeSpace = AnalyzerUtilities.GetCurrentFreeSpace();
         var minRequiredSpace = ApplicationConfigReader.Instance.MinRecommendedWorkingDiskSpace;
         if (freeSpace <= minRequiredSpace)
         {
            var messageBoxResult = MessageBox.Show(this,
               string.Format(Properties.Resources.FreeSpaceWarn, Math.Round(freeSpace, 2), minRequiredSpace,
                  Environment.NewLine), _selfTesterAppName, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.No)
               Application.Current.Shutdown();
         }

         InitializeComponent();

         ConfigureFileSystemWatcher();
         var slnViewModel = SetupSolutionInfoDataGrid();
         SetupSolutionAutocomplete(slnViewModel);
         RefreshTestingRuns();
         SelectLastTestingRun();
         TesterTimer = new TimerViewModel();
         TimerLabel.DataContext = TesterTimer;
         StartStopImage.Source = UiUtils.GetStatusImage(ButtonState.Stopped);
         DisableSelectionColumnsForNotInstalledStudios();
         SelectDefaultStartupColumn();
      }

      #region Свойства

      public TimerViewModel TesterTimer { get; set; } // vm таймера для фонового анализа            

      #endregion

      private void HighlightFirstMenuItem()
      {
         if (TestingRunMenuItem.Items.Count <= 0)
            return;

         var firstItem = TestingRunMenuItem.Items.Cast<MenuItem>().FirstOrDefault();
         if (firstItem != null)
         {
            var defaultBrush = firstItem.Background;
            UiUtils.ResetHighlighting(TestingRunMenuItem.Items.OfType<MenuItem>(), defaultBrush);
            firstItem.Background = UiUtils.DefaulHighlightBrush;
         }
      }

      private void SetNewStatusValue(StartupConfigurationMode configMode, SolutionInfoViewModel localSlnInfo,
         AnalysisStatus newStatus)
      {
         switch (configMode)
         {
            case StartupConfigurationMode.PvsVs2010:
               RunInUiSynchronizationContext(() => localSlnInfo.LaunchStatusOnPvsVs2010 = newStatus);
               break;
            case StartupConfigurationMode.PvsVs2012:
               RunInUiSynchronizationContext(() => localSlnInfo.LaunchStatusOnPvsVs2012 = newStatus);
               break;
            case StartupConfigurationMode.PvsVs2013:
               RunInUiSynchronizationContext(() => localSlnInfo.LaunchStatusOnPvsVs2013 = newStatus);
               break;
            case StartupConfigurationMode.PvsVs2015:
               RunInUiSynchronizationContext(() => localSlnInfo.LaunchStatusOnPvsVs2015 = newStatus);
               break;
            default:
               throw new InvalidOperationException(
                  string.Format("Unsupported Visual Studio version: {0}", configMode));
         }
      }

      private void SetUiCompletedState()
      {
         Interlocked.Exchange(ref _isRunningNow, 0);
         TesterTimer.Stop();
         TestingProgressBar.Visibility = Visibility.Hidden;
         var collectResults = UiUtils.CollectResults(_currentSelectedSolutions);
         var hasDiffs = collectResults.Item2 > 0;
         var resultTooltip = hasDiffs
            ? string.Format(Properties.Resources.HaveDiffs, collectResults.Item2)
            : Properties.Resources.HaveNoDiffs;
         AnalisysResultImage.Source = hasDiffs ? UiUtils.RedBall : UiUtils.GreenBall;
         AnalisysResultImage.ToolTip = resultTooltip;
         RefreshTestingRuns();
         HighlightFirstMenuItem();
         UiUtils.SetMenuItemsEnabled(TestingRunMenuItem, true);
         ApproveButton.IsEnabled = true;
      }

      private void SetupSolutionInfoDataGrid(IEnumerable<SolutionInfoViewModel> solutionInfoViewModels)
      {
         SlnInfoDataGrid.ItemsSource = solutionInfoViewModels;
      }

      /// <summary>
      ///    Состояние запуска в UI
      /// </summary>
      private void SetUiRunningState()
      {
         TestingProgressBar.Value = 0;
         TestingProgressBar.Visibility = Visibility.Visible;
         TesterTimer.Start();
         AnalisysResultImage.Source = UiUtils.OrangeBall;
         AnalisysResultImage.ToolTip = Properties.Resources.RunningTooltip;
         UiUtils.SetMenuItemsEnabled(TestingRunMenuItem, false);
         ApproveButton.IsEnabled = false;
      }

      /// <summary>
      ///    Получение выбранных решений
      /// </summary>
      /// <returns>Выбранные решения</returns>
      private IEnumerable<Tuple<StartupConfigurationMode, SolutionInfoViewModel>> GetCurrentSelectedSolutions()
      {
         var selectedCells = SlnInfoDataGrid.SelectedCells;
         return selectedCells.Count == 0
            ? Enumerable.Empty<Tuple<StartupConfigurationMode, SolutionInfoViewModel>>()
            : (from cellInfo in selectedCells
               let slnViewModel = cellInfo.Item as SolutionInfoViewModel
               let configMode = UiUtils.SlnInfoGridHeaderToStartupConfigurationMode(cellInfo.Column.Header.ToString())
               select Tuple.Create(configMode, slnViewModel)).ToArray();
      }

      /// <summary>
      ///    Подсветка выбранного запуска в меню
      /// </summary>
      private void HighlightSelectedRun()
      {
         var menuItems = TestingRunMenuItem.Items.OfType<MenuItem>();
         TestRunEntity selectedEntity = _selectedRunFolder;
         foreach (var item in from item in menuItems
            let header = item.Header.ToString()
            where string.Compare(selectedEntity.ToString(), header, StringComparison.CurrentCultureIgnoreCase) == 0
            select item)
         {
            item.Background = UiUtils.DefaulHighlightBrush;
            break;
         }
      }

      /// <summary>
      ///    Факт существование выбранных решений на запуск
      /// </summary>
      /// <param name="success">true - если выбранные решения есть, false - в противном случае</param>
      /// <param name="caption">Сообщение, зависящее от успеха операции</param>
      /// <param name="errorMessage">Сообщение об ошибке</param>
      /// <returns>Выбранные решения</returns>
      private IEnumerable<Tuple<StartupConfigurationMode, SolutionInfoViewModel>> ExistSelectedSolutions(
         out bool success, out string caption, out string errorMessage)
      {
         if (string.IsNullOrEmpty(_selectedRunFolder) || !Directory.Exists(_selectedRunFolder))
         {
            caption = Properties.Resources.SelectRunCaption;
            errorMessage = Properties.Resources.SelectRunMsg;
            success = false;
            return Enumerable.Empty<Tuple<StartupConfigurationMode, SolutionInfoViewModel>>();
         }

         var selectedSolutions = GetCurrentSelectedSolutions();
         var solutions = selectedSolutions as Tuple<StartupConfigurationMode, SolutionInfoViewModel>[] ??
                         selectedSolutions.ToArray();
         if (solutions.Length == 0)
         {
            caption = Properties.Resources.SelectRunCaption;
            errorMessage = Properties.Resources.SelectRunMsg;
            success = false;
            return Enumerable.Empty<Tuple<StartupConfigurationMode, SolutionInfoViewModel>>();
         }

         caption = errorMessage = string.Empty;
         success = true;
         return solutions;
      }

      #region Замена эталонных логов в неблокирующем режиме

      private async void ApproveButton_OnClick(object sender, RoutedEventArgs e)
      {
         bool success;
         string caption, errorMessage;
         var selectedSolutions = ExistSelectedSolutions(out success, out caption, out errorMessage);
         var filteredSolutions = new List<Tuple<StartupConfigurationMode, SolutionInfoViewModel>>();
         var solutions = selectedSolutions as Tuple<StartupConfigurationMode, SolutionInfoViewModel>[] ??
                         selectedSolutions.ToArray();

         // Фильтруем решения, с подходящей конфигурацией для замены эталонов и с подходящим статусом анализа
         foreach (var selectedSolution
            in solutions.Where(selectedSolution => selectedSolution.Item1.IsSuitForEtalons()))
            switch (selectedSolution.Item1)
            {
               case StartupConfigurationMode.PvsVs2010:
                  if (selectedSolution.Item2.LaunchStatusOnPvsVs2010.IsInApproveSuitableStatus())
                     filteredSolutions.Add(selectedSolution);
                  break;
               case StartupConfigurationMode.PvsVs2012:
                  if (selectedSolution.Item2.LaunchStatusOnPvsVs2012.IsInApproveSuitableStatus())
                     filteredSolutions.Add(selectedSolution);
                  break;
               case StartupConfigurationMode.PvsVs2013:
                  if (selectedSolution.Item2.LaunchStatusOnPvsVs2013.IsInApproveSuitableStatus())
                     filteredSolutions.Add(selectedSolution);
                  break;
               case StartupConfigurationMode.PvsVs2015:
                  if (selectedSolution.Item2.LaunchStatusOnPvsVs2015.IsInApproveSuitableStatus())
                     filteredSolutions.Add(selectedSolution);
                  break;
            }

         if (!success)
         {
            MessageBox.Show(this, caption, errorMessage, MessageBoxButton.OK, MessageBoxImage.Information);
            return;
         }

         if (filteredSolutions.Count == 0)
         {
            MessageBox.Show(this, Properties.Resources.NoSuitableSolutionsMsg, _selfTesterAppName, MessageBoxButton.OK,
               MessageBoxImage.Warning);
            return;
         }

         var messageBoxResult = MessageBox.Show(this,
            string.Format(Properties.Resources.WannaContinue, Environment.NewLine), _selfTesterAppName,
            MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
         if (messageBoxResult != MessageBoxResult.Yes)
            return;

         // Заменяем эталонные логи локально...
         var logsToReplace =
            await AnalyzerUtilities.GetLogsToReplaceAsync(filteredSolutions, _selectedRunFolder).ConfigureAwait(true);

         var localReplaceExecutor = new ReplaceEtalonsExecutor(ApplicationConfigReader.Instance.EtalonLogFolder,
            logsToReplace);
         await localReplaceExecutor.UpdateEtalonsAsync().ConfigureAwait(true);

         // ...  И на build-сервере
         if (Directory.Exists(ApplicationConfigReader.Instance.BldSrvEtalonsFolder))
         {
            var buildServerReplaceExecutor =
               new ReplaceEtalonsExecutor(ApplicationConfigReader.Instance.BldSrvEtalonsFolder, logsToReplace);
            await buildServerReplaceExecutor.UpdateEtalonsAsync().ConfigureAwait(true);
         }
         else
         {
            MessageBox.Show(this,
               string.Format(Properties.Resources.EtalonApproveBuildServerPathNotFound,
                  ApplicationConfigReader.Instance.BldSrvEtalonsFolder), _selfTesterAppName, MessageBoxButton.OK,
               MessageBoxImage.Error);
         }

         var sourceVerticalOffset = 0.0;
         var slnScrollViewer = UiUtils.GetScrollViewer(SlnInfoDataGrid) as ScrollViewer;
         if (slnScrollViewer != null)
            sourceVerticalOffset = slnScrollViewer.ContentVerticalOffset;

         SetupSolutionInfoDataGrid(TestedSolutionsManager.RefreshTestingRunModel(_selectedRunFolder));
         RefreshTestingRuns();
         HighlightSelectedRun();

         if (slnScrollViewer != null)
            slnScrollViewer.ScrollToVerticalOffset(sourceVerticalOffset);
      }

      #endregion

      private void RunInUiSynchronizationContext(Action uiDependantAction)
      {
         Dispatcher.BeginInvoke(uiDependantAction);
      }

      #region Поля

      // Текущее состояние выделенных решений      
      private readonly ISet<SolutionInfoViewModel> _currentSelectedSolutions =
         new SortedSet<SolutionInfoViewModel>(SolutionInfoViewModel.DefaultReadOrderComparer);

      private readonly string _selfTesterAppName = Properties.Resources.ApplicationName;
      private int _currentTotalCount; // Текущее кол-во решений
      private int _isRunningNow; // Запущен ли тестер в текущий момент
      private AutoResetTimer _killingTimer;
      private string _selectedRunFolder = string.Empty;
      private string _selectedSolutionToSearch;
      private BackgroundWorker _testWorker; // Фоновый объект для анализа      

      #endregion

      #region Запуск/остановка тестирования

      private void OnStartStopTester_Click(object sender, RoutedEventArgs e)
      {
         var startStopButtonTag = (ButtonState) StartStopButton.Tag;
         StartStopButton.IsEnabled = false;
         bool actionSuccess;
         string warningMessage;
         var appName = _selfTesterAppName;

         switch (startStopButtonTag)
         {
            case ButtonState.Stopped:
               // Тестер остановлен: Запускаем                                                                 
               actionSuccess = StartPrepare(out warningMessage);
               if (!actionSuccess)
               {
                  if (warningMessage == Properties.Resources.SelectSln)
                  {
                     MessageBox.Show(this, warningMessage, appName, MessageBoxButton.OK, MessageBoxImage.Warning);
                     StartStopButton.IsEnabled = true;
                  }
               }
               else
               {
                  UiUtils.TurnButtonState(startStopButtonTag, StartStopButton, StartStopImage);
                  StartStopButton.IsEnabled = true;
               }
               break;
            case ButtonState.Running: // Тестер запущен: Останавливаем
               actionSuccess = StopPrepare(out warningMessage);
               if (!actionSuccess)
                  MessageBox.Show(this, warningMessage, appName, MessageBoxButton.OK, MessageBoxImage.Warning);

               UiUtils.TurnButtonState(startStopButtonTag, StartStopButton, StartStopImage);
               StartStopButton.IsEnabled = true;
               break;
            default:
               throw new ArgumentOutOfRangeException();
         }

         // Поднимаемся к ячейке проекта, который будет запущен первым                           
         var firstToRun = _currentSelectedSolutions.FirstOrDefault();
         if (firstToRun != null)
            SlnInfoDataGrid.ScrollIntoView(firstToRun);
      }

      private bool StopPrepare(out string warningMessage)
      {
         if (_killingTimer != null)
         {
            _killingTimer.Dispose();
            _killingTimer = null;
         }

         this.BlurApply(UiUtils.DefaultBlurRadius, TimeSpan.FromMilliseconds(200), TimeSpan.Zero);
         var killingWaitWindow = new WaitWindow(_selfTesterAppName, "Killing Visual Studio's...") {Owner = this};
         var countdownThreshold = 10;
         const int launchTimerInterval = 500;

         /* В течении countdownThreshold * launchTimerInterval миллисекунд будем
            принудительно завершать процессы, порожденные SelfTester'ом */
         _killingTimer = AutoResetTimer.Create(() =>
            {
               if (countdownThreshold-- > 0)
               {
                  ProcessUtils.MandatoryKill(Process.GetCurrentProcess(), false);
               }
               else
               {
                  _killingTimer.Dispose();
                  RunInUiSynchronizationContext(() =>
                  {
                     killingWaitWindow.Close();
                     this.BlurDisable(TimeSpan.FromSeconds(1), TimeSpan.Zero);
                  });
               }
            }, launchTimerInterval)
            .Shedule(() => { RunInUiSynchronizationContext(() => killingWaitWindow.ShowDialogAlt()); });

         if (_testWorker != null && _testWorker.IsBusy)
         {
            _testWorker.CancelAsync();
            SetupSolutionInfoDataGrid(TestedSolutionsManager.RefreshTestingRunModel(_selectedRunFolder));
            SetUiCompletedState();
            UiUtils.ResetActiveStatuses(_currentSelectedSolutions, true);
            warningMessage = string.Empty;
            return true;
         }

         warningMessage = Properties.Resources.AnalisysNotRunningNow;
         return false;
      }

      private bool StartPrepare(out string warningMessage)
      {
         if (_currentSelectedSolutions.Count > 0)
            _currentSelectedSolutions.Clear();
         if (_currentTotalCount > 0)
            _currentTotalCount = 0;

         var dataGridCellInfos = SlnInfoDataGrid.SelectedCells;
         foreach (var cellInfo in dataGridCellInfos)
         {
            var slnInfo = cellInfo.Item as SolutionInfoViewModel;
            if (slnInfo == null)
               continue;

            _currentSelectedSolutions.Add(slnInfo);
            var headerString = cellInfo.Column.Header.ToString();
            var startupConfigurationMode = UiUtils.SlnInfoGridHeaderToStartupConfigurationMode(headerString);
            UiUtils.EstablishLaunchStatus(startupConfigurationMode, slnInfo, AnalysisStatus.InPending,
               ref _currentTotalCount);
         }

         UiUtils.ResetFinishedStatuses(_currentSelectedSolutions);

         if (_currentTotalCount == 0)
         {
            StartStopButton.Tag = ButtonState.Stopped;
            Interlocked.Exchange(ref _isRunningNow, 0);
            warningMessage = Properties.Resources.SelectSln;
            return false;
         }

         TuneBackgroundWorker();
         var models = SetupSolutionInfoDataGrid();
         for (var i = 0; i < models.Length; i++)
         {
            var localIndex = i;
            foreach (var selectedSolution in
               _currentSelectedSolutions.Where(selectedSolution => models[localIndex].Equals(selectedSolution)))
               models[i] = selectedSolution;
         }

         SetupSolutionInfoDataGrid(models);
         SetUiRunningState();
         _testWorker.RunWorkerAsync();

         warningMessage = string.Empty;
         return true;
      }

      #endregion

      #region Действия по инициализации

      private void ConfigureFileSystemWatcher()
      {
         var newTestWatcher = new FileSystemWatcher(ApplicationConfigReader.Instance.Viva64Logs)
         {
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName
         };

         newTestWatcher.Created += (sender, args) =>
         {
            if (args.ChangeType != WatcherChangeTypes.Created)
               return;

            var newDirName = args.Name;
            string machineName;
            DateTime testingDateTime;
            AnalyzerUtilities.ExtractLaunchPartyComponents(newDirName, out machineName, out testingDateTime);

            var newRunEntity = new TestRunEntity
            {
               MachineName = machineName,
               RunningDateTime = testingDateTime,
               TestRunDirectory = args.FullPath
            };

            RunInUiSynchronizationContext(() =>
            {
               var newItem = new MenuItem {Header = newRunEntity.ToString()};
               TestingRunMenuItem.Items.Insert(0, newItem);
               HighlightFirstMenuItem();
               UiUtils.SetMenuItemsEnabled(TestingRunMenuItem, false);
            });

            _selectedRunFolder = newRunEntity.TestRunDirectory;
         };

         newTestWatcher.EnableRaisingEvents = true;
      }

      private SolutionInfoViewModel[] SetupSolutionInfoDataGrid()
      {
         var models = TestedSolutionsManager.RetrieveSlnInfoViewModels();
         SlnInfoDataGrid.ItemsSource = models;
         return models;
      }

      /// <summary>
      ///    Установка логики анализа в "фоне"
      /// </summary>
      private void TuneBackgroundWorker()
      {
         if (_testWorker != null)
         {
            _testWorker.Dispose();
            _testWorker = null;
         }

         _testWorker = new BackgroundWorker
         {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
         };

         _testWorker.DoWork += (workerSender, workEventArgs) =>
         {
            Interlocked.Exchange(ref _isRunningNow, 1);

            using (var controller = new AnalyzerController(_currentSelectedSolutions))
            {
               controller.AnalysisManagers.CollectionChanged +=
                  (ctrlSender, changedEventArgs) => // "Подписываемся" на добавление менеджера анализа
                  {
                     if (changedEventArgs.Action != NotifyCollectionChangedAction.Add)
                        return;

                     var newManager = changedEventArgs.NewItems.Cast<AnalysisManager>().FirstOrDefault();
                     if (newManager == null)
                        throw new InvalidOperationException("newManager can't be null");

                     newManager.AnalysisStatusChanged += (manSender, analysisEventArgs) =>
                        // "Подписываемся" на смену статуса анализа для очередного добавленного менеджера
                     {
                        SetNewStatusValue(
                           analysisEventArgs.StartupMode, analysisEventArgs.SolutionInfo, analysisEventArgs.NewStatus);
                        if (_testWorker.IsBusy && _testWorker.CancellationPending)
                           workEventArgs.Cancel = true;
                     };
                  };

               controller.CurrentAnalysisDone += (ctrl, currentAnalysisDoneEventArgs) =>
                  // "Подписываемся" на завершение анализа для текущего менеджера
               {
                  var currentFinishedCount = currentAnalysisDoneEventArgs.CurrentFinishedCount;
                  var progress = (int) (currentFinishedCount / (double) _currentTotalCount * 100);
                  _testWorker.ReportProgress(progress);
               };

               controller.RunAll(AnalysisStatus.InPending);
            }
         };

         _testWorker.RunWorkerCompleted += (sender, completedEventArgs) =>
         {
            var ex = completedEventArgs.Error;
            if (ex != null)
               AppEventLogger.Log(ex.ToString(), EventLogEntryType.Warning);

            if (!completedEventArgs.Cancelled)
            {
               SetUiCompletedState();
               UiUtils.ResetActiveStatuses(_currentSelectedSolutions);
            }
            else
            {
               UiUtils.ResetActiveStatuses(_currentSelectedSolutions, true);
            }

            ProcessUtils.MandatoryKill(Process.GetCurrentProcess(), false);
            if (!StartStopButton.IsEnabled)
               StartStopButton.IsEnabled = true;

            UiUtils.TurnButtonState(ButtonState.Running, StartStopButton, StartStopImage);
            if (!completedEventArgs.Cancelled && ShutdownCheckBox.IsChecked == true)
               AnalyzerUtilities.ShutdownPc();
         };

         _testWorker.ProgressChanged +=
            (sender, changedEventArgs) => { TestingProgressBar.Value = changedEventArgs.ProgressPercentage; };
      }

      /// <summary>
      ///    Заполнение меню Testing Run существующими тестовыми запусками
      /// </summary>
      private void RefreshTestingRuns()
      {
         if (TestingRunMenuItem.Items.Count > 0)
            TestingRunMenuItem.Items.Clear();

         var allTestRuns =
            TestedSolutionsManager.DeferredInitializeTestingRuns(ApplicationConfigReader.DefaultTopRecords).ToArray();
         var testRunnersMap = allTestRuns.ToDictionary(topTestRun => topTestRun.Key, topTestRun => topTestRun.Value);

         var menuItems = new List<MenuItem>();
         var eraseAllMenuItem = new MenuItem {Header = Properties.Resources.EraseAll};
         var defaultBrush = eraseAllMenuItem.Background;

         eraseAllMenuItem.Click += (sender, args) =>
         {
            UiUtils.ResetHighlighting(menuItems, defaultBrush);
            TestedSolutionsManager.CleanExistingTestRuns();
            SetupSolutionInfoDataGrid();
            RefreshTestingRuns();
         };

         var eraseAllSeparator = new Separator {Margin = new Thickness(0, 5, 0, 5)};
         foreach (var testRun in testRunnersMap.OrderByDescending(pair => pair.Key))
         {
            var testRunItem = new MenuItem {Header = testRun.Key.ToString()};
            menuItems.Add(testRunItem);

            var localTestRun = testRun;
            testRunItem.Click += (sender, args) =>
            {
               UiUtils.ResetHighlighting(menuItems, defaultBrush);
               testRunItem.Background = UiUtils.DefaulHighlightBrush;
               SlnInfoDataGrid.ItemsSource = localTestRun.Value.Value;
               _selectedRunFolder = localTestRun.Key.TestRunDirectory;
            };

            TestingRunMenuItem.Items.Add(testRunItem);
         }

         if (allTestRuns.Any())
            TestingRunMenuItem.Items.Add(eraseAllSeparator);

         TestingRunMenuItem.Items.Add(eraseAllMenuItem);
      }

      /// <summary>
      ///    Выбор последнего тестового запуска
      /// </summary>
      private void SelectLastTestingRun()
      {
         var firstMenuItem = TestingRunMenuItem.Items.OfType<MenuItem>().FirstOrDefault();
         if (firstMenuItem != null)
            firstMenuItem.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
      }

      private void DisableSelectionColumnsForNotInstalledStudios()
      {
         var disableSelectionStyle = (Style) Resources["NoSelectableColumnStyle"];

         if (!InstallationInfo.IsInstalledVs2010 || !InstallationInfo.IsInstalledPvsStudio)
            PvsVs2010Column.CellStyle = disableSelectionStyle;
         if (!InstallationInfo.IsInstalledVs2012 || !InstallationInfo.IsInstalledPvsStudio)
            PvsVs2012Column.CellStyle = disableSelectionStyle;
         if (!InstallationInfo.IsInstalledVs2013 || !InstallationInfo.IsInstalledPvsStudio)
            PvsVs2013Column.CellStyle = disableSelectionStyle;
         if (!InstallationInfo.IsInstalledVs2015 || !InstallationInfo.IsInstalledPvsStudio)
            PvsVs2015Column.CellStyle = disableSelectionStyle;
      }

      /// <summary>
      ///    Выбор столбца сетки для конфигурации запуска по-умолчанию
      /// </summary>
      private void SelectDefaultStartupColumn()
      {
         foreach (SolutionInfoViewModel solutionItem in SlnInfoDataGrid.Items)
         {
            var columnIndex = -1;
            var firstFound = false;
            if (!solutionItem.LaunchStatusOnPvsVs2010.IsInDisablingStatus())
            {
               firstFound = true;
               columnIndex = solutionItem.GetColumnIndex("LaunchStatusOnPvsVs2010");
            }
            else if (!solutionItem.LaunchStatusOnPvsVs2012.IsInDisablingStatus())
            {
               firstFound = true;
               columnIndex = solutionItem.GetColumnIndex("LaunchStatusOnPvsVs2012");
            }
            else if (!solutionItem.LaunchStatusOnPvsVs2013.IsInDisablingStatus())
            {
               firstFound = true;
               columnIndex = solutionItem.GetColumnIndex("LaunchStatusOnPvsVs2013");
            }
            else if (!solutionItem.LaunchStatusOnPvsVs2015.IsInDisablingStatus())
            {
               firstFound = true;
               columnIndex = solutionItem.GetColumnIndex("LaunchStatusOnPvsVs2015");
            }

            if (firstFound && columnIndex >= 0 && columnIndex <= SlnInfoDataGrid.Columns.Count - 1)
            {
               var localItem = solutionItem;
               RunInUiSynchronizationContext(() =>
               {
                  SlnInfoDataGrid.Focus();
                  SlnInfoDataGrid.CurrentCell = new DataGridCellInfo(localItem, SlnInfoDataGrid.Columns[columnIndex]);
                  SlnInfoDataGrid.SelectedCells.Add(SlnInfoDataGrid.CurrentCell);
               });
            }
         }
      }

      #endregion

      #region Поиск решений по автозавершению

      private void SetupSolutionAutocomplete(SolutionInfoViewModel[] slnViewModel)
      {
         SolutionAutoCompleteBox.ItemsSource =
            slnViewModel.Select(model => Path.GetFileNameWithoutExtension(model.TestSolutionInfo.AbsSolutionFileName));
      }

      private void OnCompleteSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         _selectedSolutionToSearch = e.AddedItems.Cast<string>().FirstOrDefault();
      }

      private async void OnCompletePreviewKeyUp(object sender, KeyEventArgs e)
      {
         if (e.Key != Key.Return)
            return;

         if (string.IsNullOrWhiteSpace(_selectedSolutionToSearch))
            return;

         var solutions = SlnInfoDataGrid.Items.Cast<SolutionInfoViewModel>();
         var foundSolution =
            await FindAutocompletedSolutionAsync(solutions, _selectedSolutionToSearch).ConfigureAwait(true);
         if (foundSolution != null)
         {
            SlnInfoDataGrid.SelectedItem = foundSolution;
            SlnInfoDataGrid.ScrollIntoView(SlnInfoDataGrid.SelectedItem);
         }
         else
         {
            MessageBox.Show(this,
               string.Format(Properties.Resources.AutocompleteSelectionNotFound, _selectedSolutionToSearch),
               _selfTesterAppName, MessageBoxButton.OK, MessageBoxImage.Information);
         }

         e.Handled = true;
      }

      private static Task<SolutionInfoViewModel> FindAutocompletedSolutionAsync(
         IEnumerable<SolutionInfoViewModel> solutions, string selectedSolution)
      {
         return Task.Run(() => FindAutocompletedSolution(solutions, selectedSolution));
      }

      private static SolutionInfoViewModel FindAutocompletedSolution(IEnumerable<SolutionInfoViewModel> solutions,
         string selectedSolution)
      {
         return
            solutions.FirstOrDefault(
               sln =>
                  string.Compare(selectedSolution,
                     Path.GetFileNameWithoutExtension(sln.TestSolutionInfo.AbsSolutionFileName),
                     StringComparison.CurrentCultureIgnoreCase) == 0);
      }

      #endregion

      #region Открытие pvs-warning'ов в Visual Studio

      private async void OnSlnInfoDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
      {
         var dependencyObject = e.OriginalSource as DependencyObject;
         if (dependencyObject == null)
            return;
         var currentGridCell = UiUtils.FindProperDependencyElement<DataGridCell>(dependencyObject);
         if (currentGridCell == null)
            return;

         var currentSolution = currentGridCell.DataContext as SolutionInfoViewModel;
         var headerString = currentGridCell.Column.Header.ToString();
         var startupConfigurationMode = UiUtils.SlnInfoGridHeaderToStartupConfigurationMode(headerString);
         if (currentSolution == null || startupConfigurationMode == StartupConfigurationMode.All)
            return;
         var currentSolutionStatus = startupConfigurationMode.GetStatusValue(currentSolution);

         if (_selectedRunFolder == string.Empty || !Directory.Exists(_selectedRunFolder))
         {
            MessageBox.Show(this, Properties.Resources.SelectRunMsg, _selfTesterAppName, MessageBoxButton.OK,
               MessageBoxImage.Warning);
            return;
         }

         if (currentSolutionStatus != AnalysisStatus.DiffFinished)
            return;

         var runFolderClosure = _selectedRunFolder;
         try
         {
            this.BlurApply(UiUtils.DefaultBlurRadius, TimeSpan.FromMilliseconds(200), TimeSpan.Zero);
            var waitWindow = new WaitWindow(_selfTesterAppName, Properties.Resources.DevenvStarting) {Owner = this};
            await ShowWaitingDialogAsync(waitWindow).ConfigureAwait(true);
            await
               AnalyzerUtilities.OpenAnalysisReportUntilWndValidAsync(currentSolution, startupConfigurationMode,
                     runFolderClosure)
                  .ConfigureAwait(true);
            waitWindow.Close();
         }
         finally
         {
            this.BlurDisable(TimeSpan.FromSeconds(1), TimeSpan.Zero);
         }
      }

      private Task ShowWaitingDialogAsync(WaitWindow waitWindow)
      {
         return Task.Run(() => { RunInUiSynchronizationContext(waitWindow.ShowDialogAlt); });
      }

      private async void BatchOpeningLogsButton_OnClick(object sender, RoutedEventArgs e)
      {
         var openingButton = sender as Button;
         if (openingButton == null)
            return;

         openingButton.IsEnabled = false;
         bool success;
         string caption, errorMessage;
         var deferred = ExistSelectedSolutions(out success, out caption, out errorMessage);
         if (!success)
         {
            MessageBox.Show(this, caption, errorMessage, MessageBoxButton.OK, MessageBoxImage.Information);
            return;
         }

         var solutions =
            await AnalyzerUtilities.FilterSolutionsAsync(deferred, AnalysisStatus.DiffFinished).ConfigureAwait(true);
         if (solutions.Length == 0)
         {
            MessageBox.Show(this, Properties.Resources.NoSelDiffs, _selfTesterAppName, MessageBoxButton.OK,
               MessageBoxImage.Error);
            return;
         }

         SlnInfoDataGrid.SelectedCells.Clear();
         var runFolderClosure = _selectedRunFolder;
         await
            AnalyzerUtilities.OpenAnalysisReportsAsync(solutions, runFolderClosure, false,
               () => RunInUiSynchronizationContext(() => openingButton.IsEnabled = true)).ConfigureAwait(true);
      }

      #endregion

      #region Остальные события

      /// <summary>
      ///    Выделение ячеек при щелчке на заголовок сетки
      /// </summary>
      /// <param name="sender">sender</param>
      /// <param name="e">e</param>
      private void OnSlnInfoDataGrid_MouseClick(object sender, MouseButtonEventArgs e)
      {
         var dependencyObject = e.OriginalSource as DependencyObject;
         if (dependencyObject == null)
            return;

         var dataGridColumnHeader = UiUtils.FindProperDependencyElement<DataGridColumnHeader>(dependencyObject);
         if (dataGridColumnHeader == null)
            return;

         Keyboard.ClearFocus();
         SlnInfoDataGrid.Focus();

         var currentCursor = Cursor;
         try
         {
            Cursor = Cursors.Wait;
            SlnInfoDataGrid.IsEnabled = false;

            var headerString = dataGridColumnHeader.Content.ToString();
            var startupConfigurationMode = UiUtils.SlnInfoGridHeaderToStartupConfigurationMode(headerString);
            if (startupConfigurationMode == StartupConfigurationMode.All)
            {
               SlnInfoDataGrid.SelectAll();
            }
            else
            {
               string errorMessage;
               var isVsInstalled = startupConfigurationMode.IsVsInstalled(out errorMessage);
               if (isVsInstalled)
               {
                  if ((Keyboard.Modifiers & ModifierKeys.Control) <= 0)
                     SlnInfoDataGrid.SelectedCells.Clear();
                  UiUtils.SelectColumnCellsByHeaderString(startupConfigurationMode, SlnInfoDataGrid);
               }
               else
               {
                  MessageBox.Show(this, errorMessage, _selfTesterAppName, MessageBoxButton.OK, MessageBoxImage.Warning);
               }
            }
         }
         finally
         {
            Cursor = currentCursor;
            SlnInfoDataGrid.IsEnabled = true;
         }
      }

      /// <summary>
      ///    Реакция на закрытие окна
      /// </summary>
      /// <param name="sender">sender</param>
      /// <param name="e">e</param>
      private void MainWindow_OnClosing(object sender, CancelEventArgs e)
      {
         if (Thread.VolatileRead(ref _isRunningNow) != 1)
            return;

         var result = MessageBox.Show(this, Properties.Resources.AreYouSure, _selfTesterAppName, MessageBoxButton.YesNo,
            MessageBoxImage.Question);
         switch (result)
         {
            case MessageBoxResult.Yes:
               e.Cancel = false;
               if (_testWorker != null && _testWorker.IsBusy)
                  _testWorker.CancelAsync();

               ProcessUtils.MandatoryKill(Process.GetCurrentProcess(), false);
               break;

            case MessageBoxResult.No:
               e.Cancel = true;
               break;
         }
      }

      /// <summary>
      ///    Открытие HTML-лога для текущего выбранного запуска
      /// </summary>
      /// <param name="sender">sender</param>
      /// <param name="e">e</param>
      private void TesterLogButton_OnClick(object sender, RoutedEventArgs e)
      {
         string errorMessage, caption;
         var success = UiUtils.OpenHtmlLog(_selectedRunFolder, out errorMessage, out caption);
         if (!success)
            MessageBox.Show(this, errorMessage, caption, MessageBoxButton.OK, MessageBoxImage.Information);
      }

      #endregion
   }
}