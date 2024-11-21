using System;

namespace StackAlloc
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter size to stackalloc ('q' to exit): ");
                string input = Console.ReadLine();
                if (input.Equals("q", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                                
                int size = 16;
                if (int.TryParse(input, out size))
                {
                    try
                    {
                        DoStackAlloc(size);

                        Console.WriteLine($"Allocated {size}-size array");
                    }
                    catch(StackOverflowException ex)
                    {
                        Console.WriteLine("Caught SOE!");
                    }
                }                
            }
        }

        private static unsafe void DoStackAlloc(int size)
        {
            int* buffer = stackalloc int[size];
            for (int i = 0; i < size; i++)
            {
                buffer[i] = i;
            }
        }
    }
}
