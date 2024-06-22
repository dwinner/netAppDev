namespace AcyclicVisitor;

public interface IVisitor<in TVisitable>
{
   void Visit(TVisitable aVisitable);
}

// marker interface
public interface IVisitor
{
}