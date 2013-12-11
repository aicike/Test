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
using System.IO;
using System.Drawing;
using Business;

namespace Web.Controllers
{
    public class AppWaitImgController : ManageAccountController
    {
        //
        // GET: /AppWaitImg/

        public ActionResult Index()
        {
            var appWaitImgModel = Factory.Get<IAppWaitImgModel>(SystemConst.IOC_Model.AppWaitImgModel);
            var AppWaitImg = appWaitImgModel.getAppWaitImg(LoginAccount.CurrentAccountMainID);
            ViewBag.HostName = LoginAccount.HostName;


            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "设置-App等待页面", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(AppWaitImg);
        }

        [HttpPost]
        [AllowCheckPermissions(false)]
        public ActionResult Index(AppWaitImg appWaitImg, int w, int h, int x1, int y1, int tw, int th)
        {

            appWaitImg.AccountMainID = LoginAccount.CurrentAccountMainID;
            var appWaitImgModel = Factory.Get<IAppWaitImgModel>(SystemConst.IOC_Model.AppWaitImgModel);
            if (!string.IsNullOrEmpty(appWaitImg.ImgPath))
            {
                appWaitImgModel.UpAppWaitImg(appWaitImg, w, h, x1, y1, tw, th);
            }
            return RedirectToAction("Index", "AppWaitImg");
        }
        [AllowCheckPermissions(false)]
        public ActionResult Delete()
        {
            var appWaitImgModel = Factory.Get<IAppWaitImgModel>(SystemConst.IOC_Model.AppWaitImgModel);
            var AppWaitImg = appWaitImgModel.DelAppWaitImg(LoginAccount.CurrentAccountMainID);
            if (AppWaitImg > 0)
            {
                //chengong
            }
            return RedirectToAction("Index", "AppWaitImg");
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

                Tool.SuperGetPicThumbnail(img, mapePath, 50, 1024, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);

                return Url.Content(ImagePath);

            }
            else {
                return "false";
            }
        }

    }
}
