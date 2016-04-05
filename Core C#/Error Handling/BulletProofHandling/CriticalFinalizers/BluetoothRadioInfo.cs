using System.Runtime.InteropServices;

namespace _09_CriticalFinalizers
{
   //
   // Соответствует BLUETOOTH_RADIO_INFO из Win32
   //
   [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
   struct BluetoothRadioInfo
   {
      private const int BluetoothMaxNameSize = 248;
      public uint dwSize;
      public readonly ulong address;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = BluetoothMaxNameSize)]
      public readonly string szName;
      public readonly uint ulClassOfDevice;
      public readonly ushort lmpSubversion;
      public readonly ushort manufacturer;
   }
}
