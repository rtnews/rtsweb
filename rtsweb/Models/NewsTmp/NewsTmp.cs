using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using rts.core;

namespace rtsweb.Models
{
    [Serializable]
    public class NewsTmp : Entity
    {
        public string Name
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        public DateTime Time
        {
            get;
            set;
        }

        public int Count
        {
            get;
            set;
        }
    }
}
