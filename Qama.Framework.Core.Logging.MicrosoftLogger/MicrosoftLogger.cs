using System;
using Microsoft.Extensions.Logging;
using Qama.Framework.Core.Abstractions.Logging;

namespace Qama.Framework.Core.Logging.MicrosoftLogger
{
    public class MicrosoftLogger : IEverythingLogger
    {
        private readonly ILogger _logger;

        public MicrosoftLogger(ILoggerFactory loggerFactory) =>
            _logger = loggerFactory.CreateLogger("customLogger");

        public void LogTrace(string message, params object[] args) =>
            _logger.LogTrace(message, args);

        public void LogTrace(Exception exception, string message, params object[] args) =>
            _logger.LogTrace(exception, message, args);

        public void LogTrace(LogEventId eventId, string message, params object[] args) =>
            _logger.LogTrace(eventId.GetHashCode(), message, args);

        public void LogTrace(LogEventId eventId, Exception exception, string message, params object[] args) =>
            _logger.LogTrace(eventId.GetHashCode(), exception, message, args);
        public void LogDebug(string message, params object[] args) =>
            _logger.LogDebug(message, args);

        public void LogDebug(Exception exception, string message, params object[] args) =>
            _logger.LogDebug(exception, message, args);

        public void LogDebug(LogEventId eventId, string message, params object[] args) =>
            _logger.LogDebug(eventId.GetHashCode(), message, args);

        public void LogDebug(LogEventId eventId, Exception exception, string message, params object[] args) =>
            _logger.LogDebug(eventId.GetHashCode(), exception, message, args);

        public void LogInformation(string message, params object[] args) =>
            _logger.LogInformation(message, args);

        public void LogInformation(Exception exception, string message, params object[] args) =>
            _logger.LogInformation(exception, message, args);

        public void LogInformation(LogEventId eventId, string message, params object[] args) =>
            _logger.LogInformation(eventId.GetHashCode(), message, args);

        public void LogInformation(LogEventId eventId, Exception exception, string message, params object[] args) =>
            _logger.LogInformation(eventId.GetHashCode(), exception, message, args);

        public void LogWarning(string message, params object[] args) =>
            _logger.LogInformation(message, args);

        public void LogWarning(Exception exception, string message, params object[] args) =>
            _logger.LogInformation(exception, message, args);

        public void LogWarning(LogEventId eventId, string message, params object[] args) =>
            _logger.LogInformation(eventId.GetHashCode(), message, args);

        public void LogWarning(LogEventId eventId, Exception exception, string message, params object[] args) =>
            _logger.LogInformation(eventId.GetHashCode(), exception, message, args);

        public void LogError(string message, params object[] args) =>
            _logger.LogError(message, args);

        public void LogError(Exception exception, string message, params object[] args) =>
            _logger.LogError(exception, message, args);

        public void LogError(LogEventId eventId, string message, params object[] args) =>
            _logger.LogError(eventId.GetHashCode(), message, args);

        public void LogError(LogEventId eventId, Exception exception, string message, params object[] args) =>
            _logger.LogError(eventId.GetHashCode(), exception, message, args);

        public void LogCritical(string message, params object[] args) =>
            _logger.LogCritical(message, args);

        public void LogCritical(Exception exception, string message, params object[] args) =>
            _logger.LogCritical(exception, message, args);

        public void LogCritical(LogEventId eventId, string message, params object[] args) =>
            _logger.LogCritical(eventId.GetHashCode(), message, args);

        public void LogCritical(LogEventId eventId, Exception exception, string message, params object[] args) =>
            _logger.LogCritical(eventId.GetHashCode(), exception, message, args);
    }
}
