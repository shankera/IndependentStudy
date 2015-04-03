using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Independent_Study.Models;

namespace Independent_Study.Worker
{
    public class ParseHelper
    {
        public static IEnumerable<Message> Parse(HttpRequestMessage request)
        {
            var vals = request.GetQueryNameValuePairs().ToList();
            if (vals.Count() != 1) return null;
            if (vals[0].Key.Equals("channelId", StringComparison.InvariantCultureIgnoreCase) && vals[0].Value.StartsWith("#"))
                return MessageWorker.GetMessagesByChannel(vals[0].Value);
            if (vals[0].Key.Equals("queryString", StringComparison.InvariantCultureIgnoreCase))
                return MessageWorker.GetMessagesContainingString(vals[0].Value);
            if (vals[0].Key.Equals("messageId", StringComparison.InvariantCultureIgnoreCase))
                return MessageWorker.GetMessageById(vals[0].Value);
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        } 
    }
}