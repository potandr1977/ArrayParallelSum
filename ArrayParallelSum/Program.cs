using ArrayParallelSum;

var array = await ArrayCreator.Create();

Adder.Sum(array, Adder.SumParallel, nameof(Adder.SumParallel));
Adder.Sum(array, Adder.SumPlinq, nameof(Adder.SumPlinq));
Adder.Sum(array, Adder.SumSerial, nameof(Adder.SumSerial));

Console.ReadLine();


