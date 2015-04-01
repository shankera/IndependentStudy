using System;
using System.Collections.Generic;

namespace Independent_Study.Models
{
    public class MessageDatabaseModel
    {
        public IEnumerable<Message> GetByGroup(string s)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetByMessageId(string messageId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetAfterTime(DateTime startTime)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetMessagesContaining(string value)
        {
            throw new NotImplementedException();
        }

        public void PutNewMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}