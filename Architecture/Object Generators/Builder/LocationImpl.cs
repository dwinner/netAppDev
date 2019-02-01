namespace Builder
{
   /// <summary>
   ///    Реализация интерфейса ILocation
   /// </summary>
   public class LocationImpl : ILocation
   {
      public LocationImpl(string location)
      {
         Location = location;
      }

      public string Location { get; set; }

      public override string ToString() => Location;
   }
}