using Xunit;

namespace UnitTestingSamples.Tests;

public class DeepThoughtTests
{
   [Fact]
   public void ResultOfTheAnswerToTheUltimateQuestionOfLifeTheUniverseAndEverything()
   {
      // arrange
      var expected = 42;
      var dt = new DeepThought();

      // act
      var actual =
         dt.TheAnswerToTheUltimateQuestionOfLifeTheUniverseAndEverything();

      // assert
      Assert.Equal(expected, actual);
   }
}