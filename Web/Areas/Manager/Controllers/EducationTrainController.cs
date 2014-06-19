using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;
using Poco;
using Injection;
using Poco.MerchantPoco;
using Interface.MerchantInterface;

namespace Web.Areas.Manager.Controllers
{
    /// <summary>
    /// 教育培训
    /// </summary>
    public class EducationTrainController : ManageSystemUserController
    {
        //
        // GET: /Manager/EducationTrain/

        public ActionResult Index(int? id, string strCreatdDate, string Mname, string selEnumStatus)
        {
            var m_educationtrainModel = Factory.Get<IM_EducationTrainModel>(SystemConst.IOC_Model.M_EducationTrainModel);
            PagedList<M_EducationTrain> educationtrain = null;
            if (!string.IsNullOrEmpty(selEnumStatus))
            {
                if (selEnumStatus == "all")
                {
                    educationtrain = m_educationtrainModel.GetListByStatus(null, strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
                }
                else
                {
                    educationtrain = m_educationtrainModel.GetListByStatus(int.Parse(selEnumStatus), strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
                }
            }
            else
            {
                educationtrain = m_educationtrainModel.GetListByStatus(null, strCreatdDate, Mname).ToPagedList(id ?? 1, 20);
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
            ViewBag.Title = "教育培训 - 评审 -" + SystemConst.PlatformName;
            return View(educationtrain);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="Error">1:成功 2失败</param>
        /// <returns></returns>
        public ActionResult EducationTrainInfo(int EID, int PageID, int? Error)
        {
            var m_educationtrainModel = Factory.Get<IM_EducationTrainModel>(SystemConst.IOC_Model.M_EducationTrainModel);
            var educationtrain = m_educationtrainModel.Get(EID);
            if (Error.HasValue)
            {
                ViewBag.Error = Error.Value;
            }
            else
            {
                ViewBag.Error = 0;
            }
            ViewBag.EID = EID;
            ViewBag.PageID = PageID;
            ViewBag.Title = educationtrain.Title + " - 教育培训 - 评审 -" + SystemConst.PlatformName;
            return View(educationtrain);
        }

        [HttpPost]
        public ActionResult EducationTrainInfo(int EID, int Status, int PageID)
        {
            var m_educationtrainModel = Factory.Get<IM_EducationTrainModel>(SystemConst.IOC_Model.M_EducationTrainModel);
            var result = m_educationtrainModel.UpdateStatus(EID, Status);
            if (result.HasError)
            {
                return JavaScript("window.location.href='" + Url.Action("EducationTrainInfo", "EducationTrain", new { EID = EID, PageID = PageID, Error = 2 }) + "'");
            }
            else
            {
                return JavaScript("window.location.href='" + Url.Action("EducationTrainInfo", "EducationTrain", new { EID = EID, PageID = PageID, Error = 1 }) + "'");
            }
        }
    }
}
