using System;

namespace Independent_Study.Models
{
    public class Message
    {
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string MessageId { get; set; }
        public string User { get; set; }
        public string Group { get; set; }
        public string Body { get; set; }
    }
}