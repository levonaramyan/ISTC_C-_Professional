using System;
using System.Reflection;

namespace Reflections_01
{
    class Planet
    {
        public string name;
        private string hostStar;

        public int Age { get; set; }
        private Double Periodicity { get; set; }

        public Planet(string plName, string host)
        {
            name = plName;
            hostStar = host;
        }

        private void Rotate()
        {
            Console.WriteLine("The Planet is rotating!");
        }

        public void PrintHostName()
        {
            Console.WriteLine($"The name of host star of planet {name} is: {hostStar}");
        }
    }

    class ReflectClass
    {
        // Prints all of the fields of a type of a given instance
        public static void PrintAllFieldInfo(object a)
        {
            Type type = a.GetType();

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic |
                                                BindingFlags.Static | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                Console.WriteLine($"The name of this field is: {field.Name}");
                Console.WriteLine($"This is a public field: {field.IsPublic}");
                Console.WriteLine($"The type of the field is: {field.FieldType}\n");
            }
        }

        // Prints all the methods (public and non-public separately) of input type
        public static void PrintAllMethodInfo(Type type)
        {
            MethodInfo[] publicMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            Console.WriteLine("Below are the public methods:");
            foreach (MethodInfo method in publicMethods)
            {
                Console.WriteLine($"\nThe name of the method is: {method.Name}");
                Console.WriteLine($"The return type of the method is: {method.ReturnType}");
                Console.WriteLine($"The number of its input parameters is: {method.GetParameters().Length}");
            }

            MethodInfo[] nonPublicMethods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            Console.WriteLine("\nBelow are non-public methods:");
            foreach (MethodInfo method in nonPublicMethods)
            {
                Console.WriteLine($"\nThe name of the method is: {method.Name}");
                Console.WriteLine($"The return type of the method is: {method.ReturnType}");
                Console.WriteLine($"The number of its input parameters is: {method.GetParameters().Length}");
            }
        }

        // Changes a private field hostStar of an instance planet, using reflection
        public static void ChangeHostStar (Planet planet, string name)
        {
            Type type = planet.GetType();

            FieldInfo host = type.GetField("hostStar", BindingFlags.NonPublic | BindingFlags.Instance);
            if (host != null)
            {
                host.SetValue(planet, name);
            }
        }

        // invokes a private method Rotate() of an instance planet, using reflection
        public static void RotateThePlanet(Planet planet)
        {
            Type type = planet.GetType();
            MethodInfo rotate = type.GetMethod("Rotate", BindingFlags.NonPublic | BindingFlags.Instance);
            if (rotate != null)
            {
                rotate.Invoke(planet, null);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Planet earth = new Planet("Earth", "Sun");

            // Printing the fields
            ReflectClass.PrintAllFieldInfo(earth);
            SeparateLine();

            // Printing the methods
            Type test = typeof(Planet);
            ReflectClass.PrintAllMethodInfo(test);
            SeparateLine();

            // Changing the private field hostStar
            earth.PrintHostName();
            // earth.hostStar = "Sirius" // impossible to directly access to private field
            ReflectClass.ChangeHostStar(earth, "Sirius");
            earth.PrintHostName();
            SeparateLine();

            // Invoking a clas Rotate
            ReflectClass.RotateThePlanet(earth);

            Console.ReadKey();
        }

        static void SeparateLine()
        {
            Console.WriteLine(new string('-',50));
        }
    }
}
