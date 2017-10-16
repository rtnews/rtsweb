using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rtsweb.Models
{
    public class LoginSucessModel
    {
        public string messages
        {
            get;
            set;
        }

        public string token
        {
            get;
            set;
        }
    }

    public class LoginSucess
    {
        public LoginSucessModel data
        {
            get;
            set;
        }
    }

    public class LoginFailModel
    {
        public string errors
        {
            get;
            set;
        }
    }

    public class LoginFail
    {
        public LoginFailModel data
        {
            get;
            set;
        }
    }
}
