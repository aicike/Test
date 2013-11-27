using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;

namespace Web.Controllers
{
    public class LibraryImageController : LibraryController
    {
        public ActionResult Index(int? id)
        {
            var libraryModel = Factory.Get<ILibraryImageModel>(SystemConst.IOC_Model.LibraryImageModel);
            var list = libraryModel.GetLibraryList(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 15);
            ViewBag.LibraryType = LibraryType();
            ViewBag.HostName = LoginAccount.HostName;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "素材管理-图片素材", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(list);
        }

        [HttpPost]
        public string Upload()
        {
            if (Request.Files.Count > 0)
            {
                var libraryModel = Factory.Get<ILibraryImageModel>(SystemConst.IOC_Model.LibraryImageModel);
                LibraryImage entity = new LibraryImage();
                entity.AccountMainID = LoginAccount.CurrentAccountMainID;
                
                var result = libraryModel.Upload(entity, Request.Files[0]);
                if (result.HasError)
                {
                    return AlertJS_NoTag(new Dialog(result.Error));
                }
                return "window.location.href='" + Url.Action("Index", "LibraryImage", new { HostName = LoginAccount.HostName }) + "'";
            }
            else
            {
                return AlertJS_NoTag(new Dialog("未能成功上传文件，请重试。"));
            }
        }

        public ActionResult Delete(int id)
        {
            var libraryModel = Factory.Get<ILibraryImageModel>(SystemConst.IOC_Model.LibraryImageModel);
            var result = libraryModel.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "LibraryImage", new { HostName = LoginAccount.HostName }) + "'");
        }

        public string ReName(int id, string name)
        {
            var libraryModel = Factory.Get<ILibraryImageModel>(SystemConst.IOC_Model.LibraryImageModel);
            var result = libraryModel.ReName(id, name);
            if (result.HasError)
            {
                return AlertJS_NoTag(new Dialog(result.Error));
            }
            return "window.location.href='" + Url.Action("Index", "LibraryImage", new { HostName = LoginAccount.HostName }) + "'";
        }
    }
}
