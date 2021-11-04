namespace ConsoleApp
{
    public class User
    {
        public int TelephoneNumber { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }

        public User()
        {

        }

        public User(int telephoneNumber, string firstName, string surName,
            string address, string zipCode, string city, string gender, string age)
        {
            TelephoneNumber = telephoneNumber;
            FirstName = firstName;
            SurName = surName;
            Address = address;
            ZipCode = zipCode;
            City = city;
            Gender = gender;
            Age = age;
        }

        public User FindUser()
        {

        }

        public User[] ReadUsers()
        {

        }

        public void WriteUser()
        {

        }

    }
}