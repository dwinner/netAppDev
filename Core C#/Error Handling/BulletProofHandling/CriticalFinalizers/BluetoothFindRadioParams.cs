using System.Runtime.InteropServices;

namespace _09_CriticalFinalizers
{
   //
   // Соответствует BLUETOOTH_FIND_RADIO_PARAMS из Win32
   //
   [StructLayout(LayoutKind.Sequential)]
   class BluetoothFindRadioParams
   {
      private uint dwSize;

      public BluetoothFindRadioParams()
      {
         dwSize = 4;
      }
   }
}
