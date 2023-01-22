using System;
using System.Drawing;
using Colorful;
using Console = Colorful.Console;
namespace LMS
{
    abstract class User
    {
        public string firstName;
        public string lastName;
        public int age;
        public string userID;
        private string password;

        public User(string firstName, string lastName, int age, string password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            this.password = password;
            Random getID = new Random();
            this.userID = Convert.ToString(getID.Next(10000, 99999));
        }

        public void login()
        {
            Console.WriteLine("What's your User ID?");
            string inputUserID = Console.ReadLine();


            Console.Write("Enter your password:");
            string inputPassword = "";

            while (true) {
                ConsoleKeyInfo inputKey = Console.ReadKey(true);

                if (inputKey.Key == ConsoleKey.Enter)
                {
                    break;
                }

                inputPassword += inputKey.KeyChar;
                Console.Write("*");
            }
        }

        public void logout()
        {

        }

        public void listBooksInventory()
        {

        }
    }
}

