using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args);
            var serializer = new Serializer();
            //var menu = new Menu();
            
            var aisha = new User(23423433, "Kit", "Skjoldann",
                "Udkantsdanmark", 4000, "Hillerød", "K", 34);
            
            Console.WriteLine("WriteUser eval: " + serializer.WriteUserCsv(aisha));
            
            foreach (var user in serializer.ReadUsersCsv())
            {
                Console.WriteLine(user);
            }
        }
    }
}
