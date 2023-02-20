using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Currently this is bad practice but it saves having to pass down a Dependancy injected value into every Transaction token
//(ask if this should be done since PerfCounters are most likely going to be interfaced )
public class TransactionPerformanceRegister
{
    //public static TransactionPerformanceRegister Instance = new TransactionPerformanceRegister();
    public static PerformanceCounter CreateCounter;
    public static PerformanceCounter CommitCounter;

    public static void Init()
    {
        CounterCreationDataCollection counterDataCollection = new CounterCreationDataCollection();
        Debugger.Break();

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
        PerformanceCounterCategory.Create("TandmRevit",
            "Demonstrates usage of the AverageCounter64 performance counter type.",
            PerformanceCounterCategoryType.SingleInstance, counterDataCollection);


        CreateCounter = new PerformanceCounter("TandmRevit", "CreateTransactionToken", false);
        CommitCounter = new PerformanceCounter("TandmRevit", "CommitTransactionToken", false);
    }

}
