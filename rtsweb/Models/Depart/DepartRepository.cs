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
            return this.UpdateOne(depart);
        }

        public void InsertDepart(Depart nDepart)
        {
            if (this.InsertOne(nDepart))
            {
                mDeparts.Add(nDepart);
            }
        }

        public bool DeleteDepart(string nId)
        {
            Guid id = Guid.Parse(nId);

            if (this.DeleteOne(nId) > 0)
            {
                for (int i = 0; i < mDeparts.Count; ++i)
                {
                    if (id == mDeparts[i].Id)
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
            Guid id = Guid.Parse(nId);
            foreach (var i in mDeparts)
            {
                if (i.Id == id)
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
            mDeparts = this.QueryAll();
        }

        List<Depart> mDeparts;
    }
}
