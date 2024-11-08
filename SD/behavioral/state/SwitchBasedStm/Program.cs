﻿using System.Text;

namespace SwitchBasedStm;

internal static class Program
{
   private static void Main()
   {
      var code = "1234";
      var state = State.Locked;
      var entry = new StringBuilder();

      while (true)
      {
         switch (state)
         {
            case State.Locked:
               entry.Append(Console.ReadKey().KeyChar);

               if (entry.ToString() == code)
               {
                  state = State.Unlocked;
                  break;
               }

               if (!code.StartsWith(entry.ToString()))
               {
                  // the code is blatantly wrong
                  state = State.Failed;
               }

               break;

            case State.Failed:
               Console.CursorLeft = 0;
               Console.WriteLine("FAILED");
               entry.Clear();
               state = State.Locked;
               break;

            case State.Unlocked:
               Console.CursorLeft = 0;
               Console.WriteLine("UNLOCKED");
               return;

            default:
               throw new ArgumentOutOfRangeException(nameof(state));
         }
      }
   }
}

internal enum State
{
   Locked,
   Failed,
   Unlocked
}