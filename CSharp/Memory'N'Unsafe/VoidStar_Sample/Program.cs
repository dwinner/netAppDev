// A void pointer (void*) makes no assumptions about the type of the underlying data and is
// useful for functions that deal with raw memory:

short[] a = { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 };
unsafe
{
   fixed (short* p = a)
   {
      //sizeof returns size of value-type in bytes
      Zap(p, a.Length * sizeof(short));
   }
}

foreach (var x in a)
{
   Console.WriteLine(x); // Prints all zeros
}

return;

unsafe void Zap(void* memory, int byteCount)
{
   var b = (byte*)memory;
   for (var i = 0; i < byteCount; i++)
   {
      *b++ = 0;
   }
}