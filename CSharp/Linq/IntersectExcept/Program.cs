using MoreLinq;

int[] seq1 = { 1, 2, 3 }, seq2 = { 3, 4, 5 };

seq1.Intersect(seq2).ForEach(Console.Write);
Console.WriteLine();

seq1.Except(seq2).ForEach(Console.Write);
Console.WriteLine();

seq2.Except(seq1).ForEach(Console.Write);