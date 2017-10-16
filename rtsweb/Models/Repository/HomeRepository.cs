using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rtsweb.Models
{
    public class HomeRepository : NewsRespository<HomeRepository>
    {
        protected override string GetRepositoryName()
        {
            return "HomeRepository";
        }
    }
}
