using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LifecycleSample.Common
{
   internal sealed class SuspensionManager
   {
      private const string SessionStateFilename = "_sessionState.xml";
      private static Dictionary<string, object> _sessionState = new Dictionary<string, object>();
      private static readonly List<Type> TheKnownTypes = new List<Type>();

      private static readonly DependencyProperty FrameSessionStateKeyProperty =
         DependencyProperty.RegisterAttached("_FrameSessionStateKey", typeof(string), typeof(SuspensionManager), null);

      private static readonly DependencyProperty FrameSessionStateProperty =
         DependencyProperty.RegisterAttached("_FrameSessionState", typeof(Dictionary<string, object>),
            typeof(SuspensionManager), null);

      private static readonly List<WeakReference<Frame>> RegisteredFrames = new List<WeakReference<Frame>>();

      /*
            private static Dictionary<string, object> SessionState
            {
               get { return _sessionState; }
            }
      */

      public static List<Type> KnownTypes
      {
         get { return TheKnownTypes; }
      }

      public static async Task SaveAsync()
      {
         // Сохранить состояние навигации для всех зарегистрированных фреймов
         foreach (var weakFrameReference in RegisteredFrames)
         {
            Frame frame;
            if (weakFrameReference.TryGetTarget(out frame))
            {
               SaveFrameNavigationState(frame);
            }
         }

         // Сериализовать состояние сеанса синхронным образом, чтобы
         // избежать асинхронного доступа к разделяемому состоянию
         using (var sessionData = new MemoryStream())
         {
            var serializer = new DataContractSerializer(typeof(Dictionary<string, object>), TheKnownTypes);
            serializer.WriteObject(sessionData, _sessionState);

            // Получить выходной поток для файла SessionState и записать состояние асинхронным образом
            var file =
               await
                  ApplicationData.Current.LocalFolder.CreateFileAsync(SessionStateFilename,
                     CreationCollisionOption.ReplaceExisting);
            using (var fileStream = await file.OpenStreamForWriteAsync())
            {
               sessionData.Seek(0, SeekOrigin.Begin);
               await sessionData.CopyToAsync(fileStream);
               await fileStream.FlushAsync();
            }
         }
      }

      public static async Task RestoreAsync()
      {
         _sessionState = new Dictionary<string, object>();

         // Получить входной поток для файла SessionState
         var file = await ApplicationData.Current.LocalFolder.GetFileAsync(SessionStateFilename);
         using (var inStream = await file.OpenSequentialReadAsync())
         {
            // Десериализовать состояние сеанса
            var serializer = new DataContractSerializer(typeof(Dictionary<string, object>), TheKnownTypes);
            _sessionState = (Dictionary<string, object>)serializer.ReadObject(inStream.AsStreamForRead());
         }

         // Восстановить разделенное состояние для всех зарегистрированных фреймов
         foreach (var weakFrameReference in RegisteredFrames)
         {
            Frame frame;
            if (weakFrameReference.TryGetTarget(out frame))
            {
               frame.ClearValue(FrameSessionStateProperty);
               RestoreFrameNavigationState(frame);
            }
         }
      }

      public static void RegisterFrame(Frame frame, string sessionStateKey)
      {
         if (frame.GetValue(FrameSessionStateKeyProperty) != null)
         {
            throw new InvalidOperationException("Frames can only be registered to one session state key");
         }

         if (frame.GetValue(FrameSessionStateProperty) != null)
         {
            throw new InvalidOperationException(
               "Frames must be either be registered before accessing frame session state, or not registered at all");
         }

         frame.SetValue(FrameSessionStateKeyProperty, sessionStateKey);
         RegisteredFrames.Add(new WeakReference<Frame>(frame));

         RestoreFrameNavigationState(frame);
      }

      /*
            public static void UnregisterFrame(Frame frame)
            {
               SessionState.Remove((string)frame.GetValue(FrameSessionStateKeyProperty));
               RegisteredFrames.RemoveAll(weakFrameReference =>
               {
                  Frame testFrame;
                  return !weakFrameReference.TryGetTarget(out testFrame) || testFrame == frame;
               });
            }
      */

      public static Dictionary<string, object> SessionStateForFrame(Frame frame)
      {
         var frameState = (Dictionary<string, object>)frame.GetValue(FrameSessionStateProperty);

         if (frameState != null)
            return frameState;

         var frameSessionKey = (string)frame.GetValue(FrameSessionStateKeyProperty);
         if (frameSessionKey != null)
         {
            if (!_sessionState.ContainsKey(frameSessionKey))
            {
               _sessionState[frameSessionKey] = new Dictionary<string, object>();
            }

            frameState = (Dictionary<string, object>)_sessionState[frameSessionKey];
         }
         else
         {
            frameState = new Dictionary<string, object>();
         }

         frame.SetValue(FrameSessionStateProperty, frameState);
         return frameState;
      }

      private static void RestoreFrameNavigationState(Frame frame)
      {
         var frameState = SessionStateForFrame(frame);
         if (frameState.ContainsKey("Navigation"))
         {
            frame.SetNavigationState((string)frameState["Navigation"]);
         }
      }

      private static void SaveFrameNavigationState(Frame frame)
      {
         var frameState = SessionStateForFrame(frame);
         frameState["Navigation"] = frame.GetNavigationState();
      }
   }
}