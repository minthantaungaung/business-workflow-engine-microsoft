using Newtonsoft.Json;
using NLog;
using qal.workflow.domain.Interfaces.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qal.workflow.infrastructure.Service
{
    public class LoggerManager : ILoggerManager
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public void LogRequest(string? KbzRefNo, string ActionName, object? message)
        {
            logger.Info(
                $"\n" +
                $"CtrlAction: {ActionName}, " +
                $"KbzRefNo : {KbzRefNo}, " +
                $"Request : {message ?? " - "}");
        }

        public void LogResponse(string? KbzRefNo, string ActionName, object? message)
        {
            logger.Info(
                $"\n" +
                $"CtrlAction: {ActionName}, " +
                $"KbzRefNo : {KbzRefNo}, " +
                $"Response : {message ?? " - "}");
        }

        public void LogException(string? KbzRefNo, string ActionName, Exception? ex)
        {
            logger.Error(
                $"\n" +
                $"CtrlAction: {ActionName}, " +
                $"KbzRefNo : {KbzRefNo}, " +
                $"Exception Msg : {ex?.Message ?? " - "} \n" +
                $"Raw Exception : {JsonConvert.SerializeObject(ex)}");
        }

        public void LogDebug(string message)
        {
            logger.Debug("\n" + message);
        }

        public void LogError(string message)
        {
            logger.Error("\n" + message);
        }

        public void LogInfo(string message)
        {
            logger.Info("\n" + message);
        }

        public void LogWarn(string message)
        {
            logger.Warn("\n" + message);
        }
    }
}
