namespace PhoneStoreSells
{
    class Phone
    {
        public int PhoneID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        public Phone(int id, string brand, string model)
        {
            PhoneID = id;
            Brand = brand;
            Model = model;
        }
    }
}
