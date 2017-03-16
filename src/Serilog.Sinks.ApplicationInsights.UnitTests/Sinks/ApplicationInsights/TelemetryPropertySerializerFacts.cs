using System;
using Serilog.Events;
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
            var serializer = new TelemetryPropertyDeserializer();
            // Act
            TelemetryProperty actualProperty = serializer.Deserialize(new ScalarValue(expectedProperty));
            // Assert
            Assert.IsType(expectedProperty.GetType(), actualProperty);
        }
    }
}