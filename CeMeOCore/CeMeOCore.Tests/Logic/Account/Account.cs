using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CeMeOCore.Tests.Logic.Account
{
    /// <summary>
    /// Summary description for AccountTest
    /// </summary>
    [TestClass]
    public class Account
    {
        public Account()
        {/*Nothing*/}

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestPasswordEncryption()
        {
            /*
             * http://www.miniwebtool.com/sha256-hash-generator/ SHA256
             * http://www.miniwebtool.com/sha384-hash-generator/ SHA384
             * */

            String Expected = "76039cf0241f76e5b2c5a5ed788676e71f3e6180c77da6c25aa66aeae1a48361c7d756f465fbc8f6030c92a386b11408"; //password + usernamehash mix
            String Actual = "";
            String inputUsername = "username"; // username + secret : 816af08bcbc8afe5228670c14ce2f37a23f823ad2ed9e3554da34f456c2975e7
            String inputPassword = "password"; 
            CeMeOCore.Logic.Account.Account a = new CeMeOCore.Logic.Account.Account();
            Actual = a.EncrypPassword(inputPassword, inputUsername);
            Assert.AreEqual(Expected, Actual, "The encryption did not succeed");
        }

        [TestMethod]
        public void TestHashingSHA256()
        {
            String Expected = "816af08bcbc8afe5228670c14ce2f37a23f823ad2ed9e3554da34f456c2975e7";
            System.Diagnostics.Debug.WriteLine("SHA256 length: " + Expected.Length);
            String Actual = "";
            String toHash = "username<3|v|030";
            byte[] tempSource = ASCIIEncoding.ASCII.GetBytes(toHash);
            byte[] tempHash = new System.Security.Cryptography.SHA256CryptoServiceProvider().ComputeHash(tempSource);

            StringBuilder sOutput = new StringBuilder(tempHash.Length);
            for (int i = 0; i < tempHash.Length; i++)
            {
                sOutput.Append(tempHash[i].ToString("x2"));
            }
            Actual = sOutput.ToString();
            Assert.AreEqual(Expected, Actual, "The hashen don't match!");
        }
    }
}
