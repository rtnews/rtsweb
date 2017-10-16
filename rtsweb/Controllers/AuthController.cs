using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using rtsweb.Models;
using rts.core;
using Newtonsoft.Json;
using System.Text;

namespace rtsweb.Controllers
{
    public class AuthController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Login([FromBody]LoginRequest nLoginRequest)
        {
            if ((nLoginRequest.email == "admin@123.com")
                && (nLoginRequest.password == "1234"))
            {
                string value_ = nLoginRequest.email + nLoginRequest.password;

                LoginSucess loginSucess = new LoginSucess();
                loginSucess.data = new LoginSucessModel();
                loginSucess.data.messages = "登陆成功,欢迎您的回来!";
                loginSucess.data.token = HashId.RunCommon(value_).ToString();
                return ToJsonValue(loginSucess);
            }
            else
            {
                LoginFail loginFail = new LoginFail();
                loginFail.data = new LoginFailModel();
                loginFail.data.errors = "登陆失败,用户或密码不正确";
                return ToJsonValue(loginFail, HttpStatusCode.Unauthorized);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Logout()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
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
