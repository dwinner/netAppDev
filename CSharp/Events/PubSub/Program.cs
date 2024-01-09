/*
 * События
 */

using System;

namespace _05_Events
{
   internal class Program
   {
      private static void Main(string[] args)
      {
         var player = new CorePlayer();
      }
   }

   /// <summary>
   ///    Аргументы, переданные от UI
   /// </summary>
   /// <remarks>Возникает при возникновении события включения воспроизведения</remarks>
   public class PlayEventArgs : EventArgs
   {
      public PlayEventArgs(string fileName) => FileName = fileName;

      public string FileName { get; }
   }

   public class PlayerUi
   {
      public event EventHandler<PlayEventArgs> PlayEvent; // Определить событие для уведомления о воспроизведении

      public void UserPressedPlay()
      {
         OnPlay();
      }

      protected virtual void OnPlay()
      {
         // Инициировать событие.
         var localHandler = PlayEvent;
         localHandler?.Invoke(this, new PlayEventArgs("somefile.wav"));
      }

      #region Явный способ обработки добавления и удаления операций событий

      private EventHandler<PlayEventArgs> _altPlayEvent;

      public event EventHandler<PlayEventArgs> AltPlayEvent
      {
         add => _altPlayEvent = (EventHandler<PlayEventArgs>)Delegate.Combine(_altPlayEvent, value);
         remove => _altPlayEvent = (EventHandler<PlayEventArgs>)Delegate.Remove(_altPlayEvent, value);
      }

      #endregion
   }

   public class CorePlayer
   {
      private readonly PlayerUi _ui;

      public CorePlayer()
      {
         _ui = new PlayerUi();
         // Регистрация обработчика события.
         _ui.PlayEvent += UiPlayEvent;
      }

      private void UiPlayEvent(object sender, PlayEventArgs e)
      {
         // Воспроизведение файла
      }
   }
}