namespace DIP_Sample;

internal class Research
{
   private readonly IRelationshipBrowser _browser;

   internal Research(IRelationshipBrowser browser) => _browser = browser;

   internal void Search(string name = "John")
   {
      foreach (var p in _browser.FindAllChildrenOf(name))
      {
         Console.WriteLine($"John has a child called {p.Name}");
      }
   }
}