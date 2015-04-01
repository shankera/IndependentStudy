using System.Collections.Generic;
using System.Linq;
using Independent_Study.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace Independent_Study.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private List<Message> _dataSet;
        private Fixture _fixture;
        private Mock<MessageDatabaseModel> _mock;

        [ClassInitialize]
        private void ClassSetup()
        {
            _fixture = new Fixture();
            _mock = new Mock<MessageDatabaseModel>();
        }

        [TestInitialize]
        private void SetUp()
        {
            _dataSet = _fixture.CreateMany<Message>().ToList();
        }
        [TestMethod]
        public void GetAll()
        {

        }
        [TestMethod]
        public void GetMessageUserIdValid()
        {

        }
        [TestMethod]
        public void GetMessageUserIdInvalid()
        {

        }
        [TestMethod]
        public void GetMessagesMessageIdValid()
        {

        }
        [TestMethod]
        public void GetMessagesMessageIdInvalid()
        {

        }
        [TestMethod]
        public void GetMessagesDateTimeValid()
        {

        }
        [TestMethod]
        public void GetMessagesDateTimeInvalid()
        {

        }
        [TestMethod]
        public void GetMessagesChannelValid()
        {

        }
        [TestMethod]
        public void GetMessagesChannelInvalid()
        {

        }
        [TestMethod]
        public void GetMessagesQuery()
        {

        }

        [TestMethod]
        public void PostValid()
        {
            
        }

        [TestMethod]
        public void PostChannelNull()
        {
            
        }
    }
}
