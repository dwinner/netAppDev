using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;

namespace WordEditTimerAddIn
{
   public partial class TimerDisplayPane : UserControl
   {
      private readonly Dictionary<string, DocumentTimer> _documentEditTimes;

      public TimerDisplayPane(Dictionary<string, DocumentTimer> documentEditTimes)
         : this()
      {
         _documentEditTimes = documentEditTimes;
      }

      private TimerDisplayPane()
      {
         InitializeComponent();
      }

      internal void RefreshDisplay()
      {
         // Очистить существующий список
         TimerListBox.Items.Clear();

         // Проверка мониторинга всех документов
         foreach (Document document in Globals.ThisAddIn.Application.Documents)
         {
            var isMonitored = false;
            var requiresNameChange = false;
            DocumentTimer oldNameTimer = null;
            string oldName = null;
            foreach (var documentName in _documentEditTimes.Keys)
            {
               if (document.Name == documentName)
               {
                  isMonitored = true;
                  break;
               }
               if (_documentEditTimes[documentName].Document == document)
               {
                  // Подвергается мониторингу, но имя изменилось!
                  oldName = documentName;
                  oldNameTimer = _documentEditTimes[documentName];
                  isMonitored = true;
                  requiresNameChange = true;
                  break;
               }
            }

            // Добавить новый монитор, если нужно
            if (!isMonitored)
            {
               Globals.ThisAddIn.MonitorDocument(document);
            }

            // Переименовать при необходимости
            if (requiresNameChange)
            {
               _documentEditTimes.Remove(oldName);
               _documentEditTimes.Add(document.Name, oldNameTimer);
            }
         }

         // Создать новый список
         foreach (var documentName in _documentEditTimes.Keys)
         {
            // Проверить, загружен ли все еще этот документ
            var isLoaded = Globals.ThisAddIn.Application.Documents.Cast<Document>().Any(doc => doc.Name == documentName);
            if (!isLoaded)
            {
               _documentEditTimes[documentName].IsActive = false;
               _documentEditTimes[documentName].Document = null;
            }

            // Добавить элемент
            TimerListBox.Items.Add(string.Format("{0}: {1}", documentName,
               _documentEditTimes[documentName].EditTime +
               (_documentEditTimes[documentName].IsActive
                  ? (DateTime.Now - _documentEditTimes[documentName].LastActive)
                  : TimeSpan.Zero)));
         }
      }

      private void RefreshButton_Click(object sender, EventArgs e)
      {
         RefreshDisplay();
      }
   }
}