namespace CustomersClient
{
   internal static class Program
   {
      //private const int Interations = 20000;

      private static void Main()
      {
         //Program.SimpleTest();
         //Console.Out.WriteLine();
         //Program.InjectionTest();
         //Console.Out.WriteLine();
         //Program.TimeTest();
      }

      //{

      //private static void InjectionTest()
      //	RunToString(new CustomerDependencyInjected(new ToStringReflectionEmitBuilder())
      //	{
      //		FirstName = "Jason",
      //		LastName = "InjectedReflectionEmit",
      //		Age = 30
      //	});

      //	Program.RunToString(new CustomerDependencyInjected(
      //		new ToStringDynamicMethodBuilder())
      //	{
      //		FirstName = "Jason",
      //		LastName = "InjectedDynamicMethod",
      //		Age = 30
      //	});
      //}

      //private static void SimpleTest()
      //{
      //	RunToString(new CustomerHardCoded()
      //	{
      //		FirstName = "Jason",
      //		LastName = "HardCoded",
      //		Age = 10
      //	});

      //	RunToString(new CustomerReflectionEmit()
      //	{
      //		FirstName = "Jason",
      //		LastName = "ReflectionEmit",
      //		Age = 25
      //	});

      //	try
      //	{
      //		RunToString(new CustomerReflectionEmitWithVerification()
      //		{
      //			FirstName = "Jason",
      //			LastName = "ReflectionEmitWithVerification",
      //			Age = 26
      //		});
      //	}
      //	catch(VerificationException e)
      //	{
      //		foreach(var error in e.Errors)
      //		{
      //			Console.Out.WriteLine(error.Description);
      //		}
      //	}

      //	RunToString(new CustomerReflectionEmitWithDebugging()
      //	{
      //		FirstName = "Jason",
      //		LastName = "ReflectionEmitWithDebugging",
      //		Age = 28
      //	});

      //	RunToString(new CustomerDynamicMethod()
      //	{
      //		FirstName = "Jason",
      //		LastName = "DynamicMethod",
      //		Age = 37
      //	});
      //}

      //private static void TimeTest()
      //{
      //	Program.TimeToString<CustomerHardCoded>();
      //	Program.TimeToString<CustomerReflection>();
      //	Program.TimeToString<CustomerReflectionEmit>();
      //	Program.TimeToString<CustomerReflectionEmitWithDebugging>();
      //	Program.TimeToString<CustomerDynamicMethod>();
      //}

      //private static void TimeToString<T>() where T : ICustomer, new()
      //{
      //	for(int i = 0; i < 100; i++)
      //	{
      //		var customer = new T()
      //		{
      //			FirstName = "Jason",
      //			LastName = "Timing"
      //		};

      //		var toString = customer.ToString();
      //	}

      //	var watch = Stopwatch.StartNew();

      //	for(int i = 0; i < Program.Interations; i++)
      //	{
      //		var customer = new T()
      //		{
      //			FirstName = "Jason",
      //			LastName = "Timing"
      //		};

      //		var toString = customer.ToString();
      //	}

      //	watch.Stop();

      //	Console.Out.WriteLine("Type: " + typeof(T).Name +
      //		", Time: " + watch.Elapsed.ToString());
      //}

      //private static void RunToString(ICustomer customer)
      //{
      //	Console.Out.WriteLine(customer.ToString());
      //}
   }
}