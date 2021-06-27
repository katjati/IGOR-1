using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.Global
{
    public enum LogLevels { Info, Warning, Error, Exception, Debug, TimeSummary };
    public interface ILog
    {
        /// <summary>
        /// Check if log is configured for logging (can be disabled so no messages get in).
        /// </summary>
        /// <returns></returns>
        bool IsLoggable();
        /// <summary>
        /// Enable or disable writing to the log.
        /// </summary>
        /// <param name="setting">True to enable.</param>
        void SetLoggable(bool setting);
        /// <summary>
        /// Check if log represents valid file.
        /// </summary>
        /// <returns></returns>
        bool IsValid();
        /// <summary>
        /// Get the path of the current log.
        /// </summary>
        /// <returns></returns>
        string GetLogPath();
        /// <summary>
        /// Insert empty line.
        /// </summary>
        void EmptyLine();
        /// <summary>
        /// Insert empty line.
        /// </summary>
        Task EmptyLineAsync();
        /// <summary>
        /// Creates a message string from text, date and level.
        /// </summary>
        /// <param name="logMessage">Custom message.</param>
        /// <param name="logLevel">Log level for the message.</param>
        /// <returns></returns>
        string GetMessage(string logMessage, LogLevels @logLevel = LogLevels.Info);
        /// <summary>
        /// Write one line of log message with level and date.
        /// </summary>
        /// <param name="logMessage">Custom message.</param>
        /// <param name="logLevel">Log level for the message.</param>
        void LogWrite(string logMessage, LogLevels @logLevel = LogLevels.Info);
        /// <summary>
        /// Write one line of log message with level and date.
        /// </summary>
        /// <param name="logMessage">Custom message.</param>
        /// <param name="logLevel">Log level for the message.</param>
        Task LogWriteAsync(string logMessage, LogLevels @logLevel = LogLevels.Info);
        /// <summary>
        /// Write time information based on stop watch with level INFO.
        /// </summary>
        /// <param name="logMessage">Custom message.</param>
        /// <param name="watch">Instance of the stop watch.</param>
        void LogTimeSummary(string logMessage, Stopwatch watch);
        /// <summary>
        /// Write time information based on stop watch with level INFO.
        /// </summary>
        /// <param name="logMessage">Custom message.</param>
        /// <param name="span">Time span instance.</param>
        void LogTimeSummary(string logMessage, TimeSpan span);
        /// <summary>
        /// Write time information based on stop watch with level INFO.
        /// </summary>
        /// <param name="logMessage">Custom message.</param>
        /// <param name="span">Time span instance.</param>
        /// <param name="logLevel">Log level for the message.</param>
        void LogTimeSummary(string logMessage, TimeSpan span, LogLevels logLevel);
        /// <summary>
        /// Write time information based on stop watch with level INFO.
        /// </summary>
        /// <param name="logMessage">Custom message.</param>
        /// <param name="span">Time span instance.</param>
        Task LogTimeSummaryAsync(string logMessage, TimeSpan span);

        /// <summary>
        /// Write time information based on stop watch with level INFO.
        /// </summary>
        /// <param name="logMessage">Custom message.</param>
        /// <param name="span">Time span instance.</param>
        /// <param name="logLevel">Log level for the message.</param>
        Task LogTimeSummaryAsync(string logMessage, TimeSpan span, LogLevels logLevel);
        /// <summary>
        /// Write time information based on stop watch with level INFO.
        /// </summary>
        /// <param name="logMessage">Custom message.</param>
        /// <param name="watch">Instance of the stop watch.</param>
        Task LogTimeSummaryAsync(string logMessage, Stopwatch watch);
        /// <summary>
        /// Write time information based on stop watch with a given log level.
        /// </summary>
        /// <param name="logMessage">Custom message.</param>
        /// <param name="watch">Instance of the stop watch.</param>
        /// <param name="logLevel">Log level for the message.</param>
        void LogTimeSummary(string logMessage, Stopwatch watch, LogLevels logLevel);
        /// <summary>
        /// Write time information based on stop watch with a given log level.
        /// </summary>
        /// <param name="logMessage">Custom message.</param>
        /// <param name="watch">Instance of the stop watch.</param>
        /// <param name="logLevel">Log level for the message.</param>
        Task LogTimeSummaryAsync(string logMessage, Stopwatch watch, LogLevels logLevel);
        /// <summary>
        /// Log exception information with given log level (call stack info is not logged).
        /// </summary>
        /// <param name="logMessage">Custom message.</param>
        /// <param name="exception">Exception instance.</param>
        /// <param name="logLevel">Log level for the message.</param>
        void LogException(string logMessage, Exception exception, LogLevels logLevel);
        /// <summary>
        /// Log exception information with given log level (call stack info is not logged).
        /// </summary>
        /// <param name="logMessage">Custom message.</param>
        /// <param name="exception">Exception instance.</param>
        /// <param name="logLevel">Log level for the message.</param>
        Task LogExceptionAsync(string logMessage, Exception exception, LogLevels logLevel);
        /// <summary>
        /// Log exception information with given log level optionally with call stack info.
        /// </summary>
        /// <param name="logMessage">Custom message.</param>
        /// <param name="exception">Exception instance.</param>
        /// <param name="logLevel">Log level for the message.</param>
        /// <param name="callStack">Include call stack information.</param>
        void LogException(string logMessage, Exception exception, LogLevels logLevel, bool callStack);
        /// <summary>
        /// Log exception information with given log level optionally with call stack info.
        /// </summary>
        /// <param name="logMessage">Custom message.</param>
        /// <param name="exception">Exception instance.</param>
        /// <param name="logLevel">Log level for the message.</param>
        /// <param name="callStack">Include call stack information.</param>
        Task LogExceptionAsync(string logMessage, Exception exception, LogLevels logLevel, bool callStack);
    }
    /// <summary>
    /// Log used to gather performance data.
    /// </summary>
    public interface IPerformanceLog : ILog
    {
        /// <summary>
        /// Log time summary start of given method in given class.
        /// </summary>
        /// <param name="classInstance">Instance of the current class - typically this</param>
        /// <param name="methodName">Text of monitored action/method call to be displayed in the log</param>
        /// <param name="curentMethodName">Leave null for automatic reflection use</param>
        Stopwatch LogPerformanceStart(object classInstance, string methodName,
            [CallerMemberName] string curentMethodName = null);
        /// <summary>
        /// Log time summary finish of given method in given class.
        /// </summary>
        /// <param name="classInstance">Instance of the current class - typically this</param>
        /// <param name="methodName">Text of monitored action/method call to be displayed in the log</param>
        /// <param name="watch">Instance of the watch used to calculate time</param>
        /// <param name="curentMethodName">Leave null for automatic reflection use</param>
        void LogPerformanceEnd(object classInstance, string methodName, Stopwatch watch,
            [CallerMemberName] string curentMethodName = null);
        /// <summary>
        /// Set excel header to the log.
        /// </summary>
        void LogHeader();
        /// <summary>
        /// Write standard text in a form importable to excel.
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="logLevel"></param>
        void LogWriteStandard(string logMessage, LogLevels logLevel = LogLevels.Info);
    }

    public interface IDbLog : ILog
    {

    }
}
