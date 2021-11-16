using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace ConsoleApp
{
    public static class Serializer
    {
        // current directory plus the hardcoded name on the data file
        private static readonly string Path = Directory.GetCurrentDirectory() + "/users.csv";

        public static List<User> ReadUsers()
        {
            // configure which delimiter to use
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };
            
            // create reader stream (connection) to file 
            using var reader = new StreamReader(Path);
            
            // specify it's a csv (comma separated value) stream
            using var csv = new CsvReader(reader, config);
            
            // getrecords reads every line and convert it to a user object and saves it to a list
            return csv.GetRecords<User>().ToList();
        }

        public static void WriteUser(User user)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };
            using var reader = new StreamWriter(Path, true);
            using var csv = new CsvWriter(reader, config);
            
            // writes the user to the file
            csv.WriteRecord(user);
            
            // creates a new line at the end of file, so if we write another user later,
            // it will be saved on a new line
            csv.NextRecord();
        }

        public static bool Exists(int phoneNumber)
        {
            var users = ReadUsers();

            // linq expression that check the list for the same phonenumber
            return users.Any(userFromUsers => userFromUsers.PhoneNumber == phoneNumber);
        }
        
        public static List<User> FindUsersWithSamePhoneNumber(int phoneNumber)
        {
            var users = ReadUsers();
            
            return users.Where(user => phoneNumber == user.PhoneNumber).ToList();
        }
        
        public static List<User> FindUsersWithSameFirstName(string firstName)
        {
            var users = ReadUsers();

            // linq expression that loops through users and selects users with same firstname as inputted
            return (from user in users where firstName.ToLower().Equals(user.FirstName.ToLower()) select user).ToList();
        }
        
        public static List<User> FindUsersWithSameLastName(string lastName)
        {
            var users = ReadUsers();

            return (from user in users where lastName.Equals(user.LastName) select user).ToList();
        }
    }
}
