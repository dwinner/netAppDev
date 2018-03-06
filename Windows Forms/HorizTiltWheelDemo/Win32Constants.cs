namespace HorizTiltWheelDemo
{
   internal static class Win32Constants
   {
      //taken from winuser.h in the Platform SDK
      public const int MkLbutton = 0x0001;
      public const int MkRbutton = 0x0002;
      // public const int MkShift = 0x0004;
      // public const int MkControl = 0x0008;
      public const int MkMbutton = 0x0010;
      public const int MkXbutton1 = 0x0020;
      public const int MkXbutton2 = 0x0040;
      public const int WheelDelta = 120;      
      public const int SpiGetwheelscrollchars = 0x006C;
   }
}