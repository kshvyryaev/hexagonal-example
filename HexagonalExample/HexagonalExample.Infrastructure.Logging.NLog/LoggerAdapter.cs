using System;
using NLog;
using NLog.Config;
using NLog.Targets;
using HexagonalExample.Domain.Contracts.Adapters;

namespace HexagonalExample.Infrastructure.Logging.NLog
{
    public class LoggerAdapter : ILoggerAdapter
    {
        #region Constants

        private const string BaseDirPattern = "${basedir}";
        private const string LogsFolderName = "Logs";
        private const string DebugLogsFolderName = "Debug";
        private const string InfoLogsFolderName = "Info";
        private const string WarnLogsFolderName = "Warn";
        private const string ErrorLogsFolderName = "Error";
        private const string FatalLogsFolderName = "Fatal";
        private const string LogFilePattern = "log-${shortdate}.txt";
        private const string PathSeparator = "/";

        private const string DebugFileTargetName = "DebugFileTargetName";
        private const string InfoFileTargetName = "InfoFileTargetName";
        private const string WarnFileTargetName = "WarnFileTargetName";
        private const string ErrorFileTargetName = "ErrorFileTargetName";
        private const string FatalFileTargetName = "FatalFileTargetName";
        private const string FileTargetLayoutPattern = "${longdate} ${level} ${message}  ${exception}\n";

        #endregion Constants

        #region Fields

        private readonly ILogger _logger;

        #endregion Fields

        #region Constructors

        public LoggerAdapter()
        {
            var configuration = new LoggingConfiguration();

            var debugFileName = string.Join(PathSeparator, BaseDirPattern, LogsFolderName, DebugLogsFolderName, LogFilePattern);
            var infoFileName = string.Join(PathSeparator, BaseDirPattern, LogsFolderName, InfoLogsFolderName, LogFilePattern);
            var warnFileName = string.Join(PathSeparator, BaseDirPattern, LogsFolderName, WarnLogsFolderName, LogFilePattern);
            var errorFileName = string.Join(PathSeparator, BaseDirPattern, LogsFolderName, ErrorLogsFolderName, LogFilePattern);
            var fatalFileName = string.Join(PathSeparator, BaseDirPattern, LogsFolderName, FatalLogsFolderName, LogFilePattern);

            var debugFileTarget = new FileTarget(DebugFileTargetName) { FileName = debugFileName, Layout = FileTargetLayoutPattern };
            var infoFileTarget = new FileTarget(InfoFileTargetName) { FileName = infoFileName, Layout = FileTargetLayoutPattern };
            var warnFileTarget = new FileTarget(WarnFileTargetName) { FileName = warnFileName, Layout = FileTargetLayoutPattern };
            var errorFileTarget = new FileTarget(ErrorFileTargetName) { FileName = errorFileName, Layout = FileTargetLayoutPattern };
            var fatalFileTarget = new FileTarget(FatalFileTargetName) { FileName = fatalFileName, Layout = FileTargetLayoutPattern };

            configuration.AddTarget(debugFileTarget);
            configuration.AddTarget(infoFileTarget);
            configuration.AddTarget(warnFileTarget);
            configuration.AddTarget(errorFileTarget);
            configuration.AddTarget(fatalFileTarget);

            configuration.AddRuleForOneLevel(LogLevel.Debug, debugFileTarget);
            configuration.AddRuleForOneLevel(LogLevel.Info, infoFileTarget);
            configuration.AddRuleForOneLevel(LogLevel.Warn, warnFileTarget);
            configuration.AddRuleForOneLevel(LogLevel.Error, errorFileTarget);
            configuration.AddRuleForOneLevel(LogLevel.Fatal, fatalFileTarget);

            LogManager.Configuration = configuration;

            _logger = LogManager.GetLogger(nameof(Logger));
        }

        #endregion Constructors

        #region Methods

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception exception)
        {
            _logger.Error(exception);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        #endregion Methods
    }
}
