using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.DataContracts;
using Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights.Property;

namespace Serilog.Sinks.ApplicationInsights.UnitTests.Sinks.ApplicationInsights
{
    /// <summary>
    /// Source of all the <see cref="TelemetryProperty"/> implementations.
    /// </summary>
    public class TelemetryPropertySource : IEnumerable<object[]>
    {
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {new DependencyProperty {StartTime = DateTime.UtcNow}, typeof(DependencyTelemetry)};
            yield return new object[] {new EventProperty(), typeof(EventTelemetry)};
            yield return new object[] {new MetricProperty(), typeof(MetricTelemetry)};
            yield return new object[] {new RequestProperty {StartTime = DateTime.UtcNow}, typeof(RequestTelemetry)};
        }
    }
}