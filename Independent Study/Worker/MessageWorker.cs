using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using Independent_Study.Models;

namespace Independent_Study.Worker
{
    public class MessageWorker
    {
        private static IMessageDatabaseModel _model;

        private MessageWorker()
        {
            
        }
        public static void Initialize(IMessageDatabaseModel model)
        {
            _model = model;
        }
        public static IEnumerable<Message> GetAllMessages()
        {
            return _model.GetAll();
        }

        public static IEnumerable<Message> GetMessageById(string messageId)
        {
            return _model.GetByMessageId(messageId);
        }

        public static IEnumerable<Message> GetMessageByUser(int userId)
        {
            return _model.GetByUserId(userId);
        }

        public static IEnumerable<Message> GetMessageAfterTime(DateTime startTime)
        {
            return _model.GetAfterTime(startTime);
        }
        public static IEnumerable<Message> GetMessagesByChannel(string param)
        {
            return _model.GetByChannel(param);
        }

        public static IEnumerable<Message> GetMessagesContainingString(string value)
        {
            return _model.GetMessagesContaining(value);
        }

        public static void PutNewMessage(int userId, string user, string channel, string value)
        {
            if(userId == -1)throw new ArgumentException("UserID does not exist.", "userId");
            var channelString = channel ?? "#general";
            var messageId = Guid.NewGuid().ToString();
            var userName = user ?? "Anonymous";
            _model.PutNewMessage(new Message
            {
                User = userName,
                UserId = userId,
                MessageId = messageId,
                Channel = channelString,
                Body = value,
                TimeStamp = DateTime.Now
            });
        }
    }
}