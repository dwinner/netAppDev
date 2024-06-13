namespace DIP_Sample;

public interface IRelationshipBrowser
{
   IEnumerable<Person> FindAllChildrenOf(string name);
}