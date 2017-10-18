using rts.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rtsweb.Models
{
    public class UserInfo : Entity
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
