using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface;
using Poco;
using Common;
using Business;
using System.IO;
using System.Drawing;

namespace Web.Controllers
{
    public class SetController : ManageAccountController
    {
        /// <summary>
        /// 首页
        /// </summary>
        public ActionResult Index()
        {
            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var account = accountModel.Get(LoginAccount.ID);
            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "设置-个人信息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            if (TempData["EditStatus"] != null)
            {
                var value = TempData["EditStatus"].ToString().Split('|');
                ViewBag.EditStatus = value[0];
                ViewBag.EditError = value[1];
            }
            return View(account);
        }
        [AllowCheckPermissions(false)]
        [HttpPost]
        public ActionResult Edit(Account account,int w,int h,int x1,int y1,int tw,int th)
        {
            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);

            var result = accountModel.SetEdit(account, LoginAccount.CurrentAccountMainID, x1, y1, w, h, tw, th);
            if (result.HasError)
            {
                TempData["EditStatus"] = "false|" + result.Error;
            }
            else
            {
                LoginAccount.HeadImagePath = account.HeadImagePath;
                TempData["EditStatus"] = "true|";
            }
            return RedirectToAction("Index", "set", new { HostName = LoginAccount.HostName });
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



    }
}
