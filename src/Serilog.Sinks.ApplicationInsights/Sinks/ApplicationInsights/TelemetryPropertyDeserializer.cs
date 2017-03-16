// ---------------------------------------------// Copyright 2016 Serilog Contributors
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Events;
using Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights.Property;

namespace Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights
{
    /// <summary>
    /// Deserialize to a <see cref="TelemetryProperty"/> implementations
    /// </summary>
    public class TelemetryPropertyDeserializer
    {
        /// <summary>
        /// Deserialize the given <paramref name="logEventProperty"/> to a <see cref="TelemetryProperty"/> implementation.
        /// </summary>
        /// <param name="logEventProperty"></param>
        /// <returns></returns>
        public TelemetryProperty Deserialize(LogEventPropertyValue logEventProperty)
        {
            string telemetryJson = TrimLogEventPropertyValue(logEventProperty);
            object telemetryObject = TryParseToObject(telemetryJson);
            if (telemetryObject == null) return null;

            JToken telemetryTypeTag = SelectTelemetryType(telemetryObject);
            return telemetryTypeTag == null ? null : DeserializeToSpecificTelemetryType(telemetryJson, telemetryTypeTag);
        }

        private static string TrimLogEventPropertyValue(LogEventPropertyValue logEventProperty)
        {
            return logEventProperty.ToString().Replace("\\\"", "\"").Trim('\"');
        }

        private static object TryParseToObject(string input)
        {
            try
            {
                return JsonConvert.DeserializeObject(input);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static JToken SelectTelemetryType(object telemetryObject)
        {
            var telemetryJObject = telemetryObject as JObject;

            JToken telemetryTypeTag = null;
            telemetryJObject?.TryGetValue(TelemetryProperty.Key, out telemetryTypeTag);

            return telemetryTypeTag;
        }

        private static TelemetryProperty DeserializeToSpecificTelemetryType(string input, JToken telemetryTypeTag)
        {
            Type telemetryType = Type.GetType(telemetryTypeTag.ToString());
            return JsonConvert.DeserializeObject(input, telemetryType) as TelemetryProperty;
        }
    }
}

