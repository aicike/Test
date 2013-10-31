using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Poco;
using Injection;
using Interface;
using Poco.Enum;

namespace Web.Controllers
{
    public class ProductController : ManageAccountController
    {
        //
        // GET: /Product/

        public ActionResult Index(int? id)
        {
            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            var list = productModel.GetList(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 15);



            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "产品管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(list);
        }


        public ActionResult Add()
        {

            
            List<SelectListItem> newstatus = new List<SelectListItem>();
            newstatus.Add(new SelectListItem { Text = "正常", Value = ((int)Poco.Enum.EnumProductType.Normal).ToString(), Selected = true });
            newstatus.Add(new SelectListItem { Text = "下架", Value = ((int)Poco.Enum.EnumProductType.OffShelves).ToString() });
            newstatus.Add(new SelectListItem { Text = "缺货", Value = ((int)Poco.Enum.EnumProductType.Shortages).ToString() });

            ViewData["Status"] = newstatus;

            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "产品管理 - 添加产品", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Product product,HttpPostedFileBase HousTypeImagePathFile)
        {
            product.LastSetDate = DateTime.Now.ToString("yyyy-MM-dd hhmmss");
            product.AccountMainID = LoginAccount.CurrentAccountMainID;
            product.imgFilePath = "~/Images/nopicture_icon.png";

            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            var result = productModel.AddInfo(product, HousTypeImagePathFile);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return RedirectToAction("Index", "Product");
        }


        public ActionResult Edit(int id)
        {
            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            var product = productModel.Get(id);

            List<SelectListItem> newstatus = new List<SelectListItem>();
            newstatus.Add(new SelectListItem { Text = "正常", Value = ((int)Poco.Enum.EnumProductType.Normal).ToString(), Selected = true });
            newstatus.Add(new SelectListItem { Text = "下架", Value = ((int)Poco.Enum.EnumProductType.OffShelves).ToString() });
            newstatus.Add(new SelectListItem { Text = "缺货", Value = ((int)Poco.Enum.EnumProductType.Shortages).ToString() });

            ViewData["Status"] = newstatus;
            ViewData["Tstatus"] = product.Status;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "产品管理 - 修改产品", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(product);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product product, HttpPostedFileBase HousTypeImagePathFile)
        {
            product.LastSetDate = DateTime.Now.ToString("yyyy-MM-dd hhmmss");

            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            var result = productModel.EditInfo(product, HousTypeImagePathFile);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return RedirectToAction("Index", "Product");
        }


        public ActionResult Delete(int id)
        {
            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            var result = productModel.DeleteInfo(id,LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "Product") + "'");
        }


    }
}
