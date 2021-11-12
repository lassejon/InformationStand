using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    public class Menu
    {
        private const string Options = "[O] Opret    [F] Find    [V] Vis alle    [Q] Afslut";
        private const string Header = "<<< Christian Mørk Information Systems - Nyhedsbrevregistrering >>>";
        private const string NewLinesToOptions = "\n\n\n\n\n";
        private const string NewLinesToStartMessage = "\n\n";
        private const string StartMessage = "Her kan du oprette ny bruger eller gennemsøge eksisterende.";
        private const string ChooseMenu = "\nVælg funktion : ";

        private Serializer Serializer { get; set; }

        public Menu()
        {
            Serializer = new Serializer();
        }

        public void Start()
        {
            ShowHeader();
            Console.WriteLine(StartMessage);
            ShowOptions();
            Action();
        }

        private static void ShowHeader()
        {
            Console.WriteLine(Header);
            Console.WriteLine(NewLinesToStartMessage);
        }

        private static void ShowOptions()
        {
            Console.WriteLine(NewLinesToOptions);
            Console.WriteLine(Options);
            Console.Write($"{ChooseMenu}");
        }

        private void Action()
        {
            var isValidInput = true;
            do
            {
                var choice = Console.ReadLine()?.ToLower();
                switch (choice) {
                    case "o":
                        CreateUser();
                        isValidInput = true;
                        break;
                    case "f":
                        FindUser();
                        isValidInput = true;
                        break;
                    case "v":
                        var users = Serializer.ReadUsers().ToList();
                        ShowUsers(users);
                        isValidInput = true;
                        break;
                    case "q":
                        isValidInput = true;
                        break;
                    default:
                        isValidInput = false;
                        break;
                }
            } while (!isValidInput);
        }
        

        public void FindUser()
        {
            var isValidInput = true;
            do
            {
                ShowHeader();
                Console.WriteLine("Find bruger(e) ved en af følgende oplysninger:");
                Console.WriteLine($"[T] Telefonnummer    [F] Fornavn    [E] Efternavn    [Q] Afslut");
                
                Console.Write(ChooseMenu);
                var choice = Console.ReadLine()?.ToLower();
                switch (choice)
                {
                    case "t":
                        var phoneNumber = GetPhoneNumber();
                        var usersWithSamePhoneNumber = Serializer.FindUsersWithSamePhoneNumber(phoneNumber).ToList();
                        ShowUsers(usersWithSamePhoneNumber);
                        isValidInput = true;
                        break;
                    case "f":
                        Console.Write("Fornavn: ");
                        var lastName = Console.ReadLine();
                        var usersWithSameFirstName = Serializer.FindUsersWithSameFirstName(lastName).ToList();
                        ShowUsers(usersWithSameFirstName);
                        isValidInput = true;
                        break;
                    case "e":
                        Console.Write("Efternavn: ");
                        var firstName = Console.ReadLine();
                        var usersWithSameLastName = Serializer.FindUsersWithSameLastName(firstName).ToList();
                        ShowUsers(usersWithSameLastName);
                        isValidInput = true;
                        break;
                    case "q":
                        isValidInput = true;
                        break;
                    default:
                        isValidInput = false;
                        break;
                }
            } while (!isValidInput);
        }

        public void ShowUsers(List<User> users)
        {
            var i = 0;

            do
            {
                ShowHeader();
                Console.WriteLine("Brugere:");
                Console.WriteLine();

                var maxUsersOnPage = i + 15;
                while (i < maxUsersOnPage && i < users.Count)
                {
                    Console.WriteLine($"{i + 1}: {users[i]}");
                    i++;
                }

                Console.WriteLine(NewLinesToOptions);
                i = ShowFooter(i, users.Count, users);
                
            } while(i >= 0);
            
            Start();
        }

        private int ShowFooter(int i, int count, List<User> users)
        {
            // all users can be shown on one page
            if (count < 15)
            {
                Console.WriteLine("[Q] Afslut");
                return ChoicesFromOnlyPage(users, i);
            }

            // there are more users than can be shown on the first page
            if (i == 15 && i < count)
            {
                Console.WriteLine("[N] Næste Side     [Q] Afslut");
                return ChoicesFromFirstPage(users, i);
            }

            // there are users on the left and right page
            if (i < count)
            {
                Console.WriteLine("[F] Forrige side    [N] Næste Side     [Q] Afslut");
                return ChoicesFromMiddlePage(users, i);
            }

            // currently on the last page
            Console.WriteLine("[F] Forrige side    [Q] Afslut");
            return ChoicesFromLastPage(users, i);
        }
        
        private int ChoicesFromFirstPage(List<User> users, int i)
        {
            Console.Write(ChooseMenu);
            var choice = Console.ReadLine()?.ToLower();
            switch (choice)
            {
                case "n":
                    break;
                case "q":
                    i = -1;
                    break;
                default:
                    i = 0;
                    break;
            }
            return i;
        }

        private int ChoicesFromOnlyPage(List<User> users, int i)
        {
            
            Console.Write(ChooseMenu);
            var choice = Console.ReadLine()?.ToLower();
            switch (choice)
            {
                case "q":
                    i = -1;
                    break;
                default:
                    i = 0;
                    break;
            }

            return i;
        }

        private int ChoicesFromLastPage(List<User> users, int i)
        {
            
            Console.Write(ChooseMenu);
            var choice = Console.ReadLine()?.ToLower();
            switch (choice)
            {
                case "f":
                    i -= 15;;
                    while (i % 15 != 0)
                    {
                        i--;
                    }
                    break;
                case "q":
                    i = -1;
                    break;
                default:
                    while (i % 15 != 0)
                    {
                        i--;
                    }
                    break;
            }

            return i;
        }

        private int ChoicesFromMiddlePage(List<User> users, int i)
        {
            Console.Write(ChooseMenu);
            var choice = Console.ReadLine()?.ToLower();
            switch (choice)
            {
                case "f":
                    i -= 30;
                    break;
                case "n":
                    break;
                case "q":
                    i = -1;
                    break;
                default:
                    i = i - 15;
                    break;
            }
            return i;
        }

        public void CreateUser()
        {
            Console.WriteLine(Header);
            Console.WriteLine(NewLinesToStartMessage);

            var phoneNumber = GetPhoneNumber();
            
            while (Serializer.Exists(phoneNumber))
            {
                Console.WriteLine("Telefonnummeret eksisterer i databasen. Prøv igen.");
                Console.WriteLine();
                phoneNumber = GetPhoneNumber();
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
            
            Console.WriteLine("Denne bruger: ");
            Console.WriteLine(newUser);
            Console.WriteLine("er nu gemt.");
            
            ShowOptions();
            Action();
        }

        private int GetPhoneNumber()
        {
            string phoneNumberInput;
            do
            {
                Console.Write("Telefon nr.: ");
                phoneNumberInput = Console.ReadLine();
            } while (!IsValidPhoneNumber(phoneNumberInput));

            return int.Parse(phoneNumberInput);
        }

        private bool IsValidZipCode(string zipCode)
        {
            const int lengthOfZipCode = 4;

             return zipCode.Length == lengthOfZipCode && int.TryParse(zipCode, out _);
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            const int lengthOfNumber = 8;

            return phoneNumber.Length == lengthOfNumber && int.TryParse(phoneNumber, out _);
        }
    }
}
