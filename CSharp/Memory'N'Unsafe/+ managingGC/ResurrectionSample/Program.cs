namespace ResurrectionSample;

internal static class Program
{
   private const string Filename = "tempref.tmp";

   private static void Main()
   {
      // Create and open file so it cannot be deleted
      var writer = File.CreateText(Filename);

      // Get the temporary reference in a separate method.
      // Variable will go out of scope upon return and be eligible for GC.
      CreateTempFileRef();

      GC.Collect(); // Run the garbage collector

      var tempFileRefs = TempFileRef.FailedDeletions;
      foreach (var tempFileRef in tempFileRefs)
      {
         Console.WriteLine(tempFileRef);
      }
   }

   private static void CreateTempFileRef()
   {
      var tempRef = new TempFileRef(Filename);
   }
}