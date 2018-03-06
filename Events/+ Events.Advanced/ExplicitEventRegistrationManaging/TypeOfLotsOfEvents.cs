using System;

namespace ExplicitEventRegistrationManaging
{
   public class TypeOfLotsOfEvents
   {
      private readonly EventSet _eventSet = new EventSet();

      protected EventSet EventSet { get { return _eventSet; } }

      protected static readonly EventKey FooEventKey = new EventKey();

      public event EventHandler<FooEventArgs> Foo
      {
         add { _eventSet.Add(FooEventKey, value); }
         remove { _eventSet.Remove(FooEventKey, value); }
      }

      protected virtual void OnFoo(FooEventArgs e)
      {
         _eventSet.Raise(FooEventKey, this, e);
      }

      public void SimulateFoo()
      {
         OnFoo(new FooEventArgs());
      }
   }
}