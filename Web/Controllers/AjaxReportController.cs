using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;
using Injection;
using Interface;
using System.Data;

namespace Web.Controllers
{
    public class AjaxReportController : BaseController, IController
    {
        
        /// <summary>
        /// 获取每日净增人数数据
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDayAddPeople(int AccountID)
        {
            ReportBody RB = new ReportBody();

            var UserCntDt = Factory.Get<IAccount_UserModel>(SystemConst.IOC_Model.Account_UserModel);
            DataTable dt = UserCntDt.GetWeekUserCnt(AccountID);
            string UserCntDate = "";
            //string UserCnt = "";
            List<int> UserCnt = new List<int>();
            foreach (DataRow row in dt.Rows)
            {
                UserCntDate +=  row["CreateDate"].ToString() + ",";
                UserCnt.Add(Convert.ToInt32(row["cnt"]));
            } 
            RB.categories =UserCntDate.TrimEnd(',') ;
            RB.data = Newtonsoft.Json.JsonConvert.SerializeObject(UserCnt); 
            DataView Userdv = dt.DefaultView;
            Userdv.Sort = "cnt desc";
            int maxUserCnt = int.Parse(Userdv[0]["cnt"].ToString());
            if (maxUserCnt != 0)
            {
                RB.tickInterval= Math.Ceiling(maxUserCnt * 1.0 / 5).ToString();
            }
            else
            {
                RB.tickInterval = "1";
            }

            return Json(RB);
        }


        /// <summary>
        /// 获取每日接收消息数 数据
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDayAddNews(int AccountID)
        {
            ReportBody RB = new ReportBody();

            var MessCntDt = Factory.Get<IMessageModel>(SystemConst.IOC_Model.MessageModel);
            DataTable dtMess = MessCntDt.getDayMessCnt(AccountID);
            string MessCntDate = "";
            
            List<int> MessCnt = new List<int>();
            foreach (DataRow row in dtMess.Rows)
            {
                MessCntDate +=row["sendTime"].ToString() + ",";
                MessCnt.Add(Convert.ToInt32(row["cnt"]));
            }
            RB.categories = MessCntDate.TrimEnd(',');
            RB.data = Newtonsoft.Json.JsonConvert.SerializeObject(MessCnt); 

            DataView dv = dtMess.DefaultView;
            dv.Sort = "cnt desc";
            int maxMessCnt = int.Parse(dv[0]["cnt"].ToString());
            if (maxMessCnt != 0)
            {
                RB.tickInterval = Math.Ceiling(maxMessCnt * 1.0 / 5).ToString();
            }
            else
            {
                RB.tickInterval = "1";
            }
            return Json(RB);

        }



    }
}
