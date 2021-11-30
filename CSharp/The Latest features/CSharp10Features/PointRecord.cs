namespace Guide;

public readonly record struct PointRecord(double X, double Y, double Z, PointType PointType)
{
   public PointRecord()
      : this(default, default, default, default)
   {
   }
}