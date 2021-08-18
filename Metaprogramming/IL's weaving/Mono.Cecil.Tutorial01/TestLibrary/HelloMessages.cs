using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestLibrary
{
    public static class HelloMessages
    {
        public static string GetHelloWorld()
        {
            return "Hello World!";
        }

        public static string GetSumMessage(int x, int y)
        {
            var sum = x + y;

            return "The sum of x and y is: " + sum;
        }
    }
}
