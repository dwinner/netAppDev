private static void DangerOfConditonalLinking()
{
	var block = new BufferBlock<int>();
	var nullBlock = DataflowBlock.NullTarget<int>();

	var oddBlock = new ActionBlock<int>(i => Console.WriteLine(i));

	block.LinkTo(oddBlock, i => i%2 == 1);
	block.LinkTo(nullBlock);

	int j = 0;
	while (true)
	{
		 block.Post(++j);
	}
}