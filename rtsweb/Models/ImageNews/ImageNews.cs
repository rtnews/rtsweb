using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using rts.core;

namespace rtsweb.Models
{
    [Serializable]
    public class ImageNews : Entity
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

        public string Title
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public DateTime Time
        {
            get;
            set;
        }

        public int Read
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
