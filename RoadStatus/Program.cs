using Autofac;
using RoadStatus.Core;
using System;
using static System.Console;

namespace RoadStatus
{
    public class Program : AppStart
    {
        static Program()
        {
            Configuration();
        }

        public static int Main(string[] args)
        {
            try
            {
                if(args?.Length == 0)
                {
                    throw new ArgumentException("Invalid input.", nameof(args));
                }

                using (var scope = Container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<ICallRoadAPI>();
                    var status = app.GetRoadStatus(args[0]).Result;
                    WriteLine($"The status of the {status.DisplayName} is as follows");
                    WriteLine($"\tRoad Status is {status.StatusSeverity}");
                    WriteLine($"\tRoad Status Description is {status.StatusSeverityDescription}");
                }

                return 0;
            }
            catch (AggregateException ex)
            {
                if (ex.InnerExceptions?.Count > 0)
                {
                    foreach (var inEx in ex.InnerExceptions)
                    {
                        WriteLine(inEx.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }

            return 1;
        }
    }
}
