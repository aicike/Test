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
    /// 宠物医院
    /// </summary>
    public class M_PetHospitalController : ManageMerchantController
    {
        //
        // GET: /Merchant/M_PetHospital/

        public ActionResult Index(int? id)
        {
            var m_pethospitalModel = Factory.Get<IM_PetHospitalModel>(SystemConst.IOC_Model.M_PetHospitalModel);
            var m_pethospital = m_pethospitalModel.GetListByMID(LoginMerchant.ID).ToPagedList(id ?? 1, 15);
            ViewBag.Title = "宠物医院 - " + SystemConst.PlatformName;
            if (id.HasValue)
            {
                ViewBag.pageID = id.Value;
            }
            else
            {
                ViewBag.pageID = 1;
            }
            return View(m_pethospital);
        }

    }
}
