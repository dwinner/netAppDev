/**
 * C#10 features
 */

// With global using there is no point in adding it everythere in the project files
global using System.Runtime.CompilerServices;

using CSharp10Features.ArgumentExpressions;
using CSharp10Features.InterpolatedStringHandlers;
using Guide;

RecordStructSample();
InterpolatedStringHandlerSample();
ArgumentExprSample();

void ArgumentExprSample()
{
   try
   {
      Action? func = null;
      Validation.ValidateArgument(nameof(func), func is not null);
   }
   catch (ArgumentException argEx)
   {
      Console.WriteLine(argEx.Message);
   }

   try
   {
      Enumerable.Range(0, 10).Sample(100);
   }
   catch (ArgumentException argEx)
   {
      Console.WriteLine(argEx.Message);
   }
}

// not the property pattern, but closer
if (Environment.OSVersion.Platform is PlatformID.Win32NT)
{
   Console.WriteLine("Windows NT");
}

void InterpolatedStringHandlerSample()
{
   var logger = new Logger() { EnabledLevel = LogLevel.Warning };
   var time = DateTime.Now;

   logger.LogMessage(LogLevel.Error,
      $"Error Level. CurrentTime: {time}. The time doesn't use formatting.");
   logger.LogMessage(LogLevel.Error,
      $"Error Level. CurrentTime: {time:t}. This is an error. It will be printed.");
   logger.LogMessage(LogLevel.Trace,
      $"Trace Level. CurrentTime: {time:t}. This won't be printed.");
   logger.LogMessage(LogLevel.Warning,
      "Warning Level. This warning is a string, not an interpolated string expression.");
}

void RecordStructSample()
{
   // Now the struct can have parameterless ctor and can even be a record in combination
   PointRecord pointRecord = new();
   Console.WriteLine(pointRecord);

   PointRecord pointRecord2 = new(1, 1, 1, PointType.TwoD);
   Console.WriteLine(pointRecord2);
}

