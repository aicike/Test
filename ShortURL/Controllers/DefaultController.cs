using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShortURL.Business;

namespace ShortURL.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult Index(string id)
        {
            URLMapModel model = new URLMapModel();
            string fullUrl = model.GetFullUrl(id);
            if (!string.IsNullOrEmpty(fullUrl))
            {
                return Redirect(fullUrl);
            }
            return View();
        }
    }
}
