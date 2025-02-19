﻿using System.Reactive.Linq;
using System.Runtime;
using RxHelpers;

namespace RxReactingToErrors;

internal static class Program
{
   private static void Main()
   {
      BasicOnError();
      CatchingSpecificExceptionType();
      OnErrorResumeNext();
      Retry();
      Rethrowing();
      Console.ReadLine();
   }

   private static void Rethrowing()
   {
      Demo.DisplayHeader("Catch operator - unlike try-catch block, Catch can bounce exceptions to other catchers");

      var weatherStationA =
         Observable.Throw<WeatherReport>(new OutOfMemoryException());

      weatherStationA
         .Catch((OutOfMemoryException ex) => { throw new InvalidOperationException("", ex); })
         .Catch((InvalidOperationException ex) => Observable.Empty<WeatherReport>())
         .SubscribeConsole("Catch (chain of control)");
   }

   private static void Retry()
   {
      Demo.DisplayHeader("Retry operator");

      var weatherStationA =
         Observable.Throw<WeatherReport>(new OutOfMemoryException());

      weatherStationA
         .Log()
         .Retry(3)
         .SubscribeConsole("Retry");
   }

   private static void OnErrorResumeNext()
   {
      Demo.DisplayHeader("OnErrorResumeNext operator - conct the second observable when the first completes or throws");

      var weatherStationA =
         Observable.Throw<WeatherReport>(new OutOfMemoryException());

      var weatherStationB =
         Observable.Return(new WeatherReport { Station = "B", Temperature = 20.0 });

      weatherStationA
         .OnErrorResumeNext(weatherStationB)
         .SubscribeConsole("OnErrorResumeNext(source throws)");

      weatherStationB
         .OnErrorResumeNext(weatherStationB)
         .SubscribeConsole("OnErrorResumeNext(source completed)");
   }

   private static void CatchingSpecificExceptionType()
   {
      Demo.DisplayHeader("Catch operator");

      var weatherSimulationResults =
         Observable.Throw<WeatherSimulation>(new OutOfMemoryException());

      weatherSimulationResults
         .Catch((OutOfMemoryException ex) =>
         {
            Console.WriteLine("handling OOM exception");
            return Observable.Empty<WeatherSimulation>();
         })
         .SubscribeConsole("Catch (source throws)");

      //Catch is not limited to a single exception type, it can be general to ALL exceptions 
      weatherSimulationResults
         .Catch(Observable.Empty<WeatherSimulation>())
         .SubscribeConsole("Catch (handling all exception types)");

      //of course, if the source observable completed successfully, then the Catch opertor has no effect (unlike OnErrorResumeNext) 
      Observable.Return(1)
         .Catch(Observable.Empty<int>())
         .SubscribeConsole("Catch (source completed successfully)");
   }

   private static void BasicOnError()
   {
      Demo.DisplayHeader("Basic OnError");

      // This the most basic way you would work with OnError.
      // But it's not ideal, consider using the 'Catch' operator
      var weatherSimulationResults =
         Observable.Throw<WeatherSimulation>(new OutOfMemoryException());

      weatherSimulationResults
         .Subscribe(
            _ => { },
            e =>
            {
               if (e is OutOfMemoryException)
               {
                  //a last attempt to free some memory
                  GCSettings.LargeObjectHeapCompactionMode =
                     GCLargeObjectHeapCompactionMode.CompactOnce;
                  GC.Collect();
                  GC.WaitForPendingFinalizers();

                  Console.WriteLine("GC Done");
               }
            });
   }
}