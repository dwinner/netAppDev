private static void Errors()
{
	TransformBlock<int, int> errorBlock = new TransformBlock<int, int>(i =>
	{
		 Console.WriteLine(Task.CurrentId);
		 if (i%2 == 0)
			  throw new Exception("Boom");

		 return i;
	}, new ExecutionDataflowBlockOptions() {BoundedCapacity = 4, MaxMessagesPerTask = 1});


	TransformBlock<int, int> nonErrorBlock = new TransformBlock<int, int>(i => i);


	ActionBlock<int> consumeBlock = new ActionBlock<int>(i => { Console.WriteLine(i); });

	errorBlock.LinkTo(consumeBlock); //, new DataflowLinkOptions() { PropagateCompletion = true});
	nonErrorBlock.LinkTo(consumeBlock);

	for (int i = 1; i < 10; i++)
	{
		 bool postFailed = errorBlock.Post(i);
		 nonErrorBlock.Post(i);
		 if (errorBlock.Completion.IsFaulted)
		 {
			  Console.WriteLine("Faulted");
			  ((IDataflowBlock) consumeBlock).Fault(errorBlock.Completion.Exception);
		 }
		 Thread.Sleep(1000);
	}
	Console.ReadLine();
	consumeBlock.Complete();
	try
	{
		 consumeBlock.Completion.Wait();
	}
	catch (AggregateException errors)
	{
		 foreach (Exception error in errors.Flatten().InnerExceptions)
		 {
			  Console.WriteLine(error.Message);
		 }
	}
}