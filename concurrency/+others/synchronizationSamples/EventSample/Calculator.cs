﻿using System;
using System.Threading;
using System.Threading.Tasks;

public class Calculator
{
   private readonly ManualResetEventSlim _mEvent;

   public Calculator(ManualResetEventSlim ev) => _mEvent = ev;

   public int Result { get; private set; }

   public void Calculation(int x, int y)
   {
      Console.WriteLine($"Task {Task.CurrentId} starts calculation");
      Task.Delay(new Random().Next(3000)).Wait();
      Result = x + y;

      // signal the event-completed!
      Console.WriteLine($"Task {Task.CurrentId} is ready");
      _mEvent.Set();
   }
}