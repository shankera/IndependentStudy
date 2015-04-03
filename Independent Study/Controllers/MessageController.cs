using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Independent_Study.Models;
using Independent_Study.Worker;

namespace Independent_Study.Controllers
{
    public class MessageController : ApiController
    {
        // GET: api/Message
        public IEnumerable<Message> Get()
        {
            return MessageWorker.GetAllMessages();
        }

        // GET: api/Message?userid=5 or api/Message?messageid=5
        public IEnumerable<Message> Get(int userId)
        {
             return MessageWorker.GetMessageByUser(userId);
        }

        // GET: api/Message~~~ <--- I have no idea how this will work lol
        public IEnumerable<Message> Get(DateTime startTime)
        {
            return MessageWorker.GetMessageAfterTime(startTime);
        }

        // GET: api/Message?strval=keyword
        public IEnumerable<Message> Get(string strVal)
        {
            return ParseHelper.Parse(Request);
        }

        // POST: api/Message?id=1
        public void Post(int id, string user, [FromBody]string value)
        {
            if(user == null) throw new HttpResponseException(HttpStatusCode.BadRequest);
            MessageWorker.PutNewMessage(id, user, null, value);
        }

        // PUT: api/
        public void Put(int id, string user, string channelId, [FromBody]string value)
        {
            if (user == null) throw new HttpResponseException(HttpStatusCode.BadRequest);
            MessageWorker.PutNewMessage(id, user, channelId, value);
        }

        // DELETE: api/
        public void Delete(int id)
        {

        }
    }
}
