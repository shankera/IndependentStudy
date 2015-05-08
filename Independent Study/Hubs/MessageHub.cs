using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Independent_Study.Models;
using Microsoft.AspNet.SignalR.Hubs;

namespace Independent_Study.Hubs
{
    [HubName("MessageHub")]
    public class MessageHub : Hub
    {
        public void Send(string message)
        {
            Clients.All.addNewMessage(message);
        }
    }
}