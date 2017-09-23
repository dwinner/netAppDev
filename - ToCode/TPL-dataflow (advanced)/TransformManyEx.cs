private static void TransformMany()
{
	var consumeBlock = new ActionBlock<int>(new Action<int>(Consumer));

	var tmb = new TransformManyBlock<int, int>(new Func<int, IEnumerable<int>>(ProduceMany));

	tmb.LinkTo(consumeBlock);

	tmb.Post(10);
	Console.ReadLine();
}

private static void Consumer(int obj)
{
	Console.WriteLine("{0}:Consuming {1}",Task.CurrentId,obj);
}

private static IEnumerable<int> ProduceMany(int i)
{
	for (int j = 0; j < i; j++)
	{
		 Console.WriteLine("{0}: Generating {1}", Task.CurrentId, j);
		 yield return j;
		 Thread.Sleep(1000);
	}
}