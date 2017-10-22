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
            if (this.InsertOne(nNewsTmp))
            {
                mNewsTmps.Add(nNewsTmp);
            }
        }

        public bool DeleteNewsTmp(string nId)
        {
            Guid id = Guid.Parse(nId);

            if (this.DeleteOne(nId) > 0)
            {
                for (int i = 0; i < mNewsTmps.Count; ++i)
                {
                    if (id == mNewsTmps[i].Id)
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
            mNewsTmps = this.QueryAll();
        }

        List<NewsTmp> mNewsTmps;
    }
}
