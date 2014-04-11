using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Poco.WebAPI_Poco;
using Business;
using Common;

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
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            bool isExist = userLoginInfoModel.ExistPhone(amid, phone);
            Result result = new Result();
            if (isExist)
            {
                result.Error = "该电话已经成为业主账号，请直接登录。";
            }
            else
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
                result.Entity = objs;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 业主注册
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public string Register(int userID, string userName, string phone, string pwd)
        {
            Result result = new Result();
            var um = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            var user = um.Get(userID);
            if (user == null)
            {
                result.Error = "请求错误，请稍后重试。";
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            var ulim = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            var userLoginInfo = ulim.Get(user.UserLoginInfoID);
            CommonModel model = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            var isOk = model.CheckIsUnique("UserLoginInfo", "Phone", phone, userLoginInfo.ID);
            if (isOk == false)
            {
                result.Error = "该电话已被其他账号使用。";
                result.HasError = true;
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            userLoginInfo.Name = userName;
            userLoginInfo.Phone = phone;
            userLoginInfo.LoginPwd = DESEncrypt.Encrypt(pwd);
            userLoginInfo.LoginPwdPage = "000000";
            result = ulim.Edit(userLoginInfo);
            if (result.HasError)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            //修改Property_User 
            var property_UserModel = Factory.Get<IProperty_UserModel>(SystemConst.IOC_Model.Property_UserModel);
            result= property_UserModel.EditUserLoginInfoID(phone, user.AccountMainID, userLoginInfo.ID);
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

    }
}
