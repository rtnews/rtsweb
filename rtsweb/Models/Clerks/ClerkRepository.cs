using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using rts.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rtsweb.Models
{
    public class ClerkRepository : Repository<ClerkRepository, Clerk>
    {
        public bool CanDeleteDepart(string nName)
        {
            foreach (var i in mClerks)
            {
                if (i.Depart == nName)
                {
                    return false;
                }
            }
            return true;
        }

        public void InsertClerk(Clerk nClerk)
        {
            if (this.InsertOne(nClerk))
            {
                mClerks.Add(nClerk);
            }
        }

        public bool DeleteClerk(string nId)
        {
            Guid id = Guid.Parse(nId);

            if (this.DeleteOne(nId) > 0)
            {
                for (int i = 0; i < mClerks.Count; ++i)
                {
                    if (id == mClerks[i].Id)
                    {
                        mClerks.RemoveAt(i);
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
            return JsonConvert.SerializeObject(mClerks, Formatting.Indented, timeConverter);
        }

        protected override string GetRepositoryName()
        {
            return "ClerkRepository";
        }

        public List<Clerk> GetValue()
        {
            return mClerks;
        }

        public ClerkRepository()
        {
            this.GetCollection();

            mClerks = new List<Clerk>();
            mClerks = this.QueryAll();
        }

        List<Clerk> mClerks;
    }
}
