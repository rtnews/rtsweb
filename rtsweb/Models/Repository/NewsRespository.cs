﻿using System;
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
        public List<ImageNews> GetPageInfo(int nPageId)
        {
            return this.FindAll(nPageId, 30).ToList();
        }

        public bool UpdateRead(string nId)
        {
            var update = Updater.Inc<int>(i => i.Read, 1);
            if (this.Update(nId, update))
            {
                foreach (var i in mImageNews)
                {
                    if (i.Id == nId)
                    {
                        i.Read++;
                    }
                }
                return true;
            }
            return false;
        }

        public void InsertNews(ImageNews nImageNews)
        {
            this.Insert(nImageNews);

            mImageNews.Insert(0, nImageNews);
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
            mImageNews = this.FindAll(0, 30).ToList();
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
