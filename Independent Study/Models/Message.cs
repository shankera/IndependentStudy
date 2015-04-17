using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Independent_Study.Models
{
    public class Message
    {
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string MessageId { get; set; }
        public string User { get; set; }
        public string Channel { get; set; }
        public string Body { get; set; }
        public virtual List<Channel> Channels { get; set; }
    }

    public class Channel
    {
        public string ChannelName { get; set; }
        public virtual List<Message> Messages { get; set; } 
    }
    public class MessageContext : DbContext 
    { 
        public DbSet<Message> Messages { get; set; } 
        public DbSet<Channel> Channels { get; set; } 
    } 
}
