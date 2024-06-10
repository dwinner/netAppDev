using System.Collections;

namespace TypeSafetyAndPerformance
{
   public class WorkForceEnumerator : IEnumerator
   {
      private readonly IEnumerator _enumerator;

      public WorkForceEnumerator(ArrayList employees) => _enumerator = employees.GetEnumerator();

      // NOTE: Утиная типизация "найдет" именно этот метод
      public Employee Current => (Employee)_enumerator.Current;

      public bool MoveNext() => _enumerator.MoveNext();

      public void Reset()
      {
         _enumerator.Reset();
      }

      object IEnumerator.Current => _enumerator.Current;
   }


   public class WorkForce : IEnumerable
   {
      private readonly ArrayList _employees;

      public WorkForce() => _employees = new ArrayList { new Employee() };

      IEnumerator IEnumerable.GetEnumerator() => new WorkForceEnumerator(_employees);

      public WorkForceEnumerator GetEnumerator() => new WorkForceEnumerator(_employees);
   }
}