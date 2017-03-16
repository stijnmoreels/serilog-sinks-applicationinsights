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

using Microsoft.ApplicationInsights.DataContracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights
{
    /// <summary>
    /// Abstract contract to define a Log Property for the Telemetry.
    /// </summary>
    public abstract class TelemetryProperty
    {
        /// <summary>
        /// Key used to identify the type of the <see cref="TelemetryProperty"/> implementation.
        /// </summary>
        public static readonly string Key = "Microsoft.ApplicationInsights.TelemetryProperty";

        /// <summary>
        /// Custom implementation of the <see cref="ISupportProperties"/>.
        /// </summary>
        public abstract ISupportProperties GetTelemetry();

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            string output = JsonConvert.SerializeObject(this);
            JObject jObject = JObject.Parse(output);
            jObject.Add(Key, value: this.GetType().FullName);

            return jObject.ToString();
        }
    }
}
