using Newtonsoft.Json;
using rtsweb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Hosting;
using System.Web.Http;

namespace rtsweb.Controllers
{
    public class TemplateController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage DeleteNewsTmp([FromBody]NewsDeleteRequest nNewDelete)
        {
            var respository = NewsTmpRepository.Instance();
            if (respository.DeleteNewsTmp(nNewDelete.Id))
            {
                var dir = HostingEnvironment.MapPath("~/Upload/NewsTmp/");
                dir += nNewDelete.Name;
                var directoryInfo = new DirectoryInfo(dir);
                directoryInfo.Delete(true);
                return ToJsonValue(true);
            }
            return ToJsonValue(false);
        }

        [HttpGet]
        public HttpResponseMessage GetNewsTmpList()
        {
            var respository = NewsTmpRepository.Instance();
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
