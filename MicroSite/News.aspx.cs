using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.IO;
using Common;
using Poco;

namespace MicroSite
{
    public partial class News : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BaseMenu = EnumMenu.Home;


                string url = "http://192.168.1.138/WebRequest_Other/GetAdvertorialList";
                HttpRequestHelp hrh = new HttpRequestHelp();
                string value = hrh.QueryStringForPost(url, new HttpRequestParam("AMID", 1), new HttpRequestParam("ID", 0), new HttpRequestParam("ListCnt", 10));


                var jsonStr = new { TitleImg = new List<_B_Advertorial>(), List =  new List<_B_Advertorial>() };
                var obj = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(value, jsonStr);

                var list = new List<_B_Advertorial>();
                list.AddRange(obj.TitleImg);
                list.AddRange(obj.List);
                repeaterNews.DataSource=list;
                repeaterNews.DataBind();

            }
        }


    }
}