using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface.MerchantInterface;
using Poco;

namespace Web.Areas.Merchant.Controllers
{
    /// <summary>
    /// 干洗服务
    /// </summary>
    public class M_DryCleaningController : ManageMerchantController
    {
        //
        // GET: /Merchant/M_DryCleaning/

        public ActionResult Index(int ?id)
        {
            var m_dryCleaningModel = Factory.Get<IM_DryCleaningModel>(SystemConst.IOC_Model.M_DryCleaningModel);
            var m_dryCleaning = m_dryCleaningModel.GetListByMID(LoginMerchant.ID).ToPagedList(id ?? 1, 15);
            ViewBag.Title = "干洗服务 - " + SystemConst.PlatformName;
            if (id.HasValue)
            {
                ViewBag.pageID = id.Value;
            }
            else
            {
                ViewBag.pageID = 1;
            }
            return View(m_dryCleaning);
        }

    }
}
