namespace rts.core
{
    public interface ILog
    {
        void LogFatal(string nValue);

        void LogError(string nValue);

        void LogWarn(string nValue);

        void LogMsg(string nValue);
    }
}
