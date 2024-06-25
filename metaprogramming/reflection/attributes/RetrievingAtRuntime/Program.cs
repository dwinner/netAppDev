// See https://aka.ms/new-console-template for more information

internal class Program
{
   public static void Main(string[] args)
   {
      foreach (var mi in typeof(Foo).GetMethods())
      {
         var att = (TestAttribute)Attribute.GetCustomAttribute(mi, typeof(TestAttribute));
         if (att != null)
         {
            for (var i = 0; i < att.Repetitions; i++)
            {
               try
               {
                  mi.Invoke(new Foo(), null); // Call method with no arguments
                  var dump = $"Successfully called {mi.Name}";
                  Console.WriteLine(dump);
               }
               catch (Exception ex) // Wrap exception in att.FailureMessage
               {
                  throw new Exception("Error: " + att.FailureMessage, ex);
               }
            }
         }
      }
   }
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class TestAttribute : Attribute
{
   public string FailureMessage;
   public int Repetitions;

   public TestAttribute() : this(1)
   {
   }

   public TestAttribute(int repetitions) => Repetitions = repetitions;
}

internal class Foo
{
   [Test]
   public void Method1()
   {
   }

   [Test(20)]
   public void Method2()
   {
   }

   [Test(20, FailureMessage = "Debugging Time!")]
   public void Method3()
   {
   }
}