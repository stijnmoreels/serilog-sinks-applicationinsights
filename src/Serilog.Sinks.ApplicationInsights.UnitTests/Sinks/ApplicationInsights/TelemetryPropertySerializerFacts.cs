using System.Collections;
using System.Collections.Generic;
using Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights;
using Xunit;

namespace Serilog.Sinks.ApplicationInsights.UnitTests.Sinks.ApplicationInsights
{
    /// <summary>
    /// Testing <see cref="TelemetryPropertyDeserializer"/>
    /// </summary>
    public class TelemetryPropertySerializerFacts : IEnumerable<object[]>
    {
        [Theory]
        [ClassData(typeof(TelemetryPropertySerializerFacts))]
        public void GetsDependencyProperty(TelemetryProperty expectedProperty)
        {
            // Arrange
            string dependencyProperty = expectedProperty.ToString();
            var serializer = new TelemetryPropertyDeserializer();
            // Act
            TelemetryProperty actualProperty = serializer.Deserialize(dependencyProperty);
            // Assert
            Assert.IsType(expectedProperty.GetType(), actualProperty);
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {new DependencyProperty()};
            yield return new object[] {new EventProperty()};
            yield return new object[] {new MetricProperty() };
        }
    }
}