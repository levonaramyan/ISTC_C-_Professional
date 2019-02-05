using System.Collections.Generic;

namespace PhoneStoreSells
{
    public enum Genders
    {
        male = 1,
        female = 2
    }

    class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public Genders Gender  { get; set; }
        public List<Phone> Phones { get; set; }

        public Person(int id, string name, string surname, int age, Genders gender)
        {
            Phones = new List<Phone>(0);
            ID = id;
            Name = name;
            Surname = surname;
            Age = age;
            Gender = gender;
        }
    }
}
