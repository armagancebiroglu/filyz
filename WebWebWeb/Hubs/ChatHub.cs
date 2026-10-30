using System;
using System.Web;
using Microsoft.AspNet.SignalR;
namespace SignalRChat
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message, string forWho, string image, int id)
        {
            // Call the addNewMessageToPage method to update clients. ,string forWho 
            Clients.All.addNewMessageToPage(name, message, forWho, image, id);

        }
    }
}