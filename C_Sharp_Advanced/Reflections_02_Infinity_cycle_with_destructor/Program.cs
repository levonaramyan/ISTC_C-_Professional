using System;
using System.Reflection;

// This little piece of code illustrate some interesting features of System.Reflection
// and finalizer, which activates at the end of method Main, called by Garbage Collector
// The program is infinite loop of|| program end --> start --> end ......

namespace Reflections_02_Infinity_cycle_with_destructor
{
    class Liver
    {
        ~Liver()
        {
            Program myProg = new Program();
            Type type = myProg.GetType();
            MethodInfo method = type.GetMethod("Main", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            method.Invoke(myProg, null);
        }
    }

    class Program
    {
        static void Main()
        {
            Liver again = new Liver();
            Console.WriteLine("Type any text here:");
            Console.ReadLine();
        }
    }
}
