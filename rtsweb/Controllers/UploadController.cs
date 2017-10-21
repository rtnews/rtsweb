using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using rtsweb.Models;
using rts.core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net.Http;

namespace rtsweb.Controllers
{
    public class UploadController : Controller
    {
        [HttpPost]
        public string UploadNewsTmp(HttpPostedFileBase file)
        {
            var newsTmp = UploadTemplate(file);

            var repository = NewsTmpRepository.Instance();
            repository.InsertNews(newsTmp);

            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(newsTmp, Formatting.Indented, timeConverter);
        }

        [HttpPost]
        public HttpResponseMessage UploadHomeNews(HttpPostedFileBase file)
        {
            var imageNews = UploadNews(file);

            var repository = HomeRepository.Instance();
            repository.InsertNews(imageNews);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            //IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            //return JsonConvert.SerializeObject(imageNews, Formatting.Indented, timeConverter);
            //return JsonConvert.SerializeObject(imageNews);
        }

        [HttpPost]
        public string UploadNoticeNews(HttpPostedFileBase file)
        {
            var imageNews = UploadNews(file);

            var repository = NoticeRepository.Instance();
            repository.InsertNews(imageNews);

            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(imageNews, Formatting.Indented, timeConverter);
        }

        [HttpPost]
        public string UploadGlobalNews(HttpPostedFileBase file)
        {
            var imageNews = UploadNews(file);

            var repository = GlobRepository.Instance();
            repository.InsertNews(imageNews);

            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(imageNews, Formatting.Indented, timeConverter);
        }

        ImageNews UploadNews(HttpPostedFileBase file)
        {
            var name = DateTime.Now.ToString("yyyyMMddHHmmss");
            var dir = Path.Combine(Request.MapPath("~/Upload/ImageNews"), name);
            Directory.CreateDirectory(dir);
            var fileName = file.FileName;
            var path = Path.Combine(dir, fileName);
            file.SaveAs(path);

            ImageNews imageNews = new ImageNews();
            imageNews.Name = name;
            imageNews.FileName = fileName;
            imageNews.Time = DateTime.Now;

            var doc2Png = DocHelper.Run2Png(path, dir);
            imageNews.Count = doc2Png.Pages;
            imageNews.Title = doc2Png.Title;
            imageNews.Text = doc2Png.Text;

            return imageNews;
        }

        NewsTmp UploadTemplate(HttpPostedFileBase file)
        {
            var name = DateTime.Now.ToString("yyyyMMddHHmmss");
            var dir = Path.Combine(Request.MapPath("~/Upload/NewsTmp"), name);
            Directory.CreateDirectory(dir);
            var fileName = file.FileName;
            var path = Path.Combine(dir, fileName);
            file.SaveAs(path);

            NewsTmp newTmp = new NewsTmp();
            newTmp.Name = name;
            newTmp.FileName = fileName;
            newTmp.Time = DateTime.Now;

            var doc2Png = DocHelper.Run2Png(path, dir);
            newTmp.Count = doc2Png.Pages;

            return newTmp;
        }
    }
}
