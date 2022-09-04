namespace ArrayParallelSum;

internal static class Adder
{
    public static long AddOnlyPrimeNumbersPlinq(int[] array)
    {
        long total =0;
        int arrayLen = array.Length;

        Func<int, bool> isPrime = n =>
        {
            if (n == 1) 
                return false;
            if (n == 2) 
                return true;
            var boundary = (int) Math.Floor(Math.Sqrt(n));

            for(int j = 2; j <= boundary; ++j)
                if (n % j == 0) 
                    return false;
            
            return true;
        };
        
        
        total =  array.AsParallel().Aggregate(
            0,
            (acc, item) => isPrime(item) ? acc += item : acc);
        
        /*
        Parallel.For(
            0, arrayLen,
            () => 0,
            (int i, ParallelLoopState loopState, long threadLocalStorageValue) =>
            {
                int arrayValue = array[i];
                return isPrime(arrayValue) ? threadLocalStorageValue += arrayValue : threadLocalStorageValue;
            },
            value => Interlocked.Add(ref total, value));
        */  

        return total;
    }
}
