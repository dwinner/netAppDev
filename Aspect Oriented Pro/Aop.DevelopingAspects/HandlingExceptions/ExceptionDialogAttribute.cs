using System;
using System.Reflection;
using System.Windows;
using PostSharp.Aspects;

namespace HandlingExceptions
{
   [Serializable]
   public sealed class ExceptionDialogAttribute : OnExceptionAspect
   {
      private readonly Type _exceptionType;

      public ExceptionDialogAttribute() : this(null)
      {
      }

      private ExceptionDialogAttribute(Type exceptionType)
      {
         _exceptionType = exceptionType;
         Message = "{0}";
         Caption = "Error";
      }

      public string Message { get; set; }
      public string Caption { get; set; }

      public override Type GetExceptionType(MethodBase targetMethod) => _exceptionType;
      // Метод вызывается во время сборки

      public override void OnException(MethodExecutionArgs args)
      {
         var message = string.Format(Message, args.Exception.Message);
         var dependencyObject = args.Instance as DependencyObject;
         Window window = null;
         if (dependencyObject != null)
         {
            window = Window.GetWindow(dependencyObject);
         }

         if (window != null)
         {
            MessageBox.Show(window, message, Caption, MessageBoxButton.OK, MessageBoxImage.Error);
         }
         else
         {
            MessageBox.Show(message, Caption, MessageBoxButton.OK, MessageBoxImage.Error);
         }

         args.FlowBehavior = FlowBehavior.Return; // Не перебрасываем исключение
      }
   }
}