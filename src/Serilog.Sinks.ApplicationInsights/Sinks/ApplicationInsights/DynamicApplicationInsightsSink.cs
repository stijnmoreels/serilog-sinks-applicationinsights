// Copyright 2016 Serilog Contributors
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
                yield return logEvent.ToDefaultExceptionTelemetry(formatProvider);

            foreach (KeyValuePair<string, LogEventPropertyValue> logProperty in logEvent.Properties)
            {
                ISupportProperties telemetry = PropertyDeserializer.Deserialize(logProperty.Value)?.GetTelemetry();
                if (telemetry == null) continue;

                logEvent.ForwardPropertiesToTelemetryProperties(telemetry, formatProvider);
                yield return telemetry as ITelemetry;
            }
        }
    }
}