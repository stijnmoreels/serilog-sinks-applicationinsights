using System;
using Serilog.Core;
using Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights.Property;
using Serilog.Sinks.ApplicationInsights.UnitTests.references;
using Serilog.Sinks.ApplicationInsights.UnitTests.Sinks.ApplicationInsights;
using Xunit;

namespace Serilog.Sinks.ApplicationInsights.UnitTests.Sinks
{
    /// <summary>
    /// Testing <see cref="LoggerConfigurationApplicationInsightsExtensions"/>
    /// </summary>
    public class LoggerConfigurationApplicationInsightsExtensionsFacts
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerConfigurationApplicationInsightsExtensionsFacts"/> class.
        /// </summary>
        public LoggerConfigurationApplicationInsightsExtensionsFacts()
        {
            _logger = CreateApplicationInsightsLogger();
        }

        private static Logger CreateApplicationInsightsLogger()
        {
            string instrumentationKey = ApplicationInsightsReference.InstrumentationKey;

            return new LoggerConfiguration()
                .WriteTo.DynamicApplicationInsights(instrumentationKey)
                .CreateLogger();
        }

        [Theory]
        [ClassData(typeof(TelemetryPropertySource))]
        public void ThenTelemetryPropertyIsLogged(TelemetryProperty property, Type telemetryType)
        {
            _logger.Information("My custom message with the Telemetry {mytelemetry}", property);
        }

        [Fact]
        public void ThenExceptionIsLogged()
        {
            _logger.Error(new Exception("My exception telemetry message"), "My custom message with exception {exception}");
        }

        [Fact]
        public void ThenUnknownIsSkipped()
        {
            _logger.Warning("This messages is skipped {unknown}", new Unknown());
        }

        private class Unknown {}
    }
}