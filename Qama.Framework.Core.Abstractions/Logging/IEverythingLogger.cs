using System;

namespace Qama.Framework.Core.Abstractions.Logging
{
    public interface IEverythingLogger
    {
        void LogTrace(string message, params object[] args);
        void LogTrace(Exception exception, string message, params object[] args);
        void LogTrace(LogEventId eventId, string message, params object[] args);
        void LogTrace(LogEventId eventId, Exception exception, string message, params object[] args);
        void LogDebug(string message, params object[] args);
        void LogDebug(Exception exception, string message, params object[] args);
        void LogDebug(LogEventId eventId, string message, params object[] args);
        void LogDebug(LogEventId eventId, Exception exception, string message, params object[] args);
        void LogInformation(string message, params object[] args);
        void LogInformation(Exception exception, string message, params object[] args);
        void LogInformation(LogEventId eventId, string message, params object[] args);
        void LogInformation(LogEventId eventId, Exception exception, string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogWarning(Exception exception, string message, params object[] args);
        void LogWarning(LogEventId eventId, string message, params object[] args);
        void LogWarning(LogEventId eventId, Exception exception, string message, params object[] args);
        void LogError(string message, params object[] args);
        void LogError(Exception exception, string message, params object[] args);
        void LogError(LogEventId eventId, string message, params object[] args);
        void LogError(LogEventId eventId, Exception exception, string message, params object[] args);
        void LogCritical(string message, params object[] args);
        void LogCritical(Exception exception, string message, params object[] args);
        void LogCritical(LogEventId eventId, string message, params object[] args);
        void LogCritical(LogEventId eventId, Exception exception, string message, params object[] args);
    }
}
