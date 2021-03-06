﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using System.Web.Mvc;
using Poco;
using Injection;
using Interface;
using Poco.WebAPI_Poco;
using Poco.Enum;
using Web.Models;
using Business;

namespace Web.Controllers
{
    public class WebRequest_OtherController : Controller
    {
        public string GetQRCode(int amid)
        {
            string androidUrl = string.Format("{0}/Download/{1}/android.jpg", SystemConst.WebUrlIP, amid);
            string iosUrl = string.Format("{0}/Download/{1}/ios.jpg", SystemConst.WebUrlIP, amid);
            return Newtonsoft.Json.JsonConvert.SerializeObject(new { a = androidUrl, i = iosUrl });
        }

        /// <summary>
        /// 获取图文素材
        /// </summary>
        /// <param name="amid">AccountMainID</param>
        public string GetLibraryImageText(int amid)
        {
            var libraryImageTextModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            var list = libraryImageTextModel.GetLibraryList(amid).ToList();
            List<App_ImageText> appList = new List<App_ImageText>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    App_ImageText app = new App_ImageText();
                    app.ID = item.ID;
                    app.I = SystemConst.WebUrlIP + Url.Content(item.ImagePath ?? "");
                    app.T = item.Title;
                    app.S = item.Summary;
                    app.C = item.Content;
                    if (item.LibraryImageTexts != null && item.LibraryImageTexts.Count > 0)
                    {
                        List<App_ImageText> appSubList = new List<App_ImageText>();
                        foreach (var sub in item.LibraryImageTexts)
                        {
                            App_ImageText appSub = new App_ImageText();
                            appSub.ID = sub.ID;
                            appSub.I = SystemConst.WebUrlIP + Url.Content(sub.ImagePath ?? "");
                            appSub.T = sub.Title;
                            appSub.S = sub.Summary;
                            appSub.C = sub.Content;
                            appSubList.Add(appSub);
                        }
                        app.Sub = appSubList;
                    }
                    else
                    {
                        app.Sub = new List<App_ImageText>();
                    }
                    appList.Add(app);
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(appList);
        }

        /// <summary>
        /// 获取图片素材
        /// </summary>
        /// <param name="amid">AccountMainID</param>
        public string GetLibraryImage(int amid)
        {
            var libraryImageModel = Factory.Get<ILibraryImageModel>(SystemConst.IOC_Model.LibraryImageModel);
            var list = libraryImageModel.GetLibraryList(amid).ToList();
            List<App_Image> appList = new List<App_Image>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    App_Image app = new App_Image();
                    app.ID = item.ID;
                    app.I = SystemConst.WebUrlIP + Url.Content(item.FilePath ?? "");
                    app.T = item.FileName;
                    appList.Add(app);
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(appList);
        }

        /// <summary>
        /// 获取语音素材
        /// </summary>
        /// <param name="amid">AccountMainID</param>
        public string GetLibraryVoice(int amid)
        {
            var libraryVoiceModel = Factory.Get<ILibraryVoiceModel>(SystemConst.IOC_Model.LibraryVoiceModel);
            var list = libraryVoiceModel.GetLibraryList(amid).ToList();
            List<App_Voice> appList = new List<App_Voice>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    App_Voice app = new App_Voice();
                    app.ID = item.ID;
                    app.I = SystemConst.WebUrlIP + Url.Content(item.FilePath ?? "");
                    app.T = item.FileName;
                    app.L = item.FileLength;
                    appList.Add(app);
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(appList);
        }

        /// <summary>
        /// 获取视频素材
        /// </summary>
        /// <param name="amid">AccountMainID</param>
        public string GetLibraryVideo(int amid)
        {
            var libraryVideoModel = Factory.Get<ILibraryVideoModel>(SystemConst.IOC_Model.LibraryVideoModel);
            var list = libraryVideoModel.GetLibraryList(amid).ToList();
            List<App_Video> appList = new List<App_Video>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    App_Video app = new App_Video();
                    app.ID = item.ID;
                    app.I = SystemConst.WebUrlIP + Url.Content(item.FilePath ?? "");
                    app.T = item.FileName;
                    app.L = item.FileLength;
                    appList.Add(app);
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(appList);
        }

        /// <summary>
        /// 获取软文显示列表
        /// </summary>
        /// <param name="AccountID">售楼部ID</param>
        /// <param name="ID">显示开始ID 第一次打开传0</param>
        /// <param name="ListCnt">返回列表的条数</param>
        /// <returns></returns>
        [AllowAnonymous]
        public string GetAdvertorialList(int AMID, int ID, int ListCnt)
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var list = AppAdvertorialModel.GetList(AMID, (int)EnumAdvertorialUType.UserEnd);
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
                    ADVERTORIAL.URL = SystemConst.WebUrlIP + "/Default/News?id_token=" + item.ID.TokenEncrypt();
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
                    ADVERTORIAL.S = SystemConst.WebUrlIP + Url.Content(item.AppShowImagePath ?? "");
                    ADVERTORIAL.F = SystemConst.WebUrlIP + Url.Content(item.AppShowImagePath ?? "");
                    ADVERTORIAL.URL = SystemConst.WebUrlIP + "/Default/News?id_token=" + item.ID.TokenEncrypt();
                    ListShow.Add(ADVERTORIAL);
                }
            }

            var jsonStr = new { TitleImg = TitleShow, List = ListShow };

            return Newtonsoft.Json.JsonConvert.SerializeObject(jsonStr);
        }

        /// <summary>
        /// 获取软文显示列表（微网站）
        /// </summary>
        /// <param name="AccountID">售楼部ID</param>
        /// <param name="ID">显示开始ID 第一次打开传0</param>
        /// <param name="ListCnt">返回列表的条数</param>
        /// <returns></returns>
        public JsonpResult GetAdvertorialList_Jsonp(int AMID, int ID, int ListCnt)
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var list = AppAdvertorialModel.GetList(AMID, (int)EnumAdvertorialUType.UserEnd);
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

            return this.Jsonp(jsonStr);
        }

        /// <summary>
        /// 获取软文详细信息
        /// </summary>
        /// <param name="AccountID">售楼部ID</param>
        /// <param name="ID"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public string GetAdvertorialInfo(int AMID, int ID)
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var list = AppAdvertorialModel.GetList(AMID, (int)EnumAdvertorialUType.UserEnd);
            var Info = list.Where(a => a.ID == ID).FirstOrDefault();
            _B_Advertorial ADVERTORIAL = new _B_Advertorial();
            ADVERTORIAL.I = Info.ID;
            ADVERTORIAL.T = Info.Title;
            ADVERTORIAL.D = Info.IssueDate.ToString("yyyy-MM-dd");
            ADVERTORIAL.S = SystemConst.WebUrlIP + Url.Content(Info.MainImagPath ?? "");
            ADVERTORIAL.C = Info.Content;
            return Newtonsoft.Json.JsonConvert.SerializeObject(ADVERTORIAL);
        }



        /// <summary>
        ///  销售端获取软文显示列表
        /// </summary>
        /// <param name="AccountID">售楼部ID</param>
        /// <param name="ID">显示开始ID 第一次打开传0</param>
        /// <param name="ListCnt">返回列表的条数</param>
        /// <returns></returns>
        public string GetAdvertorialListForAccount(int AMID, int ID, int ListCnt)
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var list = AppAdvertorialModel.GetList(AMID, (int)EnumAdvertorialUType.AccountEnd);
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
                    ADVERTORIAL.URL = SystemConst.WebUrlIP + "/Default/News?id_token=" + item.ID.TokenEncrypt();
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
                    ADVERTORIAL.S = SystemConst.WebUrlIP + Url.Content(item.AppShowImagePath ?? "");
                    ADVERTORIAL.F = SystemConst.WebUrlIP + Url.Content(item.AppShowImagePath ?? "");
                    ADVERTORIAL.URL = SystemConst.WebUrlIP + "/Default/News?id_token=" + item.ID.TokenEncrypt();
                    ListShow.Add(ADVERTORIAL);
                }
            }

            var jsonStr = new { TitleImg = TitleShow, List = ListShow };

            return Newtonsoft.Json.JsonConvert.SerializeObject(jsonStr);
        }

        /// <summary>
        /// 销售端获取软文详细信息
        /// </summary>
        /// <param name="AccountID">售楼部ID</param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetAdvertorialInfoForAccount(int AMID, int ID)
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var list = AppAdvertorialModel.GetList(AMID, (int)EnumAdvertorialUType.AccountEnd);
            var Info = list.Where(a => a.ID == ID).FirstOrDefault();
            _B_Advertorial ADVERTORIAL = new _B_Advertorial();
            ADVERTORIAL.I = Info.ID;
            ADVERTORIAL.T = Info.Title;
            ADVERTORIAL.D = Info.IssueDate.ToString("yyyy-MM-dd");
            ADVERTORIAL.S = SystemConst.WebUrlIP + Url.Content(Info.MainImagPath ?? "");
            ADVERTORIAL.C = Info.Content;

            return Newtonsoft.Json.JsonConvert.SerializeObject(ADVERTORIAL);
        }

        /// <summary>
        /// 获取等待界面图片
        /// </summary>
        /// <returns></returns>
        public string GetWaitImg(int AMID)
        {
            var AppWaitImgModel = Factory.Get<IAppWaitImgModel>(SystemConst.IOC_Model.AppWaitImgModel);
            var waitimg = AppWaitImgModel.getAppWaitImg(AMID);
            if (waitimg != null)
            {
                return SystemConst.WebUrlIP + Url.Content(waitimg.ImgPath ?? "");
            }
            return "";
        }

        /// <summary>
        /// 检查更新用户端
        /// </summary>
        /// <param name="type">IOS=0,Android=1,</param>
        /// <param name="amid"></param>
        /// <param name="version"></param>
        /// <returns>1:有更新 0:无更新</returns>
        public string CheckAppVersion(int type, int amid, string version)
        {
            var accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var osType = (EnumClientSystemType)type;
            var versionInfo = accountMainModel.CheckAppVersion(osType, amid, version);
            return Newtonsoft.Json.JsonConvert.SerializeObject(versionInfo);
        }

        /// <summary>
        /// 检查更新销售端
        /// </summary>
        /// <param name="type">IOS=0,Android=1,</param>
        /// <param name="amid"></param>
        /// <param name="version"></param>
        /// <returns>1:有更新 0:无更新</returns>
        public string CheckAppVersion_Sell(int type, int amid, string version)
        {
            var accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var osType = (EnumClientSystemType)type;
            var versionInfo = accountMainModel.CheckAppSellVersion(osType, amid, version);
            return Newtonsoft.Json.JsonConvert.SerializeObject(versionInfo);
        }

        /// <summary>
        /// 检查是否有相应的App权限
        /// </summary>
        /// <param name="menus">菜单对应的MenuID</param>
        /// <param name="userType">用户类型 1销售，2用户</param>
        /// /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public string CheckAppPermission(string menuIDs, int userType, int userID)
        {
            Result result = new Result();
            if (!string.IsNullOrEmpty(menuIDs))
            {
                var ids = menuIDs.ConvertToIntArray(',');
                var ut = (EnumClientUserType)userType;//当前用户身份的类型
                switch (ut)
                {
                    case EnumClientUserType.Account:
                        var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
                        var appmenus = accountModel.CheckAppPermission(ids.ToList(), userID);
                        result.Entity = appmenus;
                        break;
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }


        /// <summary>
        ///  
        /// </summary>
        /// <param name="amid">AccountMainID</param>
        public string LINSHI()
        {
            return "True";
        }

        /// <summary>
        /// 处理转发记录
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="AdvertoriaID">资讯类型</param>
        /// <param name="EnumSocialType">社交类型 枚举 0：微信 1：微信朋友圈  2新浪微博</param>
        /// <returns></returns>
        public void HandleForwardCnt(int UserID, int AdvertoriaID, int EnumSocialType)
        {
            string str = "update AppAdvertorialOperation set ForwardCnt = (ForwardCnt+1),{2}={3} where AppAdvertorialID = {0} and UserID = {1}";
            string sql = "";
            switch (EnumSocialType)
            {
                case (int)Poco.Enum.EnumSocialType.SinaWeibo:
                    sql = string.Format(str, AdvertoriaID, UserID, "ForwardWeiboCnt", "(ForwardWeiboCnt+1)");
                    break;
                case (int)Poco.Enum.EnumSocialType.WeiXin:
                    sql = string.Format(str, AdvertoriaID, UserID, "ForwardWeiXinCnt", "(ForwardWeiXinCnt+1)");
                    break;
                case (int)Poco.Enum.EnumSocialType.WeixinFriendCircle:
                    sql = string.Format(str, AdvertoriaID, UserID, "ForwardFriendCnt", "(ForwardFriendCnt+1)");
                    break;
            }

            var cmd = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            cmd.SqlExecute(sql);

        }


    }
}
