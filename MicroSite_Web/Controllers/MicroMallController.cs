using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;

namespace MicroSite_Web.Controllers
{
    public class MicroMallController : UserBaseController
    {
        //
        // GET: /MicroMall/
        //商品首页
        public ActionResult ShopIndex(int AccountMainID)
        {
            var classifyModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);
            var classify = classifyModel.GetOneLevel(AccountMainID);
            ViewBag.AMID = AccountMainID;

            return View(classify.ToList());
        }


        /// <summary>
        /// 产品列表
        /// </summary>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public ActionResult ProductList(int TypeID, int AccountMainID)
        {
            ViewBag.TypeID = TypeID;
            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            var products = productModel.GetListByTypeID(TypeID, AccountMainID);
            ViewBag.pageCount = products.ToPagedList(1, 6).TotalPageCount;

            var classifyModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);
            var classify = classifyModel.Get(TypeID);
            ViewBag.CName = classify.Name;

            ViewBag.AMID = AccountMainID;

            return View(products.ToPagedList(1, 6));
        }

        //产品列表分页
        public ActionResult ProductListItem(int page, int TypeID, int AccountMainID)
        {
            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            var products = productModel.GetListByTypeID(TypeID, AccountMainID).ToPagedList(page, 6);
            ViewBag.AMID = AccountMainID;
            return View(products);
        }

        //产品详细信息
        public ActionResult ProductInfo(int PID, int AccountMainID)
        {
            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            var product = productModel.GetProduct(PID, AccountMainID);
            return View(product);
        }

        //加入购物车
        [HttpPost]
        public string JoinShoppingCart()
        {

            return "";
        }

        //购物车
        public ActionResult ShoppingCart()
        {
            return View();
        }

        //订单确认界面
        public ActionResult OrderConfirmation(string StrJson)
        {
            return View();
        }
    }
}
