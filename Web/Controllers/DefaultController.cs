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
    public class DefaultController : Controller
    {
        public ActionResult AppView(int id)
        {
            var libraryModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            var entity = libraryModel.Get(id);
            return View(entity);
        }

        public ActionResult Calculator()
        {
            return View();
        }
    }
}
