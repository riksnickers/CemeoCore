using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CeMeOCore.Tests.Logic.TextAnalysers
{
    /// <summary>
    /// Summary description for TextAnalyserTest
    /// </summary>
    [TestClass]
    public class TextAnalyserTest
    {
        public TextAnalyserTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        public void TestAnalyserCreateAMeetingThisWeekSentence()
        {
            string sentence = "I want to create a meeting this week.";
        }

        [TestMethod]
        public void TestAnalyserCreateAMeetingBeforeSentence()
        {
            string sentence = "I want to create a meeting within this work week.";
        }

        [TestMethod]
        public void TestAnalyserCreateAMeetingThisWeekSentence()
        {
            string sentence = "I want to create a meeting this week";
        }

        [TestMethod]
        public void TestAnalyserChangeMeetingSentence()
        {

        }

        [TestMethod]
        public void TestAnalyserCancelMeetingSentence()
        {

        }
    }
}
