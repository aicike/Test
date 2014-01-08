using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Poco;

namespace MicroSite
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var menu = Session[SystemConst.Session.MicroSiteMenu] == null ? EnumMenu.Home : (EnumMenu)Convert.ToInt32(Session[SystemConst.Session.MicroSiteMenu]);
            ViewState["menu"] = menu;
        }
    }
}