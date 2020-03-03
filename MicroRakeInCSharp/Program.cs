using System;
using System.Collections.Generic;

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
        /*
            make mac and cheese
            boil water
            buy pasta
            buy cheese
            goto the store
        */
        static void Main(string[] args)
        {
            MicroRake.AddTask("goto_the_store", new MRTask("goto_the_store", new string[] {}, () => Console.WriteLine("Goto the store.")));
            MicroRake.AddTask("boil_water", new MRTask("boil_water", new string[] {}))
        }
    }
}
