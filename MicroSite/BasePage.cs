using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Poco;

namespace MicroSite
{
    public class BasePage : System.Web.UI.Page
    {
        protected User LoginUser
        {
            get
            {
                var account = Session[SystemConst.Session.LoginUser] as User;
                return LoginUser;
            }
            set { Session[SystemConst.Session.LoginUser] = value; }
        }
    }
}