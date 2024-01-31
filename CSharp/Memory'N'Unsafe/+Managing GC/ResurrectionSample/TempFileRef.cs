using System.Collections.Concurrent;

namespace ResurrectionSample;

public class TempFileRef(string filePath)
{
   internal static readonly ConcurrentQueue<TempFileRef> FailedDeletions = new();

   public readonly string FilePath = filePath;

   public Exception? DeletionError { get; private set; }

   // int _deleteAttempt; // Uncomment if re-registering the finalizer

   ~TempFileRef()
   {
      try
      {
         File.Delete(FilePath);
      }
      catch (Exception ex)
      {
         DeletionError = ex;
         FailedDeletions.Enqueue(this); // Resurrection
         // We can re-register for finalization by uncommenting:
         // if (_deleteAttempt++ < 3) GC.ReRegisterForFinalize (this);
      }
   }
}