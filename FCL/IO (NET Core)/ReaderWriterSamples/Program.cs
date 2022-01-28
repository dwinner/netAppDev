using System;
using System.IO;
using System.Text;

ReadFileUsingReader("./Program.cs");
Console.WriteLine();
string textFile = Path.ChangeExtension(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()), "txt");
WriteFileUsingWriter(textFile, new[] { "one", "two" });
Console.WriteLine($"Written temp file {textFile}");

string binFile = Path.ChangeExtension(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()), "bin");
Console.WriteLine($"writing to {binFile}");
WriteFileUsingBinaryWriter(binFile);
Console.WriteLine($"written to {binFile}");
ReadFileUsingBinaryReader(binFile);

void WriteFileUsingWriter(string fileName, string[] lines)
{
   var outputStream = File.OpenWrite(fileName);
   using StreamWriter writer = new(outputStream, Encoding.UTF8);

   var preamble = Encoding.UTF8.GetPreamble().AsSpan();
   outputStream.Write(preamble);
   foreach (var line in lines)
   {
      writer.WriteLine(line);
   }
}

void WriteFileUsingBinaryWriter(string aBinFile)
{
   var outputStream = File.Create(aBinFile);
   using BinaryWriter writer = new(outputStream);

   var d = 47.47;
   var i = 42;
   long l = 987654321;
   string s = "sample";
   writer.Write(d);
   writer.Write(i);
   writer.Write(l);
   writer.Write(s);
}

void ReadFileUsingBinaryReader(string aBinFile)
{
   var inputStream = File.Open(aBinFile, FileMode.Open);
   using BinaryReader reader = new(inputStream);

   var d = reader.ReadDouble();

   var i = reader.ReadInt32();
   var l = reader.ReadInt64();
   string s = reader.ReadString();
   Console.WriteLine($"d: {d}, i: {i}, l: {l}, s: {s}");
}

void ReadFileUsingReader(string fileName)
{
   FileStream stream = new(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
   using StreamReader reader = new(stream);

   while (!reader.EndOfStream)
   {
      var line = reader.ReadLine();
      Console.WriteLine(line);
   }
}