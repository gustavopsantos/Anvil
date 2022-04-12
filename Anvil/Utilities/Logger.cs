using System;

namespace Anvil.Utilities
{
    public class Logger
    {
        public Action<string> Info { get; }
        public Action<string> Warning { get; }
        public Action<string> Error { get; }

        public Logger(Action<string> info, Action<string> warning, Action<string> error)
        {
            Info = info;
            Warning = warning;
            Error = error;
        }
    }
}