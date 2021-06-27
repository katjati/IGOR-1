using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.Global
{
    public class LogWriter : ILog
    {
        #region Properties

        /// <summary>
        /// Full path to the log file.
        /// </summary>
        protected readonly string logPath;
        /// <summary>
        /// Determines whether log accepts new messages.
        /// </summary>
        protected bool _loggable;

        #endregion

        #region Constructors

        /// <summary>
        /// In itializes new log file. If mode is "w" then the file is deleted if already exists.
        /// All modes create the file if not exists.
        /// </summary>
        /// <param name="logfile">Full path to log file</param>
        /// <param name="mode">w for writing file from scrath, a for appending</param>
        public LogWriter(string logfile, string mode = "w")
        {
            _loggable = true;
            logPath = logfile;
            if (!logfile.IsNullOrEmpty())
            {
                if (mode == "w")
                {
                    try
                    {
                        if (File.Exists(logfile)) File.Delete(logfile);
                    }
                    catch (Exception ex)
                    {
                        LogWrite("This log file could not be deleted. " + ex.Message, LogLevels.Error);
                    }
                }
            }
        }

        #endregion


        /// <inheritdoc/>
        public bool IsLoggable()
        {
            return _loggable;
        }

        /// <inheritdoc/>
        public void SetLoggable(bool setting)
        {
            _loggable = setting;
        }
        /// <inheritdoc/>
        public bool IsValid()
        {
            return !logPath.IsNullOrEmpty() && logPath.IsValidFilePath();
        }
        public string GetLogPath()
        {
            return this.logPath;
        }
        /// <inheritdoc/>
        public string GetMessage(string logMessage, LogLevels logLevel = LogLevels.Info)
        {
            return logMessage.ToLogMessage(logLevel);
        }
        /// <inheritdoc/>
        public void LogWrite(string logMessage, LogLevels logLevel = LogLevels.Info)
        {
            if (!IsLoggable()) return;
            if (logPath.IsNullOrEmpty()) return;
            string finalMessage = GetMessage(logMessage, logLevel);
            Stopwatch timer =Stopwatch.StartNew();
            while (timer.Elapsed.TotalSeconds < 10)
            {
                try
                {
                    using (StreamWriter w = File.AppendText(logPath))
                    {
                        w.WriteLine(finalMessage);
                    }

                    return;
                }
                catch
                {
                    continue;
                }
            }
            timer.Stop();
        }
        /// <inheritdoc/>
        public async Task LogWriteAsync(string logMessage, LogLevels logLevel = LogLevels.Info)
        {
            if (logPath.IsNullOrEmpty() || !IsLoggable()) return;
            string finalMessage = GetMessage(logMessage, logLevel);
            Stopwatch timer = Stopwatch.StartNew();
            while (timer.Elapsed.TotalSeconds < 10)
            {
                try
                {
                    using (StreamWriter w = File.AppendText(logPath))
                    {
                        await w.WriteLineAsync(finalMessage);
                    }

                    return;
                }
                catch
                {
                    continue;
                }
            }
            timer.Stop();
        }

        /// <inheritdoc/>
        public void EmptyLine()
        {
            if (logPath.IsNullOrEmpty() || !IsLoggable()) return;
            try
            {
                using (StreamWriter w = File.AppendText(logPath))
                {
                    LogEmptyLine(w);
                }
            }
            catch
            {
            }
        }
        /// <inheritdoc/>
        public async Task EmptyLineAsync()
        {
            if (logPath.IsNullOrEmpty() || !IsLoggable()) return;
            try
            {
                using (StreamWriter w = File.AppendText(logPath))
                {
                    await LogEmptyLineAsync(w);
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// Add empty line and a line break at the end.
        /// </summary>
        /// <param name="txtWriter"></param>
        /// <returns></returns>
        protected void LogEmptyLine(TextWriter txtWriter)
        {
            try
            {
                txtWriter.WriteLine("");
            }
            catch
            {
            }
        }
        /// <summary>
        /// Add empty line and a line break at the end.
        /// </summary>
        /// <param name="txtWriter"></param>
        /// <returns></returns>
        protected async Task LogEmptyLineAsync(TextWriter txtWriter)
        {
            try
            {
                await txtWriter.WriteLineAsync("");
            }
            catch
            {
            }
        }
        /// <inheritdoc/>
        public void LogTimeSummary(string logMessage, Stopwatch watch)
        {
            LogTimeSummary(logMessage, watch, LogLevels.Info);
        }
        /// <inheritdoc/>
        public async Task LogTimeSummaryAsync(string logMessage, Stopwatch watch)
        {
            await LogTimeSummaryAsync(logMessage, watch, LogLevels.Info);
        }
        /// <inheritdoc/>
        public void LogTimeSummary(string logMessage, TimeSpan span)
        {
            LogTimeSummary(logMessage, span, LogLevels.Info);
        }
        /// <inheritdoc/>
        public async Task LogTimeSummaryAsync(string logMessage, TimeSpan span)
        {
            await LogTimeSummaryAsync(logMessage, span, LogLevels.Info);
        }
        /// <inheritdoc/>
        public void LogTimeSummary(string logMessage, Stopwatch watch, LogLevels logLevel)
        {
            TimeSpan ts = watch.Elapsed;
            LogTimeSummary(logMessage, ts, logLevel);
        }
        /// <inheritdoc/>
        public async Task LogTimeSummaryAsync(string logMessage, Stopwatch watch, LogLevels logLevel)
        {
            TimeSpan ts = watch.Elapsed;
            await LogTimeSummaryAsync(logMessage, ts, logLevel);
        }
        /// <inheritdoc/>
        public void LogTimeSummary(string logMessage, TimeSpan span, LogLevels logLevel)
        {
            if (logPath.IsNullOrEmpty() || !IsLoggable()) return;
            string elapsedTime = span.ToTimeString();
            string finalMessage = logMessage + ": " + elapsedTime;
            try
            {
                LogTimeSummary(finalMessage, logLevel);
            }
            catch
            {
            }
        }
        /// <inheritdoc/>
        public async Task LogTimeSummaryAsync(string logMessage, TimeSpan span, LogLevels logLevel)
        {
            if (logPath.IsNullOrEmpty() || !IsLoggable()) return;
            string elapsedTime = span.ToTimeString();
            string finalMessage = logMessage + ": " + elapsedTime;
            try
            {
                await LogTimeSummaryAsync(finalMessage, logLevel);
            }
            catch
            {
            }
        }
        /// <summary>
        /// Add time summary to the log in async mode.
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        protected void LogTimeSummary(string logMessage, LogLevels logLevel)
        {
            LogWrite($"TIME SUMMARY: {logMessage}", LogLevels.TimeSummary);
        }
        /// <summary>
        /// Add time summary to the log in async mode.
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        protected async Task LogTimeSummaryAsync(string logMessage, LogLevels logLevel)
        {
            await LogWriteAsync($"TIME SUMMARY: {logMessage}", LogLevels.TimeSummary);
        }

        /// <inheritdoc/>
        public void LogException(string logMessage, Exception exception, LogLevels logLevel)
        {
            LogException(logMessage, exception, logLevel, false);
        }
        /// <inheritdoc/>
        public async Task LogExceptionAsync(string logMessage, Exception exception, LogLevels logLevel)
        {
            await LogExceptionAsync(logMessage, exception, logLevel, false);
        }
        /// <inheritdoc/>
        public void LogException(string logMessage, Exception exception, LogLevels logLevel, bool callStack)
        {
            StringBuilder sb = new StringBuilder();
            if (!logMessage.IsNullOrEmpty()) sb.AppendLine(logMessage);
            string exceptionMessage = GetExceptionMessage(exception, callStack);
            LogWrite(exceptionMessage, logLevel);
        }
        /// <inheritdoc/>
        public async Task LogExceptionAsync(string logMessage, Exception exception, LogLevels logLevel, bool callStack)
        {
            StringBuilder sb = new StringBuilder();
            if (!logMessage.IsNullOrEmpty()) sb.AppendLine(logMessage);
            string exceptionMessage = GetExceptionMessage(exception, callStack);
            sb.Append(exceptionMessage);
            await LogWriteAsync(sb.ToString(), logLevel);
        }
        /// <summary>
        /// Get string message from exception (including inner) and optionally call stack.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="callStack"></param>
        /// <returns></returns>
        private string GetExceptionMessage(Exception exception, bool callStack)
        {
            StringBuilder sb = new StringBuilder();
            if (exception != null)
            {
                Exception currentException = exception;
                int innerCount = 0;
                while (currentException != null)
                {
                    sb.AppendLine("Exception" + (innerCount > 0 ? " (inner exception " + innerCount + ")" : "") + ": " + currentException.Message);
                    innerCount++;
                    currentException = currentException.InnerException;
                }

                if (callStack)
                {
                    sb.AppendLine("Call stack: " + exception.StackTrace);
                }
            }
            else
            {
                sb.Append("Exception is null.");
            }

            return sb.ToString();
        }
    }
}
