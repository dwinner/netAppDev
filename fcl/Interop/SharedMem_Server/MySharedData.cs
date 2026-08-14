using System.Runtime.InteropServices;

namespace SharedMem_Server;

[StructLayout(LayoutKind.Sequential)]
unsafe struct MySharedData
{
   public int Value;
   public char Letter;
   public fixed float Numbers[50];
}