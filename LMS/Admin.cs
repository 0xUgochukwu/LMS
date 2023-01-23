using System;
using System.Drawing;
using System.Net;

namespace LMS
{
    public class Admin : User
    {
        

        public Admin(string firstName, string lastName, int age, string password) : base(firstName, lastName, age, password)
        {
            this.userID = "AD" + Convert.ToString(new Random().Next(10000, 99999));
        }

        public void displayPersonalDetails()
        {

            string inputPassword = UI.getPassword();
            if (inputPassword == password)
            {
                Console.WriteLine("----------------------------------------------------------", Color.Blue);
                UI.TypeLine($"ID: {userID}\n");
                UI.TypeLine($"Name: {firstName} {lastName}\n");
                UI.TypeLine($"Age: {age}");
                UI.TypeLine($"Password: {password}\n");
                Console.WriteLine("----------------------------------------------------------", Color.Blue);
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
            UI.TypeLine("Enter the Client's UserID: ");
            string userID = Console.ReadLine();
            if(clients.ContainsKey(userID))
            {
                UI.Load(times: new Random().Next(50, 150), displayMsg: "Restricting the mf...", sequenceCode: 1);
                restrictedClients.Add(userID, clients[userID]);
                UI.ShowSuccess($"Client: {userID} has now been restricted and will no longer be allowed to borrow books");
                logs.Add(new Log($"Deleted Client: {userID}", this.userID, true));
                return;
            }
            else
            {
                logs.Add(new Log($"Restricted Client: {userID}", this.userID, false));
                UI.throwError($"Client: {userID} does not exist\n"); return;
            }
        }

        public void deleteClient()
        {
            UI.TypeLine("Enter the Client's UserID: ");
            string userID = Console.ReadLine();
            if (clients.ContainsKey(userID))
            {
                UI.Load(times: new Random().Next(50, 150), displayMsg: "Deleting Client...", sequenceCode: 1);
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
            UI.TypeLine("Enter the Admin's UserID: ");
            string userID = Console.ReadLine();
            if (admins.ContainsKey(userID))
            {
                UI.Load(times: new Random().Next(50, 150), displayMsg: "Deleting Client...", sequenceCode: 1);
                admins.Remove(userID);
                UI.ShowSuccess($"Admin: {userID} deleted successfully\n");
                logs.Add(new Log($"Deleted Admin: {userID} details", this.userID, true));
                return;
            }
            else
            {
                logs.Add(new Log($"Deleted Admin: {userID} details", this.userID, false));
                UI.throwError($"Admin: {userID} does not exist\n"); return;
            }
        }




        public void displayClientDetails()
        {
            UI.TypeLine("Enter the Client's UserID: ");
            string userID = Console.ReadLine();

            if (clients.ContainsKey(userID))
            {
                UI.Load(times: new Random().Next(50, 150), displayMsg: "Deleting Client...", sequenceCode: 1);
                Console.WriteLine("----------------------------------------------------------", Color.Blue);
                UI.TypeLine($"ID: {clients[userID]}\n");
                UI.TypeLine($"Name: {clients[userID].firstName} {clients[userID].lastName}\n");
                UI.TypeLine($"Age: {clients[userID].age}\n");
                Console.WriteLine("----------------------------------------------------------", Color.Blue);

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
                    Console.WriteLine("----------------------------------------------------------", Color.Blue);
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
            UI.TypeLine("Enter the Admin's UserID: ");
            string userID = Console.ReadLine();

            if (admins.ContainsKey(userID))
            {
                UI.Load(times: new Random().Next(50, 150), displayMsg: "Deleting Client...", sequenceCode: 1);
                Console.WriteLine("----------------------------------------------------------", Color.Blue);
                UI.TypeLine($"ID: {admins[userID]}\n", sleepTime: 20);
                UI.TypeLine($"Name: {admins[userID].firstName} {clients[userID].lastName}\n", sleepTime: 20);
                UI.TypeLine($"Age: {admins[userID].age}\n", sleepTime: 20);
                Console.WriteLine("----------------------------------------------------------", Color.Blue);
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
            foreach (Client client in clients.Values)
            {
                string hasBorrwedBooks = client.borrowedBooks == null ? "Yes" : "No";
                UI.TypeLine($"ID {client.userID}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Name: {client.firstName} {client.lastName}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Has Borrowed Books: {hasBorrwedBooks}", client.borrowedBooks == null ? Color.Red : Color.Green, sleepTime: 20);
                Console.WriteLine("----------------------------------------------------------", Color.Blue);
            }

            logs.Add(new Log($"Listed Clients", this.userID, true));
            return;
        }


        public void listBorrowedBooks()
        {
            UI.Load(times: new Random().Next(50, 150), displayMsg: "Loading books...", sequenceCode: 1);
            UI.ProgressBar(displayMsg: "Getting Borrowed Books....", interval: new Random().Next(100, 200));
            foreach (Book book in Borrowedbooks.Values)
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
                    Console.WriteLine("----------------------------------------------------------", Color.Blue);
                }
                
            }
            logs.Add(new Log($"Listed Borrowed Books", this.userID, true));
            return;
        }

        public void addBook()
        {
            UI.TypeLine("Enter Book Name: ");
            string bookName = Console.ReadLine();

            UI.TypeLine("Enter Book Author: ");
            string bookAuthor = Console.ReadLine();

            string inputPassword = UI.getPassword();

            Book book = new Book(bookName, bookAuthor);

            if (inputPassword == password)
            {
                
                UI.Load(times: new Random().Next(50, 150), displayMsg: "Adding Book to the Inventory...", sequenceCode: 1);
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
            UI.TypeLine("Enter the Books's ID: ");
            string bookID = Console.ReadLine();
            if (books.ContainsKey(bookID))
            {
                UI.Load(times: new Random().Next(50, 150), displayMsg: "Deleting Book...", sequenceCode: 1);
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
            UI.TypeLine("Enter the Books's ID: ");
            string bookID = Console.ReadLine();
            if (books.ContainsKey(bookID))
            {
                UI.Load(times: new Random().Next(50, 150), displayMsg: "Loading Book...", sequenceCode: 1);
                UI.ProgressBar();
                UI.TypeLine($"You're Modifying Book {bookID}"); UI.Escape();

                UI.TypeLine($"Enter New Book Title: ", sleepTime: 50, color: Color.Blue); UI.Escape();
                books[bookID].name = Console.ReadLine();

                UI.TypeLine($"Enter the Author's New Name: ", sleepTime: 50, color: Color.Blue); UI.Escape();
                books[bookID].author = Console.ReadLine();


                UI.Load(times: new Random().Next(50, 150), displayMsg: "Loading Book...", sequenceCode: 1);
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
            foreach(Request request in requestedBooks)
            {
                UI.TypeLine($"Request ID: {request.requestID}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Name: {request.book.name}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Book Author: {request.book.author}", sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                UI.TypeLine($"Request Time {request.reqTime}", sleepTime: 20);
                Console.WriteLine("----------------------------------------------------------", Color.Blue);
                
            }
            logs.Add(new Log("Viewed Requested Books", this.userID, true));
            return;
        }

        public void seeLogs()
        {
            UI.ProgressBar(displayMsg: "Getting Logs...", interval: new Random().Next(100, 200));
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
                UI.TypeLine($"Status : {status}", log.status ? Color.Green : Color.Red, sleepTime: 20);
                UI.TypeLine(" || ", color: Color.Blue, sleepTime: 20);
                Console.WriteLine("----------------------------------------------------------", Color.Blue);
            }
            logs.Add(new Log("Viewed Logs", this.userID, true));
            return;
        }


    }


    
}

