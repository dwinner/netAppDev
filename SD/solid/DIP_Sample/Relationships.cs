namespace DIP_Sample;

public class Relationships : IRelationshipBrowser // low-level
{
   public List<(Person, RelationShip, Person)> Relations { get; } = [];

   public IEnumerable<Person> FindAllChildrenOf(string name) =>
      Relations
         .Where(x => x.Item1.Name == name
                     && x.Item2 == RelationShip.Parent)
         .Select(r => r.Item3);

   public void AddParentAndChild(Person parent, Person child)
   {
      Relations.Add((parent, RelationShip.Parent, child));
      Relations.Add((child, RelationShip.Child, parent));
   }
}