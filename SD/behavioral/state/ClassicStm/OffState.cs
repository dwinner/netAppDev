﻿namespace ClassicStm;

public class OffState : State
{
   public OffState()
   {
      Console.WriteLine("Light turned off.");
   }

   public override void On(Switch sw)
   {
      Console.WriteLine("Turning light on...");
      sw.State = new OnState();
   }
}