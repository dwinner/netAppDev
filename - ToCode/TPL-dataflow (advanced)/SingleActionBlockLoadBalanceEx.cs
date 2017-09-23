public class LoadBalancerAsync<TInput,TOutput>
{
	private class WorkItem
	{
		public TInput Input;
		public TaskCompletionSource<TOutput> Tcs;
	}

	private ConcurrentQueue<Func<TInput,TOutput>> processorQueue; 
	private ActionBlock<WorkItem> processorBlock;

	public LoadBalancerAsync(Func<TInput,TOutput>[] processors , int maxQueueLength)
	{
		processorQueue = new ConcurrentQueue<Func<TInput, TOutput>>(processors);
		processorBlock = new ActionBlock<WorkItem>((Action<WorkItem>) ProcessWorkItem, new ExecutionDataflowBlockOptions()
			 {
				  BoundedCapacity = maxQueueLength,
				  MaxDegreeOfParallelism = processors.Length
			 });
	}

	private void ProcessWorkItem(WorkItem workItem)
	{
		Func<TInput, TOutput> operation = null;
		if (processorQueue.TryDequeue(out operation))
		{
			 try
			 {
				  Task.Run(() => workItem.Tcs.SetResult(operation(workItem.Input)));
			 }
			 catch (Exception error)
			 {
				  workItem.Tcs.SetException(error);
			 }
			 
			 processorQueue.Enqueue(operation);
		}
	}

	public Task<TOutput> DoAsync(TInput input)
	{
		TaskCompletionSource<TOutput> tcs = new TaskCompletionSource<TOutput>();
		WorkItem wi = new WorkItem() {Input = input, Tcs = tcs};

		if (!processorBlock.Post(wi))
		{
			 tcs.SetException(new TooBusyException());
		}

		return tcs.Task;
	}
}

[Serializable]
public class TooBusyException : Exception
{
	public TooBusyException()
	{
	}

	public TooBusyException(string message) : base(message)
	{
	}

	public TooBusyException(string message, Exception inner) : base(message, inner)
	{
	}

	protected TooBusyException(
		SerializationInfo info,
		StreamingContext context) : base(info, context)
	{
	}
}
 
public static class TaskExtensions
{
	public static Task[] ContinueWithMany<T>(this Task<T> source, params Func<Task<T>,Task>[] continuations )
	{
		Task[] results = new Task[continuations.Length];
		for (int i = 0; i < continuations.Length; i++)
		{
			 results[i] = continuations[i](source);
		}

		return results;
	}
}

private static void SingleActionBlockLoadBalance()
{
	LoadBalancerAsync<int, string> balancer = new LoadBalancerAsync<int, string>(
		 new Func<int, string>[]
			  {
					i => (i*10).ToString(),
					i => (i*100).ToString(),
					i => (i*1000).ToString(),
			  }, 5);

	for (int i = 0; i < 10; i++)
	{
		 balancer.DoAsync(i)
					.ContinueWithMany(
						 ct =>
						 ct.ContinueWith(t => Console.WriteLine(t.Result),
											  TaskContinuationOptions.OnlyOnRanToCompletion),
						 ct =>
						 ct.ContinueWith(t => Console.WriteLine(t.Exception.InnerExceptions.First()),
											  TaskContinuationOptions.OnlyOnFaulted)
			  );
	}
	Console.ReadLine();
}