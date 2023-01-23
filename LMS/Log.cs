using System;
namespace LMS
{
    public class Log
    {
        public string message;
        public string ID { get; }
        public string timeStamp;
        public string actorID;
        public bool status;
        public Log(string logMessage, string actorID, bool status)
        {
            this.status = status;
            this.actorID = actorID;
            this.message = logMessage;
            this.ID = "LOG" + Convert.ToString(new Random().Next(10000, 99999));
            this.timeStamp = DateTime.Now.ToString();
    }
    }
}

