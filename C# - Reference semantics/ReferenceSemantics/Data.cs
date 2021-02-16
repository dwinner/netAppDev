using System;

namespace ReferenceSemantics
{
   public class Data
   {
      private int _anumber;

      public Data(int anumber) => _anumber = anumber;

      public ref int GetNumber() => ref _anumber;

      public ref readonly int GetReadonlyNumber() => ref _anumber;

      public void Show() => Console.WriteLine($"Data: {_anumber}");
   }
}