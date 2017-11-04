using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using rtsweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace rtsweb.Controllers
{
    public class DpartController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetDutyRep()
        {
            var respository = DepartRepository.Instance();
            var respository1 = DpartRepository.Instance();

            var response = new DpartRepResponse();
            var dutyList = new List<Dpart>();
            foreach (var i in respository.GetValue())
            {
                var index = this.GetDepartDuty(i);
                var dpart = respository1.GetDepartDuty(index, i.Name);
                if (null != dpart)
                {
                    dutyList.Add(dpart);
                }
            }
            response.Dparts = dutyList;

            return ToJsonValue(response);
        }

        [HttpGet]
        public HttpResponseMessage GetDutyList()
        {
            var respository = DepartRepository.Instance();
            var respository1 = DpartRepository.Instance();

            var dutyList = new List<Dpart>();
            foreach (var i in respository.GetValue())
            {
                var index = this.GetDepartDuty(i);
                var dpart = respository1.GetDepartDuty(index, i.Name);
                if (null != dpart)
                {
                    dutyList.Add(dpart);
                }
            }

            return ToJsonValue(dutyList);
        }
        
        [HttpPost]
        public HttpResponseMessage AddDpart([FromBody]DpartRequest nDpartRequest)
        {
            var respository = DpartRepository.Instance();

            Dpart dpart = new Dpart();
            dpart.Identifier = nDpartRequest.Identifier;
            dpart.ClerkId = nDpartRequest.ClerkId;
            dpart.Name = nDpartRequest.Name;
            dpart.Depart = nDpartRequest.Depart;
            dpart.Phone = nDpartRequest.Phone;
            dpart.Icon = nDpartRequest.Icon;

            respository.InsertDpart(dpart);

            return ToJsonValue(dpart);
        }

        [HttpPost]
        public HttpResponseMessage DeleteDpart([FromBody]NewsDeleteRequest nNewDelete)
        {
            var respository = DpartRepository.Instance();
            return ToJsonValue(respository.DeleteDpart(nNewDelete.Id));
        }

        int GetDepartDuty(Depart nDepart)
        {
            var dataTime = Convert.ToDateTime(nDepart.DutyTime);
            var timeSpan = DateTime.Now - dataTime;
            return timeSpan.Days;
        }

        HttpResponseMessage ToJsonValue(Object nObject, HttpStatusCode nHttpStatusCode)
        {
            String value_;
            if (nObject is String || nObject is Char)
            {
                value_ = nObject.ToString();
            }
            else
            {
                value_ = JsonConvert.SerializeObject(nObject);
            }
            HttpResponseMessage result_ = new HttpResponseMessage();
            result_.StatusCode = nHttpStatusCode;
            result_.Content = new StringContent(value_, Encoding.GetEncoding("UTF-8"), "application/json");
            return result_;
        }

        HttpResponseMessage ToJsonValue(Object nObject)
        {
            String value_;
            if (nObject is String || nObject is Char)
            {
                value_ = nObject.ToString();
            }
            else
            {
                value_ = JsonConvert.SerializeObject(nObject);
            }
            HttpResponseMessage result_ = new HttpResponseMessage();
            result_.Content = new StringContent(value_, Encoding.GetEncoding("UTF-8"), "application/json");
            return result_;
        }
    }
}
