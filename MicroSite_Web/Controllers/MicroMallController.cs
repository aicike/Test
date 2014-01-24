using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Business;

namespace MicroSite_Web.Controllers
{
    public class MicroMallController : UserBaseController
    {
        //
        // GET: /MicroMall/
        //商品首页
        public ActionResult ShopIndex(int AMID)
        {
            var classifyModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);
            var classify = classifyModel.GetOneLevel(AMID);
            ViewBag.AMID = AMID;

            return View(classify.ToList());
        }


        /// <summary>
        /// 产品列表
        /// </summary>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public ActionResult ProductList(int TypeID, int AMID)
        {
            ViewBag.TypeID = TypeID;
            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            var products = productModel.GetListByTypeID(TypeID, AMID);
            ViewBag.pageCount = products.ToPagedList(1, 6).TotalPageCount;

            var classifyModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);
            var classify = classifyModel.Get(TypeID);
            ViewBag.CName = classify.Name;

            ViewBag.AMID = AMID;

            return View(products.ToPagedList(1, 6));
        }

        //产品列表分页
        public ActionResult ProductListItem(int page, int TypeID, int AMID)
        {
            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            var products = productModel.GetListByTypeID(TypeID, AMID).ToPagedList(page, 6);
            ViewBag.AMID = AMID;
            return View(products);
        }

        //产品详细信息
        public ActionResult ProductInfo(int PID, int AMID)
        {
            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            var product = productModel.GetProduct(PID, AMID);
            ViewBag.AMID = AMID;
            return View(product);
        }


        //购物车
        public ActionResult ShoppingCart(int AMID)
        {
            ViewBag.AMID = AMID;
            return View();
        }

        //订单确认界面
        public ActionResult OrderConfirmation(int AMID, int? adsID, int? IsError)
        {
            ViewBag.AMID = AMID;
            if (adsID.HasValue)
            {
                ViewBag.adsID = adsID.Value;
            }
            else
            {
                ViewBag.adsID = 0;
            }
            if (IsError.HasValue)
            {
                //IsError 1:操作失败 2：提交订单失败
                ViewBag.Error = IsError;
            }
            else
            {
                ViewBag.Error = 0;
            }
            return View();
        }

        //获取收货地址 AJAX
        [HttpPost]
        public string GetAdsInfo(int adsID, int AMID, int UserID)
        {
            UserDeliveryAddressModel AdsModel = Factory.Get<IUserDeliveryAddressModel>(SystemConst.IOC_Model.UserDeliveryAddressModel) as UserDeliveryAddressModel;
            UserDeliveryAddress DBUda = null;
            UserDeliveryAddress uda = new UserDeliveryAddress();
            if (adsID != 0)
            {
                //根据adsID 获取收货地址
                DBUda = AdsModel.Get(AMID, UserID, adsID);
            }
            else
            {
                //根据UserID 获取第一个收货地址
                DBUda = AdsModel.GetListByUserID(UserID, AMID).FirstOrDefault();
            }
            if (DBUda != null)
            {
                uda.Receiver = DBUda.Receiver;
                uda.RPhone = DBUda.RPhone;
                uda.ID = DBUda.ID;
                uda.Address = DBUda.Province.Name + " " + DBUda.City.Name + " " + DBUda.District.Name + " " + DBUda.Address;
                return Newtonsoft.Json.JsonConvert.SerializeObject(uda);
            }
            else
            {
                return null;
            }

        }

        //提交订单
        [HttpPost]
        public ActionResult OrderConfirmation(int AMID)
        {
            try
            {
                var orderModel = Factory.Get<IOrderModel>(SystemConst.IOC_Model.OrderModel);
                //商品ID
                string HPIDS = Request.Form["HPIDS"];
                //商品ID 对应的 数量
                string HPIDSandCnt = Request.Form["HPIDSandCnt"];
                //用户ID
                int HuserID = int.Parse(Request.Form["HuserID"]);
                //地址ID
                int AID = int.Parse(Request.Form["AID"]);

                //提交订单
                Result result = orderModel.Micro_AddOrder(HPIDS, HPIDSandCnt, HuserID, AID, AMID);

                if (result.HasError)
                {
                    return RedirectToAction("OrderConfirmation", "MicroMall", new { AMID = AMID, adsID = AID, IsError = 2 });
                }
                else
                {
                    //支付界面
                    return View();

                }

            }
            catch
            {
                string AID = Request.Form["AID"];
                if (AID != null)
                {
                    return RedirectToAction("OrderConfirmation", "MicroMall", new { AMID = AMID, adsID = AID, IsError = 1 });
                }
                else
                {
                    return RedirectToAction("OrderConfirmation", "MicroMall", new { AMID = AMID, IsError = 1 });
                }

            }
        }





    }
}
