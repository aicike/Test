using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Controllers;
using Poco.Enum;

namespace Web.Controllers
{
    public class LibraryController : ManageAccountController
    {
        public ActionResult Text(int? index)
        {
            var libraryModel = Factory.Get<ILibraryTextModel>(SystemConst.IOC_Model.LibraryTextModel);
            var list = libraryModel.GetLibraryList(LoginAccount.CurrentAccountMainID).ToPagedList(index ?? 1, 15);
            ViewBag.LibraryType = LibraryType();
            ViewBag.HostName = LoginAccount.HostName;
            return View(list);
        }

        public ActionResult AddText()
        {
            ViewBag.LibraryType = LibraryType();
            ViewBag.HostName = LoginAccount.HostName;
            return View();
        }

        [HttpPost]
        public ActionResult AddText(LibraryText libraryText)
        {
            var libraryModel = Factory.Get<ILibraryTextModel>(SystemConst.IOC_Model.LibraryTextModel);
            var result = libraryModel.Add(libraryText, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Text", "Library", new { HostName = LoginAccount.HostName }) + "'");
        }

        public ActionResult EditText(int id)
        {
            var libraryModel = Factory.Get<ILibraryTextModel>(SystemConst.IOC_Model.LibraryTextModel);
            var entity = libraryModel.Get(id);
            ViewBag.LibraryType = LibraryType();
            ViewBag.HostName = LoginAccount.HostName;
            return View(entity);
        }

        [HttpPost]
        public ActionResult EditText(LibraryText libraryText)
        {
            var libraryModel = Factory.Get<ILibraryTextModel>(SystemConst.IOC_Model.LibraryTextModel);
            var result = libraryModel.Edit(libraryText, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Text", "Library", new { HostName = LoginAccount.HostName }) + "'");
        }

        public ActionResult DeleteText(int id)
        {
            var libraryModel = Factory.Get<ILibraryTextModel>(SystemConst.IOC_Model.LibraryTextModel);
            var result = libraryModel.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Text", "Library", new { HostName = LoginAccount.HostName }) + "'");
        }

        private List<LibraryType> LibraryType()
        {
            List<LibraryType> type = new List<Poco.LibraryType>();
            type.Add(new LibraryType() { ID = 0, Name = "文字" });
            type.Add(new LibraryType() { ID = 1, Name = "图片" });
            type.Add(new LibraryType() { ID = 2, Name = "语音" });
            type.Add(new LibraryType() { ID = 3, Name = "视频" });
            type.Add(new LibraryType() { ID = 4, Name = "图文" });
            return type;
        }
    }
}
