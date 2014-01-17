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
    public class MicroMallController : BaseController
    {
        //
        // GET: /MicroMall/
        //商品首页
        public ActionResult ShopIndex()
        {
            var classifyModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);
            var classify = classifyModel.GetOneLevel(1);
            return View(classify);
        }


        /// <summary>
        /// 产品列表
        /// </summary>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public ActionResult ProductList(int TypeID)
        {

            return View();
        }

        //产品列表分页
        public ActionResult ProductListItem(int page)
        {
            return View();
        }

        //产品详细信息
        public ActionResult ProductInfo(int PID)
        {
            return View();
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
