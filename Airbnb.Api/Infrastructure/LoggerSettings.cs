﻿namespace Airbnb.Api.Infrastructure
{
    public class LoggerSettings
    {
        public string AppName { get; set; } = "Airbnb";
        public bool WriteToFile { get; set; } = false;
        public bool StructuredConsoleLogging { get; set; } = false;
        public string MinimumLogLevel { get; set; } = "Information";
    }
}
