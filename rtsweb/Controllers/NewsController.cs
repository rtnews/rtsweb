using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using rtsweb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Hosting;

namespace rtsweb.Controllers
{
    public class NewsController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage UpdateHomeRead([FromBody]NewsUpdateRequest nNewsUpdate)
        {
            var respository = HomeRepository.Instance();
            return ToJsonValue(respository.UpdateRead(nNewsUpdate.Id));
        }

        [HttpPost]
        public HttpResponseMessage DeleteHomeNews([FromBody]NewsDeleteRequest nNewDelete)
        {
            var respository = HomeRepository.Instance();
            if (respository.DeleteNews(nNewDelete.Id))
            {
                var dir = HostingEnvironment.MapPath("~/Upload/ImageNews/");
                dir += nNewDelete.Name;
                var directoryInfo = new DirectoryInfo(dir);
                directoryInfo.Delete(true);
                return ToJsonValue(true);
            }
            return ToJsonValue(false);
        }

        [HttpGet]
        public HttpResponseMessage GetHomeList()
        {
            var respository = HomeRepository.Instance();
            string value = respository.GetStringValue();
            return ToJsonValue(value);
        }

        [HttpPost]
        public HttpResponseMessage DeleteNoticeNews([FromBody]NewsDeleteRequest nNewDelete)
        {
            var respository = NoticeRepository.Instance();
            if (respository.DeleteNews(nNewDelete.Id))
            {
                var dir = HostingEnvironment.MapPath("~/Upload/ImageNews/");
                dir += nNewDelete.Name;
                var directoryInfo = new DirectoryInfo(dir);
                directoryInfo.Delete(true);
                return ToJsonValue(true);
            }
            return ToJsonValue(false);
        }

        [HttpGet]
        public HttpResponseMessage GetNoticeList()
        {
            var respository = NoticeRepository.Instance();
            string value = respository.GetStringValue();
            return ToJsonValue(value);
        }

        [HttpPost]
        public HttpResponseMessage DeleteGlobNews([FromBody]NewsDeleteRequest nNewDelete)
        {
            var respository = GlobRepository.Instance();
            if (respository.DeleteNews(nNewDelete.Id))
            {
                var dir = HostingEnvironment.MapPath("~/Upload/ImageNews/");
                dir += nNewDelete.Name;
                var directoryInfo = new DirectoryInfo(dir);
                directoryInfo.Delete(true);
                return ToJsonValue(true);
            }
            return ToJsonValue(false);
        }

        [HttpGet]
        public HttpResponseMessage GetGlobList()
        {
            var respository = GlobRepository.Instance();
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
