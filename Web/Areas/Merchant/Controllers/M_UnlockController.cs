using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Poco;
using Interface.MerchantInterface;
using Poco.MerchantPoco;

namespace Web.Areas.Merchant.Controllers
{
    /// <summary>
    /// 开锁换锁
    /// </summary>
    public class M_UnlockController : ManageMerchantController
    {
        //
        // GET: /Merchant/M_Unlock/

        public ActionResult Index(int ?id)
        {
            var m_unlockModel = Factory.Get<IM_UnlockModel>(SystemConst.IOC_Model.M_UnlockModel);
            var m_unlock = m_unlockModel.GetListByMID(LoginMerchant.ID).ToPagedList(id ?? 1, 15);
            return View(m_unlock);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(M_Unlock m_unlock)
        {
            return View();
        }

        public ActionResult Edit(int id)
        {

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(M_Unlock m_unlock)
        {

            return View();
        }

        public ActionResult Delete(int id)
        {

            return View();
        }
    }
}
