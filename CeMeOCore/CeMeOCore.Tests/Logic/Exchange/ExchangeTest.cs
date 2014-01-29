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

            ExchangeImpl ex = new ExchangeImpl();
            ex.SendMail(username: username, password: password, domain: domain);

        }


    }
}
