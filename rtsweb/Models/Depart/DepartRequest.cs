using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rtsweb.Models
{
    public class DepartRequest
    {
        public string Identifier { get; set; }

        public string Name { get; set; }
    }

    public class DepartListRespone
    {
        public List<Depart> Departs { get; set; }

        public List<Clerk> Clerks { get; set; }
    }
}
