
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RoadStatus.Core;
using RoadStatus.Entities;
using System;

namespace RoadStatus.Test
{
    [TestClass]
    public class RoadStatusApiContract 
    {
        [TestMethod]
        public void GetRoadStatus_NullArg_ThrowArgumentNullException()
        {
            var svc = Substitute.For<ICallRoadAPI>();
            svc.When(fake => fake.GetRoadStatus(null))
                .Do(call => { throw new ArgumentNullException(); });

            Assert.ThrowsException<ArgumentNullException>(() => svc.GetRoadStatus(null));
        }

        [TestMethod]
        public void GetRoadStatus_InvalidRoadName_ThrowException()
        {
            var svc = Substitute.For<ICallRoadAPI>();
            var road = "test";
            svc.When(fake => fake.GetRoadStatus(Arg.Is("test")))
                .Do(call => { throw new Exception(); });

            Assert.ThrowsException<Exception>(() => svc.GetRoadStatus(road));
        }

        [TestMethod]
        public void GetRoadStatus_ValidRoadName_ReturnStatus()
        {
            var svc = Substitute.For<ICallRoadAPI>();
            var road = "A2";
            svc.GetRoadStatus(Arg.Is(road)).Returns(new RoadCorridor()
            {
                DisplayName = road, StatusSeverity="Good", StatusSeverityDescription ="Description"
            });

            var status = svc.GetRoadStatus(road).Result;

            Assert.IsNotNull(status.DisplayName);
            Assert.AreEqual("A2", status.DisplayName);
            Assert.IsNotNull(status.StatusSeverity);
            Assert.IsNotNull(status.StatusSeverityDescription);
        }
    }
}
