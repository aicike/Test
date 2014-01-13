using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Poco;

namespace MicroSite.Admin
{
    public class BasePage_Admin : System.Web.UI.Page
    {
        protected Account LoginAccount
        {
            get
            {
                var account = Session[SystemConst.Session.MicroSiteLoginAccount] as Account;
                return account;
            }
            set { Session[SystemConst.Session.MicroSiteLoginAccount] = value; }
        }
    }
}