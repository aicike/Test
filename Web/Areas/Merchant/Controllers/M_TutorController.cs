using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Poco;
using Interface.MerchantInterface;
using Injection;

namespace Web.Areas.Merchant.Controllers
{
    /// <summary>
    /// 家教
    /// </summary>
    public class M_TutorController : ManageMerchantController
    {
        //
        // GET: /Merchant/M_Tutor/

        public ActionResult Index(int? id)
        {
            var m_tutorModel = Factory.Get<IM_TutorModel>(SystemConst.IOC_Model.M_TutorModel);
            var m_tutor = m_tutorModel.GetListByMID(LoginMerchant.ID).ToPagedList(id ?? 1, 15);
            ViewBag.Title = "家教 - " + SystemConst.PlatformName;
            if (id.HasValue)
            {
                ViewBag.pageID = id.Value;
            }
            else
            {
                ViewBag.pageID = 1;
            }
            return View(m_tutor);
        }

    }
}
