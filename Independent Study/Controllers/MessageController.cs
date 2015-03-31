using System;
using System.Collections.Generic;
using System.Linq;
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

        // GET: api/Message/5
        public IEnumerable<Message> Get(int userId)
        {
            var vals = Request.GetQueryNameValuePairs().ToList();
            if (vals.Count() != 1) return null;
            if (vals[0].Key.Equals("userId", StringComparison.InvariantCultureIgnoreCase))
                return MessageWorker.GetMessageByUser(Int32.Parse(vals[0].Value));
            return vals[0].Key.Equals("messageId", StringComparison.InvariantCultureIgnoreCase) 
                ? MessageWorker.GetMessageById(Int32.Parse(vals[0].Value)) : null;
        }

        // GET: api/Message/5
        public IEnumerable<Message> Get(DateTime startTime)
        {
            return MessageWorker.GetMessageAfterTime(startTime);
        }

        // GET: api/Message/5
        public IEnumerable<Message> Get(string strVal)
        {
            var vals = Request.GetQueryNameValuePairs().ToList();
            if (vals.Count() != 1) return null;
            if (vals[0].Key.Equals("channelId", StringComparison.InvariantCultureIgnoreCase) && vals[0].Value.StartsWith("#"))
                return MessageWorker.GetMessagesByChannel(vals[0].Value);
            return vals[0].Key.Equals("messageId", StringComparison.InvariantCultureIgnoreCase)
                ? MessageWorker.GetMessageById(Int32.Parse(vals[0].Value)) : null;
        }

        // POST: api/Message
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Message/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Message/5
        public void Delete(int id)
        {
        }
    }
}
