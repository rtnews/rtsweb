using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rtsweb.Models
{
    public class NewsDeleteRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class NewsUpdateRequest
    {
        public string Id { get; set; }
    }

    public class StartRepRespone
    {
        public List<ImageNews> HomeList { get; set; }
        public List<ImageNews> News0List { get; set; }
        public List<ImageNews> News1List { get; set; }
        public List<Dpart> Dparts { get; set; }
    }
}
