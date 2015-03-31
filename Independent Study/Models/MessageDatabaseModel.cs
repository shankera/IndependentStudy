using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public IEnumerable<Message> GetByMessageId(long messageId)
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
    }
}