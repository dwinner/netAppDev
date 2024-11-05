using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerifyHeap
{
    class Program
    {
        static void Main(string[] args)
        {
            const string HeapVerifyVariable = "COMPLUS_HeapVerify";
            string heapVerify = Environment.GetEnvironmentVariable(HeapVerifyVariable);
            Console.WriteLine($"{HeapVerifyVariable}={heapVerify}");

            while(true)
            {
                const int AllocAmount = 100;
                Console.Write($"AllocAmount = {AllocAmount}, WriteAmount=");
                string input = Console.ReadLine();
                int writeAmount;
                if (int.TryParse(input, out writeAmount))
                {
                    int[] buffer = new int[AllocAmount];
                    unsafe
                    {
                        fixed(int* beginning = buffer)
                        {
                            int* p = beginning;
                            for (int i=0;i<writeAmount;i++)
                            {
                                *p = i;
                                p++;
                            }
                        }
                    }
                    GC.Collect(2);
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }            
        }
    }
}
