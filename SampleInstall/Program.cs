//This installer program is only compatible with windows (see proj file) as Perf counters are also win only

using System.Diagnostics;

Console.WriteLine("Starting ...");

if (PerformanceCounterCategory.Exists(TransactionPerformanceRegister.Category))
{
    //Delete the Perf Counter Data instead : TODO make this a flag on the binary instead
    Console.WriteLine("ALready Created ...");

}
else
{
    CounterCreationDataCollection counterDataCollection = new CounterCreationDataCollection();

    // Add the counter.
    CounterCreationData averageCount64 = new CounterCreationData();
    //What are the rest of these types
    averageCount64.CounterType = PerformanceCounterType.NumberOfItems32;
    averageCount64.CounterName = "CreateTransactionToken";
    counterDataCollection.Add(averageCount64);

    // Add the base counter.
    CounterCreationData averageCount64Base = new CounterCreationData();
    averageCount64Base.CounterType = PerformanceCounterType.NumberOfItems32;
    averageCount64Base.CounterName = "CommitTransactionToken";
    counterDataCollection.Add(averageCount64Base);

    // Create the category.
    PerformanceCounterCategory.Create(TransactionPerformanceRegister.Category,
        "Demonstrates usage of the AverageCounter64 performance counter type.",
        PerformanceCounterCategoryType.SingleInstance, counterDataCollection);

}


Console.WriteLine("Finished work...");

Console.ReadLine();

//PerformanceCounterCategory.Delete(String)

//https://stackoverflow.com/questions/140149/deleting-windows-performance-counter-categories

