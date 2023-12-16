namespace ConstructorArguments;

public class AClassWithArguments(string thisIsAnArgument)
{
   public virtual void AFakeableMethod()
   {
      Console.Write(thisIsAnArgument);
   }
}