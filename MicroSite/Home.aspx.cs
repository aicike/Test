using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;

namespace MicroSite
{
    public partial class Home : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int accountMainID = 1;
            var value = DESEncrypt.Encrypt(accountMainID + "");
            ViewState["menu"] = value;

        }
    }
}