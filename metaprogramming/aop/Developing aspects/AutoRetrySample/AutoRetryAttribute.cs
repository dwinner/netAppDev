using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace AutoRetrySample
{
   /// <summary>
   ///    Aspect that, when applied to a method, causes invocations of this method to be retried if the method ends with
   ///    specified exceptions.
   /// </summary>
   [PSerializable]
   [LinesOfCodeAvoided(5)]
   public sealed class AutoRetryAttribute : MethodInterceptionAspect
   {
      const int DefaultMaxRetries = 5;
      const float DefaultDelay = 3.0f;
      Type[] defaultHanledExceptions = { typeof (WebException), typeof (DataException) };

      /// <summary>
      ///    Initializes a new <see cref="AutoRetryAttribute" /> with default values.
      /// </summary>
      public AutoRetryAttribute()
      {
         MaxRetries = DefaultMaxRetries;
         Delay = DefaultDelay;
         HandledExceptions = defaultHanledExceptions;
      }

      /// <summary>
      ///    Gets or sets the maximum number of retries. The default value is <see cref="DefaultMaxRetries" />
      /// </summary>
      public int MaxRetries { get; set; }

      /// <summary>
      ///    Gets or sets the delay before retrying, in seconds. The default value is <see cref="DefaultDelay" />.
      /// </summary>
      public float Delay { get; set; }

      /// <summary>
      ///    Gets or sets the type of exceptions that cause the method invocation to be retried. The default value is
      ///    <see cref="WebException" /> and <see cref="DataException" />.
      /// </summary>
      public Type[] HandledExceptions { get; set; }

      /// <summary>
      ///    Method invoked <i>instead</i> of the original method.
      /// </summary>
      /// <param name="args">Method invocation context.</param>
      public override void OnInvoke(MethodInterceptionArgs args)
      {
         for (var retryIndex = 0;; retryIndex++)
         {
            try
            {
               // Invoke the intercepted method
               args.Proceed();

               // If we get here, it means the execution was successful.
               return;
            }
            catch (Exception e) when (ExceptionTypeFilter(e))
            {
               // The intercepted method threw an exception. Figure out if we can retry the method.
               if (retryIndex < MaxRetries)
               {
                  // Yes, we can retry. Write some message and wait a bit.

                  Console.WriteLine(
                     "Method failed with exception {0}. Sleeping {1} s and retrying. This was our {2}th attempt.",
                     e.GetType().Namespace, Delay, retryIndex + 1);

                  Thread.Sleep(TimeSpan.FromSeconds(Delay));

                  // Continue to the next iteration.
               }
               else
               {
                  // No, we cannot retry. Retry the exception.
                  throw;
               }
            }
         }
      }

      bool ExceptionTypeFilter(Exception exception)
         => HandledExceptions == null || HandledExceptions.Any(type => type.IsInstanceOfType(exception));
   }
}