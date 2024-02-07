using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qal.workflow.domain.Interfaces.IService
{
    public interface ILoggerManager
    {
        void LogInfo(string message);

        void LogWarn(string message);

        void LogDebug(string message);

        void LogError(string message);

        void LogRequest(string? KbzRefNo, string ActionName, object? message);
        void LogResponse(string? KbzRefNo, string ActionName, object? message);
        void LogException(string? KbzRefNo, string ActionName, Exception? ex);
    }
}
