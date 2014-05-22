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

namespace Web.Controllers
{
    public class WebRequest_MerchantController : Controller
    {
        /// <summary>
        /// 获取外卖
        /// </summary>
        /// <returns></returns>
        public string GetTakeOut(int amid, int ID, int ListCnt)
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
                    TakeOutPrice = a.TakeOutPrice
                }).ToList();
            var newID = ID + ListCnt;
            var obj = new { LastID = newID, List = list };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
