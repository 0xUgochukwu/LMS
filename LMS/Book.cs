using System;
namespace LMS
{
    public class Book
    {
        public string ID { get; }
        public string name;
        public string author;
        public string regDateTime;
        public bool isBorrowed;
        public string dueDate;

        public Book(string bookName, string bookAuthor)
        {
            this.ID = "BB" + Convert.ToString(new Random().Next(10000, 99999));
            this.regDateTime = DateTime.Now.ToString();
            this.isBorrowed = false;

            this.name = bookName;
            this.author = bookAuthor;
        }

        public void bookBorrowed(int borrowPeriod)
        {
            this.isBorrowed = true;
            this.dueDate = DateTime.Now.AddDays(borrowPeriod).ToString("dd/MM/yyyy");

        }
    }
}

