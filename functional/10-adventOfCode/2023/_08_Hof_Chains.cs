using AdventOfCode.Common;

namespace AdventOfCode._2023;

public class Node
{
   public string Id { get; set; }

   public string Left { get; set; }

   public string Right { get; set; }
}

public class _08_Hof_Chains
{
   private const string TestInputOne = @"RL

AAA = (BBB, CCC)
BBB = (DDD, EEE)
CCC = (ZZZ, GGG)
DDD = (DDD, DDD)
EEE = (EEE, EEE)
GGG = (GGG, GGG)
ZZZ = (ZZZ, ZZZ)";

   private const string TestInputTwo = @"LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)";

   private const string TestInputThree = @"LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)";

   private static IEnumerable<Node> ParseNotes(string input) =>
      input.Split(Environment.NewLine)
         .Skip(2)
         .Select(x => new Node
         {
            Id = x[..3],
            Left = x.Substring(7, 3),
            Right = x.Substring(12, 3)
         });

   private static Func<string, Node> ToNodeLookup(IEnumerable<Node> n) =>
      n.ToDictionary(x => x.Id, x => x)
         .ToLookupWithDefault(default);

   private static IEnumerable<char> ParseInstructions(string input) =>
      input.Split(Environment.NewLine)
         .First()
         .Select(x => x)
         .ToArray()
         .Map(x => new IndefiniteEnumerable<char>(i =>
            x[i % x.Length]));

   private static IEnumerable<Node> FindNodes(IEnumerable<Node> nodes, char identifier) =>
      nodes.Where(x => x.Id.EndsWith(identifier));

   [Fact]
   public void Day08_Test01()
   {
      var nodes = ParseNotes(TestInputOne);
      nodes.Should().BeEquivalentTo(new[]
      {
         new Node
         {
            Id = "AAA",
            Left = "BBB",
            Right = "CCC"
         },
         new Node
         {
            Id = "BBB",
            Left = "DDD",
            Right = "EEE"
         },
         new Node
         {
            Id = "CCC",
            Left = "ZZZ",
            Right = "GGG"
         },
         new Node
         {
            Id = "DDD",
            Left = "DDD",
            Right = "DDD"
         },
         new Node
         {
            Id = "EEE",
            Left = "EEE",
            Right = "EEE"
         },
         new Node
         {
            Id = "GGG",
            Left = "GGG",
            Right = "GGG"
         },
         new Node
         {
            Id = "ZZZ",
            Left = "ZZZ",
            Right = "ZZZ"
         }
      });
   }

   [Fact]
   public void Day08_Test02()
   {
      var iterator = TestInputOne
         .Map(ParseInstructions)
         .Take(10)
         .ToArray();
      iterator.Should().BeEquivalentTo(new[] { 'R', 'L', 'R', 'L', 'R', 'L', 'R', 'L', 'R', 'L' });
   }

   [Fact]
   public void Day08_Test03()
   {
      var network = ParseNotes(TestInputOne).Map(ToNodeLookup);

      var finalIndex = TestInputOne
         .Map(ParseInstructions)
         .Scan("AAA", (currLoc, x) => x switch
         {
            'R' => network(currLoc).Right,
            'L' => network(currLoc).Left
         })
         .FindIndex(x => x == "ZZZ");
      finalIndex.Should().Be(2);
   }

   public int FindStepsFromBeginningToEnd(string input, string startingNode = "AAA", string endingNode = "ZZZ")
   {
      var network = ParseNotes(input).Map(ToNodeLookup);

      var finalIndex = input
         .Map(ParseInstructions)
         .Scan(startingNode, (currLoc, x) => x switch
         {
            'R' => network(currLoc).Right,
            'L' => network(currLoc).Left
         })
         .FindIndex(x => x == endingNode);
      return finalIndex;
   }

   [Fact]
   public void Day08_Test04()
   {
      var iterator = TestInputTwo
         .Map(ParseInstructions)
         .Take(10)
         .ToArray();
      iterator.Should().BeEquivalentTo(new[] { 'L', 'L', 'R', 'L', 'L', 'R', 'L', 'L', 'R', 'L' });
   }


   [Fact]
   public void Day08_Test05()
   {
      var network = ParseNotes(TestInputTwo).Map(ToNodeLookup);

      var finalIndex = TestInputTwo
         .Map(ParseInstructions)
         .Scan("AAA", (currLoc, x) => x switch
         {
            'R' => network(currLoc).Right,
            'L' => network(currLoc).Left
         })
         .FindIndex(x => x == "ZZZ");
      finalIndex.Should().Be(6);
   }

   [Fact]
   public void Day08_Part01()
   {
      var input = File.ReadAllText("./2023/Day08Input.txt");

      var finalIndex = FindStepsFromBeginningToEnd(input);

      finalIndex.Should().Be(11309);
   }

   [Fact]
   public void Day08_Test06()
   {
      var network = ParseNotes(TestInputThree).ToArray();
      var networkLookup = ToNodeLookup(network);

      var startingNodes = FindNodes(network, 'A').Select(x => x.Id).ToArray();
      var endingNodes = FindNodes(network, 'Z');

      var finalIndex = TestInputThree
         .Map(ParseInstructions)
         .Scan(startingNodes, (currLoc, x) => currLoc.Select(y => x switch
         {
            'L' => networkLookup(y).Left,
            'R' => networkLookup(y).Right
         }).ToArray()).FindIndex(x => x.All(y => y.EndsWith('Z')));

      finalIndex.Should().Be(6);
   }

   [Fact]
   public void Day08_Part02()
   {
      var input = File.ReadAllText("./2023/Day08Input.txt");

      var network = ParseNotes(input).ToArray();
      var networkLookup = ToNodeLookup(network);

      var startingNodes = FindNodes(network, 'A').Select(x => x.Id).ToArray();
      var endingNodes = FindNodes(network, 'Z');

      var finalIndex = input
         .Map(ParseInstructions)
         .Scan(startingNodes, (currLoc, x) => currLoc.Select(y => x switch
         {
            'L' => networkLookup(y).Left,
            'R' => networkLookup(y).Right
         }).ToArray()).FindIndex(x => x.All(y => y.EndsWith('Z')));

      finalIndex.Should().Be(6);
   }
}