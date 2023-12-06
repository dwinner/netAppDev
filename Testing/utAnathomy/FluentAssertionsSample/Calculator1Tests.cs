using FluentAssertions;

namespace FluentAssertionsSample;

public class Calculator1Tests
{
   [Fact]
   public void Sum_of_two_numbers()
   {
      double first = 10;
      double second = 20;
      var sut = new Calculator1();

      var result = sut.Sum(first, second);

      result.Should().Be(30);
   }
}