﻿using AdventOfCode.Common;

namespace AdventOfCode._2023;

public record Race
{
   public long Time { get; set; }
   public long Distance { get; set; }
}

public class _06_Hof_Day
{
   private readonly string _realInput = @"Time:        55     99     97     93
Distance:   401   1485   2274   1405";

   private readonly string _testInput = @"Time:      7  15   30
Distance:  9  40  200";

   public IEnumerable<Race> ParseRaces(string input)
   {
      var lines = input.Split("\r\n");
      var times = lines[0].Replace("Time: ", "").Trim()
         .Split(" ", StringSplitOptions.RemoveEmptyEntries)
         .Select(long.Parse);

      var distances = lines[1].Replace("Distance: ", "").Trim()
         .Split(" ", StringSplitOptions.RemoveEmptyEntries)
         .Select(long.Parse);

      var races = times.Zip(distances)
         .Select(x => new Race
         {
            Time = x.First,
            Distance = x.Second
         }).ToArray();

      return races;
   }

   public Race ParseRace(string input)
   {
      var lines = input.Split("\r\n");
      var time = lines[0].Replace("Time: ", string.Empty).Replace(" ", "").ToLong();
      var distance = lines[1].Replace("Distance: ", string.Empty).Replace(" ", "").ToLong();

      return new Race
      {
         Time = time,
         Distance = distance
      };
   }

   public int RaceResult(int chargeTime, int raceDuration) =>
      chargeTime * (raceDuration - chargeTime);

   public decimal FindApexDistance(Race r) =>
      (decimal)r.Time / 2;

   public int FindFirstWin(Race r)
   {
      const int a = -1;
      var b = r.Time;
      var c = r.Distance * -1;

      var answer = (b * -1 + Math.Sqrt(b * b - 4 * a * c)) / (2 * a);

      return (int)Math.Ceiling(answer);
   }

   [Fact]
   public void Day06_Test01()
   {
      var races = ParseRaces(_testInput);
      races.Should().BeEquivalentTo(new[]
      {
         new Race
         {
            Time = 7,
            Distance = 9
         },
         new Race
         {
            Time = 15,
            Distance = 40
         },
         new Race
         {
            Time = 30,
            Distance = 200
         }
      });
   }

   public long CalculatePartOneAnswer(string input) =>
      ParseRaces(input)
         .Select(NumberOfWaysToWinV2)
         .Product();

   public long CalculatePartTwoAnswer(string input) =>
      ParseRace(input)
         .Map(NumberOfWaysToWinV2);

   public long NumberOfWaysToWinV2(Race race)
   {
      var firstWin = FindFirstWin(race);
      var apex = FindApexDistance(race);

      var single = apex == (int)apex;
      var answer =
         single
            ? (apex - firstWin) * 2 - 1
            : (Math.Ceiling(apex) - firstWin) * 2;


      return (long)answer;
   }


   [Theory]
   [InlineData(0, 0)]
   [InlineData(1, 6)]
   [InlineData(2, 10)]
   [InlineData(3, 12)]
   [InlineData(4, 12)]
   [InlineData(5, 10)]
   [InlineData(6, 6)]
   [InlineData(7, 0)]
   public void Day07_Test02(int chargeTime, int expectedResult) =>
      RaceResult(chargeTime, 7).Should().Be(expectedResult);

   [Fact]
   public void Day07_Test03()
   {
      var races = ParseRaces(_testInput);
      var results = races.Select(NumberOfWaysToWinV2);
      results.Should().BeEquivalentTo(new[] { 4, 8, 9 });
   }


   [Fact]
   public void Day07_Test04() =>
      CalculatePartOneAnswer(_testInput).Should().Be(288);

   [Fact]
   public void Day07_Part01() =>
      CalculatePartOneAnswer(_realInput).Should().Be(2374848);

   [Fact]
   public void Day07_Test05() =>
      ParseRace(_testInput).Should().BeEquivalentTo(new Race
      {
         Time = 71530,
         Distance = 940200
      });

   [Fact]
   public void Day07_Test06() =>
      ParseRaces(_testInput).Select(FindApexDistance)
         .Should().BeEquivalentTo(new[] { 3.5, 7.5, 15 });

   [Fact]
   public void Day07_Test07() =>
      ParseRaces(_testInput).Select(FindFirstWin)
         .Should().BeEquivalentTo(new[] { 2, 4, 10 });

   [Fact]
   public void Day07_Test08()
   {
      var races = ParseRaces(_testInput);
      var results = races.Select(NumberOfWaysToWinV2);
      results.Should().BeEquivalentTo(new[] { 4, 8, 9 });
   }


   [Fact]
   public void Day07_Part02() =>
      CalculatePartTwoAnswer(_realInput).Should().Be(39132886);
}