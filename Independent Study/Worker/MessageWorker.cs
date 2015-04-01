﻿using System;
using System.Collections.Generic;
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
            return _model.GetByGroup(param);
        }

        public static IEnumerable<Message> GetMessagesContainingString(string value)
        {
            return _model.GetMessagesContaining(value);
        }

        public static void PutNewMessage(int userId, string user, string channel, string value)
        {
            var channelString = channel ?? "#general";
            var messageId = Guid.NewGuid().ToString();
            _model.PutNewMessage(new Message
            {
                UserId = userId,
                MessageId = messageId,
                Channel = channelString,
                Body = value,
                TimeStamp = DateTime.Now
            });
        }
    }
}