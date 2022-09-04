namespace ArrayParallelSum;

internal static class ArrayCreator
{
    public static async Task<int[]> Create(int arrayLen = 10)
    {
        var taskAmout = GetTaskAmount();
        var partitionSize = (int) arrayLen / taskAmout;
        var firstPartitionSize = arrayLen % taskAmout;
        var tasks = new List<Task<int[]>>();

        var firstTask = Task.Run(() => FillPartition(firstPartitionSize));
        tasks.Add(firstTask);

        for (int i = 0; i < taskAmout; i++)
        {
            var task = Task.Run(() => FillPartition(partitionSize));
            tasks.Add(task);
        }
        await Task.WhenAll(tasks);

        var res = new List<int>();
        tasks.ForEach(t => res.AddRange(t.Result));

        return res.ToArray();
    }

    private static int[] FillPartition(int partitionSize)
    {
        var partition = new int[partitionSize];

        for (int i = 0; i < partitionSize; i++)
        { 
            partition[i] = GetRandomValue();
        }

        return partition;
    }

    private static int GetRandomValue() => 7; //new Random().Next();

    private static int GetTaskAmount() => ((int)Math.Log2(Environment.ProcessorCount)) + 4;
}


