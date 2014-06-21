using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface.MerchantInterface;
using Poco;
using Poco.WebAPI_Poco;
using Poco.Enum;
using Interface;
using System.Text;

namespace Web.Controllers
{
    public class WebRequest_MerchantController : Controller
    {
        /// <summary>
        /// 获取外卖
        /// </summary>
        /// <returns></returns>
        public string GetTakeOutList(int amid, int ID, int ListCnt)
        {
            var takeOutModel = Factory.Get<IM_TakeOutModel>(SystemConst.IOC_Model.M_TakeOutModel);
            int status = (int)EnumDataStatus.Enabled;
            var list = takeOutModel.List().Where(a => a.M_CommunityMappings.Any(b => b.AccountMainID == amid) && a.EnumDataStatus == status && a.IsPublish)
                .OrderByDescending(a => a.PublishDate).Skip(ID).Take(ListCnt).ToList()
                .Select(a => new App_TakeOut
                {
                    ID = a.ID,
                    ImgPath = "",
                    Title = a.Title,
                    Summary = a.M_TakeOutDetails.Select(c => c.Title).ToList().ConvertToString(","),
                    Price = a.M_TakeOutDetails.Sum(b => b.Price),
                    TakeOutPrice = a.TakeOutPrice,
                    MerchantID = a.MerchantID,
                    MerchantName = a.Merchant.Name,
                    ImagePath = SystemConst.WebUrlIP + Url.Content(a.ImagePath ?? "")
                }).ToList();
            var newID = ID + list.Count;
            var obj = new { LastID = newID, List = list };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public string GetTakeOut(int id)
        {
            var takeOutModel = Factory.Get<IM_TakeOutModel>(SystemConst.IOC_Model.M_TakeOutModel);
            var obj = takeOutModel.Get(id);
            var entity = new App_TakeOut
            {
                ID = obj.ID,
                ImgPath = "",
                Title = obj.Title,
                Summary = obj.M_TakeOutDetails.Select(c => c.Title).ToList().ConvertToString(","),
                Price = obj.M_TakeOutDetails.Sum(b => b.Price),
                TakeOutPrice = obj.TakeOutPrice,
                MerchantID = obj.MerchantID,
                ItemList = obj.M_TakeOutDetails.Select(a => new App_TakeOutItem
                {
                    ID = a.ID,
                    Title = a.Title,
                    Price = a.Price
                }).ToList()
            };

            StringBuilder sb = new StringBuilder();
            sb.Append("<html><head><meta name='viewport' content='width=device-width, user-scalable=no' />");
            sb.Append("<style>.main img{max-width: 98% !important;}</style></head>");
            sb.AppendFormat("<body style=' background-color: #F8F8F8'><div class='main' style='width: 100%;'>{0}</div></body></html>", obj.Content ?? "");
            entity.Content = sb.ToString();

            return Newtonsoft.Json.JsonConvert.SerializeObject(entity);
        }

        /// <summary>
        /// 根据商家ID获取商家信息
        /// </summary>
        /// <param name="id">商家ID</param>
        /// <returns></returns>
        public string GetMerchant(int MerchantID)
        {
            var merchantModel = Factory.Get<IMerchantModel>(SystemConst.IOC_Model.MerchantModel);
            var obj = merchantModel.Get(MerchantID);
            string str1 = "<html><head><meta name='viewport' content='width=device-width, user-scalable=no' /><style>.main img{max-width: 98% !important;}</style></head><body style='background-color: #F8F8F8'><div style='width: 100%;'>";
            string str2 = "</div></body></html>";
            var app_Merchant = new App_Merchant
            {
                ID = obj.ID,
                Name = obj.Name,
                Address = obj.Address,
                Phone = obj.Phone,
                Introduction = str1 + obj.Introduction + str2
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(app_Merchant);
        }

        #region-----------------管道疏通----------------------------
        /// <summary>
        /// 获取管道疏通商户列表
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="PageIndex">要显示的页 第一页传0  之后的传ListPageIndex</param>
        /// <param name="ListCnt">没页显示的数目</param>
        /// <returns></returns>
        public string GetMerchantList_PipelineDredge(int amid, int PageIndex, int ListCnt)
        {
            int status = (int)EnumDataStatus.Enabled;
            var pipelineDredgeModel = Factory.Get<IM_PipelineDredgeModel>(SystemConst.IOC_Model.M_PipelineDredgeModel);


            string str1 = "<html><head><meta name='viewport' content='width=device-width, user-scalable=no' /><style>.main img{max-width: 98% !important;}</style></head><body style='background-color:#F8F8F8'><div style='width: 100%;'>";
            string str2 = "</div></body></html>";


            var list = pipelineDredgeModel.List().Where(a => a.M_CommunityMappings.Any(b => b.AccountMainID == amid) && a.EnumDataStatus == status && a.IsPublish)
                .OrderByDescending(a => a.PublishDate)
                .Skip(PageIndex).Take(ListCnt).GroupBy(a => a.MerchantID).ToList()
                .Select(a => new App_Merchant
                {
                    ID = a.FirstOrDefault().MerchantID,
                    Name = a.FirstOrDefault().Merchant.Name,
                    Address = a.FirstOrDefault().Merchant.Address,
                    Logo =SystemConst.WebUrlIP + a.FirstOrDefault().Merchant.LogoShow.Replace("~", ""),
                    Phone = a.FirstOrDefault().Merchant.Phone,
                    Introduction = str1 + a.FirstOrDefault().Merchant.Introduction + str2
                }).ToList();
            var newPageIndex = PageIndex + +list.Count;
            var obj = new { ListPageIndex = newPageIndex, List = list };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 获取管道疏通列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="MID">商户ID</param>
        /// <param name="PageIndex">要显示的页 第一页传0  之后的传ListPageIndex</param>
        /// <param name="ListCnt">没页显示的数目</param>
        /// <returns></returns>
        public string GetPipelineDredgeList(int AMID, int MID, int PageIndex, int ListCnt)
        {
            int status = (int)EnumDataStatus.Enabled;
            var pipelineDredgeModel = Factory.Get<IM_PipelineDredgeModel>(SystemConst.IOC_Model.M_PipelineDredgeModel);
            var list = pipelineDredgeModel.List().Where(a => a.M_CommunityMappings.Any(b => b.AccountMainID == AMID) && a.EnumDataStatus == status && a.IsPublish && a.MerchantID == MID)
                .OrderByDescending(a => a.PublishDate).Skip(PageIndex).Take(ListCnt).ToList()
                .Select(a => new App_PipelineDredge
                {
                    ID = a.ID,
                    Title = a.Title,
                    Phone = a.Phone,
                    Price = a.Price.ToString("C"),
                    PublishDate = a.PublishDate.ToString(),
                    Content = a.Content
                }).ToList();
            var newPageIndex = PageIndex + +list.Count;
            var obj = new { ListPageIndex = newPageIndex, List = list };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 获取管道疏通详细信息
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public string GetPipelineDredgeInfo(int PID)
        {
            var pipelineDredgeModel = Factory.Get<IM_PipelineDredgeModel>(SystemConst.IOC_Model.M_PipelineDredgeModel);
            var pipeline = pipelineDredgeModel.Get(PID);
            App_PipelineDredge apd = new App_PipelineDredge();
            apd.Content = pipeline.Content;
            apd.ID = PID;
            apd.MID = pipeline.MerchantID;
            apd.MName = pipeline.Merchant.Name;
            apd.Phone = pipeline.Phone;
            apd.Price = pipeline.Price.ToString("C");
            apd.PublishDate = pipeline.PublishDate.ToString();
            apd.Title = pipeline.Title;
            return Newtonsoft.Json.JsonConvert.SerializeObject(apd);
        }

        #endregion

        #region-----------------开锁换锁----------------------------
        /// <summary>
        /// 获取开锁换锁商户列表
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="PageIndex">要显示的页 第一页传0  之后的传ListPageIndex</param>
        /// <param name="ListCnt">没页显示的数目</param>
        /// <returns></returns>
        public string GetMerchantList_Unlock(int amid, int PageIndex, int ListCnt)
        {
            int status = (int)EnumDataStatus.Enabled;
            var unlockModel = Factory.Get<IM_UnlockModel>(SystemConst.IOC_Model.M_UnlockModel);
            
            string str1 = "<html><head><meta name='viewport' content='width=device-width, user-scalable=no' /><style>.main img{max-width: 98% !important;}</style></head><body style='background-color:#F8F8F8'><div style='width: 100%;'>";
            string str2 = "</div></body></html>";

            var list = unlockModel.List().Where(a => a.M_CommunityMappings.Any(b => b.AccountMainID == amid) && a.EnumDataStatus == status && a.IsPublish)
                .OrderByDescending(a => a.PublishDate)
                .Skip(PageIndex).Take(ListCnt).GroupBy(a => a.MerchantID).ToList()
                .Select(a => new App_Merchant
                {
                    ID = a.FirstOrDefault().MerchantID,
                    Name = a.FirstOrDefault().Merchant.Name,
                    Address = a.FirstOrDefault().Merchant.Address,
                    Logo = SystemConst.WebUrlIP + a.FirstOrDefault().Merchant.LogoShow.Replace("~", ""),
                    Phone = a.FirstOrDefault().Merchant.Phone,
                    Introduction = str1 + a.FirstOrDefault().Merchant.Introduction + str2
                }).ToList();
            var newPageIndex = PageIndex + +list.Count;
            var obj = new { ListPageIndex = newPageIndex, List = list };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }


        /// <summary>
        /// 获取开锁换锁列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="MID">商户ID</param>
        /// <param name="PageIndex">要显示的页 第一页传0  之后的传ListPageIndex</param>
        /// <param name="ListCnt">没页显示的数目</param>
        /// <returns></returns>
        public string GetUnlockList(int AMID, int MID, int PageIndex, int ListCnt)
        {
            int status = (int)EnumDataStatus.Enabled;
            var unlockModel = Factory.Get<IM_UnlockModel>(SystemConst.IOC_Model.M_UnlockModel);
            var list = unlockModel.List().Where(a => a.M_CommunityMappings.Any(b => b.AccountMainID == AMID) && a.EnumDataStatus == status && a.IsPublish && a.MerchantID == MID)
                .OrderByDescending(a => a.PublishDate).Skip(PageIndex).Take(ListCnt).ToList()
                .Select(a => new App_Unlock
                {
                    ID = a.ID,
                    Title = a.Title,
                    Phone = a.Phone,
                    Price = a.Price.ToString("C"),
                    PublishDate = a.PublishDate.ToString(),
                    Content = a.Content
                }).ToList();
            var newPageIndex = PageIndex + ListCnt;
            var obj = new { ListPageIndex = newPageIndex, List = list };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 获取开锁换锁详细信息
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public string GetUnlockInfo(int UID)
        {
            var unlockModel = Factory.Get<IM_UnlockModel>(SystemConst.IOC_Model.M_UnlockModel);
            var unlock = unlockModel.Get(UID);
            App_Unlock apd = new App_Unlock();
            apd.Content = unlock.Content;
            apd.ID = UID;
            apd.MID = unlock.MerchantID;
            apd.MName = unlock.Merchant.Name;
            apd.Phone = unlock.Phone;
            apd.Price = unlock.Price.ToString("C");
            apd.PublishDate = unlock.PublishDate.ToString();
            apd.Title = unlock.Title;
            return Newtonsoft.Json.JsonConvert.SerializeObject(apd);
        }

        #endregion

        #region-----------------搬家----------------------------
        /// <summary>
        /// 获取搬家商户列表
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="PageIndex">要显示的页 第一页传0  之后的传ListPageIndex</param>
        /// <param name="ListCnt">没页显示的数目</param>
        /// <returns></returns>
        public string GetMerchantList_Move(int amid, int PageIndex, int ListCnt)
        {
            int status = (int)EnumDataStatus.Enabled;
            var moveModel = Factory.Get<IM_MoveModel>(SystemConst.IOC_Model.M_MoveModel);

            string str1 = "<html><head><meta name='viewport' content='width=device-width, user-scalable=no' /><style>.main img{max-width: 98% !important;}</style></head><body style='background-color:#F8F8F8'><div style='width: 100%;'>";
            string str2 = "</div></body></html>";

            var list = moveModel.List().Where(a => a.M_CommunityMappings.Any(b => b.AccountMainID == amid) && a.EnumDataStatus == status && a.IsPublish)
                .OrderByDescending(a => a.PublishDate)
                .Skip(PageIndex).Take(ListCnt).GroupBy(a => a.MerchantID).ToList()
                .Select(a => new App_Merchant
                {
                    ID = a.FirstOrDefault().MerchantID,
                    Name = a.FirstOrDefault().Merchant.Name,
                    Address = a.FirstOrDefault().Merchant.Address,
                    Logo = SystemConst.WebUrlIP + a.FirstOrDefault().Merchant.LogoShow.Replace("~", ""),
                    Phone = a.FirstOrDefault().Merchant.Phone,
                    Introduction = str1 + a.FirstOrDefault().Merchant.Introduction + str2
                }).ToList();
            var newPageIndex = PageIndex + +list.Count;
            var obj = new { ListPageIndex = newPageIndex, List = list };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }


        /// <summary>
        /// 获取搬家列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="MID">商户ID</param>
        /// <param name="PageIndex">要显示的页 第一页传0  之后的传ListPageIndex</param>
        /// <param name="ListCnt">没页显示的数目</param>
        /// <returns></returns>
        public string GeMoveList(int AMID, int MID, int PageIndex, int ListCnt)
        {
            int status = (int)EnumDataStatus.Enabled;
            var moveModel = Factory.Get<IM_MoveModel>(SystemConst.IOC_Model.M_MoveModel);

            var list = moveModel.List().Where(a => a.M_CommunityMappings.Any(b => b.AccountMainID == AMID) && a.EnumDataStatus == status && a.IsPublish && a.MerchantID == MID)
                .OrderByDescending(a => a.PublishDate).Skip(PageIndex).Take(ListCnt).ToList()
                .Select(a => new App_Move
                {
                    ID = a.ID,
                    Title = a.Title,
                    Phone = a.Phone,
                    Price = a.Price.ToString("C"),
                    PublishDate = a.PublishDate.ToString(),
                    Content = a.Content
                }).ToList();
            var newPageIndex = PageIndex + +list.Count;
            var obj = new { ListPageIndex = newPageIndex, List = list };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 获取管道疏通详细信息
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public string GetMoveInfo(int MID)
        {
            var moveModel = Factory.Get<IM_MoveModel>(SystemConst.IOC_Model.M_MoveModel);
            var move = moveModel.Get(MID);
            App_Move apd = new App_Move();
            apd.Content = move.Content;
            apd.ID = MID;
            apd.MID = move.MerchantID;
            apd.MName = move.Merchant.Name;
            apd.Phone = move.Phone;
            apd.Price = move.Price.ToString("C");
            apd.PublishDate = move.PublishDate.ToString();
            apd.Title = move.Title;
            return Newtonsoft.Json.JsonConvert.SerializeObject(apd);
        }
        #endregion

        #region-----------------家教----------------------------
        /// <summary>
        /// 获取家教列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="PageIndex">要显示的页 第一页传0  之后的传ListPageIndex</param>
        /// <param name="ListCnt">没页显示的数目</param>
        /// <returns></returns>
        public string GetTutorList(int AMID, int PageIndex, int ListCnt)
        {
            int status = (int)EnumDataStatus.Enabled;
            var m_tutorModel = Factory.Get<IM_TutorModel>(SystemConst.IOC_Model.M_TutorModel);

            var list =m_tutorModel.List().Where(a => a.M_CommunityMappings.Any(b => b.AccountMainID == AMID) && a.EnumDataStatus == status && a.IsPublish)
                .OrderByDescending(a => a.PublishDate).Skip(PageIndex).Take(ListCnt).ToList()
                .Select(a => new App_Tutor
                {
                    ID = a.ID,
                    Title = a.Title,
                    Price = a.Price.ToString("C") + "/" + a.PriceRemark,
                    PublishDate = a.PublishDate.ToString(),
                    ShowImage = SystemConst.WebUrlIP +a.ShowImage.Replace("~",""),
                    Remark = a.Remark,
                    MID=a.MerchantID,
                    MName=a.Merchant.Name
                }).ToList();

            var newPageIndex = PageIndex + ListCnt;
            var obj = new { ListPageIndex = newPageIndex, List = list };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            
        }
        #endregion

        #region-----------------干洗服务----------------------------
        /// <summary>
        /// 获取干洗服务列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="PageIndex">要显示的页 第一页传0  之后的传ListPageIndex</param>
        /// <param name="ListCnt">没页显示的数目</param>
        /// <returns></returns>
        public string GetDryCleaningList(int AMID, int PageIndex, int ListCnt)
        {
            int status = (int)EnumDataStatus.Enabled;
            var m_drcleaningModel = Factory.Get<IM_DryCleaningModel>(SystemConst.IOC_Model.M_DryCleaningModel);

            var list = m_drcleaningModel.List().Where(a => a.M_CommunityMappings.Any(b => b.AccountMainID == AMID) && a.EnumDataStatus == status && a.IsPublish)
                .OrderByDescending(a => a.PublishDate).Skip(PageIndex).Take(ListCnt).ToList()
                .Select(a => new App_DryCleaning
                {
                    ID = a.ID,
                    Title = a.Title,
                    Price = a.Price.ToString("C") + "/" + a.PriceRemark,
                    PublishDate = a.PublishDate.ToString(),
                    ShowImage = SystemConst.WebUrlIP + a.ShowImage.Replace("~", ""),
                    Remark = a.Remark,
                    MID = a.MerchantID,
                    MName = a.Merchant.Name
                }).ToList();

            var newPageIndex = PageIndex + ListCnt;
            var obj = new { ListPageIndex = newPageIndex, List = list };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);

        }
        #endregion

        #region-----------------教育培训----------------------------
        /// <summary>
        /// 获取教育培训列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="PageIndex">要显示的页 第一页传0  之后的传ListPageIndex</param>
        /// <param name="ListCnt">没页显示的数目</param>
        /// <returns></returns>
        public string GetEducationTrainList(int AMID, int PageIndex, int ListCnt)
        {
            int status = (int)EnumDataStatus.Enabled;
            var m_educationtrainModel = Factory.Get<IM_EducationTrainModel>(SystemConst.IOC_Model.M_EducationTrainModel);

            var list = m_educationtrainModel.List().Where(a => a.M_CommunityMappings.Any(b => b.AccountMainID == AMID) && a.EnumDataStatus == status && a.IsPublish)
                .OrderByDescending(a => a.PublishDate).Skip(PageIndex).Take(ListCnt).ToList()
                .Select(a => new App_EducationTrain
                {
                    ID = a.ID,
                    Title = a.Title,
                    Price = a.Price.ToString("C") + "/" + a.PriceRemark,
                    PublishDate = a.PublishDate.ToString(),
                    ShowImage = SystemConst.WebUrlIP + a.ShowImage.Replace("~", ""),
                    Remark = a.Remark,
                    MID = a.MerchantID,
                    MName = a.Merchant.Name
                }).ToList();

            var newPageIndex = PageIndex + ListCnt;
            var obj = new { ListPageIndex = newPageIndex, List = list };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);

        }
        #endregion

        #region-----------------宠物医院----------------------------
        /// <summary>
        /// 获取宠物医院列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="PageIndex">要显示的页 第一页传0  之后的传ListPageIndex</param>
        /// <param name="ListCnt">没页显示的数目</param>
        /// <returns></returns>
        public string GetPetHospitalList(int AMID, int PageIndex, int ListCnt)
        {
            int status = (int)EnumDataStatus.Enabled;
            var m_pethospitalModel = Factory.Get<IM_PetHospitalModel>(SystemConst.IOC_Model.M_PetHospitalModel);

            var list = m_pethospitalModel.List().Where(a => a.M_CommunityMappings.Any(b => b.AccountMainID == AMID) && a.EnumDataStatus == status && a.IsPublish)
                .OrderByDescending(a => a.PublishDate).Skip(PageIndex).Take(ListCnt).ToList()
                .Select(a => new App_PetHospital
                {
                    ID = a.ID,
                    Title = a.Title,
                    Price = a.Price.ToString("C") + "/" + a.PriceRemark,
                    PublishDate = a.PublishDate.ToString(),
                    ShowImage = SystemConst.WebUrlIP + a.ShowImage.Replace("~", ""),
                    Remark = a.Remark,
                    MID = a.MerchantID,
                    MName = a.Merchant.Name
                }).ToList();

            var newPageIndex = PageIndex + ListCnt;
            var obj = new { ListPageIndex = newPageIndex, List = list };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);

        }
        #endregion

        #region-----------------家政服务----------------------------
        /// <summary>
        /// 获取家政服务列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="PageIndex">要显示的页 第一页传0  之后的传ListPageIndex</param>
        /// <param name="ListCnt">没页显示的数目</param>
        /// <returns></returns>
        public string GetDomesticList(int AMID, int PageIndex, int ListCnt)
        {
            int status = (int)EnumDataStatus.Enabled;
            var m_domesticModel = Factory.Get<IM_DomesticModel>(SystemConst.IOC_Model.M_DomesticModel);

            var list = m_domesticModel.List().Where(a => a.M_CommunityMappings.Any(b => b.AccountMainID == AMID) && a.EnumDataStatus == status && a.IsPublish)
                .OrderByDescending(a => a.PublishDate).Skip(PageIndex).Take(ListCnt).ToList()
                .Select(a => new App_Domestic
                {
                    ID = a.ID,
                    Title = a.Title,
                    Price = a.Price.ToString("C") + "/" + a.PriceRemark,
                    PublishDate = a.PublishDate.ToString(),
                    ShowImage = SystemConst.WebUrlIP + a.ShowImage.Replace("~", ""),
                    Remark = a.Remark,
                    MID = a.MerchantID,
                    MName = a.Merchant.Name
                }).ToList();

            var newPageIndex = PageIndex + ListCnt;
            var obj = new { ListPageIndex = newPageIndex, List = list };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);

        }
        #endregion


    }
}
