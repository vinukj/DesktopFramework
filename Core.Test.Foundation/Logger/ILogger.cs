
using System;

namespace Core.Test.Foundation.Logger
{
    public interface ILogger
    {
        void Write(string s);
        void Write(Exception exception);
        void Clear();
    }
}
