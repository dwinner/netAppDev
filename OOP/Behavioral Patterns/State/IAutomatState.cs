namespace State
{
   public interface IAutomatState
   {
      string GotApplication();

      string CheckApplication();

      string RentAppartment();

      string DispenseKeys();
   }
}