using static System.Console;

//WriteLine("Exercises 6.4 Refactored.");

WriteLine("Executing the refactored version:");
new JobService2().Execute();

internal record class ComplexJob
{
   public bool partOneFinished, partTwoFinished;

   public ComplexJob(bool partOne = false, bool partTwo = false)
   {
      partOneFinished = partOne;
      partTwoFinished = partTwo;
   }
}

internal record class JobService2
{
   private ComplexJob Initiate() => new();

   private ComplexJob ExecutePartOne(ComplexJob job) =>
      // Some code to finish Part 1       
      job with { partOneFinished = true };

   private ComplexJob ExecutePartTwo(ComplexJob job) =>
      // Part 2 can be executed only after Part 1.
      job with { partTwoFinished = true };

   private void PrintStatus(ComplexJob job)
   {
      var Status = job.partOneFinished && job.partTwoFinished
         ? "The process is completed successfully."
         : "Process is incomplete.";
      WriteLine(Status);
   }

   public void Execute()
   {
      // This calling sequence is OK
      var job = Initiate();
      var afterPartOne = ExecutePartOne(job);
      var afterPartTwo = ExecutePartTwo(afterPartOne);
      PrintStatus(afterPartTwo);

      //// This calling sequence will cause a compile-time error.
      //ComplexJob afterPartOne = ExecutePartOne(job);
      //ComplexJob job = Initiate();
      //ComplexJob afterPartTwo = ExecutePartTwo(afterPartOne);
      //PrintStatus(afterPartTwo);
   }
}