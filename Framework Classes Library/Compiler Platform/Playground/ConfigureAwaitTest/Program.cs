using System;
using System.Threading.Tasks;

namespace ConfigureAwaitTest
{
    internal class Program
    {
        private int _first;
        private int _second;
        private int _third;

        public string Name { get; set; }

        static void Main(string[] args)
        {
        }
    }

    // TODO: Add references to the analyzers in the Analyzers section in Solution Explorer after compilation of the solution.

    internal class Person
    {
        int _age;
        string _name;
    }

    internal partial class Foo
    {
        void If(int x)
        {
            if (x > 0)
                Console.WriteLine(x);
            else if (x == 0)
                Console.WriteLine(0);
            else if (x < 0)
                Console.WriteLine(-x);
        }

        void For()
        {
            for (var i = 0; i < 10; i++)
                Console.WriteLine(i);
        }

        void ForEach()
        {
            foreach (var x in new[] { 1, 2, 3 })
                Console.WriteLine(x);
        }

        void While()
        {
            while (true)
                Console.Write('.');
        }
    }

    internal partial class Foo
    {
        // NOTE: These methods will only trigger ConfigureAwait requirement violations if the project is compiled as a class library.
        //       Tweak the project settings accordingly after adding references to the compiled analyzers.

        async Task Bar(Task t)
        {
            await t;
        }

        async Task<int> Baz(Task<int> t)
        {
            return await t;
        }

        async Task Qux(Task t)
        {
            await t.ConfigureAwait(false);
        }
    }
}