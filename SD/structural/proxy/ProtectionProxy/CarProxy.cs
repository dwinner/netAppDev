namespace ProtectionProxy;

public class CarProxy(Driver driver) : ICar
{
   private readonly Car _car = new();

   public void Drive()
   {
      if (driver.Age >= 16)
      {
         _car.Drive();
      }
      else
      {
         Console.WriteLine("Driver too young");
      }
   }
}