﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface;
using Poco;

namespace Web.Controllers
{
    public class AppWaitImgController : ManageAccountController
    {
        //
        // GET: /AppWaitImg/

        public ActionResult Index()
        {
            var appWaitImgModel = Factory.Get<IAppWaitImgModel>(SystemConst.IOC_Model.AppWaitImgModel);
            var AppWaitImg = appWaitImgModel.getAppWaitImg(LoginAccount.CurrentAccountMainID);
            ViewBag.HostName = LoginAccount.HostName;
            return View(AppWaitImg);
        }

        [HttpPost]
        [AllowCheckPermissions(false)]
        public ActionResult Index(AppWaitImg appWaitImg,HttpPostedFileBase HousShowImagePathFile)
        {

            appWaitImg.AccountMainID = LoginAccount.CurrentAccountMainID;
            var appWaitImgModel = Factory.Get<IAppWaitImgModel>(SystemConst.IOC_Model.AppWaitImgModel);
            if (HousShowImagePathFile != null)
            {
                appWaitImgModel.UpAppWaitImg(appWaitImg, HousShowImagePathFile);
            }
            return RedirectToAction("Index", "AppWaitImg");
        }
        [AllowCheckPermissions(false)]
        public ActionResult Delete()
        {
            var appWaitImgModel = Factory.Get<IAppWaitImgModel>(SystemConst.IOC_Model.AppWaitImgModel);
            var AppWaitImg = appWaitImgModel.DelAppWaitImg(LoginAccount.CurrentAccountMainID);
            if (AppWaitImg > 0)
            { 
                //chengong
            }
            return RedirectToAction("Index", "AppWaitImg");
        }
    }
}