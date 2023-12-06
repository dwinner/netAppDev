namespace CalculatorTests;

public class Calculator1Tests
{
   [Fact]
   public void Sum_of_two_numbers()
   {
      // Arrange
      double first = 10;
      double second = 20;
      var calculator = new Calculator1();

      // Act
      var result = calculator.Sum(first, second);

      // Assert
      Assert.Equal(30, result);
   }
}