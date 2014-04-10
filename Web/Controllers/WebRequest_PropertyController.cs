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
    public class WebRequest_PropertyController : Controller
    {
        /// <summary>
        /// 验证随机码与物业是否匹配 true：匹配，false:错误
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <param name="RandomCode"></param>
        /// <returns>true false</returns>
        public string CheckPropertyRandomCode(int AccountMainID, string RandomCode)
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

        /// <summary>
        /// 根据房号获取物业登记的手机号码列表
        /// </summary>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        public string GetUserInfoByPhone(int amid, string phone)
        {
            var model = Factory.Get<IProperty_UserModel>(SystemConst.IOC_Model.Property_UserModel);
            var list = model.GetHouseByUserPhone(amid, phone);
            List<App_PropertyUser> objs = new List<App_PropertyUser>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    App_PropertyUser ap = new App_PropertyUser();
                    ap.PropertyUserID = item.ID;
                    ap.Name = item.UserName;
                    ap.Phone = item.Phone;
                    ap.RoomNum = item.Property_House.RoomNumber;
                    ap.BuildingNum = item.Property_House.BuildingNum;
                    ap.CellNum = item.Property_House.CellNum;
                    objs.Add(ap);
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(objs);
        }
    }
}
