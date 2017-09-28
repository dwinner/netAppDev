private static void FirstAttempt()
{
   CancellationTokenSource cts = new CancellationTokenSource();

   var divideBlock =
       new TransformBlock<Tuple<int, int>, int>(pair => pair.Item1/pair.Item2,
           new ExecutionDataflowBlockOptions()
           {
               CancellationToken = cts.Token
           });

   var divideConsumer = new ActionBlock<int>((result =>
   {
       Console.WriteLine(result);

       Thread.Sleep(500);
   }), new ExecutionDataflowBlockOptions() {BoundedCapacity = 11, CancellationToken = cts.Token})
       ;

   divideBlock.LinkTo(divideConsumer, new DataflowLinkOptions() {PropagateCompletion = true});

   divideBlock.Completion.ContinueWith(dbt => divideConsumer.Complete());

   Random rnd = new Random();
   cts.CancelAfter(TimeSpan.FromSeconds(2));

   for (int i = 0; i < 10; i++)
   {
       // divideBlock.Post(new Tuple<int, int>(rnd.Next(0, 10), rnd.Next(0, 10)));
       divideBlock.Post(new Tuple<int, int>(rnd.Next(1, 10), rnd.Next(1, 10)));
       //Thread.Sleep(250);
   }

   divideBlock.Complete();
   divideBlock.Completion.Wait();
   Console.WriteLine("Posting done");
   divideConsumer.Completion.Wait();
}