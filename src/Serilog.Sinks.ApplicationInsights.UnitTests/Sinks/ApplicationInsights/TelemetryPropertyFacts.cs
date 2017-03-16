using System;
using Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights;
using Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights.Property;
using Xunit;

namespace Serilog.Sinks.ApplicationInsights.UnitTests.Sinks.ApplicationInsights
{
    /// <summary>
    /// Testing <see cref="TelemetryProperty"/>
    /// </summary>
    public class TelemetryPropertyFacts
    {
        [Theory]
        [ClassData(typeof(TelemetryPropertySource))]
        public void GetsExpectedTelemetry(TelemetryProperty expectedProperty, Type expectedType)
        {
            Assert.IsType(expectedType, expectedProperty.GetTelemetry());
        }
    }
}
