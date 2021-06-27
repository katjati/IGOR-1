using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Igor.Library.Models;

namespace Igor.Library.Global
{
    public static class GeneralHelpers
    {
        /// <summary>
        /// Check whether enumerable list is null or has no items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        [ContractAnnotation("null => true")]
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return true;
            }

            /* If this is a list, use the Count property for efficiency. 
             * The Count property is O(1) while IEnumerable.Count() is O(N). */
            if (enumerable is ICollection<T> collection)
            {
                return collection.Count < 1;
            }

            return !enumerable.Any();
        }
        /// <summary>
        /// Check if a string is valid file path or file name and not a folder path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsValidFilePath(this string path)
        {
            try
            {
                path = path.ToLowerInvariant();
                string filename = Path.GetFileName(path);
                string ext = Path.GetExtension(path);
                return (!filename.IsNullOrEmpty() && !ext.IsNullOrEmpty());
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Generate full string with message, date and log level.
        /// </summary>
        /// <param name="logMessage">Custom message.</param>
        /// <param name="logLevel">Log level for the message.</param>
        /// <returns></returns>
        public static string ToLogMessage(this string logMessage, LogLevels logLevel = LogLevels.Info)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(logLevel.ToLogLevel().ToUpper());
            sb.Append(": ");
            sb.Append(DateTime.Now.ToStandardDateString());
            sb.Append(" ");
            sb.Append(DateTime.Now.ToStandardTimeString());
            sb.Append(": ");
            sb.Append(logMessage);

            return sb.ToString();
        }
        /// <summary>
        /// Convert date to string in format yyyy-mm-dd without the time using standard '-' separator.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToStandardDateString(this DateTime date)
        {
            return date.ToStandardDateString("-");
        }

        /// <summary>
        /// Convert date to string in format yyyy-mm-dd without the time using specified separator.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToStandardDateString(this DateTime date, string separator)
        {
            return date.ToString(String.Format("yyyy{0}MM{0}dd", separator));
        }

        /// <summary>
        /// Convert date to string in format HH:mm:ss without the date using the standard ':' separator.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToStandardTimeString(this DateTime date)
        {
            return date.ToStandardTimeString(":");
        }

        /// <summary>
        /// Convert date to string in format HH:mm:ss without the date using provided separator.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToStandardTimeString(this DateTime date, string separator)
        {
            return date.ToString(String.Format("HH{0}mm{0}ss", separator));
        }
        /// <summary>
        /// Get string representation of the log level.
        /// Empty if cannot detemine.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static string ToLogLevel(this LogLevels level)
        {
            switch (level)
            {
                case LogLevels.Info:
                    return "INFO";
                case LogLevels.Debug:
                    return "DEBUG";
                case LogLevels.TimeSummary:
                    return "TIMESUMMARY";
                case LogLevels.Warning:
                    return "WARNING";
                case LogLevels.Error:
                    return "ERROR";
                case LogLevels.Exception:
                    return "EXCEPTION";
                default:
                    return string.Empty;
            }
        }
        /// <summary>
        /// Get the string representation of elapsed time in default format.
        /// </summary>
        /// <param name="input">Time span</param>
        /// <returns></returns>
        public static string ToTimeString(this TimeSpan input) => input.ToTimeString(true, true, true, true);
        /// <summary>
        /// Get the string representation of elapsed time with given format (particular time parts).
        /// </summary>
        /// <param name="input">Time span</param>
        /// <param name="hours">Show hours</param>
        /// <param name="minutes">Show minutes</param>
        /// <param name="seconds">Show seconds</param>
        /// <param name="miliseconds">Show miliseconds</param>
        /// <returns></returns>
        public static string ToTimeString(this TimeSpan input, bool hours, bool minutes, bool seconds, bool miliseconds)
        {
            string hSep = (minutes || seconds) ? ":" : (miliseconds ? "." : "");
            string h = hours ? $"{input.Hours:00}{hSep}" : "";

            string mSep = seconds ? ":" : (miliseconds ? "." : "");
            string m = minutes ? $"{input.Minutes:00}{mSep}" : "";

            string sSep = miliseconds ? "." : "";
            string s = seconds ? $"{input.Seconds:00}{sSep}" : "";
            string ms = miliseconds ? $"{input.Milliseconds / 10:00}" : "";
            return h + m + s + ms;
        }
        /// <summary>
        /// Combine 1-4 paths and check for nulls.
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <param name="path3"></param>
        /// <param name="path4"></param>
        /// <returns></returns>
        [NotNull]
        public static string PathCombine(string path1, string path2, string path3 = null, string path4 = null)
        {
            return Path.Combine(path1 ?? string.Empty, path2 ?? string.Empty, path3 ?? string.Empty,
                path4 ?? string.Empty);
        }
        /// <summary>
        /// Creates the directory in the given path.
        /// Does nothing if folder already exists and returns true..
        /// </summary>
        /// <param name="path">Path to directory</param>
        /// <param name="retryCnt">Number of attempts</param>
        /// <param name="delay">Delay between attempts</param>
        /// <returns></returns>
        public static bool CreateDirectory(this string path, int retryCnt = 3, int delay = 500)
        {
            if (path.IsNullOrEmpty()) return false;
            if (Directory.Exists(path))
                return false;
            Exception exc = null;
            while (retryCnt-- >= 0)
            {
                try
                {
                    Directory.CreateDirectory(path);
                    return true;
                }
                catch (Exception e)
                {
                    exc = e;
                }
                if (Directory.Exists(path))
                    return true;
                Thread.Sleep(delay);
            }
            return false;
        }
        /// <summary>
        /// Get int representation of the string or 0.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this string value)
        {
            if (value.IsNullOrEmpty()) return 0;
            try
            {
                return int.Parse(value);
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// Get decimal representation of the string or 0.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string value)
        {
            if (value.IsNullOrEmpty()) return 0;
            try
            {
                return Decimal.Parse(value);
            }
            catch
            {
                return 0;
            }
        }
    }


}
