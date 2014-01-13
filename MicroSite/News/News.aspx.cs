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
using Injection;
using Interface;
using Poco.Enum;

namespace MicroSite.News
{
    public partial class News : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BaseMenu = EnumMenu.Home;


                string url = "http://192.168.1.166/WebRequest_Other/GetAdvertorialList";
                HttpRequestHelp hrh = new HttpRequestHelp();
                string value = hrh.QueryStringForPost(url, new HttpRequestParam("AMID", 1), new HttpRequestParam("ID", 0), new HttpRequestParam("ListCnt", 10));
                var jsonStr = new { TitleImg = new List<_B_Advertorial>(), List = new List<_B_Advertorial>() };
                var obj = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(value, jsonStr);

                var list = new List<_B_Advertorial>();
                list.AddRange(obj.TitleImg);
                list.AddRange(obj.List);
                //list.OrderBy(a => a.I);
                //int page = Convert.ToInt32(Math.Ceiling(list.Count * 1.0 / 10));


                //var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
                ////var newID1s = AppAdvertorialModel.GetList(1, (int)EnumAdvertorialUType.UserEnd).Where(a => a.stick == 1).Select(a => a.ID).ToList();
                //var newID2s = AppAdvertorialModel.GetList(1, (int)EnumAdvertorialUType.UserEnd).Where(a => a.stick == 0).Select(a => a.ID).ToList();
                //List<int> ids = new List<int>();
                //if (obj.List != null && newID2s != null)
                //{
                //    if (newID2s.Count > 10)
                //    {
                //        int i = 10;
                //        while (true)
                //        {
                //            ids.Add(newID2s[i]);
                //            if (i + 10 >= ids.Count)
                //            {
                //                break;
                //            }
                //            else
                //            {
                //                i += 10;
                //            }
                //        }
                //    }
                //}
                if (obj.List.LastOrDefault() != null)
                {
                    ViewState["page"] = obj.List.LastOrDefault().I;
                }
                else {
                    ViewState["page"] = 0;
                }
                repeaterNews.DataSource = list;
                repeaterNews.DataBind();
            }
        }

    }
}