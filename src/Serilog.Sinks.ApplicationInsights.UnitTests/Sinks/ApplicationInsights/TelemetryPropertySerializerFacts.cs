using System;
using Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights;
using Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights.Property;
using Xunit;

namespace Serilog.Sinks.ApplicationInsights.UnitTests.Sinks.ApplicationInsights
{
    /// <summary>
    /// Testing <see cref="TelemetryPropertyDeserializer"/>
    /// </summary>
    public class TelemetryPropertySerializerFacts
    {
        [Theory]
        [ClassData(typeof(TelemetryPropertySource))]
        public void GetsDependencyProperty(TelemetryProperty expectedProperty, Type telemetryType)
        {
            // Arrange
            string dependencyProperty = expectedProperty.ToString();
            var serializer = new TelemetryPropertyDeserializer();
            // Act
            TelemetryProperty actualProperty = serializer.Deserialize(dependencyProperty);
            // Assert
            Assert.IsType(expectedProperty.GetType(), actualProperty);
        }
    }
}