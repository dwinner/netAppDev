using MiscThings.Geometry;
using static System.StringSplitOptions;

namespace MiscThings.Tests.Geometry;

public class PointsDistanceBaseTests
{
   private const string RsUrl = "https://algs4.cs.princeton.edu/99hull/rs1423.txt";
   private const string KwUrl = "https://algs4.cs.princeton.edu/99hull/kw1260.txt";
   private IReadOnlyList<Point2D> _kwOriginal = null!;
   private IReadOnlyList<Point2D> _rsOriginal = null!;
   protected Point2D[] KwArray = null!;
   protected Point2D[] RsArray = null!;

   [OneTimeSetUp]
   public async Task InitAsync()
   {
      _rsOriginal = (await GetPointsAsync(RsUrl).ConfigureAwait(false)).AsReadOnly();
      _kwOriginal = (await GetPointsAsync(KwUrl).ConfigureAwait(false)).AsReadOnly();
   }

   [SetUp]
   public void SetUp()
   {
      RsArray = new Point2D[_rsOriginal.Count];
      for (var i = 0; i < _rsOriginal.Count; i++)
      {
         RsArray[i] = _rsOriginal[i];
      }

      KwArray = new Point2D[_kwOriginal.Count];
      for (var i = 0; i < _kwOriginal.Count; i++)
      {
         KwArray[i] = _kwOriginal[i];
      }
   }

   private static async Task<Point2D[]> GetPointsAsync(string url)
   {
      var content = await GetContentAsync(url).ConfigureAwait(false);
      var lines = content.Split(new[] { '\n' }, TrimEntries | RemoveEmptyEntries);
      var arrayLen = int.Parse(lines[0]);
      var points = new Point2D[arrayLen];
      for (var i = 0; i < arrayLen; i++)
      {
         var currentLine = lines[i + 1];
         var pointParts = currentLine.Split(new[] { ' ' }, RemoveEmptyEntries);
         var (x, y) = (int.Parse(pointParts[0]), int.Parse(pointParts[1]));
         var point = new Point2D(x, y);
         points[i] = point;
      }

      return points;
   }

   private static async Task<string> GetContentAsync(string uriString)
   {
      using var httpClient = new HttpClient();
      var content = await httpClient.GetStringAsync(uriString)
         .ConfigureAwait(false);
      return content;
   }
}