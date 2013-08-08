using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interface;
using Poco;
using Injection;
using Poco.WebAPI_Poco;

namespace Web.Controllers
{
    public class WebRequest_AutoMessageController : Controller
    {
        /// <summary>
        /// 获取首次引导信息
        /// </summary>
        /// <returns></returns>
        public string GetFirstAutoMessageList(int accountMainID)
        {
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            var list = autoMessage_KeywordModel.GetFirstAutoMessage(accountMainID);
            Result result = new Result();
            List<App_AutoMessage> appList = new List<App_AutoMessage>();
            foreach (var item in list)
            {
                appList.Add(new App_AutoMessage
                {
                    ID = item.ID,
                    RuleName = item.RuleName
                });
            }
            result.Entity = appList;
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

    }
}
