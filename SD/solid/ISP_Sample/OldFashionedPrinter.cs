namespace ISP_Sample;

public class OldFashionedPrinter : IMachine
{
   public void Print(Document d)
   {
      // yep
   }

   public void Fax(Document d)
   {
      throw new NotImplementedException();
   }

   [Obsolete("Not supported", true)]
   public void Scan(Document d)
   {
      throw new NotImplementedException();
   }
}