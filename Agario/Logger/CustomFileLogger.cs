using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Logger
{
    /// <summary>
    /// Author:    Aurora Zuo & Sasha Rybalkiina
    /// Date:      Apr.14, 2023
    /// Course:    CS 3500, University of Utah, School of Computing
    /// Copyright: CS 3500, Aurora Zuo & Zhuofei Lyu - This work may not 
    ///            be copied for use in Academic Coursework.
    ///
    /// We, Aurora Zuo and Sasha Rybalkina, certify that we wrote this code from scratch and
    /// did not copy it in part or whole from another source.  All 
    /// references used in the completion of the assignments are cited 
    /// in our README file.
    ///
    /// File Content: 
    ///     Custom file logger class that implements ILogger and contains all the basic
    ///     functionalities of a logger.
    /// </summary>
	public class CustomFileLogger : ILogger
    {
        private readonly string _filePath;
        private readonly string _name;
        private readonly string[] _logLevels = { "Trace", "Debug", "Infor", "Warni", "Error", "Criti" };

        /// <summary>
        /// Constructor initiates the logger
        /// </summary>
        /// <param name="filePath"></param>
        public CustomFileLogger(string name)
        {
            _name = name;
            _filePath = (Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                + Path.DirectorySeparatorChar + $"CS3500-{name}.log").Replace(@"\", @"\");
        }

        /// <summary>
        /// Begins a logical operation scope
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state">The identifier for the scope</param>
        /// <returns>An IDisposable that ends the logical operation scope on dispose</returns>
        /// <exception cref="NotImplementedException"></exception>
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a string message of the state and exception
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string logMessage = $"{DateTime.Now.ToString("u")} ({System.Threading.Thread.CurrentThread.ManagedThreadId})" +
                            $" - {_logLevels[(int)logLevel]} - {formatter(state, exception)}";
            File.AppendAllText(_filePath, logMessage);
        }
    }
}
