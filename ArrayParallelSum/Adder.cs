namespace ArrayParallelSum;

using System.Linq;

internal static class Adder
{
    private static bool IsPrime(int n)
    {
        if (n == 1)
        {
            return false;
        }

        if (n == 2)
        {
            return true;
        }

        var boundary = (int)Math.Floor(Math.Sqrt(n));

        for (int j = 2; j <= boundary; ++j)
            if (n % j == 0)
            {
                return false;
            }

        return true;
    }

    public static long SumPlinq(int[] array) => array.AsParallel().Where(IsPrime).Sum(x => (long)x);

    public static long SumParallel(int[] array)
    {
        long total = 0;
        int arrayLen = array.Length;

        Parallel.For(
            0, arrayLen,
            () => 0,
            (int i, ParallelLoopState loopState, long threadLocalStorageValue) =>
            {
                int arrayValue = array[i];
                return IsPrime(arrayValue) ? threadLocalStorageValue += arrayValue : threadLocalStorageValue;
            },
            value => Interlocked.Add(ref total, value));

        return total;
    }

    public static long SumSerial(int[] array)
    {
        long total = 0;

        array.ToList().ForEach(x => total = IsPrime(x) ? total += x : total); 

        return total;
    }

    public static long Sum(int[] array,Func<int[], long> sumFunc, string caption)
    {
        var timeStart = DateTimeOffset.Now;
        var res = sumFunc(array);
        var timeEnd = DateTimeOffset.Now;
        var timeSpan = timeEnd - timeStart;
        Console.WriteLine($"{caption}: {res} : {timeSpan}");

        return res;
    }
}
