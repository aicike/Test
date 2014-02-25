using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.SSO;
using Poco;

namespace MicroSite_Web.Controllers
{
    public class SSOServiceRedirectController : Controller
    {

        public ActionResult Index(string Token, string BackURL)
        {
            #region 单点登录分站验证

            //令牌验证结果
            if (Request.QueryString["Token"] != "$Token$")
            {
                //持有令牌
                string tokenValue = Request.QueryString["Token"];
                //调用WebService获取主站凭证
                TokenService tokenService = new TokenService();
                object o = tokenService.TokenGetCredence(tokenValue);
                if (o != null)
                {
                    //令牌正确
                    var account = Newtonsoft.Json.JsonConvert.DeserializeObject<Account>(o.ToString());
                    Session[SystemConst.Session.LoginAccount] = account;
                }
            }

            #endregion
            return Redirect(BackURL);
        }
    }
}
