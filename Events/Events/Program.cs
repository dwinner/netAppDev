/**
 * События
 */

using System;

namespace _05_Events
{
   class Program
   {
      static void Main(string[] args)
      {
         CorePlayer player = new CorePlayer();
      }
   }

   /// <summary>
   /// Аргументы, переданные от UI
   /// </summary>
   /// <remarks>Возникает при возникновении события включения воспроизведения</remarks>
   public class PlayEventArgs : EventArgs
   {
      private string fileName;

      public string FileName { get { return fileName; } }

      public PlayEventArgs(string fileName)
      {
         this.fileName = fileName;
      }
   }

   public class PlayerUI
   {
      public event EventHandler<PlayEventArgs> PlayEvent;   // Определить событие для уведомления о воспроизведении

      #region Явный способ обработки добавления и удаления операций событий

      private EventHandler<PlayEventArgs> altPlayEvent;

      public event EventHandler<PlayEventArgs> AltPlayEvent
      {
         add { altPlayEvent = (EventHandler<PlayEventArgs>)Delegate.Combine(altPlayEvent, value); }
         remove { altPlayEvent = (EventHandler<PlayEventArgs>)Delegate.Remove(altPlayEvent, value); }
      }

      #endregion

      public void UserPressedPlay()
      {
         OnPlay();
      }

      protected virtual void OnPlay()
      {
         // Инициировать событие.
         EventHandler<PlayEventArgs> localHandler = PlayEvent;
         if (localHandler != null)
         {
            localHandler(this, new PlayEventArgs("somefile.wav"));
         }
      }
   }

   public class CorePlayer
   {
      private PlayerUI ui;

      public CorePlayer()
      {
         ui = new PlayerUI();
         // Регистрация обработчика события.
         ui.PlayEvent += new EventHandler<PlayEventArgs>(UiPlayEvent);
      }

      void UiPlayEvent(object sender, PlayEventArgs e)
      {
         // Воспроизведение файла
      }
   }
}
