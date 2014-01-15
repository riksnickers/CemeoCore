using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CeMeOCore.Logic.Organiser;
using CeMeOCore.Logic.Range;

namespace CeMeOCore.Tests.Logic.Random
{
    [TestClass]
    public class SortedListTest
    {
        [TestMethod]
        public void TestSortedListAutoSort()
        {
            StringBuilder sb1 = new StringBuilder();
            SortedList<int, string> list = new SortedList<int, string>();
            list.Add(5, "Optie 5");
            list.Add(4, "Optie 4");
            list.Add(8, "Optie 8");
            list.Add(1, "Optie 1");
            list.Add(10, "Optie 10");
            list.Add(2, "Optie 2");
            list.Add(3, "Optie 3");
            list.Add(6, "Optie 6");
            list.Add(7, "Optie 7");
            list.Add(9, "Optie 9");

            StringBuilder sb2 = new StringBuilder();
            List<string> nonsList = new List<string>();
            nonsList.Add("Optie 5");
            nonsList.Add("Optie 4");
            nonsList.Add("Optie 8");
            nonsList.Add("Optie 1");
            nonsList.Add("Optie 10");
            nonsList.Add("Optie 2");
            nonsList.Add("Optie 3");
            nonsList.Add("Optie 6");
            nonsList.Add("Optie 7");
            nonsList.Add("Optie 9");


            foreach (string item in list.Values)
            {
                sb1.AppendLine(item);
            }

            foreach (string item in nonsList)
            {
                sb2.AppendLine(item);
            }

            Console.WriteLine("Sorted List:\n" + sb1.ToString() + "\n\n" + "Normal List\n" + sb2.ToString() );

            Assert.AreNotEqual(sb1, sb2);
        }

        [TestMethod]
        public void TestDupplicatedValues()
        {
            DateRange dr1 = new DateRange(new DateTime(2014, 01, 01), new DateTime(2014, 01, 02));
            DateRange dr2 = new DateRange(new DateTime(2014, 01, 02), new DateTime(2014, 01, 03));
            DateRange dr3 = new DateRange(new DateTime(2014, 01, 01), new DateTime(2014, 01, 03));
            DateRange dr4 = new DateRange(new DateTime(2014, 01, 04), new DateTime(2014, 01, 05));

            StringBuilder sb1 = new StringBuilder();
            SortedList<DateRange, string> list = new SortedList<DateRange, string>(new DateRange.Comparer());
            list.Add(dr1, "dr1");
            list.Add(dr2, "dr2");
            list.Add(dr3, "dr3");
            list.Add(dr4, "dr4");

            foreach (DateRange item in list.Keys)
            {
                sb1.AppendLine(list.IndexOfKey(item) + ":" + list[item]);
            }

            Console.WriteLine("Sorted List:\n" + sb1.ToString());

            Assert.AreNotEqual(sb1, "lol");
        }
    }
}
