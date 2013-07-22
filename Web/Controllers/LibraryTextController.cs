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
    public class LibraryTextController : LibraryController
    {
        public ActionResult Index(int? index)
        {
            var libraryModel = Factory.Get<ILibraryTextModel>(SystemConst.IOC_Model.LibraryTextModel);
            var list = libraryModel.GetLibraryList(LoginAccount.CurrentAccountMainID).ToPagedList(index ?? 1, 15);
            ViewBag.LibraryType = LibraryType();
            ViewBag.HostName = LoginAccount.HostName;
            return View(list);
        }

        public ActionResult Add()
        {
            ViewBag.LibraryType = LibraryType();
            ViewBag.HostName = LoginAccount.HostName;
            return View();
        }

        [HttpPost]
        public ActionResult Add(LibraryText libraryText)
        {
            var libraryModel = Factory.Get<ILibraryTextModel>(SystemConst.IOC_Model.LibraryTextModel);
            var result = libraryModel.Add(libraryText, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "LibraryText", new { HostName = LoginAccount.HostName }) + "'");
        }

        public ActionResult Edit(int id)
        {
            var libraryModel = Factory.Get<ILibraryTextModel>(SystemConst.IOC_Model.LibraryTextModel);
            var entity = libraryModel.Get(id);
            ViewBag.LibraryType = LibraryType();
            ViewBag.HostName = LoginAccount.HostName;
            return View(entity);
        }

        [HttpPost]
        public ActionResult Edit(LibraryText libraryText)
        {
            var libraryModel = Factory.Get<ILibraryTextModel>(SystemConst.IOC_Model.LibraryTextModel);
            var result = libraryModel.Edit(libraryText, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "LibraryText", new { HostName = LoginAccount.HostName }) + "'");
        }

        public ActionResult Delete(int id)
        {
            var libraryModel = Factory.Get<ILibraryTextModel>(SystemConst.IOC_Model.LibraryTextModel);
            var result = libraryModel.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "LibraryText", new { HostName = LoginAccount.HostName }) + "'");
        }

    }
}
