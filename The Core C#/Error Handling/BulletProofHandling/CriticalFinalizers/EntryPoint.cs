/**
 * Критичные финализаторы
 */

using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace _09_CriticalFinalizers
{
   public static class EntryPoint
   {
      private const int ErrorSuccess = 0;

      private static void Main()
      {
         SafeFileHandle radioHandle;
         using (var radioFindHandle =
            BluetoothFindFirstRadio(new BluetoothFindRadioParams(), out radioHandle))
         {
            if (!radioFindHandle.IsInvalid)
            {
               var radioInfo = new BluetoothRadioInfo { dwSize = 520 };
               var result = BluetoothGetRadioInfo(radioHandle, ref radioInfo);
               if (result == ErrorSuccess)
               {
                  // Вывести информацию на консоль
                  Console.WriteLine("address: {0:X}", radioInfo.address);
                  Console.WriteLine("szName = {0}", radioInfo.szName);
                  Console.WriteLine("ulClassOfDevice = {0}", radioInfo.ulClassOfDevice);
                  Console.WriteLine("lmpSubversion = {0}", radioInfo.lmpSubversion);
                  Console.WriteLine("manufacturer = {0}", radioInfo.manufacturer);
               }
               radioHandle.Dispose();
            }
         }
         Console.ReadKey();
      }

      [DllImport("Irprops.cpl")]
      private static extern SafeBluetoothRadioFindHandle
         BluetoothFindFirstRadio([MarshalAs(UnmanagedType.LPStruct)] BluetoothFindRadioParams pbtfrp,
            out SafeFileHandle phRadio);

      [DllImport("Irprops.cpl")]
      private static extern uint BluetoothGetRadioInfo(SafeFileHandle hRadio, ref BluetoothRadioInfo pRadioInfo);
   }
}