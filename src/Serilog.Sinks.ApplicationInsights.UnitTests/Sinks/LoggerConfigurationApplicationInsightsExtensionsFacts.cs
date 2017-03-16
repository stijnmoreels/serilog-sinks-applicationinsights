using System;
using Serilog.Core;
using Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights;
using Serilog.Sinks.ApplicationInsights.UnitTests.references;
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

        [Fact]
        public void ThenDependencyIsLogged()
        {
           _logger.Information("My custom message with dependency {mydependency}", new DependencyProperty
            {
                DependencyCall = "call",
                EnlapsedTime = TimeSpan.FromDays(1),
                StartTime = DateTime.UtcNow,
                DependencyName = "my dependency",
                Success = false
            });
        }

        [Fact]
        public void ThenMetricIsLogged()
        {
            _logger.Debug("My custom message with metric {mymetric}", new MetricProperty
            {
                MetricName = "my metric",
                MetricValue = 10
            });
        }

        [Fact]
        public void ThenEventIsLogged()
        {
            _logger.Fatal("My custom message with event {myevent}", new EventProperty
            {
                Name = "my event"
            });
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

        private class Unknown { }
    }
}