using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;

namespace LMS
{

    
    public class Client : User
    {
        public Dictionary<string, Book> borrowedBooks = new Dictionary<string, Book>();

        public Client(string firstName, string lastName, string age, string address, string password) : base(firstName, lastName, age, address, password)
        {
            this.userID = "CL" + Convert.ToString(new Random().Next(10000, 99999));
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            this.address = address;
            this.password = password;
        }

        public void displayPersonalDetails()
        {
            

            string inputPassword = UI.getPassword();
            if (inputPassword == password)
            {
                UI.Escape(); UI.TypeLine($"======= YOUR PERSONAL DETAILS ======", Color.DarkBlue); UI.Escape();
                Console.WriteLine("----------------------------------------------------------", Color.Blue);
                UI.TypeLine($"ID: {userID}\n");
                UI.TypeLine($"Name: {firstName} {lastName}\n");
                UI.TypeLine($"Age: {age}\n");
                UI.TypeLine($"Address: {address}\n");
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

        public void requestABook()
        {
            UI.TypeLine("Enter Book Name: ", Color.Blue);
            string bookName = Console.ReadLine();

            UI.TypeLine("Enter Book Author: ", Color.Blue);
            string bookAuthor = Console.ReadLine();

            requestedBooks.Add(new Request(new Book(bookName, bookAuthor)));

            logs.Add(new Log($"Requested a Book", this.userID, true));
        }


        public void borrowBook()
        {
            UI.TypeLine("Enter the ID of the Book you wish to borrow from Aptech Library: ", Color.Blue);
            string bookID = Console.ReadLine();

            if (borrowedBooks.ContainsKey(bookID))
            {
                UI.TypeLine("In how many days do you wish to return our book? ", Color.Blue);
                int dueDays;
                try
                {
                    dueDays = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    UI.throwError("Something is wrong with your input and by default the due date has been automatically set to three days from now");
                    dueDays = 3;
                }
                string inputPassword = UI.getPassword();

                if (inputPassword == this.password)
                {
                    UI.Load(times: new Random().Next(50, 150), displayMsg: "Getting Book...", sequenceCode: 1);
                    UI.ProgressBar(displayMsg: "Updating Your Book Inventory....", interval: new Random().Next(100, 200));
                    if (restrictedClients.ContainsKey(this.userID))
                    {
                        UI.throwError("You haven't returned the book you borrowed and you want to collect another one, Is that not Madness?\n Come on will you get out from here before Is slap you..");
                        logs.Add(new Log($"Borrowed Book {bookID}", this.userID, false));
                        return;
                    }
                    borrowedBooks.Add(bookID, books[bookID]);
                    BorrowedBooks.Add(books[bookID]);
                    books[bookID].bookBorrowed(dueDays);
                    UI.ShowSuccess($"Book {bookID} borrowed successfully, \n Nice choice, I hope you enjoy reading this ;)");

                    logs.Add(new Log($"Borrowed Book: {bookID}", this.userID, true));
                    return;

                }
                else
                {
                    UI.passwordErr();
                    logs.Add(new Log($"Borrowed Book: {bookID}", this.userID, false));
                    return;
                }

            }
            else
            {
                UI.throwError($"Book: {bookID} does not exist\n");
                logs.Add(new Log($"Borrowed Book: {bookID}", this.userID, false));
                return;
            }
        }

        public void returnBook()
        {
            UI.TypeLine("Enter the ID of the Book you wish to return: ", Color.Blue);
            string bookID = Console.ReadLine();

            if (borrowedBooks.ContainsKey(bookID))
            {
                string inputPassword = UI.getPassword();

                if(inputPassword == this.password)
                {
                    UI.Load(times: new Random().Next(50, 150), displayMsg: "Returning Book...", sequenceCode: 1);
                    UI.ProgressBar(displayMsg: "Updating Book Inventory....", interval: new Random().Next(100, 200));
                    borrowedBooks.Remove(bookID);
                    books[bookID].isBorrowed = false;
                    UI.ShowSuccess($"Book {bookID} returned successfully, \n I hope you had a good read ;)");

                    logs.Add(new Log($"Returned Book: {bookID}", this.userID, true));
                    return;
                    
                }
                else
                {
                    UI.passwordErr();
                    logs.Add(new Log($"Returned Book:{bookID}", this.userID, false));
                    return;
                }

            }
            else
            {
                UI.throwError($"Book: {bookID} does not exist\n");
                logs.Add(new Log($"Returned Book: {bookID}", this.userID, false));
                return;
            }
        }

        public void listBooksInPossession()
        {
            UI.Escape(); UI.TypeLine($"======= HERE ARE THE BOOKS YOU HAVE ======", Color.DarkBlue); UI.Escape();
            foreach (Book book in borrowedBooks.Values)
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
                logs.Add(new Log($"Client {this.userID} Viewed Books In Possession", this.userID, true));
                return;
            }
        }
    }
}

