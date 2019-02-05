using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace PhoneStoreSells
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathPhone = @"C:\Users\Aramyan\Desktop\phones.txt";
            string pathPeople = @"C:\Users\Aramyan\Desktop\people.txt";
            string exportJSONpath = @"C:\Users\Aramyan\Desktop\peopleAndPhones.json";
            Society wholeSociety = new Society("Whole Society", 0, 200, GenderGroup.both);
            PhoneStore store = new PhoneStore("My Mobile Store");
            wholeSociety.GetMembersFromList(DataManipulation.GetPeopleFromFileAsync(pathPeople).Result);
            store.AvailablePhones = DataManipulation.GetPhonesFromFileAsync(pathPhone).Result;

            // selling all the phones randomly to the people from society
            wholeSociety.BuyAllPhonesFromStore(store);

            // ordering the list of people by the age, then by the number of their phones
            wholeSociety.People.OrderBy(person => person.Age).ThenBy(person => person.Phones.Count);

            string serializedPeople = JsonConvert.SerializeObject(wholeSociety.People);

            File.WriteAllText(exportJSONpath, serializedPeople);
            Console.WriteLine("Successfully finished!!!");

            Console.ReadKey();
        }
    }
}
