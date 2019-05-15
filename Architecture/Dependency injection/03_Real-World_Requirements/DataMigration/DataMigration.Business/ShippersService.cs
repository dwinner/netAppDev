namespace DataMigration.Business
{
   public class ShippersService
   {
      private readonly IShippersRepository _sourceRepository;
      private readonly IShippersRepository _targetRepository;

      public ShippersService(
         [Source] IShippersRepository sourceRepository,
         [Target] IShippersRepository targetRepository)
      {
         _sourceRepository = sourceRepository;
         _targetRepository = targetRepository;
      }

      // NOTE: commented to avoid service locator
      //public ShippersService(IShippersRepository sourceRepository, IShippersRepository targetRepository)
      //{
      //    this.sourceRepository = sourceRepository;
      //    this.targetRepository = targetRepository;
      //}

      public void MigrateShippers()
      {
         foreach (var shipper in _sourceRepository.GetShippers())
         {
            _targetRepository.AddShipper(shipper);
         }
      }
   }
}