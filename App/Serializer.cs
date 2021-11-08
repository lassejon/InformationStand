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

        public bool Find(User user)
        {
            return true;
        }

        public IEnumerable<User> ReadUsers()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };
            using var reader = new StreamReader(Path);
            using var csv = new CsvReader(reader, config);
            
            return csv.GetRecords<User>().ToList();
        }

        public bool WriteUser(User user)
        {
            if (Exists(user))
            {
                return false;
            }
            
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };
            using var reader = new StreamWriter(Path, true);
            using var csv = new CsvWriter(reader, config);
            csv.WriteRecord(user);
            
            return true;
        }

        private bool Exists(User user)
        {
            var users = ReadUsers();

            return users.Any(userFromUsers => userFromUsers.PhoneNumber == user.PhoneNumber);
        }

        public List<User> FindUsersWithSamePhoneNumber(int phoneNumber)
        {
            var users = ReadUsers();

            return users.Where(user => phoneNumber == user.PhoneNumber).ToList();
        }
        
        public List<string> FindUsersWithSameFirstName(string firstName)
        {
            var users = ReadUsers();

            return (from user in users where firstName.Equals(user.FirstName) select user.FirstName).ToList();
        }
        
        public List<string> FindUsersWithSameLastName(string lastName)
        {
            var users = ReadUsers();

            return (from user in users where lastName.Equals(user.LastName) select user.LastName).ToList();
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
            if (Exists(user))
            {
                return false;
            }
            
            File.AppendAllText(Path, user + "\n");
            return true;
        }
    }
}
