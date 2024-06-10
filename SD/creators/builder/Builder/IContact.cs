namespace Builder
{
   /// <summary>
   ///    Èíòåðôåéñ êîíòàêòîâ.
   /// </summary>
   public interface IContact
   {
      string FirstName { get; set; }
      string LastName { get; set; }
      string Title { get; set; }
      string Organization { get; set; }
   }
}