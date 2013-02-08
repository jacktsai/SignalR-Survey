using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

public class ChatHub : Hub
{
    public void Send(string name, string message)
    {
        Clients.All.broadcastMessage(name, string.Format("[{0:HH':'mm':'ss}] {1}", DateTime.Now, message));
    }
}