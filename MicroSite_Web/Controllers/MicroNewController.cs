using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Poco.Enum;

namespace MicroSite_Web.Controllers
{
    public class MicroNewController : UserBaseController
    {
        public ActionResult Index(int AMID, int ID, int ListCnt)
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var list = AppAdvertorialModel.GetList(AMID, (int)EnumAdvertorialUType.UserEnd, (int)EnumAdverClass.AdverTorial);
            PagedList<AppAdvertorial> RtitleImg = null;
            PagedList<AppAdvertorial> RListImg = null;
            if (ID == 0)
            {
                RtitleImg = list.Where(a => a.stick == 1).ToPagedList(1, 5);
                RListImg = list.Where(a => a.stick == 0).ToPagedList(1, ListCnt);
            }
            else
            {
                RListImg = list.Where(a => a.stick == 0 && a.ID < ID).ToPagedList(1, ListCnt);
            }
            List<_B_Advertorial> TitleShow = new List<_B_Advertorial>();
            if (RtitleImg != null)
            {
                foreach (var item in RtitleImg)
                {
                    _B_Advertorial ADVERTORIAL = new _B_Advertorial();
                    ADVERTORIAL.I = item.ID;
                    ADVERTORIAL.T = item.Title;
                    ADVERTORIAL.P = item.Depict;
                    ADVERTORIAL.S = SystemConst.WebUrlIP + Url.Content(item.AppShowImagePath ?? "");
                    TitleShow.Add(ADVERTORIAL);
                }
            }
            List<_B_Advertorial> ListShow = new List<_B_Advertorial>();
            if (RListImg != null)
            {
                foreach (var item in RListImg)
                {
                    _B_Advertorial ADVERTORIAL = new _B_Advertorial();
                    ADVERTORIAL.I = item.ID;
                    ADVERTORIAL.T = item.Title;
                    ADVERTORIAL.P = item.Depict;
                    ADVERTORIAL.D = item.IssueDate.ToString("yyyy-MM-dd");
                    ADVERTORIAL.S = SystemConst.WebUrlIP + Url.Content(item.MinImagePath ?? "");
                    ADVERTORIAL.F = SystemConst.WebUrlIP + Url.Content(item.AppShowImagePath ?? "");
                    ListShow.Add(ADVERTORIAL);
                }
            }
            ViewBag.TitleShow = TitleShow;
            ViewBag.ListShow = ListShow;
            if (ListShow.Count > 0)
            {
                ViewBag.LastID = ListShow.LastOrDefault().I;
            }
            else
            {
                ViewBag.LastID = 0;
            }
            return View();
        }

        public string GetPage(int AMID, int ID, int ListCnt)
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var list = AppAdvertorialModel.GetList(AMID, (int)EnumAdvertorialUType.UserEnd, (int)EnumAdverClass.AdverTorial);
            PagedList<AppAdvertorial> RtitleImg = null;
            PagedList<AppAdvertorial> RListImg = null;
            if (ID == 0)
            {
                RtitleImg = list.Where(a => a.stick == 1).ToPagedList(1, 5);
                RListImg = list.Where(a => a.stick == 0).ToPagedList(1, ListCnt);
            }
            else
            {
                RListImg = list.Where(a => a.stick == 0 && a.ID < ID).ToPagedList(1, ListCnt);
            }
            List<_B_Advertorial> TitleShow = new List<_B_Advertorial>();
            if (RtitleImg != null)
            {
                foreach (var item in RtitleImg)
                {
                    _B_Advertorial ADVERTORIAL = new _B_Advertorial();
                    ADVERTORIAL.I = item.ID;
                    ADVERTORIAL.T = item.Title;
                    ADVERTORIAL.P = item.Depict;
                    ADVERTORIAL.S = SystemConst.WebUrlIP + Url.Content(item.AppShowImagePath ?? "");
                    TitleShow.Add(ADVERTORIAL);
                }
            }
            List<_B_Advertorial> ListShow = new List<_B_Advertorial>();
            if (RListImg != null)
            {
                foreach (var item in RListImg)
                {
                    _B_Advertorial ADVERTORIAL = new _B_Advertorial();
                    ADVERTORIAL.I = item.ID;
                    ADVERTORIAL.T = item.Title;
                    ADVERTORIAL.P = item.Depict;
                    ADVERTORIAL.D = item.IssueDate.ToString("yyyy-MM-dd");
                    ADVERTORIAL.S = SystemConst.WebUrlIP + Url.Content(item.MinImagePath ?? "");
                    ADVERTORIAL.F = SystemConst.WebUrlIP + Url.Content(item.AppShowImagePath ?? "");
                    ListShow.Add(ADVERTORIAL);
                }
            }
            var jsonStr = new { TitleImg = TitleShow, List = ListShow };
            return Newtonsoft.Json.JsonConvert.SerializeObject(jsonStr);
        }

        public ActionResult View(int id)
        {
            ViewBag.ID = id;
            return View();
        }
    }
}
