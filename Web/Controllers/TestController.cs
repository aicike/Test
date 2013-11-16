using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;
using System.IO;

namespace Web.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return View();
        }

        public void QrCode()
        {
            QrCodeModel model = new QrCodeModel();


            MemoryStream ms = model.Get_Android_DownloadUrl("http://192.168.1.166/Test/Download/");
            if (null != ms)
            {
                Response.BinaryWrite(ms.ToArray());
            }
        }

        public ActionResult Download()
        {
            var rrr = Request;
            string path = Server.MapPath("~/Download/BuyAHouse.apk");
            //application/vnd.android.package-archive
            return File(path, "application/vnd.android.package-archive");
        }

        public ActionResult GetEndDate()
        {
            CommonModel cm = new CommonModel();
            DateTime dt = Convert.ToDateTime("2013-11-05");
            ViewBag.aa = cm.GetEndDate(dt, 6, 1, 0);
            return View();
        }

        [HttpGet]
        public ActionResult TextSMS()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TextSMS(string txtPhone,string txtContent)
        {
            SMS_Model model = new SMS_Model();
            model.SendSMS(txtPhone, txtContent);
            return View();
        }
    }
}
