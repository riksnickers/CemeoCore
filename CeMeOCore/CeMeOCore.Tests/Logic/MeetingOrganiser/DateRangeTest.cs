using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CeMeOCore.Logic.Organiser;
using System.Collections.Generic;
using CeMeOCore.Logic.Range;

namespace CeMeOCore.Tests.Logic.MeetingOrganiser
{
    [TestClass]
    public class DateRangeTest
    {
        [TestMethod]
        public void ComparerTest()
        {
            //Create the date ranges
            DateRange range1 = new DateRange(new DateTime(2013, 12, 28), new DateTime(2013, 12, 31));
            DateRange range2 = new DateRange(new DateTime(2014, 01, 01), new DateTime(2014, 01, 20));
            
            //Create the test Dates
            DateRange testRange1 = new DateRange(new DateTime(2013, 12, 29));
            DateRange testRange2 = new DateRange(new DateTime(2014, 01, 06));

            //Create test strings
            string december = "december";
            string januarie = "januarie";

            //Create a normal and a custom dictionary
            Dictionary<DateRange, string> dictionary = new Dictionary<DateRange,string>();
            Dictionary<DateRange, string> dictionaryWithComparer = new Dictionary<DateRange, string>(/*new DateRange.EqualityComparer()*/);

            //Fill dictionaries
            dictionary.Add(range1, december);
            dictionary.Add(range2, januarie);
            dictionaryWithComparer.Add(range1, december);
            dictionaryWithComparer.Add(range2, januarie);

            //Will be not be the same
            Assert.IsFalse(dictionary.ContainsKey(testRange1), december);
            Assert.IsFalse(dictionary.ContainsKey(testRange2), januarie);

            //Well be the same
            Assert.IsTrue(dictionaryWithComparer.ContainsKey(testRange1), december);
            Assert.IsTrue(dictionaryWithComparer.ContainsKey(testRange2), januarie);
        }

        [TestMethod]
        public void EqualTest()
        {
            //Create the date ranges
            DateRange range1 = new DateRange(new DateTime(2014, 01, 01), new DateTime(2014, 01, 20));
            DateRange range2 = new DateRange(new DateTime(2014, 01, 01), new DateTime(2014, 01, 20));

            Assert.IsTrue(range1.Equals( range2 ));
        }

        [TestMethod]
        public void ConvertProposalDateRangeToNormalDateRange()
        {
            //Create a normal range
            DateRange range1 = new DateRange(new DateTime(2014, 01, 01), new DateTime(2014, 01, 20));
            //Create a proposal range
            ProposalDateRange pdr = new ProposalDateRange(new DateTime(2014, 01, 01), new DateTime(2014, 01, 20));

            //IS the proposal range equal to the normal range? Yes it is. Convertion succeeded
            Assert.IsTrue(range1.Equals(pdr));
        }
    }
}
