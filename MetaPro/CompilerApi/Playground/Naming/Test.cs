using System;

namespace Naming
{
    public class Test
    {
        public void MethodA(int aR, int aReport, int anIsFool)
        {
            Console.WriteLine($"{aR} {aReport}");
            if (anIsFool == 1)
            {
                Console.WriteLine(anIsFool);
            }
        }

        public void MethodB(int anA, int aB, int aC)
        {
            Console.WriteLine(anA);
            Console.WriteLine(aB);
            Console.WriteLine(aC);
        }

        public void MethodC(string aComment, string aResume, string anOrig)
        {
            Console.WriteLine(aComment);
            Console.WriteLine(aResume);
            Console.WriteLine(anOrig);
        }
    }
}
