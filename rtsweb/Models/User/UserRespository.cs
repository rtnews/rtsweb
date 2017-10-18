using rts.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rtsweb.Models
{
    public class UserRespository : Repository<UserRespository, UserInfo>
    {
        public UserInfo UserInfo { get; set; }

        protected override string GetRepositoryName()
        {
            return "UserRespository";
        }

        void InitUserInfo()
        {
            UserInfo = this.QueryOne();

            if (null == UserInfo)
            {
                UserInfo = new UserInfo();

                UserInfo.Name = "admin@rtnews.com";
                UserInfo.Password = "rtnews123";
                this.InsertOne(UserInfo);
            }
        }

        public UserRespository()
        {
            this.GetCollection();

            this.InitUserInfo();
        }
    }
}
