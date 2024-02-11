using Microsoft.Data.SqlClient;

namespace StudentList
{
    internal class Program
    {
        private static StudentListConnection list;
        static void Main(string[] args)
        {
            list = new StudentListConnection();
            ShowMeny();
            
        }
        private static void ShowMeny()
        {
            string command = "";
            while (command != "exit")
            {
                Console.Clear();
                // Display title as the C# console adressbok app.
                Console.WriteLine("***********************************");
                Console.WriteLine("Console Student list application in C#\r");
                Console.WriteLine("***********************************\n");

                // Ask the user to choose an option.
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\ta - Add student");
                Console.WriteLine("\tb - Remove student");
                Console.WriteLine("\tc - Find student");
                Console.WriteLine("\td - Update students data");

                Console.WriteLine("Please enter your option: ");
                command = Console.ReadLine().ToLower();
                switch (command)
                {
                    case "a":
                        list.AddStudent();
                        break;
                    case "b":
                        list.RemoveStudent();
                        break;
                    case "c":
                        list.ListStudent();
                        break;
                    case "d":
                        list.AddStudent();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
