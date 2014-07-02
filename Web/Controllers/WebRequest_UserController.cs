using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Poco.WebAPI_Poco;
using Common;
using Poco.Enum;
using Business;

namespace Web.Controllers
{
    public class WebRequest_UserController : Controller
    {
        /// <summary>
        /// 安卓登录
        /// </summary>
        public string UserLogin(string email, string loginPwd, int accountMainID, string clientID)
        {
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            var result = userLoginInfoModel.App_Login(new App_UserLoginInfo() { Email = email, Phone = email, Pwd = loginPwd, AccountMainID = accountMainID, ClientID = clientID, EnumClientSystemType = (int)EnumClientSystemType.Android });
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 安卓临时登录
        /// </summary>
        public string UserTempLogin(string email, string loginPwd, int accountMainID, string clientID)
        {
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            var result = userLoginInfoModel.App_LoginForTempLogin(new App_UserLoginInfo() { Email = email, Phone = email, Pwd = loginPwd, AccountMainID = accountMainID, ClientID = clientID });
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }


        /// <summary>
        /// IOS登录
        /// </summary>
        public string UserLoginIOS(string email, string loginPwd, int accountMainID, string clientID)
        {
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            var result = userLoginInfoModel.App_Login(new App_UserLoginInfo() { Email = email, Phone = email, Pwd = loginPwd, AccountMainID = accountMainID, ClientID = clientID, EnumClientSystemType = (int)EnumClientSystemType.IOS });
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }


        /// <summary>
        /// IOS临时登录
        /// </summary>
        public string UserTempLoginIOS(string email, string loginPwd, int accountMainID, string clientID)
        {
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            var result = userLoginInfoModel.App_LoginForTempLogin(new App_UserLoginInfo() { Email = email, Phone = email, Pwd = loginPwd, AccountMainID = accountMainID, ClientID = clientID });
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 注册
        /// </summary>
        [HttpPost]
        public string UserRegister(App_UserLoginInfo userLoginInfo)
        {
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            var result = userLoginInfoModel.Register(userLoginInfo);
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }
        /// <summary>
        /// 每次打开应用时，提交的clientID
        /// </summary>
        [HttpPost]
        [ValidateInput(false)]
        public string PostClientID(string clientID, int accountMainID, int? userID, int systemType = 1)
        {
            var clientInfoModel = Factory.Get<IClientInfoModel>(SystemConst.IOC_Model.ClientInfoModel);
            var result = clientInfoModel.PostClientID(clientID, accountMainID, userID, (EnumClientSystemType)systemType);
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 获取App登录页logo
        /// </summary>
        public string GetAppLogo(int accountMainID)
        {
            var accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            string path = SystemConst.WebUrlIP + (accountMainModel.Get(accountMainID).LogoImageThumbnailPath).Replace("~", "");
            return path;
        }

        /// <summary>
        /// 检查邮箱是否已存在
        /// </summary>
        [HttpPost]
        public string CheckEmailOnRegister(string email, int? userLoginInfoID)
        {
            Result result = new Result();
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            if (userLoginInfoModel.ExistEmail(email, userLoginInfoID))
            {
                result.Error = "已存在相同邮箱。";
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 检查电话是否已存在
        /// </summary>
        [HttpPost]
        public string CheckPhoneOnRegister(string phone, int? userLoginInfoID)
        {
            Result result = new Result();
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            if (userLoginInfoModel.ExistPhone(phone, userLoginInfoID))
            {
                result.Error = "已存在相同电话。";
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 获取销售人员信息
        /// </summary>
        [HttpPost]
        public string GetAccountList(int accountMainID)
        {
            var account_AccountMainModel = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);

            string hostUrl = SystemConst.WebUrlIP;


            string headimg = "";

            var accountList = account_AccountMainModel.GetAccountListByAccountMainID(accountMainID)
            .Select(a => new App_Account
            {
                ID = a.ID,
                Name = a.Name,
                HeadImagePath = a.HeadImagePath,
                Email = a.Email,
                Phone = a.Phone,
                Role = a.Account_Roles.FirstOrDefault().Role.Name,
                AutoMessage = a.AutoMessage_Replys.FirstOrDefault() == null ? "" : a.AutoMessage_Replys.FirstOrDefault().Content
            }).ToList();
            foreach (var item in accountList)
            {
                headimg = "";
                if (!string.IsNullOrEmpty(item.HeadImagePath))
                {
                    headimg = hostUrl + Url.Content(item.HeadImagePath ?? "~/Images/default_avatar.png");
                }
                item.HeadImagePath = headimg;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(accountList);
        }

        /// <summary>
        /// 获取项目列表信息
        /// </summary>
        public string GetProjectList(int accountMainID)
        {
            var accountMainHousesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var list = accountMainHousesModel.GetList(accountMainID).OrderBy(a => a.ID).Select(a => new App_AccountMainHouse
            {
                ID = a.ID,
                HName = a.HName
            }).ToList();
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 绑定User和Account
        /// </summary>
        public string BindUserAccount(int accountID, int userID)
        {
            var account_UserModel = Factory.Get<IAccount_UserModel>(SystemConst.IOC_Model.Account_UserModel);
            var result = account_UserModel.BindUser_Account(accountID, userID);
            result.Entity = accountID;
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 获取绑定的AccountID
        /// </summary>
        public string GetBindAccountID(int userID, int accountMainID)
        {
            var account_UserModel = Factory.Get<IAccount_UserModel>(SystemConst.IOC_Model.Account_UserModel);
            var account = account_UserModel.GetBindAccountID(userID, accountMainID);
            Result result = new Result();

            if (account != null)
            {
                string img = null;
                if (string.IsNullOrEmpty(account.HeadImagePath) == false)
                {
                    img = SystemConst.WebUrlIP + account.HeadImagePath.Replace("~", "");
                }
                else
                {
                    img = "";
                }
                result.Entity = new { id = account.ID, n = account.Name, i = img };
            }
            else
            {
                result.HasError = true;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public string SetUserInfo(int userID, string field, string value)
        {
            Result result = new Result();
            var um = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            var user = um.Get(userID);
            if (user == null)
            {
                result.Error = "请求错误，用户不存在或不可用。";
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            var ulim = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            var userLoginInfo = ulim.Get(user.UserLoginInfoID);
            CommonModel model = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            switch (field)
            {
                case "name":
                    userLoginInfo.Name = value;
                    break;
                case "phone":
                    var isOk = model.CheckIsUnique("UserLoginInfo", "Phone", value, userLoginInfo.ID);
                    if (isOk == false)
                    {
                        result.Error = "该电话已被其他账号使用。";
                        result.HasError = true;
                        return Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    }

                    userLoginInfo.Phone = value;
                    break;
                case "email":
                    var EmailisOk = model.CheckIsUnique("UserLoginInfo", "Email", value, userLoginInfo.ID);
                    if (EmailisOk == false)
                    {
                        result.Error = "该邮箱已被其他账号使用。";
                        result.HasError = true;
                        return Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    }
                    userLoginInfo.Email = value;
                    break;
                case "pwd":
                    userLoginInfo.LoginPwd = DESEncrypt.Encrypt(value);
                    break;
                case "headimg":
                    userLoginInfo.HeadImagePath = value.Replace(SystemConst.WebUrlIP, "~");
                    break;
            }
            userLoginInfo.LoginPwdPage = "000000";
            result = ulim.Edit(userLoginInfo);
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 获取售楼部坐标
        /// </summary>
        public string GetCoordinate(int accountMainID)
        {
            Result result = new Result();
            var AccountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var accountmain = AccountMainModel.Get(accountMainID);
            App_Coordinate ac = new App_Coordinate();
            if (string.IsNullOrEmpty(accountmain.Lat))
            {
                result.Error = "售楼部尚未定位坐标";
            }
            else
            {
                ac.Lat = accountmain.Lat;
                ac.Lng = accountmain.Lng;
                ac.Name = accountmain.Name;
                result.Entity = ac;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        public string FindPwd(string phone)
        {
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            var result = userLoginInfoModel.FindPwd(phone);
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 获取售楼部信息
        /// </summary>
        public string GetAccountMainInfo(int amid)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var entity = accountMainModel.Get(amid);
            Result result = new Result();
            if (entity == null)
            {
                result.Error = "参数错误。";
            }
            else
            {
                var house = entity.AccountMainHousess.FirstOrDefault();
                result.Entity = new
                {
                    n = entity.Name,//售楼部名称
                    a = entity.SaleAddress,//地址
                    p = string.IsNullOrEmpty(entity.SalePhone) ? "" : entity.SalePhone,//电话
                    i = house == null ? "" : house.HIntroduce ?? "",//介绍
                    sp = house == null ? "" : house.PreSalePermit ?? "",//预售证
                    cd = house == null ? "" : house.HCompletedDate.ToString("yyyy年MM月dd日"),//竣工时间
                    t = house == null ? "" : house.Traffic ?? ""//交通
                };
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 获取VIP信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetVIPInfo(int userID)
        {
            IVipInfoModel model = Factory.Get<IVipInfoModel>(SystemConst.IOC_Model.VipInfoModel);
            var vipinfo = model.GetVIPInfoByID(userID);
            Result result = new Result();
            if (vipinfo == null)
            {
                result.Error = "未找到会员卡信息，请稍后重试。";
            }
            else
            {
                App_VIPInfo entity = new App_VIPInfo();
                entity.ID = vipinfo.ID;
                entity.Balance = vipinfo.CardInfo.Balance;
                entity.score = vipinfo.score;
                entity.UserName = vipinfo.User.UserLoginInfo.Name;
                entity.UserPhone = vipinfo.User.UserLoginInfo.Phone;
                entity.CreateDate = vipinfo.CreateDate.ToString("yyyy-MM-dd");
                entity.CardNumber = vipinfo.CardInfo.CardPrefix.PrefixName + "." + vipinfo.CardInfo.CardNum;
                result.Entity = entity;

            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 检查是否有VIP信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string CheckHasVIPInfo(int userID)
        {
            IVipInfoModel model = Factory.Get<IVipInfoModel>(SystemConst.IOC_Model.VipInfoModel);
            var vipinfo = model.GetVIPInfoByID(userID);
            if (vipinfo == null)
            {
                return "false";
            }
            else
            {
                return "true";
            }
        }

        public string GetVIPInfoExpenseDetailList(int userID)
        {
            IVIPInfoExpenseDetailModel model = Factory.Get<IVIPInfoExpenseDetailModel>(SystemConst.IOC_Model.VIPInfoExpenseDetailModel);
            var list = model.GetByUserID(userID).ToList().Select(a => new App_VIPInfoExpenseDetail()
            {
                ID = a.ID,
                ExpenseDate = a.ExpenseDate.ToString("yyyy-MM-dd HH:mm:ss"),
                ExpensePrice = a.ExpensePrice,
                ExpenseType = a.EnumVIPOperate == (int)EnumVIPOperate.Consume ? "消费" : "充值"

            });
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 校验消费卡片
        /// </summary>
        /// <param name="CardNum">前缀卡号</param>
        /// <param name="Money">金额</param>
        /// <returns></returns>
        public string CheckCardIsRight(string CardNums, double Money)
        {
            string cards = DESEncrypt.Decrypt(CardNums);
            Result result = new Result();
            string[] cardStr = cards.Split('.');
            if (cardStr.Length != 3)
            {
                result.HasError = true;
                result.Error = "该卡号无效！";
            }
            else
            {
                string CardNum = cardStr[1];
                string Prefix = cardStr[0];
                int AccountMainID = int.Parse(cardStr[2]);

                var CardModel = Factory.Get<ICardInfoModel>(SystemConst.IOC_Model.CardInfoModel);
                var vipModel = Factory.Get<IVipInfoModel>(SystemConst.IOC_Model.VipInfoModel);

                var cardinfo = CardModel.GetCardInfoBy(CardNum.Trim(), Prefix.Trim(), AccountMainID);
                if (cardinfo == null)
                {
                    result.HasError = true;
                    result.Error = "该卡号无效！";
                }
                else
                {
                    var vipinfo = vipModel.GetInfoBYCardID(cardinfo.ID, AccountMainID);
                    if (vipinfo != null)
                    {
                        if (cardinfo.Status == 0)
                        {
                            result.HasError = true;
                            result.Error = "该卡已被冻结！";

                        }
                        else
                        {
                            App_VIPInfo b_vip = new App_VIPInfo();
                            b_vip.ID = vipinfo.ID;
                            b_vip.CardNumber = cardinfo.CardPrefix.PrefixName + "." + cardinfo.CardNum;
                            b_vip.Balance = cardinfo.Balance;
                            b_vip.UserID = vipinfo.UserID ?? 0;
                            b_vip.UserName = vipinfo.User.UserLoginInfo.Name;
                            b_vip.UserPhone = vipinfo.User.UserLoginInfo.Phone;
                            b_vip.CardID = cardinfo.ID;
                            if (Convert.ToDecimal(Money) > b_vip.Balance)
                            {
                                result.HasError = true;
                                result.Error = "该卡余额不足！";
                            }
                            result.Entity = b_vip;
                        }

                    }
                    else
                    {
                        result.HasError = true;
                        result.Error = "该卡还未绑定用户！";
                    }


                }
            }


            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 卡片消费
        /// </summary>
        /// <param name="CardNums">卡号  前缀.号码.amid</param>
        /// <param name="Money">消费金额</param>
        /// <param name="AccountID">当前销售人ID</param>
        /// <returns></returns>
        public string CardConsumption(string CardNums, double Money, int AccountID)
        {
            Result result = new Result();
            var CardModel = Factory.Get<ICardInfoModel>(SystemConst.IOC_Model.CardInfoModel);
            var vipModel = Factory.Get<IVipInfoModel>(SystemConst.IOC_Model.VipInfoModel);

            string cards = DESEncrypt.Decrypt(CardNums);

            //int CardID, int UserID, int vipinfoID, int AccountMainID

            string[] cardStr = cards.Split('.');
            if (cardStr.Length != 3)
            {
                result.HasError = true;
                result.Error = "该卡号无效！";
            }
            else
            {
                string CardNum = cardStr[1];
                string Prefix = cardStr[0];
                int AccountMainID = int.Parse(cardStr[2]);


                var cardinfo = CardModel.GetCardInfoBy(CardNum.Trim(), Prefix.Trim(), AccountMainID);
                if (cardinfo == null)
                {
                    result.HasError = true;
                    result.Error = "该卡无效！";
                }
                else
                {
                    var vipinfo = vipModel.GetInfoBYCardID(cardinfo.ID, AccountMainID);
                    if (vipinfo != null)
                    {
                        if (cardinfo.Status == 0)
                        {
                            result.HasError = true;
                            result.Error = "该卡已被冻结！";

                        }
                        else
                        {

                            if (Convert.ToDecimal(Money) > cardinfo.Balance)
                            {
                                result.HasError = true;
                                result.Error = "该卡余额不足！";
                            }
                            else
                            {
                                //App_VIPInfo b_vip = new App_VIPInfo();
                                //b_vip.ID = vipinfo.ID;
                                //b_vip.Balance = cardinfo.Balance;
                                //b_vip.UserID = vipinfo.UserID ?? 0;
                                //b_vip.UserName = vipinfo.User.UserLoginInfo.Name;
                                //b_vip.UserPhone = vipinfo.User.UserLoginInfo.Phone;
                                //b_vip.CardID = cardinfo.ID;

                                var res = CardModel.Consumption(Convert.ToDecimal(Money), cardinfo.ID, AccountMainID, AccountID);
                                if (res.HasError)
                                {
                                    result.HasError = true;
                                    result.Error = "操作失败，请稍后再试！";
                                }
                                else
                                {
                                    App_VIPInfo appvip = new App_VIPInfo();
                                    appvip.Balance = cardinfo.Balance - Convert.ToDecimal(Money);
                                    appvip.UserID = vipinfo.UserID ?? 0;
                                    appvip.CardNumber = cardinfo.CardPrefix.PrefixName + "." + cardinfo.CardNum;
                                    appvip.CreateDate = vipinfo.CreateDate.ToString("yyyy-MM-dd hh:mm");
                                    appvip.score = vipinfo.score;
                                    appvip.Status = cardinfo.Status == 0 ? "冻结" : "正常";
                                    appvip.VIPType = "";
                                    result.Entity = appvip;
                                }

                            }
                        }

                    }
                    else
                    {
                        result.HasError = true;
                        result.Error = "该卡还未绑定用户！";
                    }


                }
            }


            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }


        /// <summary>
        /// 根据会员卡号回去用户信息
        /// </summary>
        /// <param name="CardNums"></param>
        /// <returns></returns>
        public string GetUserInfoByCardNums(string CardNums)
        {
            string cards = DESEncrypt.Decrypt(CardNums);
            Result result = new Result();
            string[] cardStr = cards.Split('.');
            if (cardStr.Length != 3)
            {
                result.HasError = true;
                result.Error = "该卡号无效！";
            }
            else
            {
                string CardNum = cardStr[1];
                string Prefix = cardStr[0];
                int AccountMainID = int.Parse(cardStr[2]);

                var CardModel = Factory.Get<ICardInfoModel>(SystemConst.IOC_Model.CardInfoModel);
                var vipModel = Factory.Get<IVipInfoModel>(SystemConst.IOC_Model.VipInfoModel);

                var cardinfo = CardModel.GetCardInfoBy(CardNum.Trim(), Prefix.Trim(), AccountMainID);
                if (cardinfo == null)
                {
                    result.HasError = true;
                    result.Error = "该卡号无效！";
                }
                else
                {
                    var vipinfo = vipModel.GetInfoBYCardID(cardinfo.ID, AccountMainID);
                    if (vipinfo != null)
                    {
                        string hostUrl = SystemConst.WebUrlIP;
                        var UserID = vipinfo.UserID ?? 0;
                        var UserModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
                        var user = UserModel.Get(UserID);
                        App_User au = new App_User();
                        au.Name = user.UserLoginInfo.Name;
                        au.Phone = user.UserLoginInfo.Phone;
                        if (user.SEX.HasValue)
                        {
                            if (user.SEX == 0)
                            {
                                au.SEX = "女";
                            }
                            else if (user.SEX == 1)
                            {
                                au.SEX = "男";
                            }
                        }
                        else
                        {
                            au.SEX = "未填写";
                        }
                        au.Age = user.Age;
                        au.Address = user.Address;
                        au.IDCard = user.IDCard;
                        au.Email = user.UserLoginInfo.Email;
                        au.ID = user.ID;
                        au.HeadImagePath = hostUrl + Url.Content(user.UserLoginInfo.HeadImagePath ?? "~/Images/default_avatar.png");
                        result.Entity = au;
                    }
                    else
                    {
                        result.HasError = true;
                        result.Error = "该卡还未绑定用户！";
                    }

                }
            }


            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 字符串加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetStrEncryption(string str)
        {
            string cards = DESEncrypt.Encrypt(str);
            return cards;
        }
    }
}
