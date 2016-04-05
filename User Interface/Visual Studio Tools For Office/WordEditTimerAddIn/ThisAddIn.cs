using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools;

namespace WordEditTimerAddIn
{
   public partial class ThisAddIn
   {
      private Dictionary<string, DocumentTimer> _documentEditTimes;
      // Карта объектов таймера, ассоциированная с именами документов

      private List<CustomTaskPane> _timerDisplayPanes; // Список панелей задач документов офиса

      private void ThisAddIn_Startup(object sender, EventArgs e)
      {
         // Инициализация таймеров и панелей отображения
         _documentEditTimes = new Dictionary<string, DocumentTimer>();
         _timerDisplayPanes = new List<CustomTaskPane>();

         // Добавление обработчиков событий
         var officeApp = Application;
         officeApp.DocumentOpen += OnOfficeDocumentOpen;
         ((ApplicationEvents4_Event)officeApp).NewDocument += OnOfficeNewDocument;
         officeApp.DocumentBeforeClose += OnOfficeDocumentBeforeClose;
         officeApp.WindowActivate += OnOfficeWindowActivate;

         try
         {
            MonitorDocument(officeApp.ActiveDocument); // Запуск мониторинга активного документа
            AddTaskPaneToWindow(officeApp.ActiveDocument.ActiveWindow); // Добавление в окно сцециальной панели задач
         }
         catch (COMException comEx)
         {
            MessageBox.Show(comEx.Message);
         }
      }

      private void AddTaskPaneToWindow(Window window)
      {
         // Проверка панели задач в окне
         CustomTaskPane docPane = null;
         CustomTaskPane paneToRemove = null;
         foreach (var displayPane in _timerDisplayPanes)
         {
            try
            {
               if (displayPane.Window == window)
               {
                  docPane = displayPane;
                  break;
               }
            }
            catch (ArgumentNullException)
            {
               paneToRemove = displayPane;
            }
         }

         // Удалим сомнительную панель задач
         _timerDisplayPanes.Remove(paneToRemove);

         // Добавить панель задач к документу
         if (docPane == null)
         {
            var pane = CustomTaskPanes.Add(new TimerDisplayPane(_documentEditTimes), "Document Edit Timer", window);
            _timerDisplayPanes.Add(pane);
            pane.VisibleChanged += OnPaneVisibleChanged;
         }
      }

      private void OnPaneVisibleChanged(object sender, EventArgs e)
      {
         // Получить панель задач и переключить видимость
         using (var taskPane = sender as CustomTaskPane)
         {
            if (taskPane == null || !taskPane.Visible)
               return;

            var timerDisplayPane = taskPane.Control as TimerDisplayPane;
            if (timerDisplayPane != null)
            {
               timerDisplayPane.RefreshDisplay();
            }
         }
      }

      internal void MonitorDocument(Document document)
      {
         _documentEditTimes.Add(document.Name, new DocumentTimer
         {
            Document = document,
            EditTime = TimeSpan.Zero,
            IsActive = true,
            LastActive = DateTime.Now
         });
      }

      private void OnOfficeWindowActivate(Document aDocument, Window aWindow)
      {
         if (_documentEditTimes.ContainsKey(Application.ActiveDocument.Name))
         {
            // Проверить правильность установки флажка в ленте, начать с получения таймера
            var documentTimer = _documentEditTimes[Application.ActiveDocument.Name];

            // Установить флажок
            Globals.Ribbons.TimerRibbon.SetPauseStatus(!documentTimer.IsActive);
         }
      }

      private void OnOfficeDocumentBeforeClose(Document aDocument, ref bool cancel)
      {
         // Заморозить таймер
         _documentEditTimes[aDocument.Name].EditTime += DateTime.Now - _documentEditTimes[aDocument.Name].LastActive;
         _documentEditTimes[aDocument.Name].IsActive = false;
         _documentEditTimes[aDocument.Name].Document = null;

         // Удалить панель задач
         RemoveTaskPaneFromWindow(aDocument.ActiveWindow);
      }

      private void RemoveTaskPaneFromWindow(Window window)
      {
         // Проверить наличие панели задач в окне
         var dockPane = _timerDisplayPanes.FirstOrDefault(taskPane => taskPane.Window == window);

         // Удалить панель задач документа
         if (dockPane != null)
         {
            CustomTaskPanes.Remove(dockPane);
            _timerDisplayPanes.Remove(dockPane);
         }
      }

      private void OnOfficeNewDocument(Document aNewDocument)
      {
         // Мониторинг нового документа
         MonitorDocument(aNewDocument);
         AddTaskPaneToWindow(aNewDocument.ActiveWindow);

         // Установить флажок
         Globals.Ribbons.TimerRibbon.SetPauseStatus(false);
      }

      private void OnOfficeDocumentOpen(Document aDocument)
      {
         if (_documentEditTimes.ContainsKey(aDocument.Name))
         {
            // Мониторинг старого документа
            _documentEditTimes[aDocument.Name].LastActive = DateTime.Now;
            _documentEditTimes[aDocument.Name].IsActive = true;
            _documentEditTimes[aDocument.Name].Document = aDocument;
         }
         else
         {
            // Мониторинг нового документа
            MonitorDocument(aDocument);
            AddTaskPaneToWindow(aDocument.ActiveWindow);
         }
      }

      internal void ToggleTaskPaneDisplay()
      {
         // Проверить наличие панели задач в окне
         AddTaskPaneToWindow(Application.ActiveDocument.ActiveWindow);

         // Переключить панель задач документа
         var docPane = _timerDisplayPanes.FirstOrDefault(pane => pane.Window == Application.ActiveDocument.ActiveWindow);
         if (docPane != null)
         {
            docPane.Visible = !docPane.Visible;
         }
      }

      internal void PauseOrResumeTimer(bool pause)
      {
         // Получить таймер
         var documentTimer = _documentEditTimes[Application.ActiveDocument.Name];

         if (pause && documentTimer.IsActive)
         {
            // Заморозить таймер
            documentTimer.EditTime += DateTime.Now - documentTimer.LastActive;
            documentTimer.IsActive = false;
         }
         else if (!pause && !documentTimer.IsActive)
         {
            // Возобновить таймер
            documentTimer.IsActive = true;
            documentTimer.LastActive = DateTime.Now;
         }
      }

      private void ThisAddIn_Shutdown(object sender, EventArgs e)
      {
      }

      #region VSTO generated code

      /// <summary>
      ///    Required method for Designer support - do not modify
      ///    the contents of this method with the code editor.
      /// </summary>
      private void InternalStartup()
      {
         Startup += ThisAddIn_Startup;
         Shutdown += ThisAddIn_Shutdown;
      }

      #endregion
   }
}