Console.WriteLine("Starting async Task<IEnumerable<int>>. Data arrives in one group.");

foreach (var data in await RangeTaskAsync(0, 10, 500)
            .ConfigureAwait(false))
{
   Console.WriteLine(data);
}

Console.WriteLine("Starting async Task<IEnumerable<int>>. Data arrives as available.");

await foreach (var number in RangeAsync(0, 10, 500))
{
   Console.WriteLine(number);
}

return;

static async Task<IEnumerable<int>> RangeTaskAsync(int start, int count, int delay)
{
   var data = new List<int>();
   for (var i = start; i < start + count; i++)
   {
      await Task.Delay(delay).ConfigureAwait(false);
      data.Add(i);
   }

   return data;
}


static async IAsyncEnumerable<int> RangeAsync(int start, int count, int delay)
{
   for (var i = start; i < start + count; i++)
   {
      await Task.Delay(delay).ConfigureAwait(false);
      yield return i;
   }
}