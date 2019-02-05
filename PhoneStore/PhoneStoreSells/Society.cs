using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneStoreSells
{
    public enum GenderGroup
    {
        male = 1,
        female,
        both
    }

    class Society
    {
        public readonly string name;
        public readonly int ageMin;
        public readonly int ageMax;
        public readonly GenderGroup gender;

        protected List<Person> people;
        
        public List<Person> People
        {
            get => people;
        }

        public Society(string name, int ageMin, int ageMax, GenderGroup gender)
        {
            this.name = name;
            this.ageMin = ageMin;
            this.ageMax = ageMax;
            this.gender = gender;
            people = new List<Person>(0);
        }

        // Adding a member in society of it corresponds to the age and gender constraints
        public void AddMember(Person memberCandidate)
        {
            if (memberCandidate.Age >= ageMin && memberCandidate.Age <= ageMax &&
                (gender == GenderGroup.both) || (int)memberCandidate.Gender == (int)gender)
                people.Add(memberCandidate);
        }

        // Adding corresponding members from a list of Persons
        public void GetMembersFromList(List<Person> candidates)
        {
            foreach (Person candidate in candidates) AddMember(candidate);
        }

        // Black Friday Sales: Society randomly byes all of the phones from the given store
        public void BuyAllPhonesFromStore(PhoneStore store)
        {
            int peopleNum = people.Count();
            Random rand = new Random();
            while (store.AvailablePhones.Count > 0)
            {
                int buyerIndex = rand.Next(0, peopleNum);
                int phoneIndex = rand.Next(0, store.AvailablePhones.Count);
                store.Sell(store.AvailablePhones[phoneIndex].PhoneID, people[buyerIndex]);
            }
        }
    }
}
