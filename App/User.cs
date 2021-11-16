using CsvHelper.Configuration.Attributes;

namespace ConsoleApp
{
    public class User
    {
        // index helps csvhelper automatically  connect each data point on each line
        // in the data file to the correct object property
        [Index(0)]
        public int PhoneNumber { get; set; }
        [Index(1)]
        public string FirstName { get; set; }
        [Index(2)]
        public string LastName { get; set; }
        [Index(3)]
        public string Address { get; set; }
        [Index(4)]
        public int ZipCode { get; set; }
        [Index(5)]
        public string City { get; set; }
        [Index(6)]
        public string Gender { get; set; }
        [Index(7)]
        public int Age { get; set; }

        // constructor the make a new User object
        public User(int phoneNumber, string firstName, string lastName,
            string address, int zipCode, string city, string gender, int age)
        {
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            ZipCode = zipCode;
            City = city;
            Gender = gender;
            Age = age;
        }

        // string representation method
        public override string ToString()
        {
            var userString = $"{PhoneNumber}," +
                             $" {FirstName} " +
                             $"{LastName}," +
                             $" {Address}," +
                             $" {ZipCode}," +
                             $" {City}," +
                             $" {Gender}," +
                             $" {Age}";
            
            return userString;
        }
    }
}