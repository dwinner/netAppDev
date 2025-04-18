﻿using System;

internal class LocalFunctionsSample
{
   private int myVar;

   public int MyProperty
   {
      get
      {
         void foo() => Console.WriteLine("Foo");
         foo();
         return myVar;
      }
      set => myVar = value;
   }


   public static void IntroLocalFunctions()
   {
      static int Add(int x, int y) => x + y;

      var result = Add(3, 7);
      Console.WriteLine("called the local function with this result: {result}");
   }

   public static void LocalFunctionWithClosure()
   {
      var z = 3;

      var result = Add(1, 2);
      Console.WriteLine("called the local function with this result: {result}");

      int Add(int x, int y) => x + y + z;
   }
}