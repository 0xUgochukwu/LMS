using System;
namespace LMS
{
    public class Request
    {
        public string requestID { get; }
        public Book book;
        public string reqTime;
        public Request(Book book)
        {
            this.book = book;
            this.requestID = "RE" + Convert.ToString(new Random().Next(10000, 99999));
            this.reqTime = DateTime.Now.ToString();
        }
    }
}

