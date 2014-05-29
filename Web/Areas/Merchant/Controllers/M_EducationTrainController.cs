using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Poco;
using Interface.MerchantInterface;

namespace Web.Areas.Merchant.Controllers
{
    /// <summary>
    /// 教育培训
    /// </summary>
    public class M_EducationTrainController : ManageMerchantController
    {
        //
        // GET: /Merchant/M_EducationTrain/

        public ActionResult Index(int ?id)
        {
            var m_educationtrainModel = Factory.Get<IM_EducationTrainModel>(SystemConst.IOC_Model.M_EducationTrainModel);
            var m_educationtrain = m_educationtrainModel.GetListByMID(LoginMerchant.ID).ToPagedList(id ?? 1, 15);
            ViewBag.Title = "教育培训 - " + SystemConst.PlatformName;
            if (id.HasValue)
            {
                ViewBag.pageID = id.Value;
            }
            else
            {
                ViewBag.pageID = 1;
            }
            return View(m_educationtrain);
        }

    }
}
