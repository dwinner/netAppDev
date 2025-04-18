﻿using System;
using System.Collections.Generic;

Stack<char> alphabet = new();
alphabet.Push('A');
alphabet.Push('B');
alphabet.Push('C');

Console.Write("First iteration: ");
foreach (var item in alphabet)
{
   Console.Write(item);
}

Console.WriteLine();

Console.Write("Second iteration: ");
while (alphabet.Count > 0)
{
   Console.Write(alphabet.Pop());
}

Console.WriteLine();