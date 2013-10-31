using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Poco.WebAPI_Poco;

namespace Web.Controllers
{
    public class WebRequest_MilkController : Controller
    {
        /// <summary>
        /// 查找规则，定多少天，每天几瓶
        /// </summary>
        /// <param name="amid"></param>
        /// <returns></returns>
        public string GetOrderMType(int amid)
        {
            var model = Factory.Get<IOrderMTypeModel>(SystemConst.IOC_Model.OrderMTypeModel);
            var list = model.GetList(amid).Select(a => new App_OrderMType()
            {
                ID = a.ID,
                Days = a.DateCnt,
                CountPerDay = a.Count,
                Name = a.Name
            }).ToList();
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }
    }
}
