using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysis.Framework.Logging
{
    public class LoggerManager
    {
        #region Fields
        /// <summary>
        /// Holds the singleton instance of LoggerManager
        /// </summary>
        private static LoggerManager _loggerManager;

        /// <summary>
        /// Dictionary of loaded loggers
        /// </summary>
        private Dictionary<string, Logger> _loggers;

        /// <summary>
        /// Use to lock thread when updating the _loggers.
        /// </summary>
        private object _locker = new object();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the Logger for logging exception related to Application Settings functional area
        /// </summary>
        public static Logger ApplicationSettingLogger
        {
            get
            {
                return GetLogger("ApplicationSettingService");
            }
        }

        /// <summary>
        /// Gets the Logger for logging exception related to order functional area
        /// </summary>
        public static Logger BusinessLogger
        {
            get
            {
                return GetLogger("BusinessLogger");
            }
        }

        /// <summary>
        /// Gets the Logger for logging exception related to payment functional area
        /// </summary>
        public static Logger WebApplicationLogger
        {
            get
            {
                return GetLogger("WebApplicationLogger");
            }
        }

        /// <summary>
        /// Gets the Logger for logging related to place order area
        /// </summary>
        public static Logger OrderStepLogger
        {
            get
            {
                return GetLogger("OrderStepLogger");
            }
        }

        /// <summary>
        /// Gets the Logger for logging related to place order area
        /// </summary>
        public static Logger OrderIntegrationLogger
        {
            get
            {
                return GetLogger("OrderIntegrationLogger");
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the LoggerManager class. 
        /// Initializes the internal list of loggers.
        /// </summary>
        protected LoggerManager()
        {
            _loggers = new Dictionary<string, Logger>();
        }

        /// <summary>
        /// Initialize the singleton instance of the LoggerManager
        /// </summary>
        static LoggerManager()
        {
            if (_loggerManager == null)
            {
                _loggerManager = Nested.LoggerManager;
                _loggerManager.Init(ConfigurationManager.AppSettings["Log4NetFile"]);
            }
        }
        #endregion

        #region Methods
        #region Public Methods
        /// <summary>
        /// LoggerMannager implements this method to initialize
        /// Log4Net with the location of the xml configuration file.
        /// If an application overrides this it must call the base 
        /// version first.
        /// </summary>
        /// <param name="fileName">The fully qualified file name of the Log4Net configuration file.</param>
        public virtual void Init(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                log4net.Config.XmlConfigurator.Configure(new FileInfo(fileName));
            }
            else
            {
                log4net.Config.XmlConfigurator.Configure();
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Retireves a logger for a given functional area.
        /// </summary>
        /// <param name="functionalArea">The name of the functional area to retrieve a logger.</param>
        /// <returns>The logger for the desired functional area.</returns>
        protected Logger Logger(string functionalArea)
        {
            Logger result;

            if (_loggers.ContainsKey(functionalArea))
            {
                result = _loggers[functionalArea];
            }
            else
            {
                result = new Logger(functionalArea);

                lock (_locker)
                {
                    _loggers[functionalArea] = result;
                }

            }

            return result;
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Retireves a logger for a given functional area.
        /// </summary>
        /// <param name="functionalArea">The name of the functional area to retrieve a logger.</param>
        /// <returns>The logger for the desired functional area.</returns>
        public static Logger GetLogger(string functionalArea)
        {
            return _loggerManager.Logger(functionalArea);
        }
        #endregion
        #endregion

        #region Nested Class
        /// <summary>
        /// Fully lazy initialization of the static member: triggered by the 
        /// first reference. Implementation has performance benefits, and is 
        /// fully lazy.
        /// </summary>
        /// <remarks>
        /// http://www.yoda.arachsys.com/csharp/singleton.html
        /// </remarks>
        private class Nested
        {
            /// <summary>
            /// read only instance of JobEngine
            /// </summary>
            internal static LoggerManager LoggerManager = new LoggerManager();

            /// <summary>
            /// Initializes static members of the Nested class
            /// </summary>
            static Nested()
            {
                // Explicit static constructor to tell C# compiler to not mark type as beforefieldinit
            }
        }
        #endregion
    }
}
