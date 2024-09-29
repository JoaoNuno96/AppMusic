namespace AppMusic.Entities
{
    class Buyer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Buyer(string n, string e, string p)
        {
            this.Name = n;
            this.Email = e;
            this.PhoneNumber = p;
        }
    }
}
