using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Poco;

namespace MicroSite.News
{
    public partial class NewsPage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int lastID = Convert.ToInt32(Request.QueryString["id"]);

                string url = "http://192.168.1.138/WebRequest_Other/GetAdvertorialList";
                HttpRequestHelp hrh = new HttpRequestHelp();
                string value = hrh.QueryStringForPost(url, new HttpRequestParam("AMID", 1), new HttpRequestParam("ID", lastID), new HttpRequestParam("ListCnt", 10));
                var jsonStr = new { TitleImg = new List<_B_Advertorial>(), List = new List<_B_Advertorial>() };
                var obj = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(value, jsonStr);

                repeater1.DataSource = obj.List;
                repeater1.DataBind();
            }
        }
    }
}