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
    public class LibraryImageTextController : LibraryController
    {
        public ActionResult Index(int? index)
        {
            var libraryModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            var list = libraryModel.GetLibraryList(LoginAccount.CurrentAccountMainID).ToPagedList(index ?? 1, 15);
            ViewBag.LibraryType = LibraryType();
            ViewBag.HostName = LoginAccount.HostName;
            return View(list);
        }

        public ActionResult Delete(int id)
        {
            var libraryModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            var result = libraryModel.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "LibraryText", new { HostName = LoginAccount.HostName }) + "'");
        }

        public ActionResult Add(bool isSingle)
        {
            return View();
        }
    }
}
