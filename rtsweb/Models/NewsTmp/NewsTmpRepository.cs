using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using rts.core;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;

namespace rtsweb.Models
{
    public class NewsTmpRepository : Repository<NewsTmpRepository, NewsTmp>
    {
        public void InsertNews(NewsTmp nNewsTmp)
        {
            this.Insert(nNewsTmp);

            mNewsTmps.Add(nNewsTmp);
        }

        public bool DeleteNewsTmp(string nId)
        {
            if ( this.Delete(nId) )
            {
                for (int i = 0; i < mNewsTmps.Count; ++i)
                {
                    if (nId == mNewsTmps[i].Id)
                    {
                        mNewsTmps.RemoveAt(i);
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
            return JsonConvert.SerializeObject(mNewsTmps, Formatting.Indented, timeConverter);
        }

        protected override string GetRepositoryName()
        {
            return "NewsTmpRepository";
        }

        public List<NewsTmp> GetValue()
        {
            return mNewsTmps;
        }

        public NewsTmpRepository()
        {
            this.GetCollection();

            mNewsTmps = new List<NewsTmp>();
            mNewsTmps = this.FindAll().ToList();
        }

        List<NewsTmp> mNewsTmps;
    }
}
