using System.Collections.Generic;

namespace rts.core
{
    public class LogEngine : Singleton<LogEngine>
    {
        public void LogFatal(string nValue)
        {
            foreach (ILog i in mLogs)
            {
                i.LogFatal(nValue);
            }
        }

        public void LogError(string nValue)
        {
            foreach (ILog i in mLogs)
            {
                i.LogError(nValue);
            }
        }

        public void LogWarn(string nValue)
        {
            foreach (ILog i in mLogs)
            {
                i.LogWarn(nValue);
            }
        }

        public void LogMsg(string nValue)
        {
            foreach (ILog i in mLogs)
            {
                i.LogMsg(nValue);
            }
        }

        public void LogFatal(string nClass, string nMethod, object nValue)
        {
            string logError = string.Format
                (@"[F][{0}][{1}]{2}", nClass, nMethod, nValue);
            this.LogFatal (logError);
        }

        public void LogFatal(string nClass, string nMethod, object nValue, object nValue0)
        {
            string logError = string.Format
                (@"[F][{0}][{1}]{2},{3}", nClass, nMethod, nValue, nValue0);
            this.LogFatal(logError);
        }

        public void LogFatal(string nClass, string nMethod, object nValue, object nValue0, object nValue1)
        {
            string logError = string.Format
                (@"[F][{0}][{1}]{2},{3},{4}", nClass, nMethod, nValue, nValue0, nValue1);
            this.LogFatal(logError);
        }

        public void LogError(string nClass, string nMethod, object nValue)
        {
            string logError = string.Format
                (@"[E][{0}][{1}]{2}", nClass, nMethod, nValue);
			this.LogError (logError);
        }

        public void LogError(string nClass, string nMethod, object nValue, object nValue0)
        {
            string logError = string.Format
                (@"[E][{0}][{1}]{2},{3}", nClass, nMethod, nValue, nValue0);
            this.LogError(logError);
        }

        public void LogError(string nClass, string nMethod, object nValue, object nValue0, object nValue1)
        {
            string logError = string.Format
                (@"[E][{0}][{1}]{2},{3},{4}", nClass, nMethod, nValue, nValue0, nValue1);
            this.LogError(logError);
        }

        public void LogWarn(string nClass, string nMethod, object nValue)
        {
            string logError = string.Format
                (@"[W][{0}][{1}]{2}", nClass, nMethod, nValue);
            this.LogWarn (logError);
        }

        public void LogWarn(string nClass, string nMethod, object nValue, object nValue0)
        {
            string logError = string.Format
                (@"[E][{0}][{1}]{2},{3}", nClass, nMethod, nValue, nValue0);
            this.LogWarn(logError);
        }

        public void LogWarn(string nClass, string nMethod, object nValue, object nValue0, object nValue1)
        {
            string logError = string.Format
                (@"[E][{0}][{1}]{2},{3},{4}", nClass, nMethod, nValue, nValue0, nValue1);
            this.LogWarn(logError);
        }

        public void LogMsg(string nClass, string nMethod, object nValue)
        {
            string logInfo = string.Format
                (@"[M][{0}][{1}]{2}", nClass, nMethod, nValue);
            this.LogMsg (logInfo);
        }

        public void LogMsg(string nClass, string nMethod, object nValue, object nValue0)
        {
            string logInfo = string.Format
                (@"[E][{0}][{1}]{2},{3}", nClass, nMethod, nValue, nValue0);
            this.LogMsg(logInfo);
        }

        public void LogMsg(string nClass, string nMethod, object nValue, object nValue0, object nValue1)
        {
            string logInfo = string.Format
                (@"[E][{0}][{1}]{2},{3},{4}", nClass, nMethod, nValue, nValue0, nValue1);
            this.LogMsg(logInfo);
        }

        public void EnableDebugLog()
        {
            mLogs.Add(new DebugLog());
        }

        public void Preinit()
        {
            mLogs = new List<ILog>();
        }

        public LogEngine()
        {
        }

        List<ILog> mLogs;
    }
}
