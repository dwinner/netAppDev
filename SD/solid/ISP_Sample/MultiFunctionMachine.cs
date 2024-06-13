namespace ISP_Sample;

// ok if you need a multifunction machine
public class MultiFunctionMachine : IMultiFunctionDevice
{
   // compose this out of several modules
   private readonly IPrinter? _printer;
   private readonly IScanner? _scanner;

   public MultiFunctionMachine(IPrinter? printer, IScanner? scanner)
   {
      ArgumentNullException.ThrowIfNull(printer, nameof(printer));
      ArgumentNullException.ThrowIfNull(scanner, nameof(scanner));
      _printer = printer;
      _scanner = scanner;
   }

   public void Print(Document aDocument) => _printer?.Print(aDocument);

   public void Scan(Document aDocument) => _scanner?.Scan(aDocument);
}