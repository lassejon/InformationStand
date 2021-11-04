using System;
namespace ConsoleApp
{
    public class Menu
    {
        private string Options { get; set; } = "[O] Opret    [F] Find    [V] Vis alle    [Q] Afslut";
        private string Header { get; set; } = "<<< Christian Mørk Information Systems - Nyhedsbrevregistrering >>>";
        private string NewLinesToOptions = "\n\n\n\n\n";
        private string NewLinesToStartMessage = "\n\n";
        private string StartMessage = "Her kan du oprette ny bruger eller gennemsøge eksisterende.";
        private string ChooseMenu = "Vælg funktion : ";

        public Menu()
        {

        }

        public void Start()
        {
            Console.WriteLine(Header);
            Console.WriteLine(NewLinesToStartMessage);
            Console.WriteLine(StartMessage);
            Console.WriteLine(NewLinesToOptions);
            Console.WriteLine(Options);
            Console.Write($"\n {ChooseMenu}");

            var isValidInput = true;
            do
            {
                var choice = Console.ReadLine().ToLower();
                switch (choice) {
                    case "o":

                        isValidInput = true;
                        break;
                    case "f":

                        isValidInput = true;
                        break;
                    case "v":

                        isValidInput = true;
                        break;
                    case "q":

                        isValidInput = true;
                        break;
                    default:
                        isValidInput = false;
                        break;

                }
            } while (isValidInput);
        }

        public void FindUser()
        {

        }

        public void ShowUsers()
        {

        }

        public void CreateUser()
        {

        }

    }
}
