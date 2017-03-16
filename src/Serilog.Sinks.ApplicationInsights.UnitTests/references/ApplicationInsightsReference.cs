using System.IO;
using System.Xml.Linq;

namespace Serilog.Sinks.ApplicationInsights.UnitTests.references
{
    /// <summary>
    /// Application Insights specifics read from the 'applicationinsights.config' file.
    /// </summary>
    public class ApplicationInsightsReference
    {
        public static string InstrumentationKey
        {
            get
            {
                const string applicationInsightsConfigPath = @"references\ApplicationInsights.config";
                if (!File.Exists(applicationInsightsConfigPath))
                {
                    File.Create(applicationInsightsConfigPath);
                }

                XDocument applicationInsightsFile = XDocument.Load(applicationInsightsConfigPath);
                var instrumentationKeyTag = applicationInsightsFile.Root?.LastNode as XElement;

                return instrumentationKeyTag?.Value;
            }
        }
    }
}