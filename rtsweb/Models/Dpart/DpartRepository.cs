using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using rts.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rtsweb.Models
{
    public class DpartRepository : Repository<DpartRepository, Dpart>
    {
        public Dpart GetDepartDuty(int nIndex, string nDepart)
        {
            var dparts = new List<Dpart>();
            foreach (var i in mDparts)
            {
                if (i.Depart == nDepart)
                {
                    dparts.Add(i);
                }
            }
            if (dparts.Count < 1) return null;
            dparts.Sort();
            return dparts[nIndex % dparts.Count];
        }

        public bool CanDeleteDepart(string nName)
        {
            foreach (var i in mDparts)
            {
                if (i.Depart == nName)
                {
                    return false;
                }
            }
            return true;
        }

        public void InsertDpart(Dpart nDpart)
        {
            this.Insert(nDpart);

            mDparts.Add(nDpart);
        }

        public bool DeleteDpart(string nId)
        {
            if ( this.Delete(nId) )
            {
                for (int i = 0; i < mDparts.Count; ++i)
                {
                    if (nId == mDparts[i].Id)
                    {
                        mDparts.RemoveAt(i);
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
            return JsonConvert.SerializeObject(mDparts, Formatting.Indented, timeConverter);
        }

        protected override string GetRepositoryName()
        {
            return "DpartRepository";
        }

        public List<Dpart> GetValue()
        {
            return mDparts;
        }

        public DpartRepository()
        {
            this.GetCollection();

            mDparts = new List<Dpart>();
            mDparts = this.FindAll().ToList();
        }

        List<Dpart> mDparts;
    }
}
