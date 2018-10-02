using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Solutions.OnlineSelling.Model;

namespace Solutions.OnlineSelling.Web
{
    public class ChatHub : Hub
    {
        public static string emailIDLoaded = "";

        #region Connect
        public void Connect(string userName, string email)
        {
            emailIDLoaded = email;
            var id = Context.ConnectionId;
            var chatUserDetail = BusinessLogic.ChatHub.GetUser(userName, email, id);

            // Disconnect
            Clients.All.onUserDisconnectedExisting(chatUserDetail.ConnectionId, chatUserDetail.UserName);


            // send to caller
            var connectedUsers = BusinessLogic.ChatHub.GetConnectedUsers();
            var CurrentMessage = BusinessLogic.ChatHub.GetCurrentMessages();
            Clients.Caller.onConnected(id, userName, connectedUsers, CurrentMessage);


            // send to all except caller client
            Clients.AllExcept(id).onNewUserConnected(id, userName, email);

        }
        #endregion

        #region Disconnect
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var id = Context.ConnectionId;
            var profile = BusinessLogic.ChatHub.GetUser(id);
            BusinessLogic.ChatHub.RemoveUser(id);
            Clients.All.onUserDisconnected(id, profile.UserName);

            return base.OnDisconnected(stopCalled);
        }
        #endregion

        #region Send_To_All
        public void SendMessageToAll(string userName, string message)
        {
            // store last 100 messages in cache
            BusinessLogic.ChatHub.AddAllMessageinCache(userName, message, emailIDLoaded);

            // Broad cast message
            Clients.All.messageReceived(userName, message);
        }
        #endregion

        #region Private_Messages
        public void SendPrivateMessage(string toUserId, string message, string status)
        {
            string fromUserId = Context.ConnectionId;

            var toUser = BusinessLogic.ChatHub.GetUser(toUserId);
            var fromUser = BusinessLogic.ChatHub.GetUser(fromUserId);
            if (toUser != null && fromUser != null)
            {
                if (status == "Click") BusinessLogic.ChatHub.AddPrivateMessageinCache(fromUser.EmailID, toUser.EmailID, fromUser.UserName, message);

                // send to 
                Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, message, fromUser.EmailID, toUser.EmailID, status, fromUserId);

                // send to caller user
                Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message, fromUser.EmailID, toUser.EmailID, status, fromUserId);
            }

        }
        public List<PrivateChatMessage> GetPrivateMessage(string fromid, string toid, int take)
        {
            return BusinessLogic.ChatHub.GetPrivateMessage(fromid, toid, take);
        }

        private int takeCounter = 0;
        private int skipCounter = 0;
        public List<PrivateChatMessage> GetScrollingChatData(string fromid, string toid, int start = 10, int length = 1)
        {
            takeCounter = (length * start); // 20
            skipCounter = ((length - 1) * start); // 10

            return BusinessLogic.ChatHub.GetScrollingChatData(fromid, toid, takeCounter, skipCounter);
        }
        #endregion
    }
}