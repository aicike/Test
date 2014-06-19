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


        public ActionResult Add()
        {

            ViewBag.Title = "宠物医院 - 添加信息- " + SystemConst.PlatformName;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(M_PetHospital m_pethospital, int x1, int y1, int w, int h, int tw, int th)
        {
            var m_pethospitalModel = Factory.Get<IM_PetHospitalModel>(SystemConst.IOC_Model.M_PetHospitalModel);
            m_pethospital.MerchantID = LoginMerchant.ID;
            m_pethospital.IsPublish = false;

            m_pethospital.EnumDataStatus = (int)EnumDataStatus.None;
            m_pethospital.CreatDate = DateTime.Now;

            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return JavaScript(AlertJS_NoTag(new Dialog("请选择小区。")));
            }
            var ids = hidCommunity.ConvertToIntArray(',');
            var result = m_pethospitalModel.AddInfo(m_pethospital, ids, x1, y1, w, h, tw, th);

            if (result.HasError)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog(result.Error)));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "M_PetHospital", new { Area = "Merchant" }) + "'");
        }

        public ActionResult Edit(int ID)
        {
            var m_pethospitalModel = Factory.Get<IM_PetHospitalModel>(SystemConst.IOC_Model.M_PetHospitalModel);
            var pethospital = m_pethospitalModel.GetInfoByID(LoginMerchant.ID, ID);
            ViewBag.Community = pethospital.M_CommunityMappings.Select(a => a.AccountMainID).ToArray();
            ViewBag.Title = "宠物医院 - 修改信息- " + SystemConst.PlatformName;
            return View(pethospital);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(M_PetHospital m_pethospital, int x1, int y1, int w, int h, int tw, int th)
        {
            var m_pethospitalModel = Factory.Get<IM_PetHospitalModel>(SystemConst.IOC_Model.M_PetHospitalModel);

            m_pethospital.MerchantID = LoginMerchant.ID;
            m_pethospital.IsPublish = false;
            m_pethospital.EnumDataStatus = (int)EnumDataStatus.None;


            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog("请选择小区。")));
            }
            var ids = hidCommunity.ConvertToIntArray(',');
            var result = m_pethospitalModel.EditInfo(m_pethospital, ids, x1, y1, w, h, tw, th);
            if (result.HasError)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog(result.Error)));
            }



            return JavaScript("window.location.href='" + Url.Action("Index", "M_PetHospital", new { Area = "Merchant" }) + "'");

        }


        public ActionResult Delete(int id, int PageID)
        {
            var m_pethospitalModel = Factory.Get<IM_PetHospitalModel>(SystemConst.IOC_Model.M_PetHospitalModel);
            var result = m_pethospitalModel.DeleteInfo(id, LoginMerchant.ID);
            if (result.HasError)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog(result.Error)));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "M_PetHospital", new { Area = "Merchant", id = PageID }) + "'");
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
            var m_pethospitalModel = Factory.Get<IM_PetHospitalModel>(SystemConst.IOC_Model.M_PetHospitalModel);
            m_pethospitalModel.UpdatePush(ID, ISPush);
            return JavaScript("window.location.href='" + Url.Action("Index", "M_PetHospital", new { Area = "Merchant", id = PageID }) + "'");
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
            var m_pethospitalModel = Factory.Get<IM_PetHospitalModel>(SystemConst.IOC_Model.M_PetHospitalModel);
            m_pethospitalModel.UpdateStatus(ID, (int)EnumDataStatus.WaitPayMent);
            return JavaScript("window.location.href='" + Url.Action("Index", "M_PetHospital", new { Area = "Merchant", id = PageID }) + "'");
        }






    }
}
