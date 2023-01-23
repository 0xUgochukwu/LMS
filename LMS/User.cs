using System;
using System.Collections.Generic;
using System.Drawing;
using Colorful;
using Console = Colorful.Console;
namespace LMS
{
    abstract public class User
    {
        public string firstName;
        public string lastName;
        public int age;
        public string userID;
        protected string password;
        static public Dictionary<string, Admin> admins = new Dictionary<string, Admin>();
        static public Dictionary<string, Client> clients = new Dictionary<string, Client>();
        static public Dictionary<string, Book> books = new Dictionary<string, Book>();
        static public Dictionary<string, Client> restrictedClients = new Dictionary<string, Client>();
        static public List<Request> requestedBooks = new List<Request>();
        static public List<Log> logs = new List<Log>();


        public User(string firstName, string lastName, int age, string password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            this.password = password;
        }

        static public User login()
        {
            UI.TypeLine("What's your User ID?");
            string inputUserID = Console.ReadLine();

            if (inputUserID.Substring(0, 2) == "AD")
            {
                if (admins.ContainsKey(inputUserID))
                {
                    string inputPassword = UI.getPassword();



                    if (inputPassword == admins[inputUserID].password)
                    {
                        return admins[inputUserID];
                    }
                    else
                    {
                        UI.throwError("Invalid Password...");
                        return null;
                    }
                }
                else
                {
                    UI.throwError("The UserID you entered does not exist");
                    return null;
                }
            }
            else
            {
                if (clients.ContainsKey(inputUserID))
                {
                    string inputPassword = UI.getPassword();



                    if (inputPassword == clients[inputUserID].password)
                    {
                        return clients[inputUserID];
                    }
                    else
                    {
                        UI.throwError("Invalid Password...");
                        return null;
                    }
                }
                else
                {
                    UI.throwError("The UserID you entered does not exist");
                    return null;
                }


            }
            
        }

        public void logout(string userID)
        {
            string inputPassword = UI.getPassword();

            if (inputPassword == admins[userID].password || inputPassword == clients[userID].password)
            {
                UI.AnimateLogo(); UI.ProgressBar();
                UI.TypeLine($" {admins[userID].firstName ?? clients[userID].firstName} you have Successfully" +
                    $" Logged out of your account");
                UI.Clear(); UI.GeneralMenu();
            }

            else
            {
                UI.throwError("Invalid Password...");
            }
        }



        public void listBooksInventory()
        {
            foreach(Book book in books.Values)
            {
                string bookCondition = book.isBorrowed ? "Available" : "Borrowed";
                UI.TypeLine($"ID {book.ID}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Name: {book.name}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Book Author: {book.author}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Registration TimeStamp: {book.regDateTime}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Condition {bookCondition}", book.isBorrowed ? Color.Red : Color.Green, sleepTime: 20);
                Console.WriteLine("----------------------------------------------------------", Color.Blue);
            }
        }


        public void viewBook()
        {
            UI.TypeLine("Enter the Book ID: ");
            string bookID = Console.ReadLine();

            if (books.ContainsKey(bookID))
            {
                UI.Load(times: new Random().Next(50, 150), displayMsg: "Getting Book from Inventory...", sequenceCode: 1);
                UI.ProgressBar(displayMsg: "Loading Book Details....", interval: new Random().Next(100, 200));
                Book book = books[bookID];
                string bookCondition = book.isBorrowed ? "Available" : "Borrowed";


                UI.TypeLine($"ID {book.ID}\n", sleepTime: 20);
                UI.TypeLine($"Name: {book.name}\n", sleepTime: 20);
                UI.TypeLine($"Book Author: {book.author}\n", sleepTime: 20);
                UI.TypeLine($"Registration TimeStamp: {book.regDateTime}\n", sleepTime: 20);
                UI.TypeLine($"Condition {bookCondition}\n", book.isBorrowed ? Color.Red : Color.Green, sleepTime: 20);

                if (book.isBorrowed)
                {
                    UI.TypeLine($"Due to Return On: {book.dueDate}");
                }

                Console.WriteLine("----------------------------------------------------------", Color.Blue);
                logs.Add(new Log($"Viewed Book: {bookID}", this.userID, true));
                return;
            }
            else
            {
                logs.Add(new Log($"Viewed Book: {bookID}", this.userID, false));
                UI.throwError($"Book: {bookID} does not exist\n"); return;
            }

        }


    }

}

