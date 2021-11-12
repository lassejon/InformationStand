using CsvHelper.Configuration.Attributes;

namespace ConsoleApp
{
    public class User
    {
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

        public User()
        {
            
        }

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