using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadStatus.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace RoadStatus.Test
{
    [TestClass]
   public class RoadStatusApiClient : TestStart
    {
        [TestInitialize]
        public void Startup()
        {
            Configuration();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Dispose();
        }

        [TestMethod]
        public void GetRoadStatus_RoadNameNull_ArgumentNullException()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var app = scope.Resolve<ICallRoadAPI>();
                Assert.ThrowsExceptionAsync<ArgumentNullException>(() => app.GetRoadStatus(null));
            }
        }

        [TestMethod]
        public void GetRoadStatus_ValidRoadName_Result()
        {
            string input = "A4";
            using (var scope = Container.BeginLifetimeScope())
            {
                var app = scope.Resolve<ICallRoadAPI>();
                var status = app.GetRoadStatus(input).Result;
                Assert.IsNotNull(status.DisplayName);
                Assert.AreEqual(input, status.DisplayName);
                Assert.IsNotNull(status.StatusSeverity);
                Assert.IsNotNull(status.StatusSeverityDescription);
            }
        }

        [TestMethod]
        public void GetRoadStatus_InvalidRoadName_Result()
        {
            string input = "road";
            using (var scope = Container.BeginLifetimeScope())
            {
                var app = scope.Resolve<ICallRoadAPI>();
                Assert.ThrowsExceptionAsync<Exception>(() => app.GetRoadStatus(input));
            }
        }
    }
}
