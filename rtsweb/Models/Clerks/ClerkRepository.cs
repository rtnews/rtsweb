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
            this.Insert(nClerk);

            mClerks.Add(nClerk);
        }

        public bool DeleteClerk(string nId)
        {
            if ( this.Delete(nId) )
            {
                for (int i = 0; i < mClerks.Count; ++i)
                {
                    if (nId == mClerks[i].Id)
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
            mClerks = this.FindAll().ToList();
        }

        List<Clerk> mClerks;
    }
}
