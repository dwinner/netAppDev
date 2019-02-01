namespace Builder
{
   /// <summary>
   ///    Интерфейс контактов.
   /// </summary>
   public interface IContact
   {
      string FirstName { get; set; }
      string LastName { get; set; }
      string Title { get; set; }
      string Organization { get; set; }
   }
}