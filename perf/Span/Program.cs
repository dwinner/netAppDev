using System;
using System.Runtime.InteropServices;

namespace Span
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            byte[] array = new byte[] {0, 1, 2, 3};
            Span<byte> byteSpan = new Span<byte>(array, 1, 2);
            PrintSpan(byteSpan);

            unsafe
            {
                int* stackMem = stackalloc int[4];
                Span<int> intSpan = new Span<int>(stackMem, 4);
                for (int i=0;i<intSpan.Length;i++)
                {
                    intSpan[i] = 13 + i;
                }
                PrintSpan(intSpan);
            }

            unsafe
            {
                int memSize = sizeof(int) * 4;
                IntPtr hNative = Marshal.AllocHGlobal(memSize);
                Span<int> unmanagedSpan = new Span<int>(hNative.ToPointer(), 4);
                for (int i = 0; i < unmanagedSpan.Length; i++)
                {
                    unmanagedSpan[i] = 100 + i;
                }
                PrintSpan(unmanagedSpan);
                Marshal.FreeHGlobal(hNative);
            }
            
            ReadOnlySpan<char> subString = "NonAllocatingSubstring".AsSpan().Slice(13);
            PrintSpan(subString);

        }

        private static void PrintSpan<T>(Span<T> span)
        {
            for (int i = 0; i < span.Length; i++)
            {
                ref T val = ref span[i];
                Console.Write(val);
                if (i < span.Length - 1) { Console.Write(", "); }
            }
            Console.WriteLine();
        }

        private static void PrintSpan<T>(ReadOnlySpan<T> span)
        {
            for (int i = 0; i < span.Length; i++)
            {
                T val = span[i];
                Console.Write(val);
                if (i < span.Length - 1) { Console.Write(", "); }
            }
            Console.WriteLine();
        }
    }
}
