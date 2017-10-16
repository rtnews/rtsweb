namespace rts.core
{
    public enum HashType
    {
        ModifiedFnv,
        Fnv1a,
        PJW,
        BKDR,
        SDBM
    }

    public sealed class HashId
    {
        public static uint RunHashId(string nValue, HashType nHashType)
        {
            if ("" == nValue) return 0;
            uint hash = 0;
            if (HashType.ModifiedFnv == nHashType)
            {
                hash = RunModifiedFnv(nValue);
            }
            else if (HashType.Fnv1a == nHashType)
            {
                hash = RunFnv1a(nValue);
            }
            else if (HashType.PJW == nHashType)
            {
                hash = RunPJW(nValue);
            }
            else if (HashType.BKDR == nHashType)
            {
                hash = RunBKDR(nValue);
            }
            else if (HashType.SDBM == nHashType)
            {
                hash = RunSDBM(nValue);
            }
            return hash;
        }

        public static uint RunCommon(string nValue)
        {
            if ("" == nValue) return 0;

            return RunModifiedFnv(nValue);
        }

        public static uint RunModifiedFnv(string nValue)
        {
            uint hash = RunFnv1a(nValue);
            unchecked {
                hash += hash << 13;
                hash ^= hash >> 7;
                hash += hash << 3;
                hash ^= hash >> 17;
                hash += hash << 5;
            }
            return hash;
        }

        public static uint RunFnv1a(string nValue)
        {
            uint hash = 2166136261;
            uint p = 16777619;
            foreach (var i in nValue)
            {
                unchecked {
                    hash ^= i;
                    hash *= p;
                }
            }
            return hash;
        }

        public static uint RunPJW(string nValue)
        {
            uint p = uint.MaxValue << 28;
            uint hash = 0;
            uint t = 0;
            foreach (var i in nValue)
            {
                unchecked
                {
                    hash = (hash << 4) + i;
                    if ( 0 != (t = hash & p) )
                    {
                        hash = ((hash ^ (t >> 24)) & (~p));
                    }
                }
            }
            return hash;
        }

        public static uint RunBKDR(string nValue)
        {
            uint p = 131; //31 131 1313 13131 131313
            uint hash = 0;
            foreach (var i in nValue)
            {
                unchecked
                {
                    hash = (hash * p) + i;
                }
            }
            return hash;
        }

        public static uint RunSDBM(string nValue)
        {
            uint hash = 0;
            foreach (var i in nValue)
            {
                unchecked
                {
                    hash = i + (hash << 6) + (hash << 16) - hash;
                }
            }
            return hash;
        }
    }
}
