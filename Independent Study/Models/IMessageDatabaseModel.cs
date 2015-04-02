using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Security.Cryptography.X509Certificates;

namespace Independent_Study.Models
{
    public interface IMessageDatabaseModel
    {
        IEnumerable<Message> GetAll();
        IEnumerable<Message> GetByChannel(string channelName);
        IEnumerable<Message> GetByMessageId(string messageId);
        IEnumerable<Message> GetByUserId(int userId);
        IEnumerable<Message> GetAfterTime(DateTime startTime);
        IEnumerable<Message> GetMessagesContaining(string value);
        void PutNewMessage(Message message);
    }
}