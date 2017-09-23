private static void Batching()
{
	BatchBlock<int> batchBlock = new BatchBlock<int>(10);
	ActionBlock<int[]> batchConsumer = new ActionBlock<int[]>(items =>
	{
		 Console.WriteLine("Consuming batch");
		 Thread.Sleep(1000);
	});

	batchBlock.LinkTo(batchConsumer, new DataflowLinkOptions() {PropagateCompletion = true});

	for (int i = 0; i < 20; i++)
	{
		 batchBlock.Post(1);
	}

	batchBlock.Complete();
	batchConsumer.Completion.Wait();
}        