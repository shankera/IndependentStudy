using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web.Http;
using Independent_Study.Models;
using Independent_Study.Worker;

namespace Independent_Study.Controllers
{
    public class MessageController : ApiController
    {
        public IEnumerable<Message> Get()
        {
            return MessageWorker.GetAllMessages();
        }

        [WebGet(UriTemplate = "?userId={userId}")]
        [OperationContract]
        public IEnumerable<Message> GetByUser(int userId)
        {
             return MessageWorker.GetMessageByUser(userId);
        }

        [WebGet(UriTemplate = "?afterTime={startTime}")]
        [OperationContract]
        public IEnumerable<Message> GetAfterStartTime(DateTime startTime)
        {
            return MessageWorker.GetMessageAfterTime(startTime);
        }

        [WebGet(UriTemplate = "?channelId={channelId}")]
        [OperationContract]
        public IEnumerable<Message> GetByChannel(string channelId)
        {
            return MessageWorker.GetMessagesByChannel(channelId);
        }

        [WebGet(UriTemplate = "?queryString={strVal}")]
        [OperationContract]
        public IEnumerable<Message> GetByQuery(string strVal)
        {
            return MessageWorker.GetMessagesContainingString(strVal);
        }
        
        [WebGet(UriTemplate = "?messageId={messageId}")]
        [OperationContract]
        public IEnumerable<Message> GetByMessage(string messageId)
        {
            return MessageWorker.GetMessageById(messageId);
        }
        
        [WebInvoke(Method = "PUT", UriTemplate = "user/{username}/Id/{userId}/{strVal}")]
        [OperationContract]
        public void Post(string username, int id, string strVal)
        {
            MessageWorker.PutNewMessage(id, username, null, strVal);
        }

        [WebInvoke(Method = "PUT", UriTemplate = "user/{username}/Id/{userId}/{channel}{strVal}")]
        [OperationContract]
        public void Post(string username, int id, string channel, string strVal)
        {
            MessageWorker.PutNewMessage(id, username, channel, strVal);
        }

        // DELETE: api/
        public void Delete(int id)
        {

        }
    }
}
