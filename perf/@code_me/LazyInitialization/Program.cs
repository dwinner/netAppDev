using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LazyInitialization
{
    class MyClass
    {
        int[] arr;
        public MyClass(int size)
        {
            this.arr = new int[size];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyClass obj = null;
            LazyInitializer.EnsureInitialized(ref obj, () => new MyClass(99));
        }
    }
}
