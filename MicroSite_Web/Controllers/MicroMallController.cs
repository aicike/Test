using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroSite_Web.Controllers
{
    public class MicroMallController : BaseController
    {
        //
        // GET: /MicroMall/

        public ActionResult ShopIndex()
        {
            return View();
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
    }
}
