namespace Adapter.Canonical
{
   public sealed class ObjectAdapter : Target
   {
      private readonly Adaptee _adaptee = new Adaptee();

      public override void Request()
         => _adaptee.SpecificRequest();
   }
}