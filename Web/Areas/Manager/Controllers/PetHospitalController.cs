using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;
using Poco.MerchantPoco;
using Injection;
using Interface.MerchantInterface;
using Poco;

namespace Web.Areas.Manager.Controllers
{
    /// <summary>
    /// 宠物医院
    /// </summary>
    public class PetHospitalController : ManageSystemUserController
    {
        //
        // GET: /Manager/PetHospital/

        public ActionResult Index(int? id, string strCreatdDate, string Mname, string selEnumStatus)
        {
            var m_pethospitalModel = Factory.Get<IM_PetHospitalModel>(SystemConst.IOC_Model.M_PetHospitalModel);
            PagedList<M_PetHospital> pethospital = null;
            if (!string.IsNullOrEmpty(selEnumStatus))
            {
                if (selEnumStatus == "all")
                {
                    pethospital = m_pethospitalModel.GetListByStatus(null, strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
                }
                else
                {
                    pethospital = m_pethospitalModel.GetListByStatus(int.Parse(selEnumStatus), strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
                }
            }
            else
            {
                pethospital = m_pethospitalModel.GetListByStatus(null, strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
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
            ViewBag.Title = "宠物医院 - 评审 -" + SystemConst.PlatformName;
            return View(pethospital);
        }

    }
}
