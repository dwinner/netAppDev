namespace VisitorViaDLR
{
   public abstract class PersonVisitor<T>
   {
      public T DynamicVisit(Person aPerson) => Visit((dynamic) aPerson);

      protected abstract T Visit(Person aPerson);

      protected virtual T Visit(Customer aCustomer) => Visit((Person) aCustomer);

      protected virtual T Visit(Employee anEmployee) => Visit((Person) anEmployee);
   }
}