namespace CalculatorTests;

public class Calculator2Tests : IDisposable
{
   private readonly Calculator1 _calculator1 = new();

   public void Dispose()
   {
      _calculator1.CleanUp();
   }

   [Fact]
   public void Sum_of_two_numbers()
   {
      // Arrange
      double first = 10;
      double second = 20;

      // Act
      var result = _calculator1.Sum(first, second);

      // Assert
      Assert.Equal(30, result);
   }
}