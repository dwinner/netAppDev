namespace DeliveryServiceTests;

public class DeliveryServiceTests
{
   [InlineData(-1, false)]
   [InlineData(0, false)]
   [InlineData(1, false)]
   [InlineData(2, true)]
   [Theory]
   public void Detects_an_invalid_delivery_date(int daysFromNow, bool expected)
   {
      var sut = new DeliveryService();
      var deliveryDate = DateTime.Now.AddDays(daysFromNow);
      var delivery = new Delivery
      {
         Date = deliveryDate
      };

      var isValid = sut.IsDeliveryValid(delivery);

      Assert.Equal(expected, isValid);
   }

   [InlineData(-1)]
   [InlineData(0)]
   [InlineData(1)]
   [Theory]
   public void Detects_an_invalid_delivery_date2(int daysFromNow)
   {
      var sut = new DeliveryService();
      var deliveryDate = DateTime.Now.AddDays(daysFromNow);
      var delivery = new Delivery
      {
         Date = deliveryDate
      };

      var isValid = sut.IsDeliveryValid(delivery);

      Assert.False(isValid);
   }

   [Fact]
   public void The_soonest_delivery_date_is_two_days_from_now()
   {
      var sut = new DeliveryService();
      var deliveryDate = DateTime.Now.AddDays(2);
      var delivery = new Delivery
      {
         Date = deliveryDate
      };

      var isValid = sut.IsDeliveryValid(delivery);

      Assert.True(isValid);
   }

   [Theory]
   [MemberData(nameof(Data))]
   public void Detects_an_invalid_delivery_date3(
      DateTime deliveryDate,
      bool expected)
   {
      var sut = new DeliveryService();
      var delivery = new Delivery
      {
         Date = deliveryDate
      };

      var isValid = sut.IsDeliveryValid(delivery);

      Assert.Equal(expected, isValid);
   }

   public static List<object[]> Data() =>
      new()
      {
         new object[] { DateTime.Now.AddDays(-1), false },
         new object[] { DateTime.Now, false },
         new object[] { DateTime.Now.AddDays(1), false },
         new object[] { DateTime.Now.AddDays(2), true }
      };
}