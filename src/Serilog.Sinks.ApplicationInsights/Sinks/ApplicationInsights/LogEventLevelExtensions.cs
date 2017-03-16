﻿// Copyright 2016 Serilog Contributors
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
using Serilog.Events;

// ReSharper disable once CheckNamespace
namespace Serilog.Sinks.ApplicationInsights
{
    /// <summary>
    /// Extension Method(s) for <see cref="LogEventLevel"/> instances
    /// </summary>
    public static class LogEventLevelExtensions
    {
        /// <summary>
        /// To the severity level.
        /// </summary>
        /// <param name="logEventLevel">The log event level.</param>
        /// <returns></returns>
        public static SeverityLevel? ToSeverityLevel(this LogEventLevel logEventLevel)
        {
            switch (logEventLevel)
            {
                case LogEventLevel.Verbose:
                case LogEventLevel.Debug:
                    return SeverityLevel.Verbose;
                case LogEventLevel.Information:
                    return SeverityLevel.Information;
                case LogEventLevel.Warning:
                    return SeverityLevel.Warning;
                case LogEventLevel.Error:
                case LogEventLevel.Fatal:
                    return SeverityLevel.Error;
            }

            return null;
        }
    }
}