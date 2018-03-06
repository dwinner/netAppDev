using LiveShaping.Infrastructure;

namespace LiveShaping.Data
{
   public class LapRacerInfo : BindableObject
   {
      private int _lap;
      private int _position;
      private PositionChange _positionChange;
      public Racer Racer { get; set; }

      public int Lap
      {
         get { return _lap; }
         set { SetProperty(ref _lap, value); }
      }

      public int Position
      {
         get { return _position; }
         set { SetProperty(ref _position, value); }
      }

      public PositionChange PositionChange
      {
         get { return _positionChange; }
         set { SetProperty(ref _positionChange, value); }
      }
   }
}