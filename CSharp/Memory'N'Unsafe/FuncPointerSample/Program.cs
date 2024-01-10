unsafe
{
   delegate*<string, int> functionPointer = &GetLength;
   var length = functionPointer("Hello, world");
   Console.WriteLine(length);

   var pointer = (IntPtr)functionPointer;
   Console.WriteLine(pointer);

// Don't try this at home!
   var pointer2 = (delegate*<string, decimal>)(IntPtr)functionPointer;
   var pointer3 = pointer2("Hello, unsafe world");
   Console.WriteLine(pointer3);
   return;

   static int GetLength(string s) => s.Length;
}