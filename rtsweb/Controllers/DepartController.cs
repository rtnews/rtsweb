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
    public class DepartController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage ChangeDutyTime([FromBody]DepartDutyTime nDepartDutyTime)
        {
            var respository = DepartRepository.Instance();
            bool change = respository.ChangeDutyTime(nDepartDutyTime.Identifier, nDepartDutyTime.DutyTime);

            return this.ToJsonValue(change);
        }

        [HttpPost]
        public HttpResponseMessage AddDepart([FromBody]DepartRequest nDepartRequest)
        {
            var respository = DepartRepository.Instance();

            Depart depart = new Depart();
            depart.Identifier = nDepartRequest.Identifier;
            depart.Name = nDepartRequest.Name;
            depart.DutyTime = DateTime.Now;
            respository.InsertDepart(depart);

            return this.ToJsonValue(depart);
        }

        [HttpPost]
        public HttpResponseMessage DeleteDepart([FromBody]NewsDeleteRequest nNewDelete)
        {
            var respository = DepartRepository.Instance();
            var respository1 = ClerkRepository.Instance();
            if (respository1.CanDeleteDepart(nNewDelete.Name))
            {
                return ToJsonValue(respository.DeleteDepart(nNewDelete.Id));
            }
            return ToJsonValue(false);
        }

        [HttpGet]
        public HttpResponseMessage GetDepartList()
        {
            var departListRespone = new DepartListRespone();

            var departs = DepartRepository.Instance();
            var clerks = ClerkRepository.Instance();

            departListRespone.Departs = departs.GetValue();
            departListRespone.Clerks = clerks.GetValue();

            return ToJsonValue(departListRespone);
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
