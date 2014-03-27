using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Poco;
using Injection;
using Interface;
using Common;
using Business;
using System.IO;
using System.Drawing;
using Poco.Enum;

namespace Web.Controllers
{
    public class AppAdvertorialAccountController : ManageAccountController
    {
        //
        // GET: /AppAdvertorialAccount/

        public ActionResult Index(int? id)
        {
            ViewBag.HostName = LoginAccount.HostName;

            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var list = AppAdvertorialModel.GetList(LoginAccount.CurrentAccountMainID, (int)EnumAdvertorialUType.AccountEnd).ToPagedList(id ?? 1, 15);

            var AccountserverModel = Factory.Get<IAccountMain_ServiceModel>(SystemConst.IOC_Model.AccountMain_ServiceModel);
            var hasAPP = AccountserverModel.CheckService(EnumService.House_Service, LoginAccount.CurrentAccountMainID);
            ViewBag.HasAPP = hasAPP;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "设置-销售端资讯", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.UrlAddress = SystemConst.WebUrl;
            ViewBag.AMID = LoginAccount.CurrentAccountMainID;
            return View(list);
        }

        public ActionResult Add()
        {
            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "设置-销售端资讯-添加项目", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.UrlAddress = SystemConst.WebUrl;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(AppAdvertorial appAdver, int AType, int w, int h, int x1, int y1, int tw, int th)
        {
            if (w <= 0)
            {
                return JavaScript(AlertJS_NoTag(new Dialog("请在图片上选择展示区域")));
            }
            if (AType == (int)EnumAdverTorialType.url)
            {
                appAdver.Content = "";
                appAdver.EnumAdverURLType = (int)EnumAdverURLType.Ordinary;
            }
            appAdver.EnumAdverTorialType = AType;
            appAdver.AccountMainID = LoginAccount.CurrentAccountMainID;
            appAdver.Sort = 0;
            appAdver.IssueDate = DateTime.Now;
            appAdver.EnumAdvertorialUType = (int)EnumAdvertorialUType.AccountEnd;
            appAdver.BrowseCnt = 0;
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            Result result = AppAdvertorialModel.AddAppAdvertorial(appAdver, w, h, x1, y1, tw, th);
            return RedirectToAction("Index", "AppAdvertorialAccount");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "设置-销售端资讯-修改项目", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var AppAdvertorial = AppAdvertorialModel.Get(id);
            ViewBag.stick = AppAdvertorial.stick;
            return View(AppAdvertorial);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(AppAdvertorial appadver, int AType, int w, int h, int x1, int y1, int tw, int th)
        {
            //if (HousShowImagePathFile != null)
            //{
            //    if (w <= 0)
            //    {
            //        return JavaScript(AlertJS_NoTag(new Dialog("请在图片上选择展示区域")));
            //    }
            //}
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);

            var appadvertorials = AppAdvertorialModel.Get(appadver.ID);
            if (appadvertorials.MainImagPath != appadver.MainImagPath)
            {
                if (w <= 0)
                {
                    return JavaScript(AlertJS_NoTag(new Dialog("请在图片上选择展示区域")));
                }
            }
            if (AType == (int)EnumAdverTorialType.url)
            {
                appadver.Content = "";
                if (appadver.EnumAdverURLType.HasValue)
                {

                }
                else
                {
                    appadver.EnumAdverURLType = (int)EnumAdverURLType.Ordinary;
                }
            }
            appadver.EnumAdverTorialType = AType;
            appadver.EnumAdvertorialUType = (int)EnumAdvertorialUType.AccountEnd;
            Result result = AppAdvertorialModel.EditAppAdvertorial(appadver, w, h, x1, y1, tw, th);
            if (result.HasError)
            {
                return JavaScript(" isCommit = true;" + AlertJS_NoTag(new Dialog(result.Error)));
            }
            return RedirectToAction("Index", "AppAdvertorialAccount");
        }

        public ActionResult Delete(int id)
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var result = AppAdvertorialModel.DelAppAdvertorial(id, (int)EnumAdvertorialUType.AccountEnd);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "AppAdvertorialAccount", new { HostName = LoginAccount.HostName }) + "'");
        }

        //校验是否可以置顶
        [HttpPost]
        [AllowCheckPermissions(false)]
        public string chickStick(int ID)
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var AppAdvertorial = AppAdvertorialModel.GetList(LoginAccount.CurrentAccountMainID, (int)EnumAdvertorialUType.AccountEnd);
            if (AppAdvertorial.Where(a => a.stick == 1).Count() >= 5)
            {
                return "No";
            }
            else
            {
                return "Yes";
            }

        }

        //校验修改是否可以置顶
        [HttpPost]
        [AllowCheckPermissions(false)]
        public string chickUpdStick(int ID)
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var AppAdvertorial = AppAdvertorialModel.GetList(LoginAccount.CurrentAccountMainID, (int)EnumAdvertorialUType.AccountEnd);
            if (AppAdvertorial.Where(a => a.stick == 1 && a.ID != ID).Count() >= 5)
            {
                return "No";
            }
            else
            {
                return "Yes";
            }

        }

        //置顶 isok =1 置顶 0 取消置顶
        [AllowCheckPermissions(false)]
        [HttpPost]
        public ActionResult Stick(int AdvertorialID, int isok, int Sort)
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);

            AppAdvertorialModel.EditAppAdvertorialStick(AdvertorialID, isok, LoginAccount.CurrentAccountMainID, Sort, (int)EnumAdvertorialUType.AccountEnd);

            return RedirectToAction("Index", "AppAdvertorialAccount", new { HostName = LoginAccount.HostName });
        }

        //排序
        [AllowCheckPermissions(false)]
        [HttpPost]
        public ActionResult Sort(int AdvertorialID, int Sort, int Type)
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);

            AppAdvertorialModel.EditAppAdvertorialSort(AdvertorialID, LoginAccount.CurrentAccountMainID, Sort, Type, (int)EnumAdvertorialUType.AccountEnd);

            return RedirectToAction("Index", "AppAdvertorialAccount", new { HostName = LoginAccount.HostName });
        }

        //预览
        [AllowCheckPermissions(false)]
        public ActionResult Preview()
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var list = AppAdvertorialModel.GetList(LoginAccount.CurrentAccountMainID, (int)EnumAdvertorialUType.AccountEnd);
            ViewBag.TitleImg = list.Where(a => a.stick == 1).ToPagedList(1, 5);
            ViewBag.ListImg = list.Where(a => a.stick == 0).ToPagedList(1, 5);

            return View();
        }

        [HttpPost]
        [AllowCheckPermissions(false)]
        public string UpImg()
        {
            if (Request.Files.Count > 0)
            {
                string Path = Tool.GetTemporaryPath();
                CommonModel com = new CommonModel();
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                var LastName = token + com.CreateRandom("", 5) + Request.Files[0].FileName.GetFileSuffix();
                //图片显示界面
                var ImagePath = Path + "\\" + LastName;
                var mapePath = Server.MapPath(Path) + "\\" + LastName;
                int dataLengthToRead = (int)Request.Files[0].InputStream.Length;//获取下载的文件总大小
                byte[] buffer = new byte[dataLengthToRead];

                int r = Request.Files[0].InputStream.Read(buffer, 0, dataLengthToRead);//本次实际读取到字节的个数
                Stream tream = new MemoryStream(buffer);
                Image img = Image.FromStream(tream);

                Tool.SuperGetPicThumbnail(img, mapePath, 70, 1280, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);

                return Url.Content(ImagePath);

            }
            else
            {
                return "false";
            }
        }

        //查看详细信息

        [AllowCheckPermissions(false)]
        public ActionResult SelecInfo(int? id, int AID, int AMID, int? type)
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            ViewBag.aao = AppAdvertorialModel.GetInfo(AID, AMID);

            if (!type.HasValue)
            {
                type = 0;
            }
            //已读用户

            var AppAdvertorialOperationModel = Factory.Get<IAppAdvertorialOperationModel>(SystemConst.IOC_Model.AppAdvertorialOperationModel);
            var aaoperation = AppAdvertorialOperationModel.getAOlist_account(AID, type.Value,LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 15);

            ViewBag.AID = AID;
            ViewBag.AMID = AMID;
            ViewBag.type = type.Value;
            return View(aaoperation);
        }

        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="AMID"></param>
        public void CreateQrCode(string Url)
        {
            QrCodeModel model = new QrCodeModel();

            MemoryStream ms = model.Get_Android_DownloadUrl(Url);
            if (null != ms)
            {
                Response.BinaryWrite(ms.ToArray());
            }
        }
    }
}
