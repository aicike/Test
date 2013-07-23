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
    public class LibraryVideoController : LibraryController
    {
        public ActionResult Index(int? index)
        {
            var libraryModel = Factory.Get<ILibraryVideoModel>(SystemConst.IOC_Model.LibraryVideoModel);
            var list = libraryModel.GetLibraryList(LoginAccount.CurrentAccountMainID).ToPagedList(index ?? 1, 15);
            ViewBag.LibraryType = LibraryType();
            ViewBag.HostName = LoginAccount.HostName;
            return View(list);
        }

        [HttpPost]
        public string Upload()
        {
            if (Request.Files.Count > 0)
            {
                var libraryModel = Factory.Get<ILibraryVideoModel>(SystemConst.IOC_Model.LibraryVideoModel);
                LibraryVideo entity = new LibraryVideo();
                entity.AccountMainID = LoginAccount.CurrentAccountMainID;
                var result = libraryModel.Upload(entity, Request.Files[0]);
                if (result.HasError)
                {
                    return AlertJS_NoTag(new Dialog(result.Error));
                }
                return "window.location.href='" + Url.Action("Index", "LibraryVideo", new { HostName = LoginAccount.HostName }) + "'";
            }
            else
            {
                return AlertJS_NoTag(new Dialog("未能成功上传文件，请重试。"));
            }
        }

        public ActionResult Delete(int id)
        {
            var libraryModel = Factory.Get<ILibraryVideoModel>(SystemConst.IOC_Model.LibraryVideoModel);
            var result = libraryModel.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "LibraryVideo", new { HostName = LoginAccount.HostName }) + "'");
        }

        public string ReName(int id, string name)
        {
            var libraryModel = Factory.Get<ILibraryVideoModel>(SystemConst.IOC_Model.LibraryVideoModel);
            var result = libraryModel.ReName(id, name);
            if (result.HasError)
            {
                return AlertJS_NoTag(new Dialog(result.Error));
            }
            return "window.location.href='" + Url.Action("Index", "LibraryVideo", new { HostName = LoginAccount.HostName }) + "'";
        }
    }
}
