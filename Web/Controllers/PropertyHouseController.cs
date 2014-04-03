using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;

namespace Web.Controllers
{
    public class PropertyHouseController : Controller
    {
        public ActionResult Index(int? id)
        {
            List<PropertyComplexEntity> objs = new List<PropertyComplexEntity>();
            var list = objs.AsQueryable().ToPagedList(id ?? 1, 100);
            return View(list);
        }
    }
}
