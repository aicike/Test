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
using Poco.Enum;

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

        public ActionResult Add()
        {

            ViewBag.Title = "教育培训 - 添加信息- " + SystemConst.PlatformName;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(M_EducationTrain m_educationtrain, int x1, int y1, int w, int h, int tw, int th)
        {
            var m_educationtrainModel = Factory.Get<IM_EducationTrainModel>(SystemConst.IOC_Model.M_EducationTrainModel);
            m_educationtrain.MerchantID = LoginMerchant.ID;
            m_educationtrain.IsPublish = false;

            m_educationtrain.EnumDataStatus = (int)EnumDataStatus.None;
            m_educationtrain.CreatDate = DateTime.Now;

            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return JavaScript(AlertJS_NoTag(new Dialog("请选择小区。")));
            }
            var ids = hidCommunity.ConvertToIntArray(',');
            var result = m_educationtrainModel.AddInfo(m_educationtrain, ids, x1, y1, w, h, tw, th);

            if (result.HasError)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog(result.Error)));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "M_EducationTrain", new { Area = "Merchant" }) + "'");
        }



        public ActionResult Edit(int ID)
        {
            var m_educationtrainModel = Factory.Get<IM_EducationTrainModel>(SystemConst.IOC_Model.M_EducationTrainModel);
            var educationtrain = m_educationtrainModel.GetInfoByID(LoginMerchant.ID, ID);
            ViewBag.Community = educationtrain.M_CommunityMappings.Select(a => a.AccountMainID).ToArray();
            ViewBag.Title = "教育培训 - 修改信息- " + SystemConst.PlatformName;
            return View(educationtrain);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(M_EducationTrain m_educationtrain, int x1, int y1, int w, int h, int tw, int th)
        {
            var m_educationtrainModel = Factory.Get<IM_EducationTrainModel>(SystemConst.IOC_Model.M_EducationTrainModel);

            m_educationtrain.MerchantID = LoginMerchant.ID;
            m_educationtrain.IsPublish = false;
            m_educationtrain.EnumDataStatus = (int)EnumDataStatus.None;


            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog("请选择小区。")));
            }
            var ids = hidCommunity.ConvertToIntArray(',');
            var result = m_educationtrainModel.EditInfo(m_educationtrain, ids, x1, y1, w, h, tw, th);
            if (result.HasError)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog(result.Error)));
            }



            return JavaScript("window.location.href='" + Url.Action("Index", "M_EducationTrain", new { Area = "Merchant" }) + "'");

        }


        public ActionResult Delete(int id, int PageID)
        {
            var m_educationtrainModel = Factory.Get<IM_EducationTrainModel>(SystemConst.IOC_Model.M_EducationTrainModel);
            var result = m_educationtrainModel.DeleteInfo(id, LoginMerchant.ID);
            if (result.HasError)
            {
                return JavaScript("CloseLayout_Later();" + AlertJS_NoTag(new Dialog(result.Error)));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "M_EducationTrain", new { Area = "Merchant", id = PageID }) + "'");
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
            var m_educationtrainModel = Factory.Get<IM_EducationTrainModel>(SystemConst.IOC_Model.M_EducationTrainModel);
            m_educationtrainModel.UpdatePush(ID, ISPush);
            return JavaScript("window.location.href='" + Url.Action("Index", "M_EducationTrain", new { Area = "Merchant", id = PageID }) + "'");
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
            var m_educationtrainModel = Factory.Get<IM_EducationTrainModel>(SystemConst.IOC_Model.M_EducationTrainModel);
            m_educationtrainModel.UpdateStatus(ID, (int)EnumDataStatus.WaitPayMent);
            return JavaScript("window.location.href='" + Url.Action("Index", "M_EducationTrain", new { Area = "Merchant", id = PageID }) + "'");
        }



    }
}
