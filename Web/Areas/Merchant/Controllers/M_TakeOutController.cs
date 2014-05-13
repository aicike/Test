using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Interface.MerchantInterface;
using Injection;
using Poco;

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
            return View(list);
        }
    }
}
