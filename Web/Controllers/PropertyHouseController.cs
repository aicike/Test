using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;
using Controllers;
using Common;

namespace Web.Controllers
{
    public class PropertyHouseController : ManageAccountController
    {
        public ActionResult Index(int? id)
        {
            List<PropertyComplexEntity> objs = new List<PropertyComplexEntity>();
            var list = objs.AsQueryable().ToPagedList(id ?? 1, 100);

            //提示消息
            if (TempData["Msg"] != null)
            {
                var msg = TempData["Msg"].ToString();
                ViewBag.Msg = msg;
                ViewBag.HasError = 1;
            }
            if (TempData["HasError"] != null)
            {
                ViewBag.HasError = TempData["HasError"].ToString();
            }
            return View(list);
        }

        /// <summary>
        /// 导入用户数据是
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public ActionResult Upload(HttpPostedFileBase file)
        {
            var result= Tool.GetXLSXInfo(file);
            if (result.HasError)
            {
                TempData["Msg"] = result.Error;
                TempData["HasError"] = 1;
            }
            else
            {
                //检查Excel数据并导入





                TempData["HasError"] = 0;
            }
            return RedirectToAction("Index", "PropertyHouse", new { HostName = LoginAccount.HostName });
        }
    }
}
