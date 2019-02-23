using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace PrintNow
{
    public class chatHub : Hub
    {
        public async Task JoinRoom(string roomName)
        {
            await Groups.Add(Context.ConnectionId, roomName);
            Clients.Group(roomName).addChatMessage(Context.User.Identity.Name + " joined.");
        }

        public void Send(string name, string message,string group)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.Group(group).addNewMessageToPage(name, message);

        }
    }
}