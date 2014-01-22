using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Controllers;
using Common;
using Business;
using System.IO;
using System.Drawing;

namespace MicroSite_Web.Controllers
{
    public class PanoramaController : ManageAccountController
    {
        public ActionResult Index(int? id)
        {
            var panoramaModel = Factory.Get<IPanoramaModel>(SystemConst.IOC_Model.PanoramaModel);

            var list = panoramaModel.GetByAccountMainID(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 15);

            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Panorama panorama)
        {
            var panoramaModel = Factory.Get<IPanoramaModel>(SystemConst.IOC_Model.PanoramaModel);
            panorama.AccountMainID = LoginAccount.CurrentAccountMainID;
            var result = panoramaModel.Add(panorama);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "Panorama", new { HostName = LoginAccount.HostName }) + "'");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var panoramaModel = Factory.Get<IPanoramaModel>(SystemConst.IOC_Model.PanoramaModel);
            var result = panoramaModel.Get(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(Panorama panorama)
        {
            var panoramaModel = Factory.Get<IPanoramaModel>(SystemConst.IOC_Model.PanoramaModel);
            var result = panoramaModel.Edit(panorama);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "Panorama", new { HostName = LoginAccount.HostName }) + "'");
        }

        public ActionResult Delete(int id)
        {
            var panoramaModel = Factory.Get<IPanoramaModel>(SystemConst.IOC_Model.PanoramaModel);
            var result = panoramaModel.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "Panorama", new { HostName = LoginAccount.HostName }) + "'");
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
                var fileSize = dataLengthToRead * 1.0 / 1024 / 1024;
                if (fileSize <= 2)
                {
                    //小于2M，不压缩
                    img.Save(mapePath);
                }
                else
                {
                    Tool.SuperGetPicThumbnail(img, mapePath, 2000, 2000, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);
                }
                return Url.Content(ImagePath);

            }
            else
            {
                return "false";
            }
        }
    }
}
