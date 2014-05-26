using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Poco;
using Poco.MerchantPoco;
using Injection;
using Interface.MerchantInterface;
using Poco.Enum;

namespace Web.Areas.Merchant.Controllers
{
    public class M_TakeOutController : ManageMerchantController
    {
        public ActionResult Index(int? id)
        {
            var takeOutModel = Factory.Get<IM_TakeOutModel>(SystemConst.IOC_Model.M_TakeOutModel);

            var list = takeOutModel.ListByMerchantID(LoginMerchant.ID).ToPagedList(id ?? 1, 15);

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "周边外卖", "", WebTitleRemark);
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
            string webTitle = string.Format(SystemConst.Business.WebTitle, "周边外卖-添加", "", WebTitleRemark);
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
        public ActionResult Add(M_TakeOut M_TakeOut, int w, int h, int x1, int y1, int tw, int th)
        {
            if (w <= 0)
            {
                return JavaScript(AlertJS_NoTag(new Dialog("请在图片上选择展示区域")));
            }

            var takeOutModel = Factory.Get<IM_TakeOutModel>(SystemConst.IOC_Model.M_TakeOutModel);
            M_TakeOut.CreatDate = DateTime.Now;
            M_TakeOut.EnumDataStatus = (int)EnumDataStatus.None;
            M_TakeOut.IsPublish = false;
            M_TakeOut.MerchantID = LoginMerchant.ID;

            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return Alert(new Dialog("请选择小区。"));
            }
            var ids = hidCommunity.ConvertToIntArray(',');
            var result = takeOutModel.Add(M_TakeOut, ids, w, h, x1, y1, tw, th);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "M_TakeOut", new { Area = "Merchant" }) + "'");
        }

        /// <summary>
        /// 修改界面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var takeOutModel = Factory.Get<IM_TakeOutModel>(SystemConst.IOC_Model.M_TakeOutModel);
            var takeOut = takeOutModel.Get(id);
            (takeOut.MerchantID == LoginMerchant.ID).NotAuthorizedPage();

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "周边外卖-修改", "", WebTitleRemark);
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
        public ActionResult Edit(M_TakeOut M_TakeOut)
        {
            var takeOutModel = Factory.Get<IM_TakeOutModel>(SystemConst.IOC_Model.M_TakeOutModel);
            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return Alert(new Dialog("请选择小区。"));
            }
            //判断之前的状态，如果是已审核或正在审核，修改后变为等待审核
            M_TakeOut.EnumDataStatus = (int)EnumDataStatus.None;
            M_TakeOut.IsPublish = false;
            var ids = hidCommunity.ConvertToIntArray(',');
            var result = takeOutModel.Edit(M_TakeOut, ids);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "M_TakeOut", new { Area = "Merchant" }) + "'");
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            var takeOutModel = Factory.Get<IM_TakeOutModel>(SystemConst.IOC_Model.M_TakeOutModel);
            var obj = takeOutModel.Get(id);
            (obj != null).NotAuthorizedPage();
            var result = takeOutModel.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "M_TakeOut", new { Area = "Merchant" }) + "'");
        }

        public ActionResult Detail(int id)
        {
            var takeOutModel = Factory.Get<IM_TakeOutModel>(SystemConst.IOC_Model.M_TakeOutModel);
            var takeOut = takeOutModel.Get(id);
            (takeOut.MerchantID == LoginMerchant.ID).NotAuthorizedPage();
            ViewBag.Content = takeOut.Content ?? "";

            var takeOutDetailModel = Factory.Get<IM_TakeOutDetailModel>(SystemConst.IOC_Model.M_TakeOutDetailModel);
            var list = takeOutDetailModel.List(id, LoginMerchant.ID);
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "周边外卖-商品信息", "", WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.Menu = 1;

            if (list.Count == 0)
            {
                list.Add(new M_TakeOutDetail() { });
            }
            ViewBag.ID = id;
            return View(list);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Detail(int hidId, string hidItems, string Content)
        {
            var takeOutModel = Factory.Get<IM_TakeOutModel>(SystemConst.IOC_Model.M_TakeOutModel);
            var takeOut = takeOutModel.Get(hidId);
            (takeOut.MerchantID == LoginMerchant.ID).NotAuthorizedPage();
            var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<M_TakeOutDetail>>(hidItems);
            Result result = new Result();
            if (list == null || list.Count == 0)
            {
                result.Error = "至少添加一条商品信息。";
            }
            else
            {
                result = takeOutModel.AddDetail(hidId, LoginMerchant.ID, list, Content);
            }
            return Json(result);
        }


        public ActionResult Check(int id)
        {
            var takeOutModel = Factory.Get<IM_TakeOutModel>(SystemConst.IOC_Model.M_TakeOutModel);
            var obj = takeOutModel.Get(id);
            (obj != null).NotAuthorizedPage();
            obj.EnumDataStatus = (int)EnumDataStatus.WaitPayMent;
            var result = takeOutModel.Edit(obj);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "M_TakeOut", new { Area = "Merchant" }) + "'");
        }

        public ActionResult Publish(int id, int status)
        {
            var takeOutModel = Factory.Get<IM_TakeOutModel>(SystemConst.IOC_Model.M_TakeOutModel);
            var obj = takeOutModel.Get(id);
            (obj != null).NotAuthorizedPage();
            if (obj.EnumDataStatus != (int)EnumDataStatus.Enabled)
            {
                false.NotAuthorizedPage();
            }
            obj.IsPublish = status == 0 ? false : true;
            obj.PublishDate = DateTime.Now;
            var result = takeOutModel.Edit(obj);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "M_TakeOut", new { Area = "Merchant" }) + "'");

        }
    }
}
