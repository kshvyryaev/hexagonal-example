using System;

namespace HexagonalExample.Domain.Contracts.Adapters
{
    public interface ILoggerAdapter
    {
        void Debug(string message);

        void Info(string message);

        void Warn(string message);

        void Error(string message);

        void Error(Exception exception);

        void Fatal(string message);
    }
}
