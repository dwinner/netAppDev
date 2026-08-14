using System.Runtime.InteropServices;

namespace SharedMem_Client;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct MySharedData
{
   public int Value;
   public char Letter;
   public fixed float Numbers[50];
}