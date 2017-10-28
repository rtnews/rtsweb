using rts.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rtsweb.Models
{
    [Serializable]
    public class Dpart : Entity, IComparable<Dpart>
    {
        public int CompareTo(Dpart other)
        {
            if (Identifier == other.Identifier)
            {
                return 0;
            }
            if (Identifier > other.Identifier)
            {
                return 1;
            }
            return -1;
        }

        public int Identifier
        {
            get;
            set;
        }

        public string ClerkId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Depart
        {
            get;
            set;
        }
    }
}
