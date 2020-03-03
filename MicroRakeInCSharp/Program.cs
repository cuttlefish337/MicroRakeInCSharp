using System;
using System.Collections.Generic;

// source: https://www.youtube.com/watch?v=0D3KfnbTdWw

namespace MicroRakeInCSharp
{
    public static class MicroRake
    {
        public static Dictionary<string, MRTask> AllTasks = new Dictionary<string, MRTask>();

        public static void AddTask(string name, MRTask mrtask)
        {
            AllTasks.Add(name, mrtask);

        }
    }

    public class MRTask
    {
        string Name;
        string[] Deps;
        Action MRAction;
        bool AlreadyRun;
        public MRTask(string name, string[] deps, Action mraction)
        {
            Name = name;
            Deps = deps;
            MRAction = mraction;
            AlreadyRun = false;
        }

        public void MRInvoke()
        {
            if (AlreadyRun == true)
                return;
            foreach(string dep in Deps)
                MicroRake.AllTasks[dep].MRInvoke();
            Execute();
            AlreadyRun = true;
        }

        public void Execute()
        {
            MRAction.Invoke();
        }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            MicroRake.AddTask("buy_cheese", new MRTask("buy_cheese", new string[] {"goto_the_store"}, () => Console.WriteLine("Buy cheese.")));
            MicroRake.AddTask("goto_the_store", new MRTask("goto_the_store", new string[] {}, () => Console.WriteLine("Goto the store.")));
            MicroRake.AddTask("boil_water", new MRTask("boil_water", new string[] {"buy_cheese", "buy_pasta"}, () => Console.WriteLine("Boil water.")));
            MicroRake.AddTask("buy_pasta", new MRTask("buy_pasta", new string[] {"goto_the_store"}, () => Console.WriteLine("Buy pasta.")));
            MicroRake.AddTask("make_mac_and_cheese", new MRTask("make_mac_and_cheese", new string[] {"buy_pasta", "buy_cheese", "boil_water"}, () => Console.WriteLine("Make mac and cheese.")));
            
            // calculate dependencies
            MicroRake.AllTasks["make_mac_and_cheese"].MRInvoke();
        }
    }
}
