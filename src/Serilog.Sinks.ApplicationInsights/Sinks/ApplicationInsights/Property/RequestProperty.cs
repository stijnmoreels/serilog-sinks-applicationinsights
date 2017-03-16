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
using Microsoft.ApplicationInsights.DataContracts;

namespace Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights.Property
{
    /// <summary>
    /// <see cref="TelemetryProperty" /> implementation to define the 'TrackRequest' method.    
    /// </summary>
    public class RequestProperty : TelemetryProperty
    {
        public string Name { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public TimeSpan Duration { get; set; }

        public string ResponseCode { get; set; }

        public bool Success { get; set; }

        /// <summary>
        /// Custom implementation of the <see cref="ISupportProperties"/>.
        /// </summary>
        public override ISupportProperties GetTelemetry()
        {
            return new RequestTelemetry(Name, StartTime, Duration, ResponseCode, Success);
        }
    }
}