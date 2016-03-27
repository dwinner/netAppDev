namespace Builder
{
   /// <summary>
   /// Интерфейс местоположения
   /// </summary>
   public interface ILocation
   {
      string Location { get; set; }
   }

   /// <summary>
   /// Реализация интерфейса ILocation
   /// </summary>
   public class LocationImpl : ILocation
   {
      public string Location { get; set; }

      public LocationImpl(string location)
      {
         Location = location;
      }

      public override string ToString()
      {
         return Location;
      }
   }
}
