using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using rts.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rtsweb.Models
{
    public class DepartRepository : Repository<DepartRepository, Depart>
    {
        public bool ChangeDutyTime(string nId, string nTime)
        {
            Depart depart = this.GetDepart(nId);
            if  (null == depart)
            {
                return false;
            }
            depart.DutyTime = nTime;

            var update = Updater.Set<string>(i => i.DutyTime, nTime);

            return this.Update(nId, update);
        }

        public void InsertDepart(Depart nDepart)
        {
            this.Insert(nDepart);

            mDeparts.Add(nDepart);
        }

        public bool DeleteDepart(string nId)
        {
            if ( this.Delete(nId) )
            {
                for (int i = 0; i < mDeparts.Count; ++i)
                {
                    if (nId == mDeparts[i].Id)
                    {
                        mDeparts.RemoveAt(i);
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
            return JsonConvert.SerializeObject(mDeparts, Formatting.Indented, timeConverter);
        }

        protected override string GetRepositoryName()
        {
            return "DepartRepository";
        }

        public Depart GetDepart(string nId)
        {
            foreach (var i in mDeparts)
            {
                if (i.Id == nId)
                {
                    return i;
                }
            }
            return null;
        }

        public List<Depart> GetValue()
        {
            return mDeparts;
        }

        public DepartRepository()
        {
            this.GetCollection();

            mDeparts = new List<Depart>();
            mDeparts = this.FindAll().ToList();
        }

        List<Depart> mDeparts;
    }
}
