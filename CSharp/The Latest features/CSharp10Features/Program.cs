/**
 * C#10 features
 */

RecordStructSample();

void RecordStructSample()
{
   PointRecord pointRecord = new (0, 0, 0);
   Console.WriteLine(pointRecord);
}
