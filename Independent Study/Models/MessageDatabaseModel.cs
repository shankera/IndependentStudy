using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Independent_Study.Models
{
    public class MessageDatabaseModel : IMessageDatabaseModel
    {
        public IEnumerable<Message> GetByChannel(string channelName)
        {
            using(var db = new MessageContext())
            {
                return db.Messages.Where(x => x.Channel.Equals(channelName)).ToList();
            }
        }

        public IEnumerable<Message> GetAll()
        {
            using (var db = new MessageContext())
            {
                return db.Messages.ToList();
            }
        }

        public IEnumerable<Message> GetByMessageId(string messageId)
        {
            using (var db = new MessageContext())
            {
                return db.Messages.Where(x => x.MessageId.Equals(messageId));
            }
        }

        public IEnumerable<Message> GetByUserId(int userId)
        {
            using (var db = new MessageContext())
            {
                return db.Messages.Where(x => x.UserId == userId);
            }
        }

        public IEnumerable<Message> GetAfterTime(DateTime startTime)
        {
            using (var db = new MessageContext())
            {
                return db.Messages.Where(x => x.TimeStamp > startTime);
            }
        }

        public IEnumerable<Message> GetMessagesContaining(string value)
        {
            using (var db = new MessageContext())
            {
                return db.Messages.Where(x => x.Body.Contains(value));
            }
        }

        public void PutNewMessage(Message message)
        {
            using (var db = new MessageContext())
            {
                db.Messages.Add(message);
            }
        }
    }
}