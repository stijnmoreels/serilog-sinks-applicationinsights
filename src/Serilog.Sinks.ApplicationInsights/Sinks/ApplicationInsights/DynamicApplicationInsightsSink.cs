using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Serilog.Events;
using Serilog.ExtensionMethods;

namespace Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights
{
    /// <summary>
    /// Writes log events as Dynamic Telemetry to a Microsoft Azure Application Insights account.
    /// </summary>
    public class DynamicApplicationInsightsSink : ApplicationInsightsSinkBase
    {
        private static readonly TelemetryPropertyDeserializer PropertyDeserializer;

        static DynamicApplicationInsightsSink()
        {
            PropertyDeserializer = new TelemetryPropertyDeserializer();
        }

        /// <summary>
        /// Creates a sink that saves logs to the Application Insights account for the given <paramref name="telemetryClient" /> instance.
        /// </summary>
        /// <param name="telemetryClient">Required Application Insights <paramref name="telemetryClient" />.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null for default provider.</param>
        /// <exception cref="ArgumentNullException"><paramref name="telemetryClient" /> cannot be null</exception>
        public DynamicApplicationInsightsSink(
            TelemetryClient telemetryClient,
            IFormatProvider formatProvider = null)
            : base(telemetryClient, LogEventToTelemetry, formatProvider) {}

        private static IEnumerable<ITelemetry> LogEventToTelemetry(LogEvent logEvent, IFormatProvider formatProvider)
        {
            if (logEvent.Exception != null)
            {
                yield return logEvent.ToDefaultExceptionTelemetry(formatProvider);
            }

            foreach (KeyValuePair<string, LogEventPropertyValue> logProperty in logEvent.Properties)
            {
                string input = GetTelemetryPropertyType(logProperty);
                ISupportProperties telemetry = DeserializeTelemetry(input);
                if (telemetry == null) yield return null;

                logEvent.ForwardPropertiesToTelemetryProperties(telemetry, formatProvider);

                yield return telemetry as ITelemetry;
            }
        }

        private static string GetTelemetryPropertyType(KeyValuePair<string, LogEventPropertyValue> logProperty)
        {
            string input = logProperty.Value.ToString();
            return input.Replace("\\\"", "\"").Trim('\"');
        }

        private static ISupportProperties DeserializeTelemetry(string input)
        {
            TelemetryProperty telemetryProperty = PropertyDeserializer.Deserialize(input);
            return telemetryProperty?.GetTelemetry();
        }
    }
}