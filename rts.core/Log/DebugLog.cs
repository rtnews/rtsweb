using System.Diagnostics;

namespace rts.core
{
    sealed class DebugLog : ILog
    {
        public void LogError(string nValue)
        {
            Debug.WriteLine(nValue);
        }

        public void LogFatal(string nValue)
        {
            Debug.WriteLine(nValue);
        }

        public void LogMsg(string nValue)
        {
            Debug.WriteLine(nValue);
        }

        public void LogWarn(string nValue)
        {
            Debug.WriteLine(nValue);
        }
    }
}
