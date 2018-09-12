using System.Collections;

namespace TypeSafetyAndPerformance
{
   public class WorkForceEnumerator : IEnumerator
   {
      private readonly IEnumerator _enumerator;

      public WorkForceEnumerator(ArrayList employees)
      {
         _enumerator = employees.GetEnumerator();
      }

      public bool MoveNext()
      {
         return _enumerator.MoveNext();
      }

      public void Reset()
      {
         _enumerator.Reset();
      }

      object IEnumerator.Current { get { return _enumerator.Current; } }

      // NOTE: Утиная типизация "найдет" именно этот метод
      public Employee Current { get { return (Employee)_enumerator.Current; } }
   }


   public class WorkForce : IEnumerable
   {
      private readonly ArrayList _employees;

      public WorkForce()
      {
         _employees = new ArrayList { new Employee() };
      }

      public WorkForceEnumerator GetEnumerator()
      {
         return new WorkForceEnumerator(_employees);
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return new WorkForceEnumerator(_employees);
      }
   }
}