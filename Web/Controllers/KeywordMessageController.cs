using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interface;
using Poco;
using Injection;
using Controllers;

namespace Web.Controllers
{
    public class KeywordMessageController : ManageAccountController
    {
        public ActionResult Index(int? index)
        {
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            var list = autoMessage_KeywordModel.List(LoginAccount.CurrentAccountMainID).ToPagedList(index ?? 1, 15);
            return View(list);
        }

        [HttpPost]
        [ValidateInput(false)]
        public string Add(string ruleName, string keys, string messageTexts, string messageFileIDs, string messageImageTextIDs)
        {
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            var result = autoMessage_KeywordModel.Add(ruleName, keys, messageTexts, messageFileIDs, messageImageTextIDs, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return AlertJS_NoTag(new Dialog(result.Error));
            }
            return "window.location.href='" + Url.Action("Index", "KeywordMessage", new { HostName = LoginAccount.HostName }) + "'";
        }
    }
}
