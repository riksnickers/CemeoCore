using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CeMeOCore.Logic.Spots;

namespace CeMeOCore.Tests.Logic.Spot
{
    [TestClass]
    public class SpotManagerTest
    {
        [TestMethod]
        public void TestAddFunction()
        {
            SpotManager sm = new SpotManager();

            PersonBlackSpot pbs = new PersonBlackSpot();
            RoomBlackSpot rbs = new RoomBlackSpot();
            ReservedSpot rs = new ReservedSpot();

            sm.AddSpot(pbs);
            sm.AddSpot(rbs);
            sm.AddSpot(rs);

            Assert.IsTrue(sm.GetPersonBlackSpots().ContainsValue(pbs));
            Assert.IsTrue(sm.GetRoomBlackSpots().ContainsValue(rbs));
            Assert.IsTrue(sm.GetReservedSpots().ContainsValue(rs));
        }
    }
}
