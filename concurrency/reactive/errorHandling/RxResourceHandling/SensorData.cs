namespace RxResourceHandling;

internal class SensorData(long data)
{
   public long Data { get; set; } = data;

   public override string ToString() => $"SensorData: {Data}";
}