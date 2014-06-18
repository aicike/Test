using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface.MerchantInterface;
using Poco;
using Poco.MerchantPoco;
using Poco.Enum;

namespace Web.Areas.Merchant.Controllers
{
    public class M_ProductController : ManageMerchantController
    {
        public ActionResult Index(int? id)
        {
            var productModel = Factory.Get<IM_ProductModel>(SystemConst.IOC_Model.M_ProductModel);

            var list = productModel.ListByMerchantID(LoginMerchant.ID).ToPagedList(id ?? 1, 15);

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "商家促销", "", WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.Menu = 1;
            return View(list);
        }


        /// <summary>
        /// 添加页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Add()
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "商家促销-添加", "", WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.Menu = 1;
            return View();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="MainHouseInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(M_Product M_Product, int w, int h, int x1, int y1, int tw, int th)
        {
            if (w <= 0)
            {
                return JavaScript(AlertJS_NoTag(new Dialog("请在图片上选择展示区域")));
            }

            var productModel = Factory.Get<IM_ProductModel>(SystemConst.IOC_Model.M_ProductModel);
            M_Product.CreatDate = DateTime.Now;
            M_Product.EnumDataStatus = (int)EnumDataStatus.None;
            M_Product.IsPublish = false;
            M_Product.MerchantID = LoginMerchant.ID;

            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return Alert(new Dialog("请选择小区。"));
            }
            var ids = hidCommunity.ConvertToIntArray(',');
            var result = productModel.Add(M_Product, ids, w, h, x1, y1, tw, th);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "M_Product", new { Area = "Merchant" }) + "'");
        }

        /// <summary>
        /// 修改界面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var productModel = Factory.Get<IM_ProductModel>(SystemConst.IOC_Model.M_ProductModel);
            var takeOut = productModel.Get(id);
            (takeOut.MerchantID == LoginMerchant.ID).NotAuthorizedPage();

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "商家促销-修改", "", WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.Menu = 1;

            ViewBag.Community = takeOut.M_CommunityMappings.Select(a => a.AccountMainID).ToArray();

            return View(takeOut);
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="MainHouseInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(M_Product M_Product, int w, int h, int x1, int y1, int tw, int th)
        {
            var productModel = Factory.Get<IM_ProductModel>(SystemConst.IOC_Model.M_ProductModel);
            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return Alert(new Dialog("请选择小区。"));
            }
            //判断之前的状态，如果是已审核或正在审核，修改后变为等待审核
            M_Product.EnumDataStatus = (int)EnumDataStatus.None;
            M_Product.IsPublish = false;
            var ids = hidCommunity.ConvertToIntArray(',');
            var result = productModel.Edit(M_Product, ids, w, h, x1, y1, tw, th);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "M_Product", new { Area = "Merchant" }) + "'");
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            var productModel = Factory.Get<IM_ProductModel>(SystemConst.IOC_Model.M_ProductModel);
            var obj = productModel.Get(id);
            (obj != null).NotAuthorizedPage();
            var result = productModel.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "M_TakeOut", new { Area = "Merchant" }) + "'");
        }
    }
}
