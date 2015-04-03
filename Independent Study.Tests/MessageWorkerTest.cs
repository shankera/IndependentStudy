using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Independent_Study.Models;
using Independent_Study.Worker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace Independent_Study.Tests
{
    [TestClass]
    public class MessageWorkerTest
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IMessageDatabaseModel> _mock= new Mock<IMessageDatabaseModel>();
        
        [TestMethod]
        public void GetAll()
        {
            var data = _fixture.CreateMany<Message>().ToList();
            _mock.Setup(x => x.GetAll()).Returns(data);

            MessageWorker.Initialize(_mock.Object);

            Assert.IsNotNull(MessageWorker.GetAllMessages());
            Assert.IsTrue(MessageWorker.GetAllMessages().Any());
        }
        [TestMethod]
        public void GetMessageUserIdValid()
        {
            var data = _fixture.CreateMany<Message>().ToList();
            var ids = data.Select(x => x.UserId);
            _mock.Setup(x => x.GetByUserId(It.IsIn(ids))).Returns(data);
            _mock.Setup(x => x.GetByUserId(It.IsNotIn(ids))).Returns((IEnumerable<Message>) null);

            MessageWorker.Initialize(_mock.Object);

            var idList = ids.ToList();

            Assert.IsNotNull(MessageWorker.GetMessageByUser(idList.First()));
            Assert.IsTrue(MessageWorker.GetMessageByUser(idList.First()).Any());
        }
        [TestMethod]
        public void GetMessageUserIdInvalid()
        {
            var data = _fixture.CreateMany<Message>().ToList();
            var ids = data.Select(x => x.UserId+10);
            _mock.Setup(x => x.GetByUserId(It.IsIn(ids))).Returns(data);
            _mock.Setup(x => x.GetByUserId(It.IsNotIn(ids))).Returns((IEnumerable<Message>)null);

            MessageWorker.Initialize(_mock.Object);

            Assert.IsNull(MessageWorker.GetMessageByUser(-1));
            Assert.IsNull(MessageWorker.GetMessageByUser(0));

        }
        [TestMethod]
        public void GetMessagesMessageIdValid()
        {
            var data = _fixture.CreateMany<Message>().ToList();
            var ids = data.Select(x => x.MessageId).ToList();
            _mock.Setup(x => x.GetByMessageId(ids[0])).Returns(data.Where(y => y.MessageId.Equals(ids[0])));
            _mock.Setup(x => x.GetByMessageId(It.IsNotIn(ids.Select(y => y)))).Returns((IEnumerable<Message>)null);

            MessageWorker.Initialize(_mock.Object);

            Assert.IsNotNull(MessageWorker.GetMessageById(ids.First()));
            Assert.IsTrue(MessageWorker.GetMessageById(ids.First()).Any());
        }
        [TestMethod]
        public void GetMessagesMessageIdInvalid()
        {
            var data = _fixture.CreateMany<Message>().ToList();
            var ids = data.Select(x => x.MessageId);
            _mock.Setup(x => x.GetByMessageId(It.IsIn(ids))).Returns(data);
            _mock.Setup(x => x.GetByMessageId(It.IsNotIn(ids))).Returns((IEnumerable<Message>)null);

            MessageWorker.Initialize(_mock.Object);

            Assert.IsNull(MessageWorker.GetMessageById("badmessageid"));
            Assert.IsNull(MessageWorker.GetMessageById(""));

        }
        [TestMethod]
        public void GetMessagesDateTimeValid()
        {
            var data = _fixture.CreateMany<Message>(10).ToList();
            var subSet = data.OrderBy(x => x.TimeStamp).Take(5).ToList();
            var validDateTime = subSet.Last().TimeStamp;
            _mock.Setup(x => x.GetAfterTime(validDateTime)).Returns(subSet);
            _mock.Setup(x => x.GetAfterTime(It.IsNotIn(subSet.Select(t=>t.TimeStamp)))).Returns((IEnumerable<Message>)null);

            MessageWorker.Initialize(_mock.Object);
            
            Assert.IsNotNull(MessageWorker.GetMessageAfterTime(validDateTime));
            Assert.IsFalse(MessageWorker.GetMessageAfterTime(validDateTime).Any(x=>x.TimeStamp>validDateTime));
        }
        [TestMethod]
        public void GetMessagesDateTimeInvalid()
        {
            var data = _fixture.CreateMany<Message>(10).ToList();
            var subSet = data.OrderByDescending(x => x.TimeStamp).Take(5).ToList();
            var validDateTime = subSet.Last().TimeStamp;
            var invalidDateTime = data.OrderBy(x => x.TimeStamp).First().TimeStamp.AddHours(1);
            _mock.Setup(x => x.GetAfterTime(validDateTime)).Returns(subSet);
            _mock.Setup(x => x.GetAfterTime(It.IsNotIn(subSet.Select(t => t.TimeStamp)))).Returns((IEnumerable<Message>)null);

            MessageWorker.Initialize(_mock.Object);
  
            Assert.IsNull(MessageWorker.GetMessageAfterTime(invalidDateTime));
        }
        [TestMethod]
        public void GetMessagesChannelValid()
        {
            var validChannels = new [] {"#general", "#random", "#important"};
            var data = _fixture.CreateMany<Message>().ToList();
            data.ForEach(x => x.Channel = validChannels[0]);
            var data2 = _fixture.CreateMany<Message>().ToList();
            data2.ForEach(x => x.Channel = validChannels[1]);
            var data3 = _fixture.CreateMany<Message>().ToList();
            data3.ForEach(x => x.Channel = validChannels[2]);
            data.AddRange(data2);
            data.AddRange(data3);

            _mock.Setup(x => x.GetByChannel("#general")).Returns(data.Where(x => x.Channel.Equals("#general")));
            _mock.Setup(x => x.GetByMessageId(It.IsNotIn(validChannels))).Returns((IEnumerable<Message>)null);

            MessageWorker.Initialize(_mock.Object);

            Assert.IsNotNull(MessageWorker.GetMessagesByChannel("#general"));
            Assert.IsTrue(MessageWorker.GetMessagesByChannel("#general").ToList().Any(x => data.Contains(x)));
        }
        [TestMethod]
        public void GetMessagesChannelInvalid()
        {
            var validChannels = new[] { "#general", "#random", "#important" };
            var data = _fixture.CreateMany<Message>().ToList();
            data.ForEach(x => x.Channel = validChannels[0]);
            var data2 = _fixture.CreateMany<Message>().ToList();
            data2.ForEach(x => x.Channel = validChannels[1]);
            var data3 = _fixture.CreateMany<Message>().ToList();
            data3.ForEach(x => x.Channel = validChannels[2]);
            data.AddRange(data2);
            data.AddRange(data3);

            var z = It.IsNotIn(validChannels);
            _mock.Setup(x => x.GetByChannel("#general")).Returns(data.Where(x => x.Channel.Equals("#general")));
            _mock.Setup(x => x.GetByMessageId(It.IsNotIn(validChannels))).Returns((IEnumerable<Message>)null);

            MessageWorker.Initialize(_mock.Object);

            Assert.IsFalse(MessageWorker.GetMessagesByChannel("#invalid").ToList().Any(x => data.Contains(x)));
        }
        [TestMethod]
        public void GetMessagesQuery()
        {
            const string searchVar = "word";
            var data = _fixture.CreateMany<Message>().ToList();
            var data2 = _fixture.CreateMany<Message>().ToList();
            data2.ForEach(x =>x.Body+=searchVar);
            data.AddRange(data2);


            _mock.Setup(x => x.GetMessagesContaining(searchVar)).Returns(data.Where(x => x.Body.Contains(searchVar)));

            MessageWorker.Initialize(_mock.Object);

            Assert.IsTrue(MessageWorker.GetMessagesContainingString("word").ToList().Any(x => data.Contains(x)));
        }

        [TestMethod]
        public void PostValid()
        {
            var data = _fixture.CreateMany<Message>().ToList();
            var count = data.Count;
            _mock.Setup(x => x.PutNewMessage(It.IsAny<Message>())).Callback((Message m) => data.Add(m));

            MessageWorker.Initialize(_mock.Object);
            MessageWorker.PutNewMessage(1, "user", "#reactive", "Improptu comments are hard to think of.");
            Assert.IsTrue(data.Count == ++count);
            MessageWorker.PutNewMessage(1, "user", "#reactive", "Improptu comments are hard to think of.");
            Assert.IsTrue(data.Count == ++count);
            MessageWorker.PutNewMessage(2, null, "#reactive", "Improptu comments are hard to think of.");
            Assert.IsTrue(data.Count == ++count);
            MessageWorker.PutNewMessage(3, "user", null, "Improptu comments are hard to think of.");
            Assert.IsTrue(data.Count == ++count);
            MessageWorker.PutNewMessage(3, "user", "#reactive", null);
            Assert.IsTrue(data.Count == ++count);
            MessageWorker.PutNewMessage(4, null, null, null);
            Assert.IsTrue(data.Count == ++count);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void PostInvalid()
        {
            var data = _fixture.CreateMany<Message>().ToList();
            var count = data.Count;
            _mock.Setup(x => x.PutNewMessage(It.IsAny<Message>())).Callback((Message m) => data.Add(m));

            MessageWorker.Initialize(_mock.Object);
            MessageWorker.PutNewMessage(-1,null,null,null);
        }
    }
}
