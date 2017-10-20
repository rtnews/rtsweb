using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using rts.core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace rtsweb.Models
{
    public abstract class NewsRespository<T> : Repository<T, ImageNews>
        where T : new()
    {
        public void InsertNews(ImageNews nImageNews)
        {
            if ( this.InsertOne(nImageNews) )
            {
                mImageNews.Add(nImageNews);
            }
        }

        public bool DeleteNews(string nId)
        {
            return (this.DeleteOne(nId) > 0);
        }

        public string GetStringValue()
        {
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(mImageNews, Formatting.Indented, timeConverter);
        }

        public List<ImageNews> GetValue()
        {
            return mImageNews;
        }

        void LoadTop()
        {
            mImageNews = this.QueryTop(0, 20);
        }

        public NewsRespository()
        {
            mImageNews = new List<ImageNews>();

            this.GetCollection();
            this.LoadTop();
        }

        List<ImageNews> mImageNews;
    }
}
