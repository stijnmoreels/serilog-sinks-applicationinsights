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

namespace Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights
{
    /// <summary>
    /// Serializer <see cref="LogEventPropertyValue"/> text to <see cref="TelemetryProperty"/> implementations
    /// </summary>
    public class TelemetryPropertyDeserializer
    {
        /// <summary>
        /// Deserialize the given <paramref name="input"/> to a <see cref="TelemetryProperty"/> implementation.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public TelemetryProperty Deserialize(string input)
        {
            object telemetryObject = TryParseTelemetryFrom(input);
            if (telemetryObject == null) return null;

            var telemetryJObject = telemetryObject as JObject;
            JToken telemetryTypeTag = null;
            telemetryJObject?.TryGetValue(TelemetryProperty.Key, out telemetryTypeTag);

            if (telemetryTypeTag == null)
            {
                return null;
            }

            Type telemetryType = Type.GetType(telemetryTypeTag.ToString());
            return JsonConvert.DeserializeObject(input, telemetryType) as TelemetryProperty;
        }

        private static object TryParseTelemetryFrom(string input)
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
    }
}

