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

namespace MicroSite_Web.Controllers
{
    public class ProductController : ManageAccountController
    {
        //
        // GET: /Product/

        public ActionResult Index(int? id)
        {
            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            var list = productModel.GetList(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 15);

            if (id.HasValue)
            {
                ViewBag.pageID = id.Value;
            }
            ViewBag.HostName = LoginAccount.HostName;
            ViewBag.WeiURL = SystemConst.MicroSiteHostName + "." + SystemConst.WebUrl + "/MicroMall/ShopIndex?AMID=" + LoginAccount.CurrentAccountMainID;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "产品管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(list);
        }


        public ActionResult Add()
        {
            //产品状态
            List<SelectListItem> newstatus = new List<SelectListItem>();
            newstatus.Add(new SelectListItem { Text = "正常", Value = ((int)Poco.Enum.EnumProductType.Normal).ToString(), Selected = true });
            newstatus.Add(new SelectListItem { Text = "下架", Value = ((int)Poco.Enum.EnumProductType.OffShelves).ToString() });
            newstatus.Add(new SelectListItem { Text = "缺货", Value = ((int)Poco.Enum.EnumProductType.Shortages).ToString() });

            ViewData["Status"] = newstatus;
            //发布状态
            List<SelectListItem> Release = new List<SelectListItem>();
            Release.Add(new SelectListItem { Text = "不发布", Value = "false", Selected = true });
            Release.Add(new SelectListItem { Text = "发布", Value ="true" });
            ViewData["Release"] = Release;
            //优惠类型
            List<SelectListItem> DiscountType = new List<SelectListItem>();
            DiscountType.Add(new SelectListItem { Text = "无优惠", Value = ((int)Poco.Enum.EnumProductDiscountType.No).ToString(), Selected = true });
            DiscountType.Add(new SelectListItem { Text = "折扣", Value = ((int)Poco.Enum.EnumProductDiscountType.Discount).ToString() });
            DiscountType.Add(new SelectListItem { Text = "优惠", Value = ((int)Poco.Enum.EnumProductDiscountType.preferential).ToString() });
            DiscountType.Add(new SelectListItem { Text = "促销", Value = ((int)Poco.Enum.EnumProductDiscountType.Promotion).ToString() });
            ViewData["DiscountType"] = DiscountType;

            

            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "产品管理 - 添加产品", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Product product, string imgpath1, string imgpath2, string imgpath3, string imgpath4, string imgpath5)
        {
            product.LastSetDate = DateTime.Now.ToString("yyyy-MM-dd hhmmss");
            product.AccountMainID = LoginAccount.CurrentAccountMainID;
            //product.imgFilePath = "~/Images/nopicture_icon.png";
          
            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            if (product.EnumProductDiscountType == (int)EnumProductDiscountType.No)
            {
                product.DiscountPrice = product.Price;
            }
            var result = productModel.Add(product);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            else
            {
                var productImgModel = Factory.Get<IProductImgModel>(SystemConst.IOC_Model.ProductImgModel);
                List<string> imgpaths = new List<string>();
                if (!string.IsNullOrEmpty(imgpath1))
                {
                    imgpaths.Add(imgpath1);
                }
                if (!string.IsNullOrEmpty(imgpath2))
                {
                    imgpaths.Add(imgpath2);
                } if (!string.IsNullOrEmpty(imgpath3))
                {
                    imgpaths.Add(imgpath3);
                } if (!string.IsNullOrEmpty(imgpath4))
                {
                    imgpaths.Add(imgpath4);
                } if (!string.IsNullOrEmpty(imgpath5))
                {
                    imgpaths.Add(imgpath5);
                }

                productImgModel.AddImgInfo(product.ID, imgpaths, LoginAccount.CurrentAccountMainID);
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
            //发布状态
            List<SelectListItem> Release = new List<SelectListItem>();
            Release.Add(new SelectListItem { Text = "不发布", Value = "false", Selected = true });
            Release.Add(new SelectListItem { Text = "发布", Value = "true" });
            ViewData["Release"] = Release;
            //优惠类型
            List<SelectListItem> DiscountType = new List<SelectListItem>();
            DiscountType.Add(new SelectListItem { Text = "无优惠", Value = ((int)Poco.Enum.EnumProductDiscountType.No).ToString(), Selected = true });
            DiscountType.Add(new SelectListItem { Text = "折扣", Value = ((int)Poco.Enum.EnumProductDiscountType.Discount).ToString() });
            DiscountType.Add(new SelectListItem { Text = "优惠", Value = ((int)Poco.Enum.EnumProductDiscountType.preferential).ToString() });
            DiscountType.Add(new SelectListItem { Text = "促销", Value = ((int)Poco.Enum.EnumProductDiscountType.Promotion).ToString() });
            ViewData["DiscountType"] = DiscountType;

            ViewData["Status"] = newstatus;
            ViewData["Tstatus"] = product.Status;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "产品管理 - 修改产品", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(product);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product product, string imgpath1, string imgpath2, string imgpath3, string imgpath4, string imgpath5)
        {
            product.LastSetDate = DateTime.Now.ToString("yyyy-MM-dd hhmmss");

            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            if (product.EnumProductDiscountType == (int)EnumProductDiscountType.No)
            {
                product.DiscountPrice = product.Price;
            }
            var result = productModel.Edit(product);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            else
            {
                var productImgModel = Factory.Get<IProductImgModel>(SystemConst.IOC_Model.ProductImgModel);
                List<string> imgpaths = new List<string>();
                if (!string.IsNullOrEmpty(imgpath1))
                {
                    imgpaths.Add(imgpath1);
                }
                if (!string.IsNullOrEmpty(imgpath2))
                {
                    imgpaths.Add(imgpath2);
                } if (!string.IsNullOrEmpty(imgpath3))
                {
                    imgpaths.Add(imgpath3);
                } if (!string.IsNullOrEmpty(imgpath4))
                {
                    imgpaths.Add(imgpath4);
                } if (!string.IsNullOrEmpty(imgpath5))
                {
                    imgpaths.Add(imgpath5);
                }
                productImgModel.EditImgInfo(product.ID, imgpaths, LoginAccount.CurrentAccountMainID);
            }
            return RedirectToAction("Index", "Product");
        }

        public ActionResult Delete(int id)
        {
            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            var result = productModel.DeleteInfo(id, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "Product") + "'");
        }


        /// <summary>
        /// 产品预览
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [AllowCheckPermissions(false)]
        public ActionResult SelectProduct(int ID)
        {
            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            var product = productModel.GetPInfo(ID, LoginAccount.CurrentAccountMainID);

            var productImgModel = Factory.Get<IProductImgModel>(SystemConst.IOC_Model.ProductImgModel);
            ViewBag.ProductIMG = product.ProductImg.ToList();
            return View(product);
        }

        [HttpPost]
        public ActionResult Release(int productID,bool Release,int pageID)
        {
            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            var result = productModel.UPRelease(productID, LoginAccount.CurrentAccountMainID, Release);
            return RedirectToAction("Index", "Product", new { HostName = LoginAccount.HostName, id = pageID });
        }

    }
}
