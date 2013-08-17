﻿using System;
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


            MemoryStream ms = model.Get_Android_DownloadUrl("http://61.150.34.203/Test/Download/");
            if (null != ms)
            {
                Response.BinaryWrite(ms.ToArray());
            }
        }

        public ActionResult Download()
        {
            string path = Server.MapPath("~/Download/BuyAHouse.apk");
            //application/vnd.android.package-archive
            return File(path, "application/vnd.android.package-archive");
        }
    }
}
