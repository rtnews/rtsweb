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
        public HttpResponseMessage AddDepart([FromBody]DepartRequest nDepartRequest)
        {
            var respository = DepartRepository.Instance();

            Depart depart = new Depart();
            depart.Identifier = nDepartRequest.Identifier;
            depart.Name = nDepartRequest.Name;
            respository.InsertDepart(depart);

            return this.ToJsonValue(depart);
        }

        [HttpPost]
        public HttpResponseMessage DeleteDepart([FromBody]NewsDeleteRequest nNewDelete)
        {
            var respository = DepartRepository.Instance();
            return ToJsonValue(respository.DeleteDepart(nNewDelete.Id));
        }

        [HttpGet]
        public HttpResponseMessage GetDepartList()
        {
            var respository = DepartRepository.Instance();
            string value = respository.GetStringValue();
            return ToJsonValue(value);
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
