namespace FunctionalBasedArc;

public class ApplicationService
{
   private readonly AuditManager _auditManager;
   private readonly string _directoryName;
   private readonly Persister _persister;

   public ApplicationService(string directoryName, int maxEntriesPerFile)
   {
      _directoryName = directoryName;
      _auditManager = new AuditManager(maxEntriesPerFile);
      _persister = new Persister();
   }

   public void AddRecord(string visitorName, DateTime timeOfVisit)
   {
      var files = _persister.ReadDirectory(_directoryName);
      var update = _auditManager.AddRecord(
         files, visitorName, timeOfVisit);
      _persister.ApplyUpdate(_directoryName, update);
   }
}