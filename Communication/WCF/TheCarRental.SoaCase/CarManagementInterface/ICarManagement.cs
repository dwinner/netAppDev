using System.Collections.Generic;
using System.ServiceModel;

namespace CarManagementInterface
{
   [ServiceContract]
   public interface ICarManagement
   {
      [OperationContract]
      int InsertNewCar(Car car);

      [OperationContract]
      bool RemoveCar(Car car);

      [OperationContract]
      void UpdateMilage(Car car);

      [OperationContract]
      List<Car> ListCars();

      [OperationContract]
      byte[] GetCarPicture(string carId);
   }
}