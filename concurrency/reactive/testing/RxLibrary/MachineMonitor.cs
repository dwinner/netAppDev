using System;
using System.Reactive.Linq;

namespace RxLibrary;

public class MachineMonitor
{
   private const int MaximalAlertBurstTimeInSeconds = 5;
   private const int MinimalAlertPauseInSeconds = 5;
   private const int MaxTimeWithNoMovementInSeconds = 1;
   private const double RiskyTemperature = 70;
   private readonly IConcurrencyProvider _concurrencyProvider;
   private readonly IProximitySensor _proximitySensor;
   private readonly ITemperatureSensor _temperatureSensor;

   public MachineMonitor(
      IConcurrencyProvider concurrencyProvider,
      ITemperatureSensor temperatureSensor,
      IProximitySensor proximitySensor)
   {
      _concurrencyProvider = concurrencyProvider;
      _temperatureSensor = temperatureSensor;
      _proximitySensor = proximitySensor;
   }

   /// <summary>
   ///    The maximal amount of time we consider a sequence of notifications as a burst
   ///    even if the notifications are emitted close to each other, after this amount
   ///    of time a burst is "closed"
   /// </summary>
   public TimeSpan MaxAlertBurstTime { get; set; } = TimeSpan.FromSeconds(MaximalAlertBurstTimeInSeconds);

   /// <summary>
   ///    the amount of time we allow between two consecutive alerts.
   ///    if two alerts are notified with a short time between them, we consider them as one
   /// </summary>
   public TimeSpan MinAlertPause { get; set; } = TimeSpan.FromSeconds(MinimalAlertPauseInSeconds);

   /// <summary>
   ///    if no proximity notification is emitted during this time, we conclude that
   ///    there is no one near the sensor
   /// </summary>
   public TimeSpan MaximalTimeWithNoMovementInSeconds { get; set; } =
      TimeSpan.FromSeconds(MaxTimeWithNoMovementInSeconds);

   public IObservable<Alert> ObserveAlerts() =>
      Observable.Defer(() =>
      {
         var scheduler = _concurrencyProvider.TimeBasedOperations;
         var proximities = _proximitySensor.Readings.Publish().RefCount();
         var temperatures = _temperatureSensor.Readings.Replay(1).RefCount();

         var riskyTem = temperatures.Where(t => t >= RiskyTemperature);
         var proximityWindowBoundaries = proximities.Throttle(MaximalTimeWithNoMovementInSeconds);

         return (from proximityWindows in proximities.Window(proximityWindowBoundaries)
               from t in proximityWindows.CombineLatest(riskyTem, (_, t) => t)
               select t)
            .FilterBursts(MinAlertPause, MaxAlertBurstTime, scheduler)
            .Select(_ => new Alert("Temperature is too hot! Please move away", scheduler.Now));
      });
}