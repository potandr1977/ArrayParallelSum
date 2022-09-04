using ArrayParallelSum;

var array = await ArrayCreator.Create();

var res = Adder.AddOnlyPrimeNumbersPlinq(array);

Console.WriteLine(res);


