using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection;
using System.Data;

namespace Business
{
    public class Account_UserModel : BaseModel<Account_User>, IAccount_UserModel
    {
        public Result ChangeGroup(int userID, int accountID, int groupID)
        {
            var entity = List().Where(a => a.UserID == userID && a.AccountID == accountID).FirstOrDefault();
            if (entity == null)
            {
                throw new ApplicationException(SystemConst.Notice.NotAuthorized);
            }
            entity.GroupID = groupID;
            return Edit(entity);
        }

        /// <summary>
        /// 判断当前用户是否属于该销售代表
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="UID"></param>
        /// <returns></returns>
        public bool ChickUserInAccount(int SID, int UID)
        {
            var axcountUser = List().Any(a => a.AccountID == SID && a.UserID == UID);
            return axcountUser;
        }

        public int UpdUserTooAccount(int userID, int AmiAccountID, int groupID)
        {
            string sql = string.Format("update Account_User set AccountID={0},GroupID={1} where UserID = {2}", AmiAccountID, groupID, userID);

            return base.SqlExecute(sql);
        }


        /// <summary>
        /// 用户和Account关系绑定
        /// </summary>
        public Result BindUser_Account(int accountID, int userID)
        {
            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var account = accountModel.Get(accountID);
            var group = account.Groups.Where(a => a.IsDefaultGroup == true).FirstOrDefault();
            Account_User accountUser = new Account_User();
            accountUser.AccountID = accountID;
            accountUser.UserID = userID;
            accountUser.GroupID = group.ID;
            accountUser.CreateDate = DateTime.Now;
            return Add(accountUser);
        }

        /// <summary>
        /// 获取绑定的AccountID
        /// </summary>
        public Account GetBindAccountID(int userID, int accountMainID)
        {
            var entity = List().Where(a => a.UserID == userID && a.User.AccountMainID == accountMainID && a.Account.Account_AccountMains.Any(b => b.AccountMainID == accountMainID)).FirstOrDefault();
            if (entity != null)
            {
                return entity.Account;
            }
            return null ;
        }

        /// <summary>
        /// 获取用户数
        /// </summary>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        public int GetAccountUser(int AccountID)
        {
            var AccountUser = List().Where(a => a.AccountID == AccountID);
            if (AccountUser != null)
            {
                return AccountUser.Count();
            }
            return 0;
        }
        /// <summary>
        /// 获取一个星期的日增用户数
        /// </summary>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        public DataTable GetWeekUserCnt(int AccountID)
        {
            //结束日期
            string EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            //起始日期
            string BeGinDate = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
            string sql = string.Format(@" select CreateDate,count(CreateDate) as cnt  from 
                        (select  Convert(varchar(50),CreateDate,23) as CreateDate from dbo.Account_User where AccountID = {0} and CreateDate > '{1}' and CreateDate < '{2}') a 
                         group by a.CreateDate", AccountID, BeGinDate, EndDate);


            CommonModel model = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            var result = model.SqlQuery<_B_UserCount>(sql);
            DataTable dt = new DataTable();
            dt.Columns.Add("CreateDate");
            dt.Columns.Add("cnt",typeof(Int32));
            for (int i = 6; i >= 0; i--)
            {
                DataRow row = dt.NewRow();
                row["CreateDate"] = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd");
                row["cnt"] = 0;
                dt.Rows.Add(row);
            }

            foreach(var item  in result)
            {
                for(int i =0 ;i<dt.Rows.Count;i++)
                {
                    if (item.CreateDate == dt.Rows[i]["CreateDate"].ToString())
                    {
                        dt.Rows[i]["cnt"] = item.cnt;
                    }
                }
            }
           

            return dt;
        }
    }
}
