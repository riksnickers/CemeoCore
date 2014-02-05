using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CeMeOCore.Logic.Exchange;

namespace CeMeOCore.Tests.Logic.Exchange
{
    [TestClass]
    public class ExchangeTest
    {
        [TestMethod]
        public void ExchangeEmailTest()
        {
            string username = "aon_1";
            string password = "jefjef91";
            string domain = "cemeo.be";

            ExchangeImpl ex = new ExchangeImpl(username: username, password: password, domain: domain, userId: 1);
            ex.SendMail();
        }
    }
}
