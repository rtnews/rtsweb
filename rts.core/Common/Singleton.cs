namespace rts.core
{
    public class Singleton<T> where T : new()
    {
        public static T Instance()
        {
            if (null == mT) 
            {
                mT = new T();
            }
            return mT;
        }

        static T mT = default(T);
    }
}
