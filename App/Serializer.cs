using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace ConsoleApp
{
    public class Serializer
    {
        private string Path { get; set; }
        
        public Serializer(string fileName = "/users.csv")
        {
            Path = Directory.GetCurrentDirectory() + fileName;
        }

        public IEnumerable<User> ReadUsers()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };
            using var reader = new StreamReader(Path);
            using var csv = new CsvReader(reader, config);
            
            return csv.GetRecords<User>().ToList();
        }

        public void WriteUser(User user)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };
            using var reader = new StreamWriter(Path, true);
            using var csv = new CsvWriter(reader, config);
            csv.WriteRecord(user);
            csv.NextRecord();
        }

        public bool Exists(int phoneNumber)
        {
            var users = ReadUsers();

            return users.Any(userFromUsers => userFromUsers.PhoneNumber == phoneNumber);
        }

        public IEnumerable<User> FindUsersWithSamePhoneNumber(int phoneNumber)
        {
            var users = ReadUsers();

            return users.Where(user => phoneNumber == user.PhoneNumber).ToList();
        }
        
        public IEnumerable<User> FindUsersWithSameFirstName(string firstName)
        {
            var users = ReadUsers();

            return (from user in users where firstName.ToLower().Equals(user.FirstName.ToLower()) select user).ToList();
        }
        
        public IEnumerable<User> FindUsersWithSameLastName(string lastName)
        {
            var users = ReadUsers();

            return (from user in users where lastName.Equals(user.LastName) select user).ToList();
        }
        
        private IEnumerable<string> ReadUsersOld()
        {
            IEnumerable<string> users;
         
            try
            {
                users = File.ReadLines(Path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                users = Enumerable.Empty<string>();
            }

            return users;
        }
        
        private bool WriteUserOld(User user)
        {
            if (Exists(user.PhoneNumber))
            {
                return false;
            }
            
            File.AppendAllText(Path, user + "\n");
            return true;
        }
    }
}
