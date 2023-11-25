namespace BasicAggregateOperators
{
   internal class StudentGrade
   {
      public string Id { get; set; }
      public string Name { get; set; }
      public double Grade { get; set; }

      public override string ToString() => $"Id: {Id}, Name: {Name}, Grade: {Grade}";
   }
}