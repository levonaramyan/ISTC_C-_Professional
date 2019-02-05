using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PhoneStoreSells
{
    static class DataManipulation
    {
        // Returns a phone from a "|"-separated line of data table
        private static Phone GetPhoneFromLine(string line)
        {            
            string[] elems = line.Split('|');
            string brand = elems[0];
            string model = elems[1];
            int id = int.Parse(elems[2]);
            Phone phone = new Phone(id,brand,model);

            return phone;
        }

        // Returns a person from a "|"-separated line of data table
        private static Person GetPersonFromLine(string line)
        {
            string[] elems = line.Split('|');
            int id = int.Parse(elems[0]);
            string name = elems[1];
            string surname = elems[2];
            int age = int.Parse(elems[3]);
            Genders gender = elems[4].ToLower() == "male" ? Genders.male : Genders.female;

            Person person = new Person(id,name,surname,age,gender);

            return person;
        }

        // returns an array of phones from the "|"-separated data with given path
        public static List<Phone> GetPhonesFromFile (string path)
        {
            List<Phone> result = new List<Phone>(0);

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                do
                {
                    line = sr.ReadLine();
                    try
                    {
                        result.Add(GetPhoneFromLine(line));
                    }
                    catch
                    {
                        Console.WriteLine($"Not a Phone format:\n{line}");
                    }

                } while (!sr.EndOfStream);

            }

            return result;
        }

        // returns an array of people from the "|"-separated data with given path
        public static List<Person> GetPeopleFromFile (string path)
        {
            List<Person> result = new List<Person>(0);

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                do
                {
                    line = sr.ReadLine();
                    try
                    {
                        result.Add(GetPersonFromLine(line));
                    }
                    catch
                    {
                        Console.WriteLine($"Not a Person format:\n{line}");
                    }

                } while (!sr.EndOfStream);
            }

            return result;
        }

        // Asynchronously: returns an array of persons from the "|"-separated data with given path
        public static async Task<List<Person>> GetPeopleFromFileAsync(string path)
        {
            return await Task.Run(() => GetPeopleFromFile(path));
        }

        // Asynchronously: returns an array of phones from the "|"-separated data with given path
        public static async Task<List<Phone>> GetPhonesFromFileAsync(string path)
        {
            return await Task.Run(() => GetPhonesFromFile(path));
        }
    }
}
