using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface;
using Poco;
using Business;
using System.Data;

namespace Web.Controllers
{
    public class HomeController : ManageAccountController
    {
        public ActionResult Index()
        {
            //判断是否打开配置向导
            //获取网站管理员
            var model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var account = model.GetAccountAdminListByAccountMain(LoginAccount.CurrentAccountMainID);
            if (account != null)
            {
                if (account.Count() > 0)
                {
                    if (LoginAccount.ID == account.ToList()[0].ID)
                    {
                        AccountMainGuideModel AMG = new AccountMainGuideModel();
                        bool isUntreated;
                        AMG.getUntreated(LoginAccount.CurrentAccountMainID, out isUntreated);
                        if (isUntreated)
                        {
                            ViewBag.AccountAdmin = "true";
                        }
                        else
                        {
                            ViewBag.AccountAdmin = "false";
                        }
                    }
                    else
                    {
                        ViewBag.AccountAdmin = "false";
                    }
                }
                else
                {
                    ViewBag.AccountAdmin = "false";
                }
            }


            ViewBag.UNAme = LoginAccount.Name;

            //获取未读消息数
            var messageModel = Factory.Get<IMessageModel>(SystemConst.IOC_Model.MessageModel);
            ViewBag.UnreadCnt = messageModel.getUnreadCnt(LoginAccount.ID);
            //获取用户数
            var AccountUserModel = Factory.Get<IAccount_UserModel>(SystemConst.IOC_Model.Account_UserModel);
            ViewBag.UserCnt = AccountUserModel.GetAccountUser(LoginAccount.ID);
            //获取每日净增人数
            var UserCntDt = Factory.Get<IAccount_UserModel>(SystemConst.IOC_Model.Account_UserModel);
            DataTable dt = UserCntDt.GetWeekUserCnt(LoginAccount.ID);
            string UserCntDate = "[";
            string UserCnt = "[";
            foreach (DataRow row in dt.Rows)
            {
                UserCntDate += "'" + row["CreateDate"].ToString() + "',";
                UserCnt +=  row["cnt"].ToString() + ",";
            }
            ViewBag.WeetUserCntDate = UserCntDate.TrimEnd(',') + "]";
            ViewBag.WeetUserCnt = UserCnt.TrimEnd(',') + "]";
            DataView Userdv = dt.DefaultView;
            Userdv.Sort = "cnt desc";
            int maxUserCnt = int.Parse(Userdv[0]["cnt"].ToString());
            if (maxUserCnt != 0)
            {
                ViewBag.maxUserCnt = Math.Ceiling(maxUserCnt * 1.0 / 5);
            }
            else
            {
                ViewBag.maxUserCnt = 1;
            }
            //获取每日接收消息数
            var MessCntDt = Factory.Get<IMessageModel>(SystemConst.IOC_Model.MessageModel);
            DataTable dtMess = MessCntDt.getDayMessCnt(LoginAccount.ID);
            string MessCntDate = "[";
            string MessCnt = "[";
            foreach (DataRow row in dtMess.Rows)
            {
                MessCntDate += "'" + row["sendTime"].ToString() + "',";
                MessCnt += row["cnt"].ToString() + ",";
            }
            ViewBag.WeetMessCntDate = MessCntDate.TrimEnd(',') + "]";
            ViewBag.WeetMessCnt = MessCnt.TrimEnd(',') + "]";

            DataView dv = dtMess.DefaultView;
            dv.Sort = "cnt desc";
            int maxMessCnt = int.Parse(dv[0]["cnt"].ToString());
            if (maxMessCnt != 0)
            {
                ViewBag.maxMessCnt = Math.Ceiling(maxMessCnt * 1.0 / 5);
            }
            else
            {
                ViewBag.maxMessCnt = 1;
            }

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "首页" ,LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;


            return View();
        }
    }
}
