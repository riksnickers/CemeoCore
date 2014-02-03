using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CeMeOCore.Logic.ActiveDirectory;
using CeMeOCore.DAL.Models;

namespace CeMeOCore.Tests.Logic.ActiveDirectory
{
    [TestClass]
    public class CemeoADTest
    {
        [TestMethod]
        public void AuthenticateActiveDirectory()
        {
            string username = "tomboffe";
            string password = "jefjef91";
            string domain = "cemeo.be";
            bool val = CemeoAD.Authenticate(username: username, password: password, domain: domain);
            Assert.IsTrue(val);
        }

        [TestMethod]
        public void GetNameFromUserAD()
        {
            string name = CemeoAD.GetUserIdFromDisplayName("tychabuekers", "jefjef91");
            Assert.AreEqual("tycha", name);
        }

        [TestMethod]
        public void GetRegisterBindingModelFromAD()
        {
            RegisterBindingModel erbm = CemeoAD.GetRegisterBindingModelFromAD("tomboffe", "jefjef91");

            Assert.AreEqual("tom", erbm.FirstName);
            Assert.AreEqual("boffe", erbm.LastName);
            Assert.AreEqual("tomboffe@cemeo.be", erbm.EMail);
            Assert.AreEqual("tomboffe", erbm.UserName);
            Assert.AreEqual("jefjef91", erbm.Password);
            Assert.AreEqual("jefjef91", erbm.ConfirmPassword);

            erbm = CemeoAD.GetRegisterBindingModelFromAD("tychabuekers", "jefjef91");

            Assert.AreEqual("tycha", erbm.FirstName);
            Assert.AreEqual("buekers", erbm.LastName);
            Assert.AreEqual("tychabuekers@cemeo.be", erbm.EMail);
            Assert.AreEqual("tychabuekers", erbm.UserName);
            Assert.AreEqual("jefjef91", erbm.Password);
            Assert.AreEqual("jefjef91", erbm.ConfirmPassword);
        }
    }
}
