﻿using rts.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rtsweb.Models
{
    [Serializable]
    public class Depart : Entity
    {
        public string Identifier
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}