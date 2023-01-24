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
        public string age;
        public string userID;
        protected string password;
        public string address;
        static public Dictionary<string, Admin> admins = new Dictionary<string, Admin>();
        static public Dictionary<string, Client> clients = new Dictionary<string, Client>();
        static public Dictionary<string, Book> books = new Dictionary<string, Book>();
        static public List<Book> BorrowedBooks = new List<Book>();
        static public Dictionary<string, Client> restrictedClients = new Dictionary<string, Client>();
        static public List<Request> requestedBooks = new List<Request>();
        static public List<Log> logs = new List<Log>();


        public User(string firstName, string lastName, string age, string address, string password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            this.password = password;
            this.address = address;
        }

        static public void login(bool isAdmin)
        {
            UI.TypeLine("What's your User ID? ", Color.Blue);
            string inputUserID = Console.ReadLine();

            if (isAdmin)
            {
                if (admins.ContainsKey(inputUserID))
                {
                    string inputPassword = UI.getPassword();



                    if (inputPassword == admins[inputUserID].password)
                    {
                        logs.Add(new Log($"Admin {inputUserID} Logged In", inputUserID, true));
                        UI.AdminMenu(admins[inputUserID]);
                    }
                    else
                    {
                        logs.Add(new Log($"Admin {inputUserID} Logged In", inputUserID, false));
                        UI.throwError("Invalid Password...");
                        UI.InitialMenu(isAdmin);
                    }
                }
                else
                {
                    logs.Add(new Log($"Admin {inputUserID} Logged In", inputUserID, false));
                    UI.throwError("The UserID you entered does not exist");
                    UI.InitialMenu(isAdmin);
                }
            }
            else
            {
                if (clients.ContainsKey(inputUserID))
                {
                    string inputPassword = UI.getPassword();



                    if (inputPassword == clients[inputUserID].password)
                    {
                        logs.Add(new Log($"Client {inputUserID} Logged In", inputUserID, true));
                        UI.ClientMenu(clients[inputUserID]);
                    }
                    else
                    {
                        logs.Add(new Log($"Client {inputUserID} Logged In", inputUserID, false));
                        UI.throwError("Invalid Password...");
                        UI.InitialMenu(isAdmin);
                    }
                }
                else
                {
                    logs.Add(new Log($"Client {inputUserID} Logged In", inputUserID, false));
                    UI.throwError("The UserID you entered does not exist");
                    UI.InitialMenu(isAdmin);
                }


            }
            
        }

        public void logout(string userID, bool isAdmin)
        {
            string inputPassword = UI.getPassword();

            if(isAdmin)
            {
                if (inputPassword == admins[userID].password)
                {
                    UI.AnimateLogo(); UI.ProgressBar();
                    UI.ShowSuccess($" {admins[userID].firstName} you have Successfully" +
                        $" Logged out of your account");
                    logs.Add(new Log($"Admin: {userID} Logged Out", userID, true));
                    UI.GeneralMenu();
                }
                else
                {
                    logs.Add(new Log($" Admin: {userID} Logged Out", userID, false));
                    UI.throwError("Invalid Password...");
                }
            }

            else
            {
                if (inputPassword == clients[userID].password)
                {
                    UI.AnimateLogo(); UI.ProgressBar();
                    UI.ShowSuccess($" {clients[userID].firstName} you have Successfully" +
                        $" Logged out of your account");
                    logs.Add(new Log($"Client: {userID} Logged Out", userID, true));
                    UI.GeneralMenu();
                }
                else
                {
                    logs.Add(new Log($" Client: {userID} Logged Out", userID, false));
                    UI.throwError("Invalid Password...");
                }
            }
        }


        public void listBooksInventory()
        {
            UI.Escape(); UI.TypeLine($"======= HERE ARE ALL THE BOOKS IN THE INVENTORY ======", Color.DarkBlue); UI.Escape();
            foreach (Book book in books.Values)
            {
                string bookCondition = book.isBorrowed ? "Borrowed" : "Available";
                UI.TypeLine($"ID: {book.ID}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Title: {book.name}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Book Author: {book.author}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Registration TimeStamp: {book.regDateTime}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Condition: {bookCondition}", book.isBorrowed ? Color.Red : Color.Green, sleepTime: 20);
                UI.Escape();
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------", Color.Blue);
                
            }

            logs.Add(new Log($"{this.userID} Listed Books In Inventory", this.userID, true));
        }


        public void viewBook()
        {

            UI.TypeLine("Enter the Book ID: ", Color.Blue);
            string bookID = Console.ReadLine();

            if (books.ContainsKey(bookID))
            {
                
                UI.Load(times: new Random().Next(50, 70), displayMsg: "Getting Book from Inventory", sequenceCode: 1);
                UI.ProgressBar(displayMsg: "Loading Book Details....", interval: new Random().Next(30, 70));

                UI.Escape(); UI.TypeLine($"======= HERE ARE {bookID}'S DETAILS ======", Color.DarkBlue); UI.Escape();
                Book book = books[bookID];
                string bookCondition = book.isBorrowed ? "Borrowed" : "Available";


                UI.TypeLine($"ID: {book.ID}\n", sleepTime: 20);
                UI.TypeLine($"Title: {book.name}\n", sleepTime: 20);
                UI.TypeLine($"Book Author: {book.author}\n", sleepTime: 20);
                UI.TypeLine($"Registration TimeStamp: {book.regDateTime}\n", sleepTime: 20);
                UI.TypeLine($"Condition: {bookCondition}\n", book.isBorrowed ? Color.Red : Color.Green, sleepTime: 20);

                if (book.isBorrowed)
                {
                    UI.TypeLine($"Due to Return On: {book.dueDate}");
                }
                UI.Escape();
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------", Color.Blue);
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

