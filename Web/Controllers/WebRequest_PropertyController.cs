using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;

namespace Web.Controllers
{
    public class WebRequest_PropertyController : Controller
    {
        // 物业
        // GET: /WebRequest_Property/

        /// <summary>
        /// 验证随机码与物业是否匹配 true：匹配，false:错误
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <param name="RandomCode"></param>
        /// <returns>true false</returns>
        public string CheckPropertyRandomCode(int AccountMainID,string RandomCode)
        {
            var accountmain = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            bool IsRight = accountmain.CheckPropertyRandomCode(AccountMainID, RandomCode);
            if (IsRight)
            {
                return "true";
            }
            else
            {
                return "false";
            }

        }










    }
}
