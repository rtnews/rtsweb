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
        [HttpGet]
        public HttpResponseMessage GetStartRep()
        {
            var startRespone = new StartRepRespone();

            var respository = HomeRepository.Instance();
            startRespone.HomeList = respository.GetValue();

            var respository1 = GlobRepository.Instance();
            startRespone.News0List = respository1.GetValue();

            var respository2 = NoticeRepository.Instance();
            startRespone.News1List = respository2.GetValue();

            var respository3 = DpartRepository.Instance();
            var respository4 = DepartRepository.Instance();

            var dutyList = new List<Dpart>();
            foreach (var i in respository4.GetValue())
            {
                var index = this.GetDepartDuty(i);
                var dpart = respository3.GetDepartDuty(index, i.Name);
                dutyList.Add(dpart);
            }

            startRespone.Dparts = dutyList;

            return ToJsonValue(startRespone);
        }

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
                IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
                timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                value_ = JsonConvert.SerializeObject(nObject, Formatting.Indented, timeConverter);
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
                IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
                timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                value_ = JsonConvert.SerializeObject(nObject, Formatting.Indented, timeConverter);
            }
            HttpResponseMessage result_ = new HttpResponseMessage();
            result_.Content = new StringContent(value_, Encoding.GetEncoding("UTF-8"), "application/json");
            return result_;
        }
    }
}
