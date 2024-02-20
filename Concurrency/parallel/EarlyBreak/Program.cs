Parallel.ForEach("Hello, world", (c, loopState) =>
{
   if (c == ',')
   {
      loopState.Break();
   }
   else
   {
      Console.Write(c);
   }
});