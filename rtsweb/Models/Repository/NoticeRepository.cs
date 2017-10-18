using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rtsweb.Models
{
    public class NoticeRepository : NewsRespository<NoticeRepository>
    {
        protected override string GetRepositoryName()
        {
            return "NoticeRepository";
        }
    }
}
