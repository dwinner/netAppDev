await PrintAsync(7..11);
return;

async Task PrintAsync(Range range)
{
   var message = "Hello, from C#13";
   await Task.Delay(1000);

   // ref-struct
   ReadOnlySpan<char> span = message[range];
   Console.WriteLine(span.ToString());
}