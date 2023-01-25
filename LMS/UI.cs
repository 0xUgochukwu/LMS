using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Timers;
using Colorful;
using static System.Net.Mime.MediaTypeNames;
using Console = Colorful.Console;

namespace LMS
{
    public class UI
    {

        static string[] logos = new string[]
        {
            "\n      ___           ___ \n     /  /\\         /  /\\\n    /  /::\\       /  /:/\n   /  /:/\\:\\     /  /:/ \n  /  /::\\ \\:\\   /  /:/  \n /__/:/\\:\\_\\:\\ /__/:/   \n \\__\\/  \\:\\/:/ \\  \\:\\   \n      \\__\\::/   \\  \\:\\  \n      /  /:/     \\  \\:\\ \n     /__/:/       \\  \\:\\\n     \\__\\/         \\__\\/\n",
            "\n      ___                   \n     /  /\\                  \n    /  /::\\                 \n   /  /:/\\:\\    ___     ___ \n  /  /:/~/::\\  /__/\\   /  /\\\n /__/:/ /:/\\:\\ \\  \\:\\ /  /:/\n \\  \\:\\/:/__\\/  \\  \\:\\  /:/ \n  \\  \\::/        \\  \\:\\/:/  \n   \\  \\:\\         \\  \\::/   \n    \\  \\:\\         \\__\\/    \n     \\__\\/                  \n",
            "\n      ___                   \n     /\\  \\                  \n    /::\\  \\                 \n   /:/\\:\\  \\                \n  /:/ /::\\  \\   ___     ___ \n /:/_/:/\\:\\__\\ /\\  \\   /\\__\\\n \\:\\/:/  \\/__/ \\:\\  \\ /:/  /\n  \\::/__/       \\:\\  /:/  / \n   \\:\\  \\        \\:\\/:/  /  \n    \\:\\__\\        \\::/  /   \n     \\/__/         \\/__/    \n",
            "\n      ___           ___ \n     /\\  \\         /\\__\\\n    /::\\  \\       /:/  /\n   /:/\\:\\  \\     /:/  / \n  /::\\~\\:\\  \\   /:/  /  \n /:/\\:\\ \\:\\__\\ /:/__/   \n \\/__\\:\\/:/  / \\:\\  \\   \n      \\::/  /   \\:\\  \\  \n      /:/  /     \\:\\  \\ \n     /:/  /       \\:\\__\\\n     \\/__/         \\/__/\n"
        };
        static string[,] sequence = new string[,] {
                { "/", "-", "\\", "|" },
                { ".   ", "..  ", "... ", "...." },
                { "=>   ", "==>  ", "===> ", "====>" },
            };
        static int Delay { get; set; } = 200;
        static int totalSequences = sequence.GetLength(0);
        static int counter = 0;


        static public void WriteLogo(Color? color = null)
        {
            Console.WriteLine(logos[0], color ?? Color.Blue);

        }

        public static void LoadBooks()
        {
            string[,] books = new string[,]
            {
                {"Pride and Prejudice", "Jane Austen"},
                {"The Red and the Black", "Stendhal"},
                {"Sands of Time", "Sydney Sheldon"},
                {"Chasing Red", "NicoleWrites"},
                {"Midnight Bayou", "Nora Roberts" },
                {"Wuthering Heights", "Emily Bronte"},
                {"Harry Porter and Sorcere's Stone", "J.K Rowling"},
                {"Robinson Crusoe", "Daniel Defoe"},
                {"Gulliver's Travels", "Jonathan Smith"},
                {"hings Fall Apart", "Chinua Achebe"},
            };


            for (int i = 0; i < books.GetLength(0); i++)
            {
                Book book = new Book(books[i, 0], books[i, 1]);
                User.books.Add(book.ID, book);
            }

        }

        static public void AnimateLogo(int j = 20)
        {

            Console.Clear();
            for (int i = 0; i <= j; i++)
            {
                Console.Write(logos[i % 4], i % 2 == 0 ? Color.Blue : Color.White);
                Thread.Sleep(500);
                Console.Clear();
            }

            Clear();

        }

        static public void TypeLine(string line, Color? color = null, int sleepTime = 50)
        {
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i], color ?? Color.White);
                System.Threading.Thread.Sleep(sleepTime);
            }
        }

        static public void TypeLineL(string line, Color? color = null, int sleepTime = 50)
        {
            WriteLogo();
            for (int i = 0; i < line.Length; i++)
            {
                
                Console.Write(line[i], color ?? Color.White);
                System.Threading.Thread.Sleep(sleepTime);
            }
        }

        static public void TypeLogo()
        {
            TypeLine(logos[0], Color.Blue, 10);
        }

        static public void Clear()
        {
            Console.Clear();
            WriteLogo();
        }

        static public void Load(int times, string displayMsg = "", int sequenceCode = 0, Color? color = null)
        {
            Console.Clear();
            TypeLineL(displayMsg);
            Clear();

            for (int i = 0; i < times; i++)
            {
                Clear();
                Console.Write(displayMsg);
                TypeLine(sequence[sequenceCode, i % 4], color ?? Color.White);
                System.Threading.Thread.Sleep(200);

            }

            Clear();
        }

        static public void Load2(int times, string displayMsg = "", int sequenceCode = 0)
        {
            Clear();
            TypeLine(displayMsg);
            Clear();
            int timesCheck = 0;
            while (timesCheck++ < times + displayMsg.Length)
            {

                counter++;

                

                sequenceCode = sequenceCode > totalSequences - 1 ? 0 : sequenceCode;

                int counterValue = counter % 4;

                Clear();
                Console.Write(displayMsg + sequence[sequenceCode, counterValue]);

                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
                //Console.CursorVisible = false;
                Thread.Sleep(Delay);
            }


            Clear();
            
            
        }

        public static void ProgressBar(string displayMsg = "", int interval = 20, Color? color = null)
        {
            string spaces = "                         ";
            string loader = "";
            int count = 0;
            for (int i = 0; i < 101; i++)
            {
                Clear();

                Console.Write($"{displayMsg}");
                Console.Write($"[");
                Console.Write(loader + spaces, color ?? Color.Green);
                Console.Write($"] {sequence[0, count % 4]}  {i}%");
                Thread.Sleep(interval);

                if (i % 2 == 0) count++;
                if (i % 4 == 0 && i != 100)
                {
                    loader += "#";
                    spaces = spaces.Remove(spaces.Length - 1);
                    
                }
            }

            Clear();
        }

        public static string getPassword(bool needText = true)
        {
            if(needText)
            {
                TypeLine("Enter your password: ", Color.Blue);
            }
            string inputPassword = "";

            while (true)
            {
                ConsoleKeyInfo inputKey = Console.ReadKey(true);

                if (inputKey.Key == ConsoleKey.Enter)
                {
                    break;
                }

                if (inputKey.Key == ConsoleKey.Backspace && inputPassword.Length > 0)
                {
                    inputPassword = inputPassword.Remove(inputPassword.Length - 1);
                    Console.Write("\b \b");
                    continue;
                }

                inputPassword += inputKey.KeyChar;
                Console.Write("*");
            }
            Escape();

            return inputPassword;
        }
        
        public static void throwError(string errorMsg)
        {
            TypeLine(errorMsg, color: Color.Red);

        }

        public static void ShowSuccess(string successMsg)
        {
            Clear();
            TypeLine(successMsg, color: Color.Green);

        }

        static public void Escape( )
        {
            Console.WriteLine();
        }

        static public void passwordErr() {
            throwError("Invalid Password...");
        }

        static public void menuOption(int option, string message)
        {
            Console.Write($"[ ");
            Console.Write(option, Color.Blue);
            Console.Write($" ]  ");

            Console.Write(message);
            Console.Write(".\n", Color.Blue);

        }

        public static void Welcome()
        {
            Console.Clear();
            TypeLogo();
            Console.Clear();
            AnimateLogo(10);
            //Clear();
            //AnimateLogo();
            TypeLine("Welcome to Aptech Library\nHow do you want to proceed?", Color.DarkBlue); Escape();
        }

        public static void displayOptions(string[] options)
        {
            Console.WriteLine("\n---------------------------------------------", Color.Blue);
            for (int i = 0; i < options.GetLength(0); i++)
            {
                menuOption(i + 1, options[i]);
            }
            Console.WriteLine("---------------------------------------------\n", Color.Blue);
        }

        public static void GeneralMenu()
        {
            int choice = 0;
            bool displayed = false;

            

            if (!displayed)
            {
                Welcome();
                displayed = true;
            }

            string[] options = { "Proceed As An Admin", "Proceed As A User", "Close App" };
            displayOptions(options);

            try {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                throwError("You entered an invalid input");
                GeneralMenu();
                
            }

            switch (choice)
            {
                case 1:
                    Clear();
                    AnimateLogo(4);
                    InitialMenu(true);
                    break;
                case 2:
                    Clear();
                    AnimateLogo(4);
                    InitialMenu(false);
                    break;
                case 3:
                    Clear();
                    AnimateLogo(4);
                    TypeLine("Closing Aptech Library ......");
                    Environment.Exit(0);
                    break;
                default:
                    TypeLine("You entered a wrong Value, Try Again", Color.Red);
                    GeneralMenu();
                    break;




            }
        }

        public static void InitialMenu(bool isAdmin)
        {
            int choice = 0;

           

            string[] options = { "Create An Account", "Login", "Go Back" };
            displayOptions(options);

            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                throwError("You entered an invalid input");
                InitialMenu(isAdmin ? true: false) ;

            }

            switch (choice)
            {
                case 1:
                    Clear();
                    AnimateLogo(4);
                    UI.CreateAccount(isAdmin);
                    break;
                case 2:
                    Clear();
                    AnimateLogo(4);
                    User.login(isAdmin);
                    break;
                case 3:
                    Clear();
                    AnimateLogo(4);
                    GeneralMenu();
                    break;

                default:
                    Clear();
                    AnimateLogo(4);
                    throwError("You entered a wrong Value, Try Again");
                    InitialMenu(isAdmin);
                    break;




            }
        }

        public static void Greet(string name)
        {
            Escape();
            TypeLine($"Hello {name}, What would you like to do next?", Color.DarkBlue);
        }

        public static void ClientMenu(Client client)
        {
            int choice = 0;

            

            Greet(client.firstName); Escape();

            string[] options = {
                "See Book Inventory",
                "View A Book",
                "Display Your Account Details",
                "Borrow A Book",
                "List Books In Your Possession",
                "Return A Book",
                "Request A Book",
                "Log Out"
            };
            displayOptions(options);

            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                throwError("You entered an invalid input");
                ClientMenu(client);

            }

            switch (choice)
            {
                case 1:
                    Clear();
                    AnimateLogo(4);
                    client.listBooksInventory();
                    ClientMenu(client);
                    break;
                case 2:
                    Clear();
                    AnimateLogo(4);
                    client.viewBook();
                    ClientMenu(client);
                    break;
                case 3:
                    Clear();
                    AnimateLogo(4);
                    client.displayPersonalDetails();
                    ClientMenu(client);
                    break;
                case 4:
                    Clear();
                    AnimateLogo(4);
                    client.borrowBook();
                    ClientMenu(client);
                    break;
                case 5:
                    Clear();
                    AnimateLogo(4);
                    client.listBooksInPossession();
                    ClientMenu(client);
                    break;
                case 6:
                    Clear();
                    AnimateLogo(4);
                    client.returnBook();
                    ClientMenu(client);
                    break;
                case 7:
                    Clear();
                    AnimateLogo(4);
                    client.requestABook();
                    ClientMenu(client);
                    break;
                case 8:
                    Clear();
                    AnimateLogo(4);
                    client.logout(client.userID, false);
                    break;
                default:
                    Clear();
                    AnimateLogo(4);
                    throwError("You entered a wrong Value, Try Again");
                    ClientMenu(client);
                    break;

            }
        }

        public static void AdminMenu(Admin admin)
        {
            int choice = 0;




            Greet(admin.firstName); Escape();

            string[] options = {
                "Display Your Account Details",
                "List All Books In the Inventory",
                "List Borrowed Books",
                "List Clients",
                "List Admins",
                "View Client",
                "View Admin",
                "View Book",
                "Edit Book",
                "Add New Book",
                "Delete Book",
                "Restrict Client",
                "Release Client",
                "Delete Client",
                "Delete Admin",
                "View Book Requests",
                "View Library Logs",
                "Log Out"
            };
            displayOptions(options);

            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                throwError("You entered an invalid input");
                AdminMenu(admin);

            }

            switch (choice)
            {
                case 1:
                    Clear();
                    AnimateLogo(4);
                    admin.displayPersonalDetails();
                    AdminMenu(admin);
                    break;
                case 2:
                    Clear();
                    AnimateLogo(4);
                    admin.listBooksInventory();
                    AdminMenu(admin);
                    break;
                case 3:
                    Clear();
                    AnimateLogo(4);
                    admin.listBorrowedBooks();
                    AdminMenu(admin);
                    break;
                case 4:
                    Clear();
                    AnimateLogo(4);
                    admin.listClients();
                    AdminMenu(admin);
                    break;
                case 5:
                    Clear();
                    AnimateLogo(4);
                    admin.listAdmins();
                    AdminMenu(admin);
                    break;
                case 6:
                    Clear();
                    AnimateLogo(4);
                    admin.displayClientDetails();
                    AdminMenu(admin);
                    break;
                case 7:
                    Clear();
                    AnimateLogo(4);
                    admin.displayAdminDetails();
                    AdminMenu(admin);
                    break;
                case 8:
                    Clear();
                    AnimateLogo(4);
                    admin.viewBook();
                    AdminMenu(admin);
                    break;
                case 9:
                    Clear();
                    AnimateLogo(4);
                    admin.modifyBook();
                    AdminMenu(admin);
                    break;
                case 10:
                    Clear();
                    AnimateLogo(4);
                    admin.addBook();
                    AdminMenu(admin);
                    break;
                case 11:
                    Clear();
                    AnimateLogo(4);
                    admin.deleteBook();
                    AdminMenu(admin);
                    break;
                case 12:
                    Clear();
                    AnimateLogo(4);
                    admin.restrictClient();
                    AdminMenu(admin);
                    break;
                case 13:
                    Clear();
                    AnimateLogo(4);
                    admin.releaseClient();
                    AdminMenu(admin);
                    break;
                case 14:
                    Clear();
                    AnimateLogo(4);
                    admin.deleteClient();
                    AdminMenu(admin);
                    break;
                case 15:
                    Clear();
                    AnimateLogo(4);
                    admin.deleteAdmin();
                    AdminMenu(admin);
                    break;
                case 16:
                    Clear();
                    AnimateLogo(4);
                    admin.viewBookRequests();
                    AdminMenu(admin);
                    break;
                case 17:
                    Clear();
                    AnimateLogo(4);
                    admin.seeLogs();
                    AdminMenu(admin);
                    break;
                case 18:
                    Clear();
                    AnimateLogo(4);
                    admin.logout(admin.userID, true);
                    AdminMenu(admin);
                    break;
                default:
                    Clear();
                    AnimateLogo(4);
                    throwError("You entered a wrong Value, Try Again");
                    AdminMenu(admin);
                    break;

            }
        }

        public static void CreateAccount(bool isAdmin)
        {
            if (isAdmin)
            {
                TypeLine("What is your First Name? ", Color.Blue);
                string firstName =  Console.ReadLine();

                TypeLine("What is your Last Name? ", Color.Blue);
                string lastName = Console.ReadLine();

                TypeLine("How old are you? ", Color.Blue);
                string age = Console.ReadLine();

                TypeLine("What is your house address? ", Color.Blue);
                string address = Console.ReadLine();

                TypeLine("Enter the Admin Pass: ", Color.Blue);
                string adminPass = getPassword(false); Escape();

                if (adminPass != "0000")
                {
                    throwError("Wrong Admin Pass \nYou can't create an Admin Account \nTry Creating A User Account Instead");
                    GeneralMenu();
                }

                TypeLine("Enter your preferred Password: ", Color.Blue);
                string password = getPassword(false);



                UI.Load(times: new Random().Next(50, 150), displayMsg: "Creating your Admin Account...", sequenceCode: 0);
                UI.ProgressBar(displayMsg: "Loading your Account....", interval: new Random().Next(100, 200));

                Admin admin = new Admin(firstName, lastName, age, address, password);

                User.admins.Add(admin.userID, admin);

                Clear();
                AdminMenu(admin);
            }

            else
            {
                TypeLine("What is your First Name? ", Color.Blue);
                string firstName = Console.ReadLine();

                TypeLine("What is your Last Name? ", Color.Blue);
                string lastName = Console.ReadLine();

                TypeLine("How old are you? ", Color.Blue);
                string age = Console.ReadLine();

                TypeLine("What is your house address? ", Color.Blue);
                string address = Console.ReadLine();

                TypeLine("Enter your preferred Password: ", Color.Blue);
                string password = getPassword(false);


                UI.Load(times: new Random().Next(50, 150), displayMsg: "Creating your Client Account...", sequenceCode: 0);
                UI.ProgressBar(displayMsg: "Loading your Account....", interval: new Random().Next(100, 200));

                Client client = new Client(firstName, lastName, age, address, password);

                User.clients.Add(client.userID, client);

                ClientMenu(client);
            }
        }

    }
    
}

