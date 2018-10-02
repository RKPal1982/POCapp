using System;
using System.Collections.Generic;
using System.Linq;
using Solutions.OnlineSelling.Model;

namespace Solutions.OnlineSelling.BusinessLogic
{
    public class ChatHub
    {
        #region Connect
        public static ChatUserDetail GetUser(string userName, string email, string id)
        {
            using (var dc = new SolutionsOnlineSellingEntities())
            {
                var item = dc.ChatUserDetail.FirstOrDefault(x => x.EmailID == email);
                if (item != null)
                {
                    dc.ChatUserDetail.Remove(item);
                    dc.SaveChanges();
                }

                var Users = dc.ChatUserDetail.ToList();
                if (Users.Where(x => x.EmailID == email).ToList().Count == 0)
                {
                    var userdetails = new ChatUserDetail
                    {
                        ConnectionId = id,
                        UserName = userName,
                        EmailID = email
                    };
                    dc.ChatUserDetail.Add(userdetails);
                    dc.SaveChanges();                    
                }

                return dc.ChatUserDetail.FirstOrDefault(x => x.EmailID == email);
            }
        }

        public static List<ChatUserDetail> GetConnectedUsers()
        {
            using (var dc = new SolutionsOnlineSellingEntities())
            {
                return dc.ChatUserDetail.ToList();
            }
        }

        public static List<ChatMessageDetail> GetCurrentMessages()
        {
            using (var dc = new SolutionsOnlineSellingEntities())
            {
                return dc.ChatMessageDetail.ToList();
            }
        }

        #endregion

        #region Disconnect
        public static ChatUserDetail GetUser(string id)
        {
            using (var dc = new SolutionsOnlineSellingEntities())
            {
                return dc.ChatUserDetail.FirstOrDefault(x => x.ConnectionId == id);
            }
        }
        public static void RemoveUser(string id)
        {
            using (var dc = new SolutionsOnlineSellingEntities())
            {
                var item = dc.ChatUserDetail.FirstOrDefault(x => x.ConnectionId == id);
                if (item != null)
                {
                    dc.ChatUserDetail.Remove(item);
                    dc.SaveChanges();                  
                }
            }           
        }
        #endregion

       

        #region Private_Messages
        
        public static List<PrivateChatMessage> GetPrivateMessage(string fromid, string toid, int take)
        {
            using (var dc = new SolutionsOnlineSellingEntities())
            {
                var msg = new List<PrivateChatMessage>();

                var v = (from a in dc.ChatPrivateMessageMaster
                         join b in dc.ChatPrivateMessageDetails on a.EmailID equals b.MasterEmailID into cc
                         from c in cc
                         where (c.MasterEmailID.Equals(fromid) && c.ChatToEmailID.Equals(toid)) || (c.MasterEmailID.Equals(toid) && c.ChatToEmailID.Equals(fromid))
                         orderby c.ID descending
                         select new
                         {
                             UserName = a.UserName,
                             Message = c.Message,
                             ID = c.ID
                         }).Take(take).ToList();
                v = v.OrderBy(s => s.ID).ToList();

                foreach (var a in v)
                {
                    var res = new PrivateChatMessage()
                    {
                        userName = a.UserName,
                        message = a.Message
                    };
                    msg.Add(res);
                }
                return msg;
            }
        }

       
        public static List<PrivateChatMessage> GetScrollingChatData(string fromid, string toid, int takeCounter, int skipCounter)
        {           
            using (var dc = new SolutionsOnlineSellingEntities())
            {
                var msg = new List<PrivateChatMessage>();
                var v = (from a in dc.ChatPrivateMessageMaster
                         join b in dc.ChatPrivateMessageDetails on a.EmailID equals b.MasterEmailID into cc
                         from c in cc
                         where (c.MasterEmailID.Equals(fromid) && c.ChatToEmailID.Equals(toid)) || (c.MasterEmailID.Equals(toid) && c.ChatToEmailID.Equals(fromid))
                         orderby c.ID descending
                         select new
                         {
                             UserName = a.UserName,
                             Message = c.Message,
                             ID = c.ID
                         }).Take(takeCounter).Skip(skipCounter).ToList();

                foreach (var a in v)
                {
                    var res = new PrivateChatMessage()
                    {
                        userName = a.UserName,
                        message = a.Message
                    };
                    msg.Add(res);
                }
                return msg;
            }
        }
        #endregion

        #region Save_Cache
        public static void AddAllMessageinCache(string userName, string message, string emailIDLoaded)
        {
            using (var dc = new SolutionsOnlineSellingEntities())
            {
                var messageDetail = new ChatMessageDetail
                {
                    UserName = userName,
                    Message = message,
                    EmailID = emailIDLoaded
                };
                dc.ChatMessageDetail.Add(messageDetail);
                dc.SaveChanges();
            }
        }

        public static void AddPrivateMessageinCache(string fromEmail, string chatToEmail, string userName, string message)
        {
            using (var dc = new SolutionsOnlineSellingEntities())
            {
                // Save master
                var master = dc.ChatPrivateMessageMaster.ToList().Where(a => a.EmailID.Equals(fromEmail)).ToList();
                if (master.Count == 0)
                {
                    var result = new ChatPrivateMessageMaster
                    {
                        EmailID = fromEmail,
                        UserName = userName
                    };
                    dc.ChatPrivateMessageMaster.Add(result);
                    dc.SaveChanges();
                }

                // Save details
                var resultDetails = new ChatPrivateMessageDetails
                {
                    MasterEmailID = fromEmail,
                    ChatToEmailID = chatToEmail,
                    Message = message
                };
                dc.ChatPrivateMessageDetails.Add(resultDetails);
                dc.SaveChanges();
            }
        }
        #endregion
    }


}