namespace QueueSample
{
   public interface IDocumentManager<T>
   {
      void AddDocument(T document);

      T GetDocument();

      bool IsDocumentAvailable { get; }
   }
}