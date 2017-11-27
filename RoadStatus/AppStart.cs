using Autofac;
using Microsoft.Extensions.Configuration;
using RoadStatus.Core;
using System.IO;

namespace RoadStatus
{
    public class AppStart
    {
        private static IContainer _container;

        public static IContainer Container
        {
            get { return _container; }
        }

        public static void Configuration()
        {
            var appConfig = SetAppConfig();
            Register(appConfig);
        }

        private static IConfiguration SetAppConfig()
        {
            return new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json").Build();
        }

        private static void Register(IConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.Register(c => { return new RoadAPIClient(config); }).As<ICallRoadAPI>();
            _container = builder.Build();
        }
    }
}
