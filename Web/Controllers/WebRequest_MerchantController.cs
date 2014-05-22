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
                ItemList = obj.M_TakeOutDetails.Select(a => new App_TakeOutItem
                {
                    ID = a.ID,
                    Title = a.Title,
                    Price = a.Price
                }).ToList()
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(entity);
        }

        /// <summary>
        /// 
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
                Phone = obj.Phone
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(app_Merchant);
        }
    }
}
