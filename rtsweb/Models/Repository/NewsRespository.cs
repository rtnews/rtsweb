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
        public bool UpdateRead(string nId)
        {
            var update = Updater.Inc<int>(i => i.Read, 1);

            return this.Update(nId, update);
        }

        public void InsertNews(ImageNews nImageNews)
        {
            this.Insert(nImageNews);

            mImageNews.Add(nImageNews);
        }

        public bool DeleteNews(string nId)
        {
            if ( this.Delete(nId) )
            {
                for (int i = 0; i < mImageNews.Count; ++i)
                {
                    if (nId == mImageNews[i].Id)
                    {
                        mImageNews.RemoveAt(i);
                        break;
                    }
                }
                return true;
            }
            return false;
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
            mImageNews = this.FindAll(0, 50).ToList();
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
