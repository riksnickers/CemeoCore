using CeMeOCore.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CeMeOCore;
using CeMeOCore.Models;
using System;

namespace CeMeOCore.Tests.Controllers
{
    [TestClass]
    public class MeetingControllerTest
    {
        [TestMethod]
        public void GetSpecificMeeting()
        {
            MeetingController controller = new MeetingController();

            IEnumerable<string> result = controller.Get(5);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PutMeetingDeadline()
        {
            MeetingController controller = new MeetingController();

            ChangeDeadlineMeetingBindingModel model = new ChangeDeadlineMeetingBindingModel()
            {
                MeetingID = 1,
                NewDeadLineDays = 5
            };

            Boolean result = controller.Put(model);

            Assert.IsFalse( result );
        }

        [TestMethod]
        public void GetLastMeeting()
        {
            MeetingController controller = new MeetingController();

            IEnumerable<string> result = controller.GetLast();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetLast10Meetings()
        {
            MeetingController controller = new MeetingController();

            IEnumerable<string> result = controller.GetLast(10);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetLast10MeetingsBeginningFrom5()
        {
            MeetingController controller = new MeetingController();

            IEnumerable<string> result = controller.GetLast(10, 5);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetUpcomming()
        {
            MeetingController controller = new MeetingController();

            IEnumerable<string> result = controller.GetUpcomming();

            Assert.IsNotNull( result );
        }

        [TestMethod]
        public void GetUpcommingLast5()
        {
            MeetingController controller = new MeetingController();

            IEnumerable<string> result = controller.GetUpcomming(5);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Schedule()
        {
            MeetingController controller = new MeetingController();

            ScheduleMeetingBindingModel model = new ScheduleMeetingBindingModel()
            {
                Creator = 1, //User id
                Participants = new List<int> { 1, 2, 3 }, //User id's wie uitgenodigd is
                DeadlineWorkDays = 2 //Binnen x aantal dagen
            };

            Boolean result = controller.Schedule( model );

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Cancel()
        {
            MeetingController controller = new MeetingController();

            CancelMeetingBindingModel model = new CancelMeetingBindingModel()
            {
                CanceledBy = 1,
                MeetingID = 1,
                Reason = "Because we can!"
            };

            Boolean result = controller.Cancel(model);

            Assert.IsFalse(result);
        }
    }
}
