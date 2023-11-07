using System.Text.RegularExpressions;
using AppContext.Collision;

namespace AppContext.Tests;

public partial class CollisionSystemTests
{
   [SetUp]
   public void Setup()
   {
   }

   [TestCase("https://algs4.cs.princeton.edu/61event/diffusion.txt")]
   [TestCase("https://algs4.cs.princeton.edu/61event/diffusion2.txt")]
   [TestCase("https://algs4.cs.princeton.edu/61event/diffusion3.txt")]
   [TestCase("https://algs4.cs.princeton.edu/61event/brownian.txt")]
   [TestCase("https://algs4.cs.princeton.edu/61event/brownian2.txt")]
   [TestCase("https://algs4.cs.princeton.edu/61event/billiards5.txt")]
   [TestCase("https://algs4.cs.princeton.edu/61event/pendulum.txt")]
   public async Task CollisionSystemTest(string input)
   {
      var content = await GetContentAsync(input);
      var lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      var len = int.Parse(lines[0]);
      // Assert.That(lines, Has.Length.EqualTo(len + 1));
      var particles = new Particle[len];
      for (var i = 0; i < len; i++)
      {
         var currentLine = lines[i + 1];
         var words = SplitByWords(currentLine);
         Assert.That(words, Has.Length.EqualTo(9));
         var rx = double.Parse(words[0]);
         var ry = double.Parse(words[1]);
         var vx = double.Parse(words[2]);
         var vy = double.Parse(words[3]);
         var radius = double.Parse(words[4]);
         var mass = double.Parse(words[5]);
         particles[i] = new Particle(rx, ry, vx, vy, radius, mass);
      }

      // create collision system and simulate
      var collisionSystem = new CollisionSystem(particles);
      collisionSystem.ParticleChangedEvent += (_, args) => { Console.WriteLine(args.Particle); };
      collisionSystem.Simulate(10_000);
   }

   private static async Task<string> GetContentAsync(string uriString)
   {
      using var httpClient = new HttpClient();
      var content = await httpClient.GetStringAsync(uriString)
         .ConfigureAwait(false);

      return content;
   }

   private static string[] SplitByWords(string content)
   {
      var wsRegExpr = SpaceRegExpr();
      var stringsToSort = wsRegExpr.Split(content)
         .Select(str => str.Trim())
         .Where(str => !string.IsNullOrEmpty(str))
         .ToArray();

      return stringsToSort;
   }

   [GeneratedRegex("\\s")]
   private static partial Regex SpaceRegExpr();
}