using DataStructures.UF;
using MoreLinq;

namespace Adt.Tests;

[TestFixture]
public class UnionFindTests
{
   [OneTimeSetUp]
   public void Init()
   {
      for (var i = 0; i < NodeCount; i++)
      {
         var first = Random.Next(default, ComponentCount - 1);
         var second = Random.Next(default, ComponentCount - 1);
         ComponentSet.Add((first, second));
      }
   }

   private static readonly object[] UnionImpls =
   {
      new UnionFind(ComponentCount),
      new WeightedQuickUnionFind(ComponentCount),
      new QuickUnionFind(ComponentCount),
      new QuickUnion(ComponentCount)
   };

   private static readonly Random Random = new();
   private static readonly HashSet<(int idx1, int idx2)> ComponentSet = new(EqualityComparer<(int, int)>.Default);
   private const int ComponentCount = 200;
   private const int NodeCount = 500;

   [TestCaseSource(nameof(UnionImpls))]
   public void UnionFindImplTest(IUnionFind unionFindImpl)
   {
      ComponentSet.ForEach(cmpPair =>
      {
         var (idx1, idx2) = cmpPair;
         if (!unionFindImpl.Connected(idx1, idx2))
         {
            unionFindImpl.Union(idx1, idx2);
            //Console.WriteLine("{0} {1}", idx1, idx2);
         }
      });

      Console.WriteLine("{0} components", unionFindImpl.Count);
   }
}