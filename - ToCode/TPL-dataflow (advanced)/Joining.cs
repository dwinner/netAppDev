public class Fork { }

public class Chef
{
   private readonly Resteraunt resteraunt;

   public Chef(Resteraunt resteraunt)
   {
      this.resteraunt = resteraunt;
   }

   public async Task MakeFoodAsync()
   {
      Console.WriteLine("Chef making food");
      Knife knife= await resteraunt.Knife.ReceiveAsync();
      for (int nFoodItem = 0; nFoodItem < 4; nFoodItem++)
      {
          this.resteraunt.Food.Post(new Food());
      }
      Console.WriteLine("Chef made food..");
      resteraunt.Knife.Post(knife);
   }
}

public class Knife { }

public class Waiter
{
   private readonly Resteraunt resteraunt;
   private JoinBlock<Tuple<Fork, Knife, Food>, Customer> joinBlock;
   private ActionBlock<Tuple<Tuple<Fork, Knife, Food>, Customer>> serveFoodBlock; 
   public Waiter(Resteraunt resteraunt )
   {
      this.resteraunt = resteraunt;
      joinBlock = new JoinBlock<Tuple<Fork, Knife, Food>, Customer>(new GroupingDataflowBlockOptions(){Greedy = false});

      resteraunt.ReadyToGo.LinkTo(joinBlock.Target1);
      resteraunt.Customers.LinkTo(joinBlock.Target2);

      serveFoodBlock = new ActionBlock<Tuple<Tuple<Fork, Knife, Food>, Customer>>(
          new Action<Tuple<Tuple<Fork,Knife,Food>,Customer>>(ServeFood));

      joinBlock.LinkTo(serveFoodBlock);
   }

   private void ServeFood(Tuple<Tuple<Fork, Knife, Food>, Customer> foodServiceTuple)
   {
      Fork fork = foodServiceTuple.Item1.Item1;
      Knife knife = foodServiceTuple.Item1.Item2;
      Food food = foodServiceTuple.Item1.Item3;
      Customer customer = foodServiceTuple.Item2;

      customer.EatAsync(fork,knife,food)
          .ContinueWith(eatingTask =>
          {
              resteraunt.Forks.Post(fork);
              resteraunt.Knife.Post(knife);
          });
   }
}

public class Customer
{
   private readonly int id;

   private static Random rnd = new Random();
   private JoinBlock<Fork, Knife,Food> dinningBlock;
   private ActionBlock<Tuple<Fork, Knife,Food>> eatingBlock;

   public Customer(int id)
   {
      Console.WriteLine("New hungry customer {0}",id);
      this.id = id;
     
   }

   public async Task EatAsync(Fork fork, Knife knife, Food food)
   {
      Console.WriteLine("Yummy {0}",id);
      await Task.Delay(2000);
      Console.WriteLine("Burp {0}",id);
   }
}

public class Resteraunt
{
    BufferBlock<Fork> forks = new BufferBlock<Fork>();
    BufferBlock<Knife> knives = new BufferBlock<Knife>();
    BufferBlock<Food> food = new BufferBlock<Food>();
    public Resteraunt(int numberOfForkAndKnifePairs)
    {
        Customers = new BufferBlock<Customer>();
        ReadyToGo = new JoinBlock<Fork, Knife, Food>( new GroupingDataflowBlockOptions(){Greedy = false});

        Forks.LinkTo(ReadyToGo.Target1);
        Knife.LinkTo(ReadyToGo.Target2);
        Food.LinkTo(ReadyToGo.Target3);

        for (int i = 0; i < numberOfForkAndKnifePairs; i++)
        {
            forks.Post(new Fork());
            knives.Post(new Knife());
        }
    }

    public BufferBlock<Fork> Forks { get { return forks; } }
    public BufferBlock<Knife> Knife { get { return knives; } }
    public BufferBlock<Food>  Food { get { return food; }}
    public BufferBlock<Customer> Customers { get; private set; } 

    public JoinBlock<Fork,Knife,Food> ReadyToGo { get; private set; }
}

public class Food { }

namespace Joining
{
    [ServiceContract]
    public interface IGridNode<in T>
    {
        [OperationContract]
        Task InvokeAsync(T workItem);
    }

    public class GridDispatcher<T>
    {
        private BufferBlock<T> workItems = new BufferBlock<T>();
        private BufferBlock<Uri> nodes = new BufferBlock<Uri>();
        private JoinBlock<Uri, T> scheduler = new JoinBlock<Uri, T>(new GroupingDataflowBlockOptions() { Greedy = false});
   
        private ActionBlock<Tuple<Uri, T>> dispatcher;
        private ActionBlock<T> localDispatcher;

        public GridDispatcher(IGridNode<T> localService )
        {
            localDispatcher = new ActionBlock<T>(wi =>
            {
                Console.WriteLine("Executing Locally");
                return localService.InvokeAsync(wi);
            },
            new ExecutionDataflowBlockOptions(){ BoundedCapacity = 2,MaxDegreeOfParallelism = 2});

            dispatcher  = new ActionBlock<Tuple<Uri, T>>((Action<Tuple<Uri, T>>) Dispatch);

            workItems.LinkTo(localDispatcher);
            workItems.LinkTo(scheduler.Target2);
            nodes.LinkTo(scheduler.Target1);


            scheduler.LinkTo(dispatcher);
        }

        
        public void RegisterNode(Uri uri)
        {
            nodes.Post(uri);
        }

        public void SubmitWork(T workItem)
        {
            workItems.Post(workItem);
        }

        private void Dispatch(Tuple<Uri, T> nodeAndWorkItemPair)
        {
            var cf = new ChannelFactory<IGridNode<T>>(new NetTcpBinding(), new EndpointAddress(nodeAndWorkItemPair.Item1));

            IGridNode<T> proxy = cf.CreateChannel();
            proxy.InvokeAsync(nodeAndWorkItemPair.Item2)
                .ContinueWith(t =>
                {
                    ((IClientChannel)proxy).Close();
                    nodes.Post(nodeAndWorkItemPair.Item1);
                });
        }

    }

    public class WorkItem
    {
        public int lhs { get; set; }
        public int rhs { get; set; }
    }

    public class NodeWorker : IGridNode<WorkItem>
    {
        public Task InvokeAsync(WorkItem workItem)
        {
            int result = workItem.lhs + workItem.rhs;
            Console.WriteLine(result);
            return Task.Delay(3000);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
              //TheAtomicCafe();

            //SimpleJoin();

           // InPairs();

            var host = new ServiceHost(typeof(NodeWorker));
            host.AddServiceEndpoint(typeof (IGridNode<WorkItem>), new NetTcpBinding(),"net.tcp://localhost:9000/Worker" );
            host.Open();

            GridDispatcher<WorkItem> grid = new GridDispatcher<WorkItem>(new NodeWorker());

           // grid.RegisterNode(new Uri("net.tcp://localhost:9000/Worker"));
           // grid.RegisterNode(new Uri("net.tcp://localhost:9000/Worker"));

            for (int i = 0; i < 10; i++)
            {
                
                grid.SubmitWork(new WorkItem()
                {
                    lhs = i,
                    rhs = 4,
                });
            }

            Console.ReadLine();
        }

        private static void InPairs()
        {
            var broadcastBlock = new BroadcastBlock<int>(i => i);

            var bufferOne = new BufferBlock<int>();
            var bufferTwo = new BufferBlock<int>();

            var firstJoinBlock = new JoinBlock<int, int>(new GroupingDataflowBlockOptions() {Greedy = false});

            var consumer = new ActionBlock<Tuple<int, int>>(tuple =>
            {
                if (tuple.Item1 != tuple.Item2)
                    Console.WriteLine(tuple);
            });

            bufferOne.LinkTo(firstJoinBlock.Target1);
            bufferTwo.LinkTo(firstJoinBlock.Target2);

            firstJoinBlock.LinkTo(consumer);

            broadcastBlock.LinkTo(bufferOne);
            broadcastBlock.LinkTo(bufferTwo);


            for (int nTask = 0; nTask < 4; nTask++)
            {
                int localTask = nTask;
                Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        broadcastBlock.Post(localTask);
                        // Could cause join to pair incorrectly

                        //bufferOne.Post(localTask);
                        //bufferTwo.Post(localTask);
                    }
                });
            }


            Console.ReadLine();
        }

        private static void SimpleJoin()
        {
            var bufferOne = new BufferBlock<int>();
            var bufferTwo = new BufferBlock<int>();

            var firstJoinBlock = new JoinBlock<int, int>(new GroupingDataflowBlockOptions() {Greedy = false});

            var consumer = new ActionBlock<Tuple<int, int>>(tuple => { Console.WriteLine(tuple); });

            bufferOne.LinkTo(firstJoinBlock.Target1);
            bufferTwo.LinkTo(firstJoinBlock.Target2);

            firstJoinBlock.LinkTo(consumer);


            bufferOne.Post(1);
            bufferTwo.Post(1);

            Console.ReadLine();
        }

        private static void TheAtomicCafe()
        {
            Resteraunt resteraunt = new Resteraunt(2);
            
            Waiter waiter = new Waiter(resteraunt);

            

            ConsoleKey key;
            Chef chef = new Chef(resteraunt);
            int id = 1;

            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Q)
            {
                if (key == ConsoleKey.C)
                {
                    chef.MakeFoodAsync();
                }

                if (key == ConsoleKey.N)
                {
                    resteraunt.Customers.Post(new Customer(id++));
                }
            }
        }
    }
}