using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Logger
{
    /// <summary>
    /// Author:    Aurora Zuo & Sasha Rybalkina
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
    ///     Custom file logger provider class that implements ILoggerProvider and is
    ///     responsible for instantiation, configuration and shutdown/cleanup (via IDisposable )
    ///     of one or more logger implementations.
    /// </summary>
    public class CustomFileLoggerProvider : ILoggerProvider
    {
        private CustomFileLogger logger; // custom file logger object

        /// <summary>
        /// Initiates the file logger provider
        /// </summary>
        /// <param name="filePath"></param>
        public CustomFileLoggerProvider(string filePath)
        {
            logger = new CustomFileLogger(filePath);
        }

        /// <summary>
        /// Creates and stores the custom file logger
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            return logger;
        }

        /// <summary>
        /// Disposes the file logger provider
        /// </summary>
        public void Dispose()
        {
            // tells the logger to close its file
            logger = null;
            // garbage collects the logger object
            GC.SuppressFinalize(this);
        }
    }
}