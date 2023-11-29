namespace RxResourceHandling;

internal class SensorData
{
   public SensorData(long data) => Data = data;

   public long Data { get; set; }

   public override string ToString() => $"SensorData: {Data}";
}