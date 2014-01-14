using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MicroSite
{
    public partial class ProductListItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var ss =Request.QueryString["page"];
            var ee = Request.QueryString["maid"];
        }
    }
}