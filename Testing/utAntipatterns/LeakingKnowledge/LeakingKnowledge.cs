namespace LeakingKnowledge;

public static class Calculator
{
   public static int Add(int value1, int value2) => value1 + value2;
}

public class CalculatorTests
{
   [Fact]
   public void Adding_two_numbers()
   {
      var value1 = 1;
      var value2 = 3;
      var expected = value1 + value2;

      var actual = Calculator.Add(value1, value2);

      Assert.Equal(expected, actual);
   }
}

public class CalculatorTests2
{
   [Theory]
   [InlineData(1, 3)]
   [InlineData(11, 33)]
   [InlineData(100, 500)]
   public void Adding_two_numbers(int value1, int value2)
   {
      var expected = value1 + value2;

      var actual = Calculator.Add(value1, value2);

      Assert.Equal(expected, actual);
   }
}

public class CalculatorTests4
{
   [Theory]
   [InlineData(1, 3, 4)]
   [InlineData(11, 33, 44)]
   [InlineData(100, 500, 600)]
   public void Adding_two_numbers(int value1, int value2, int expected)
   {
      var actual = Calculator.Add(value1, value2);
      Assert.Equal(expected, actual);
   }
}