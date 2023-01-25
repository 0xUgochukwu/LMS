using System;
using System.Drawing;
using System.Net;
using Colorful;
using Console = Colorful.Console;

namespace LMS
{
    public class Admin : User
    {
        

        public Admin(string firstName, string lastName, string age, string address, string password) : base(firstName, lastName, age, password, address)
        {
            this.userID = "AD" + Convert.ToString(new Random().Next(10000, 99999));
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            this.address = address;
            this.password = password;
        }

        public override void displayPersonalDetails()
        {
            
            string inputPassword = UI.getPassword();
            if (inputPassword == this.password)
            {
                UI.Escape(); UI.TypeLine("======= YOUR ADMIN PERSONAL DETAILS ======", Color.DarkBlue); UI.Escape();
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------", Color.Blue);
                UI.TypeLine($"ID: {userID}\n");
                UI.TypeLine($"Name: {firstName} {lastName}\n");
                UI.TypeLine($"Age: {age}\n");
                UI.TypeLine($"Address: {address}\n");
                UI.TypeLine($"Password: {this.password}\n");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------", Color.Blue);
                logs.Add(new Log($"Displayed Personal Account details", this.userID, true));
                return;
            }
            else
            {
                logs.Add(new Log($"Displayed Personal Account details", this.userID, false));
                UI.passwordErr(); return;
            }
        }

        public void restrictClient()
        {

            UI.TypeLine("Enter the Client's UserID: ", Color.Blue);
            string userID = Console.ReadLine();
            if(clients.ContainsKey(userID))
            {
                UI.Load(times: new Random().Next(50, 100), displayMsg: "Restricting the mf...", sequenceCode: 0);
                restrictedClients.Add(userID, clients[userID]);
                UI.ShowSuccess($"Client: {userID} has now been restricted and will no longer be allowed to borrow books");
                logs.Add(new Log($"Restricted Client: {userID}", this.userID, true));
                return;
            }
            else
            {
                logs.Add(new Log($"Restricted Client: {userID}", this.userID, false));
                UI.throwError($"Client: {userID} does not exist\n"); return;
            }
        }

        public void releaseClient()
        {
            UI.TypeLine("Enter the Client's UserID: ", Color.Blue);
            string userID = Console.ReadLine();
            if (clients.ContainsKey(userID))
            {
                UI.Load(times: new Random().Next(50, 100), displayMsg: "Releasing the mf...", sequenceCode: 0);
                restrictedClients.Remove(userID);
                UI.ShowSuccess($"Client: {userID} has now been released so the mf can now borrow books");
                logs.Add(new Log($"Released Client: {userID}", this.userID, true));
                return;
            }
            else
            {
                logs.Add(new Log($"Released Client: {userID}", this.userID, false));
                UI.throwError($"Client: {userID} does not exist\n"); return;
            }
        }

        public void deleteClient()
        {
            UI.TypeLine("Enter the Client's UserID: ", Color.Blue);
            string userID = Console.ReadLine();
            if (clients.ContainsKey(userID))
            {
                UI.Load(times: new Random().Next(50, 100), displayMsg: "Deleting Client...", sequenceCode: 0);
                clients.Remove(userID);
                UI.ShowSuccess($"Client: {userID} deleted successfully\n");
                logs.Add(new Log($"Deleted Client: {userID}", this.userID, true));
                return;
            }
            else
            {
                logs.Add(new Log($"Deleted Client: {userID}", this.userID, false));
                UI.throwError($"Client: {userID} does not exist\n"); return;
            }
        }

        public void deleteAdmin()
        {
            UI.TypeLine("Enter the Admin's UserID: ", Color.Blue);
            string userID = Console.ReadLine();
            if (admins.ContainsKey(userID))
            {
                if(userID != this.userID)
                {
                    UI.Load(times: new Random().Next(50, 100), displayMsg: "Deleting Client...", sequenceCode: 0);
                    admins.Remove(userID);
                    UI.ShowSuccess($"Admin: {userID} deleted successfully\n");
                    logs.Add(new Log($"Deleted Admin: {userID} details", this.userID, true));
                    return;
                }
                else
                {
                    logs.Add(new Log($"Deleted Admin: {userID} details", this.userID, false));
                    UI.throwError($"You can't delete yourself Admin: {userID}\n"); return;
                }
            }
            else
            {
                logs.Add(new Log($"Deleted Admin: {userID} details", this.userID, false));
                UI.throwError($"Admin: {userID} does not exist\n"); return;
            }
        }

        public void displayClientDetails()
        {

            UI.TypeLine("Enter the Client's UserID: ", Color.Blue);
            string userID = Console.ReadLine();

            
            if (clients.ContainsKey(userID))
            {
                UI.Escape(); UI.TypeLine($"======= CLIENT: {userID}'S PERSONAL DETAILS ======", Color.DarkBlue); UI.Escape();
                UI.Load(times: new Random().Next(50, 100), displayMsg: $"Loading {userID}'s details...", sequenceCode: 0);
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------", Color.Blue);
                UI.TypeLine($"ID: {clients[userID]}\n");
                UI.TypeLine($"Name: {clients[userID].firstName} {clients[userID].lastName}\n");
                UI.TypeLine($"Age: {clients[userID].age}\n");
                UI.TypeLine($"Address: {clients[userID].address}\n");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------", Color.Blue);

                foreach(Book book in clients[userID].borrowedBooks.Values)
                {
                    UI.TypeLine($"ID {book.ID}", sleepTime: 20);
                    UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                    UI.TypeLine($"Name: {book.name}", sleepTime: 20);
                    UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                    UI.TypeLine($"Book Author: {book.author}", sleepTime: 20);
                    UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                    UI.TypeLine($"Registration TimeStamp: {book.regDateTime}", sleepTime: 20);
                    UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------", Color.Blue);
                }
                logs.Add(new Log($"Displayed Client: {userID} details", this.userID, true));
                return;
            } else
            {
                logs.Add(new Log($"Displayed Client: {userID} details", this.userID, false));
                UI.throwError($"Client: {userID} does not exist\n"); return;
            }
        }

        public void displayAdminDetails()
        {
            UI.TypeLine("Enter the Admin's UserID: ", Color.Blue);
            string userID = Console.ReadLine();

            if (admins.ContainsKey(userID))
            {
                UI.Escape(); UI.TypeLine($"======= ADMIN: {userID}'S PERSONAL DETAILS ======", Color.DarkBlue); UI.Escape();
                UI.Load(times: new Random().Next(50, 100), displayMsg: "Deleting Client...", sequenceCode: 0);
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------", Color.Blue);
                UI.TypeLine($"ID: {admins[userID]}\n", sleepTime: 20);
                UI.TypeLine($"Name: {admins[userID].firstName} {clients[userID].lastName}\n", sleepTime: 20);
                UI.TypeLine($"Age: {admins[userID].age}\n", sleepTime: 20);
                UI.TypeLine($"Address: {clients[userID].address}\n");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------", Color.Blue);
                logs.Add(new Log($"Displayed Admin: {userID} details", this.userID, true));
                return;
            }
            else
            {
                logs.Add(new Log($"Displayed Admin: {userID} details", this.userID, false));
                UI.throwError($"Admin: {userID} does not exist\n"); return;
            }
        }

        public void listClients()
        {
            UI.Escape(); UI.TypeLine($"======= YOUR CLIENTS LIST ======", Color.DarkBlue); UI.Escape();
            foreach (Client client in clients.Values)
            {
                string hasBorrwedBooks = client.borrowedBooks == null ? "Yes" : "No";
                UI.TypeLine($"ID {client.userID}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Name: {client.firstName} {client.lastName}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Address: {client.address}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Has Borrowed Books: {hasBorrwedBooks}", client.borrowedBooks == null ? Color.Green : Color.Red, sleepTime: 20);
                UI.Escape();
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------", Color.Blue);
            }

            logs.Add(new Log($"Listed Clients", this.userID, true));
            return;
        }

        public void listAdmins()
        {
            UI.Escape(); UI.TypeLine($"======= YOUR ADMINS LIST ======", Color.DarkBlue); UI.Escape();
            foreach (Admin admin in admins.Values)
            {
                UI.TypeLine($"ID {admin.userID}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Name: {admin.firstName} {admin.lastName}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Age: {admin.age}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Address: {admin.address}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.Escape();
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------", Color.Blue);
            }

            logs.Add(new Log($"Listed Clients", this.userID, true));
            return;
        }

        public void listBorrowedBooks()
        {
            UI.Load(times: new Random().Next(50, 100), displayMsg: "Loading books...", sequenceCode: 0);
            UI.ProgressBar(displayMsg: "Getting Borrowed Books....", interval: new Random().Next(100, 200));

            UI.Escape(); UI.TypeLine($"======= THE BORROWED BOOKS LIST ======", Color.DarkBlue); UI.Escape();
            foreach (Book book in BorrowedBooks)
            {
                if (book.isBorrowed)
                {
                    UI.TypeLine($"ID {book.ID}", sleepTime: 20);
                    UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                    UI.TypeLine($"Title: {book.name}", sleepTime: 20);
                    UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                    UI.TypeLine($"Book Author: {book.author}", sleepTime: 20);
                    UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                    UI.TypeLine($"Registration TimeStamp: {book.regDateTime}", sleepTime: 20);
                    UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                    UI.TypeLine($"Due Date: {book.dueDate}", sleepTime: 20);
                    UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                    UI.Escape();
                    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------", Color.Blue);
                }
                
            }
            logs.Add(new Log($"Listed Borrowed Books", this.userID, true));
            return;
        }

        public void addBook()
        {
            UI.TypeLine("Enter Book Name: ", Color.Blue);
            string bookName = Console.ReadLine();

            UI.TypeLine("Enter Book Author: ", Color.Blue);
            string bookAuthor = Console.ReadLine();

            string inputPassword = UI.getPassword();

            Book book = new Book(bookName, bookAuthor);

            if (inputPassword == password)
            {
                
                UI.Load(times: new Random().Next(50, 100), displayMsg: "Adding Book to the Inventory...", sequenceCode: 0);
                books.Add(book.ID, book);
                UI.ProgressBar(displayMsg: "Updating Book Inventory....");
                logs.Add(new Log($"Added Book: {book.ID}", this.userID, true));
                return;
            }
            else
            {
                logs.Add(new Log($"Added Book: {book.ID}", this.userID, false));
                UI.passwordErr(); return;
            }
        }

        public void deleteBook()
        {
            UI.TypeLine("Enter the Books's ID: ", Color.Blue);
            string bookID = Console.ReadLine();
            if (books.ContainsKey(bookID))
            {
                UI.Load(times: new Random().Next(50, 100), displayMsg: "Deleting Book...", sequenceCode: 0);
                UI.ProgressBar(displayMsg: "Updating Book Inventory....");
                books.Remove(bookID);
                UI.ShowSuccess($"Book: {bookID} deleted successfully\n");
                logs.Add(new Log($"Deleted Book: {bookID}", this.userID, true));
                return;
            }
            else
            {
                logs.Add(new Log($"Deleted Book: {bookID}", this.userID, false));
                UI.throwError($"Book: {bookID} does not exist\n"); return;
            }
        }

        public void modifyBook()
        {
            UI.TypeLine("Enter the Books's ID: ", Color.Blue);
            string bookID = Console.ReadLine();
            if (books.ContainsKey(bookID))
            {
                UI.Load(times: new Random().Next(50, 100), displayMsg: "Loading Book...", sequenceCode: 0);
                UI.ProgressBar();
                UI.TypeLine($"You're Modifying Book {bookID}"); UI.Escape();

                UI.TypeLine($"Enter New Book Title: ", sleepTime: 50, color: Color.Blue); UI.Escape();
                books[bookID].name = Console.ReadLine();

                UI.TypeLine($"Enter the Author's New Name: ", sleepTime: 50, color: Color.Blue); UI.Escape();
                books[bookID].author = Console.ReadLine();


                UI.Load(times: new Random().Next(50, 100), displayMsg: "Loading Book...", sequenceCode: 0);
                UI.ProgressBar(displayMsg: "Modifying Book Details...");

                UI.ShowSuccess($"Book: {bookID} Updated successfully\n");
                logs.Add(new Log($"Updated Book: {bookID}", this.userID, true));
                return;
            }
            else
            {
                logs.Add(new Log($"Updated Book: {bookID}", this.userID, false));
                UI.throwError($"Book: {bookID} does not exist\n"); return;
            }
        }

        public void viewBookRequests()
        {
            UI.ProgressBar(displayMsg: "Loading Requests...", interval: 50);

            UI.Escape(); UI.TypeLine($"======= HERE ARE THE RECENT REQUESTS ======", Color.DarkBlue); UI.Escape();
            foreach (Request request in requestedBooks)
            {
                UI.TypeLine($"Request ID: {request.requestID}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Name: {request.book.name}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Book Author: {request.book.author}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Request Time {request.reqTime}", sleepTime: 20);
                UI.Escape();
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------", Color.Blue);
                
            }
            logs.Add(new Log("Viewed Requested Books", this.userID, true));
            return;
        }

        public void seeLogs()
        {
            UI.ProgressBar(displayMsg: "Getting Logs...", interval: new Random().Next(100, 200));

            UI.Escape(); UI.TypeLine($"======= HERE ARE THE LIBRARY LOGS ======", Color.DarkBlue); UI.Escape();
            foreach (Log log in logs)
            {
                string status = log.status ? "SUCCESS" : "FAILED";
                UI.TypeLine($"Log ID: {log.ID}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Log Actor: {log.actorID}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Log Message: {log.message}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"TimeStamp : {log.timeStamp}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Status: {status}", log.status ? Color.Green : Color.Red, sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.Escape();
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------", Color.Blue);
            }
            logs.Add(new Log("Viewed Logs", this.userID, true));
            return;
        }


    }


    
}

