using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MicroSite
{
    public partial class News : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BaseMenu = EnumMenu.Home;
        }
    }
}