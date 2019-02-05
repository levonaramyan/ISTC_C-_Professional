using System.Collections.Generic;
using System.Linq;

namespace PhoneStoreSells
{
    class PhoneStore
    {
        public readonly string name;
        public List<Phone> AvailablePhones { get; set; }

        public PhoneStore(string storeName)
        {
            name = storeName;
        }

        // Selling the phone with phoneID to the person buyer
        public void Sell(int phoneID, Person buyer)
        {
            Phone selected = AvailablePhones.First(phone => phone.PhoneID == phoneID);
            AvailablePhones.Remove(selected);
            buyer.Phones.Add(selected);
        }
    }
}
