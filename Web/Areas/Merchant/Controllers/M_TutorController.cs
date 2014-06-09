using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Poco;
using Interface.MerchantInterface;
using Injection;
using Poco.MerchantPoco;
using Poco.Enum;

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


        public ActionResult Add()
        {

            ViewBag.Title = "家教 - 添加信息- " + SystemConst.PlatformName;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(M_Tutor m_tutor, int x1, int y1, int w, int h, int tw, int th)
        {
            var m_tutorModel = Factory.Get<IM_TutorModel>(SystemConst.IOC_Model.M_TutorModel);
            m_tutor.MerchantID = LoginMerchant.ID;
            m_tutor.IsPublish = false;

            m_tutor.EnumDataStatus = (int)EnumDataStatus.None;
            m_tutor.CreatDate = DateTime.Now;

            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return JavaScript(AlertJS_NoTag(new Dialog("请选择小区。")));
            }
            var ids = hidCommunity.ConvertToIntArray(',');
            var result = m_tutorModel.AddInfo(m_tutor, ids, x1, y1, w, h, tw, th);

            if (result.HasError)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog(result.Error)));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "M_Tutor", new { Area = "Merchant" }) + "'");
        }


        public ActionResult Edit(int ID)
        {
            var m_tutorModel = Factory.Get<IM_TutorModel>(SystemConst.IOC_Model.M_TutorModel);
            var tutor = m_tutorModel.GetInfoByID(LoginMerchant.ID, ID);
            ViewBag.Community = tutor.M_CommunityMappings.Select(a => a.AccountMainID).ToArray();
            ViewBag.Title = "家教 - 修改信息- " + SystemConst.PlatformName;
            return View(tutor);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(M_Tutor m_tutor, int x1, int y1, int w, int h, int tw, int th)
        {
            var m_tutorModel = Factory.Get<IM_TutorModel>(SystemConst.IOC_Model.M_TutorModel);

            m_tutor.MerchantID = LoginMerchant.ID;
            m_tutor.IsPublish = false;
            m_tutor.EnumDataStatus = (int)EnumDataStatus.None;


            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog("请选择小区。")));
            }
            var ids = hidCommunity.ConvertToIntArray(',');
            var result = m_tutorModel.EditInfo(m_tutor, ids, x1, y1, w, h, tw, th);
            if (result.HasError)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog(result.Error)));
            }



            return JavaScript("window.location.href='" + Url.Action("Index", "M_Tutor", new { Area = "Merchant" }) + "'");

        }


        public ActionResult Delete(int id, int PageID)
        {
            var m_tutorModel = Factory.Get<IM_TutorModel>(SystemConst.IOC_Model.M_TutorModel);
            var result = m_tutorModel.DeleteInfo(id, LoginMerchant.ID);
            if (result.HasError)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog(result.Error)));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "M_Tutor", new { Area = "Merchant", id = PageID }) + "'");
        }



        /// <summary>
        /// 更改是否发布信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ISPush"></param>
        /// <param name="PageID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpPush(int ID, bool ISPush, int PageID)
        {
            var m_tutorModel = Factory.Get<IM_TutorModel>(SystemConst.IOC_Model.M_TutorModel);
            m_tutorModel.UpdatePush(ID, ISPush);
            return JavaScript("window.location.href='" + Url.Action("Index", "M_Tutor", new { Area = "Merchant", id = PageID }) + "'");
        }

        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ISPush"></param>
        /// <param name="PageID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpStatus(int ID, int PageID)
        {
            var m_tutorModel = Factory.Get<IM_TutorModel>(SystemConst.IOC_Model.M_TutorModel);
            m_tutorModel.UpdateStatus(ID, (int)EnumDataStatus.WaitPayMent);
            return JavaScript("window.location.href='" + Url.Action("Index", "M_Tutor", new { Area = "Merchant", id = PageID }) + "'");
        }



    }
}
