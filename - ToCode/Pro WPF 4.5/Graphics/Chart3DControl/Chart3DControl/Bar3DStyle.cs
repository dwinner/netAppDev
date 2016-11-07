

namespace Chart3DControl
{
   public class Bar3DStyle : DataSeriesSurface
   {
      private double xLength = 0.5;
      private double yLength = 0.5;
      private double zOrigin = 0;
      private bool isBarSingleColor = true;

      public bool IsBarSingleColor
      {
         get { return isBarSingleColor; }
         set { isBarSingleColor = value; }
      }

      public double ZOrigin
      {
         get { return zOrigin; }
         set { zOrigin = value; }
      }

      public double YLength
      {
         get { return yLength; }
         set { yLength = value; }
      }

      public double XLength
      {
         get { return xLength; }
         set { xLength = value; }
      }
   }
}
