using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    public static class Menu
    {
        private const string NewLinesToOptions = "\n\n\n\n\n";
        private const string Choose = "\nVælg funktion : ";

        public static void Start(string message = "Her kan du oprette ny bruger eller søge efter eksisterende.")
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine(message);
            ShowOptions();
            ActionForNextMenu();
        }

        private static void ShowHeader()
        {
            Console.WriteLine("<<< Christian Mørk Information Systems - Nyhedsbrevregistrering >>>");
            Console.WriteLine("\n\n");
        }

        private static void ShowOptions()
        {
            Console.WriteLine(NewLinesToOptions);
            Console.WriteLine("[O] Opret    [F] Find    [V] Vis alle    [Q] Afslut");
            Console.Write(Choose);
        }

        private static void ActionForNextMenu()
        {
            var choice = Console.ReadLine().ToLower();
            switch (choice)
            {
                case "o":
                    CreateUser();
                    break;
                case "f":
                    FindUser();
                    break;
                case "v":
                    var users = Serializer.ReadUsers().ToList();
                    ShowUsers(users);
                    Start();
                    break;
                case "q":
                    break;
                default:
                    Start();
                    break;
            }
        }

        private static void FindUser()
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("Find bruger(e) ved en af følgende oplysninger:");
            Console.WriteLine($"[T] Telefonnummer    [F] Fornavn    [E] Efternavn    [Q] Afslut");
            
            Console.Write(Choose);
            var choice = Console.ReadLine().ToLower();
            switch (choice)
            {
                case "t":
                    var phoneNumber = GetPhoneNumber();
                    var usersWithSamePhoneNumber = Serializer.FindUsersWithSamePhoneNumber(phoneNumber).ToList();
                    ShowUsers(usersWithSamePhoneNumber);
                    break;
                case "f":
                    Console.Write("Fornavn: ");
                    var lastName = Console.ReadLine().ToLower();
                    var usersWithSameFirstName = Serializer.FindUsersWithSameFirstName(lastName).ToList();
                    ShowUsers(usersWithSameFirstName);
                    break;
                case "e":
                    Console.Write("Efternavn: ");
                    var firstName = Console.ReadLine().ToLower();
                    var usersWithSameLastName = Serializer.FindUsersWithSameLastName(firstName).ToList();
                    ShowUsers(usersWithSameLastName);
                    break;
                case "q":
                    Start();
                    return;
            }
            FindUser();
        }

        public static void ShowUsers(List<User> users)
        {
            Console.Clear();
            var page = 0;
            var usersOnPages = UsersOnPages(users);
            do
            {
                Console.Clear();
                ShowHeader();
                Console.WriteLine("Brugere:");
                Console.WriteLine();

                if (users.Count > 0)
                {
                    usersOnPages[page].ForEach(Console.WriteLine);
                }
                page = ShowCorrectFooterAndCalculatePage(page, usersOnPages.Count);
            } while(page >= 0);
        }

        public static List<List<string>> UsersOnPages(List<User> users)
        {
            var usersOnSamePage = new List<string>();
            var usersOnPages = new List<List<string>>();
            for (var i = 0; i < users.Count; i++)
            {
                if ((i > 0 && i % 15 == 0))
                {
                    usersOnPages.Add(usersOnSamePage);
                    usersOnSamePage = new List<string>();
                }

                if (i == users.Count - 1)
                {
                    usersOnSamePage.Add($"{i + 1}: {users[i]}");
                    usersOnPages.Add(usersOnSamePage);
                    break;
                }
                usersOnSamePage.Add($"{i + 1}: {users[i]}");
            }

            return usersOnPages;
        }
        
        private static int CalculatePageToShow(int page, string choice)
        {
            switch (choice)
            {
                // go back to previous menu
                case "q":
                    page = -1;
                    break;
                // previous page
                case "f":
                    page--;
                    break;
                // next page
                case "n":
                    page++;
                    break;
            }

            // default will be to stay on current page
            return page;
        }
        

        private static int ShowCorrectFooterAndCalculatePage(int page, int count)
        {
            string choice;
            
            // all users can be shown on one page
            if (count is 1 or 0)
            {
                Console.WriteLine("[Q] Afslut");
                Console.Write(Choose);
                
                choice = Console.ReadLine().ToLower();
                switch (choice)
                {
                    case "q":
                        return CalculatePageToShow(page, choice);
                    default:
                        return page;
                }
            }

            // there are more users than can be shown on the first page
            if (page == 0)
            {
                Console.WriteLine("[N] Næste Side     [Q] Afslut");
                Console.Write(Choose);
                
                choice = Console.ReadLine().ToLower();
                switch (choice)
                {
                    case "n":
                    case "q":
                        return CalculatePageToShow(page, choice);
                    default:
                        return page;
                }
            }

            // there are users on the left and right page
            if (page < count - 1)
            {
                Console.WriteLine("[F] Forrige side    [N] Næste Side     [Q] Afslut");
                Console.Write(Choose);
                
                choice = Console.ReadLine().ToLower();
                switch (choice)
                {
                    case "f":
                    case "n":
                    case "q":
                        return CalculatePageToShow(page, choice);
                    default:
                        return page;
                }
            }

            // currently on the last page
            Console.WriteLine("[F] Forrige side    [Q] Afslut");
            Console.Write(Choose);
            
            choice = Console.ReadLine().ToLower();
            switch (choice)
            {
                case "f":
                case "q":
                    return CalculatePageToShow(page, choice);
                default:
                    return page;
            }
        }

        private static void CreateUser()
        {
            Console.Clear();
            ShowHeader();

            var phoneNumber = GetPhoneNumber();
            
            if (Serializer.Exists(phoneNumber))
            {
                Start("Telefonnummeret eksisterer page databasen. Prøv igen.");
                return;
            }
            
            Console.Write("Fornavn: ");
            var firstName = Console.ReadLine();
            
            Console.Write("Efternavn: ");
            var lastName = Console.ReadLine();
            
            Console.Write("Adresse: ");
            var address = Console.ReadLine();

            string zipCode;
            do
            {
                Console.Write("Postnummer: ");
                zipCode = Console.ReadLine();
            } while (!IsValidZipCode(zipCode));

            Console.Write("By: ");
            var city = Console.ReadLine();
            
            Console.Write("Køn (K eller M): ");
            var gender = Console.ReadLine();

            string ageInput;
            int age;
            do
            {
                Console.Write("Alder: ");
                ageInput = Console.ReadLine();
            } while (!int.TryParse(ageInput, out age));

            
            var newUser = new User(phoneNumber, firstName, lastName, address, int.Parse(zipCode),
                city, gender, age);
        
            Serializer.WriteUser(newUser);

            Start($"Denne bruger: {newUser} er nu gemt");
        }

        private static int GetPhoneNumber()
        {
            string phoneNumberInput;
            do
            {
                Console.Write("Telefon nr.: ");
                phoneNumberInput = Console.ReadLine();
            } while (!IsValidPhoneNumber(phoneNumberInput));

            return int.Parse(phoneNumberInput!);
        }

        private static bool IsValidZipCode(string zipCode)
        {
            const int lengthOfZipCode = 4;

             return zipCode.Length == lengthOfZipCode && int.TryParse(zipCode, out _);
        }
        private static bool IsValidPhoneNumber(string phoneNumber)
        {
            const int lengthOfNumber = 8;

            return phoneNumber.Length == lengthOfNumber && int.TryParse(phoneNumber, out _);
        }
    }
}
