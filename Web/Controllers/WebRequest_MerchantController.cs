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
                    MerchantName = a.Merchant.Name
                }).ToList();
            var newID = ID + ListCnt;
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
            var app_Merchant = new App_Merchant
            {
                ID = obj.ID,
                Name = obj.Name,
                Address = obj.Address,
                Phone = obj.Phone,
                Introduction=obj.Introduction
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

            var list = pipelineDredgeModel.List().Where(a => a.M_CommunityMappings.Any(b => b.AccountMainID == amid) && a.EnumDataStatus == status && a.IsPublish)
                .OrderByDescending(a => a.PublishDate)
                .Skip(PageIndex).Take(ListCnt).GroupBy(a => a.MerchantID).ToList()
                .Select(a => new App_Merchant
                {
                    ID = a.FirstOrDefault().MerchantID,
                    Name = a.FirstOrDefault().Merchant.Name,
                    Address = a.FirstOrDefault().Merchant.Address,
                    Logo = "www." + SystemConst.WebUrl + a.FirstOrDefault().Merchant.LogoShow.Replace("~", ""),
                    Phone = a.FirstOrDefault().Merchant.Phone,
                    Introduction = a.FirstOrDefault().Merchant.Introduction
                }).ToList();
            var newPageIndex = PageIndex + ListCnt;
            var obj = new { ListPageIndex = newPageIndex, List = list };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
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

            var list = unlockModel.List().Where(a => a.M_CommunityMappings.Any(b => b.AccountMainID == amid) && a.EnumDataStatus == status && a.IsPublish)
                .OrderByDescending(a => a.PublishDate)
                .Skip(PageIndex).Take(ListCnt).GroupBy(a => a.MerchantID).ToList()
                .Select(a => new App_Merchant
                {
                    ID = a.FirstOrDefault().MerchantID,
                    Name = a.FirstOrDefault().Merchant.Name,
                    Address = a.FirstOrDefault().Merchant.Address,
                    Logo = "www." + SystemConst.WebUrl + a.FirstOrDefault().Merchant.LogoShow.Replace("~", ""),
                    Phone = a.FirstOrDefault().Merchant.Phone,
                    Introduction = a.FirstOrDefault().Merchant.Introduction
                }).ToList();
            var newPageIndex = PageIndex + ListCnt;
            var obj = new { ListPageIndex = newPageIndex, List = list };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
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

            var list = moveModel.List().Where(a => a.M_CommunityMappings.Any(b => b.AccountMainID == amid) && a.EnumDataStatus == status && a.IsPublish)
                .OrderByDescending(a => a.PublishDate)
                .Skip(PageIndex).Take(ListCnt).GroupBy(a => a.MerchantID).ToList()
                .Select(a => new App_Merchant
                {
                    ID = a.FirstOrDefault().MerchantID,
                    Name = a.FirstOrDefault().Merchant.Name,
                    Address = a.FirstOrDefault().Merchant.Address,
                    Logo = "www." + SystemConst.WebUrl + a.FirstOrDefault().Merchant.LogoShow.Replace("~", ""),
                    Phone = a.FirstOrDefault().Merchant.Phone,
                    Introduction = a.FirstOrDefault().Merchant.Introduction
                }).ToList();
            var newPageIndex = PageIndex + ListCnt;
            var obj = new { ListPageIndex = newPageIndex, List = list };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }


        #endregion
    }
}
