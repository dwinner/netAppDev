﻿namespace WeakEventPattern;

public class Button
{
   public event EventHandler? Clicked;

   public void Fire()
   {
      Clicked?.Invoke(this, EventArgs.Empty);
   }
}