using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using rts.core;
using Newtonsoft.Json;

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

        public string GetStringValue()
        {
            return JsonConvert.SerializeObject(mImageNews);
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
