namespace AppMusic.Entities
{
    class Buyer
    {
        public string name { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public Buyer(string n, string e, string p)
        {
            name = n;
            email = e;
            phoneNumber = p;
        }
    }
}
