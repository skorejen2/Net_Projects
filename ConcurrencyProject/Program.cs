



using System.Collections.Concurrent;

public class Counter
{
    public static BlockingCollection<int> totalValues = new();
    private static bool _IsFinished = false;

    static void Main(string[] args)
    {
        Thread t1 = new(() => Counter.InsertOne());
        t1.Name = "Thread 1";

        Thread t2 = new(() => Counter.TakeOne());
        t2.Name = "Thread 2";

        Thread t3 = new(() =>
        {
            for (var i = 0; ; i++)
            {
                System.Console.WriteLine("Total values count: " + totalValues.Count);
                Thread.Sleep(5000);
                if (!t2.IsAlive) break;
            }
        });

        t1.Start();
        t2.Start();
        t3.Start();

        t1.Join();
        t2.Join();
        t3.Join();
        
        foreach (var item in totalValues)
        {
            System.Console.WriteLine(item + "'\n");
        }




    }

    public delegate void CountTo20();
    public static void InsertOne()
    {
        for (var a = 0; a < 50; a++)
        {
            var i = new Random().Next();
            totalValues.Add(i);
            System.Console.WriteLine(Thread.CurrentThread.Name + " added value " + i);
            Thread.Sleep(50);
        }
        totalValues.CompleteAdding();
        
    }
    public static void TakeOne()
    {

        foreach (var item in totalValues.GetConsumingEnumerable())
        {
            System.Console.WriteLine($"Thread {Thread.CurrentThread.Name} took value {item}");
            Thread.Sleep(200);
        }
    }

}

public class Producer
{



}

public class Receiver
{

}