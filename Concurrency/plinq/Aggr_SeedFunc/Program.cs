var aggregion = new[] { 1, 2, 3 }.AsParallel().Aggregate(
   () => 0, // seedFactory
   (localTotal, n) => localTotal + n, // updateAccumulatorFunc
   (mainTot, localTot) => mainTot + localTot, // combineAccumulatorFunc
   finalResult => finalResult);

Console.WriteLine(aggregion);