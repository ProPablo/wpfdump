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

    public const string Category = "TandmRevitSample";

    public static void Init()
    {
        CreateCounter = new PerformanceCounter(Category, "CreateTransactionToken", false);
        CommitCounter = new PerformanceCounter(Category, "CommitTransactionToken", false);
    }

}
