using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadStatus.Core;
using System;

namespace RoadStatus.Test
{
    [TestClass]
    public class RoadStatusApiCore : TestStart
    {
        public RoadStatusApiCore()
        {
            Configuration();
        }

        [TestMethod]
       public void ArgumentNullExceptionWhenRoadNameIsNull()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var app = scope.Resolve<ICallRoadAPI>();
                Assert.ThrowsExceptionAsync<ArgumentNullException>(() => app.GetRoadStatus(null));
            }
         }

        [TestMethod]
        public void ValidResultWhenValidRoadName()
        {
            string input = "A2";
            using (var scope = Container.BeginLifetimeScope ())
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
        public void InvalidResultWhenInvalidRoadName()
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
