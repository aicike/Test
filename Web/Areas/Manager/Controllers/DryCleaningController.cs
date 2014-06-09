using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;
using Poco;
using Injection;
using Interface.MerchantInterface;
using Poco.MerchantPoco;

namespace Web.Areas.Manager.Controllers
{
    /// <summary>
    /// 干洗服务
    /// </summary>
    public class DryCleaningController : ManageSystemUserController
    {
        //
        // GET: /Manager/DryCleaning/

        public ActionResult Index(int? id, string strCreatdDate, string Mname, string selEnumStatus)
        {
            var m_drcleaningModel = Factory.Get<IM_DryCleaningModel>(SystemConst.IOC_Model.M_DryCleaningModel);
            PagedList<M_DryCleaning> drcleaning = null;
            if (!string.IsNullOrEmpty(selEnumStatus))
            {
                if (selEnumStatus == "all")
                {
                    drcleaning = m_drcleaningModel.GetListByStatus(null, strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
                }
                else
                {
                    drcleaning = m_drcleaningModel.GetListByStatus(int.Parse(selEnumStatus), strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
                }
            }
            else
            {
                drcleaning = m_drcleaningModel.GetListByStatus(null, strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
            }
            if (string.IsNullOrEmpty(strCreatdDate))
            {
                ViewBag.strCreatdDate = "";
            }
            else
            {
                ViewBag.strCreatdDate = strCreatdDate;
            }
            if (string.IsNullOrEmpty(Mname))
            {
                ViewBag.Mname = "";
            }
            else
            {
                ViewBag.Mname = Mname;
            }
            if (string.IsNullOrEmpty(selEnumStatus))
            {
                ViewBag.selEnumStatus = "";
            }
            else
            {
                ViewBag.selEnumStatus = selEnumStatus;
            }
            if (id.HasValue)
            {
                ViewBag.pageID = id.Value;
            }
            else
            {
                ViewBag.pageID = 1;
            }
            ViewBag.Title = "干洗服务 - 评审 -" + SystemConst.PlatformName;
            return View(drcleaning);
        }

    }
}
