using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rtsweb.Models
{
    public class GlobRepository : NewsRespository<GlobRepository>
    {
        protected override string GetRepositoryName()
        {
            return "GlobRepository";
        }
    }
}
