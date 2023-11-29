using System;
using System.Reactive;
using System.Reactive.Concurrency;
using Microsoft.Reactive.Testing;
using NSubstitute;
using NSubstitute.Extensions;
using Xunit;

namespace RxLibrary.Tests.SensorMonitor;

public class SensorMonitorTests : ReactiveTest
{
   [Fact]
   public void BurstOverFiveSeconds_TemperatureIsRisky_AlertAtBurstStartAndAfterFiveSeconds()
   {
      var testScheduler = new TestScheduler();
      var oneSecond = TimeSpan.TicksPerSecond;

      var temperatures = testScheduler.CreateHotObservable(
         OnNext(310, 500.0)
      );
      var proximities = testScheduler.CreateHotObservable(
         OnNext(100, Unit.Default),
         OnNext(1 * oneSecond - 1, Unit.Default),
         OnNext(2 * oneSecond - 1, Unit.Default),
         OnNext(3 * oneSecond - 1, Unit.Default),
         OnNext(4 * oneSecond - 1, Unit.Default),
         OnNext(5 * oneSecond - 1, Unit.Default),
         OnNext(6 * oneSecond - 1, Unit.Default)
      );
      var concurrencyProvider = Substitute.For<IConcurrencyProvider>();
      concurrencyProvider.ReturnsForAll<IScheduler>(testScheduler);
      var tempSensor = Substitute.For<ITemperatureSensor>();
      tempSensor.Readings.Returns(temperatures);
      var proxSensor = Substitute.For<IProximitySensor>();
      proxSensor.Readings.Returns(proximities);

      var monitor = new MachineMonitor(concurrencyProvider, tempSensor, proxSensor);

      var res = testScheduler.Start(() => monitor.ObserveAlerts(),
         0,
         0,
         long.MaxValue);

      res.Messages.AssertEqual(
         OnNext(310, (Alert a) => a.Time.Ticks == 310),
         OnNext(6 * oneSecond - 1, (Alert _) => true)
      );
   }
}