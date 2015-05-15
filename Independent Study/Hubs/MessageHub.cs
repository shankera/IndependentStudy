using System;
using System.Collections.Generic;
using System.Net.Http;
using Independent_Study.Controllers;
using Independent_Study.Models;
using Independent_Study.Worker;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.SignalR;

namespace Independent_Study.Hubs
{
    public class MessageHub : Hub
    {
        public void Send(string message)
        {
            if (message == "") return; 
            var msg = new Message
            {
                User = "Anonymous",
                UserId = 1,
                Body = message,
                TimeStamp = DateTime.Now,
                Channel = "#general",
                MessageId = Guid.NewGuid().ToString()
            };
            Clients.All.addNewMessage(msg.ToString());
            MessageWorker.Initialize(new MessageDatabaseModel());
            MessageWorker.PutNewMessage(1, null, null, message);
        }
    }
}