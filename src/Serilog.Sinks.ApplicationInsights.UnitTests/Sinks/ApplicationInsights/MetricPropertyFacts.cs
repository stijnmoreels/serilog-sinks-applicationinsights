using Microsoft.ApplicationInsights.DataContracts;
using Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights;
using Xunit;

namespace Serilog.Sinks.ApplicationInsights.UnitTests.Sinks.ApplicationInsights
{
    /// <summary>
    /// 
    /// </summary>
    public class MetricPropertyFacts
    {
        [Fact]
        public void GetsExpectedTemenetry()
        {
            // Arrange
            var property = new MetricProperty {MetricName = "my metric", MetricValue = 10};
            // Act
            ISupportProperties telemetry = property.GetTelemetry();
            // Assert
            Assert.IsType<MetricTelemetry>(telemetry);
        }
    }
}