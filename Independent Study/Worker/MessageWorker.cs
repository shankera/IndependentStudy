using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Independent_Study.Models;

namespace Independent_Study.Worker
{
    public class MessageWorker
    {
        private static MessageDatabaseModel _model;
        public MessageWorker(MessageDatabaseModel model)
        {
            _model = model;
        }
        public static IEnumerable<Message> GetAllMessages()
        {
            return _model.GetAll();
        }

        public static IEnumerable<Message> GetMessageById(long messageId)
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
            return _model.GetByGroup(param);
        }
    }
}