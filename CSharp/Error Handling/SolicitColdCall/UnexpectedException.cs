﻿using System;

public class UnexpectedException : Exception
{
   public UnexpectedException(string message)
      : base(message)
   {
   }

   public UnexpectedException(string message, Exception innerException)
      : base(message, innerException)
   {
   }
}