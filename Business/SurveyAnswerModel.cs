using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection;

namespace Business
{
    public class SurveyAnswerModel : BaseModel<SurveyAnswer>, ISurveyAnswerModel
    {

        /// <summary>
        /// 根据主表ID 查询调研回答
        /// </summary>
        /// <param name="SMID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public IQueryable<SurveyAnswer> GetListBySMID(int SMID, int AccountMainID)
        {
            var list = List().Where(a => a.SurveyTrouble.SurveyMain.ID == SMID && a.SurveyTrouble.SurveyMain.AccountMainID == AccountMainID);
            return list;
        }

        /// <summary>
        /// 录入答案
        /// </summary>
        /// <param name="sa">列表</param>
        /// <param name="UID">？用户</param>
        /// <param name="Utype">？0用户端，1销售端</param>
        /// <returns></returns>
        public Result InsertAnswer(List<SurveyAnswer> sa, int? UID, int? Utype)
        {
            Result result = new Result();
            string CreateDate = DateTime.Now.ToString();
            int UserID = 0;
            int UserType = -1;
            string UserName = "匿名";
            CommonModel com = new CommonModel();
            string UserCode = DateTime.Now.ToString("yyyyMMddHHmmssfff") + com.CreateRandom("", 4);
            if (UID.HasValue)
            {
                UserID = UID.Value;
                UserType = Utype.Value;
                if (UserType == 0)
                {
                    var UModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
                    UserName = UModel.Get(UserID).UserLoginInfo.Name;
                }
                else
                {
                    var ACModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
                    UserName = ACModel.Get(UserID).Name;
                }
            }
            try
            {
                if (sa != null && sa.Count > 0)
                {
                    StringBuilder stringBuilderSql = new StringBuilder("INSERT INTO dbo.SurveyAnswer( SystemStatus ,SurveyTroubleID ,SurveyOptionID ,[Content],CreateDate,UserName,UserID,UserType,UserCode) ");
                    foreach (var item in sa)
                    {
                        stringBuilderSql.AppendFormat(" SELECT 0,{0},{1},'{2}','{3}','{4}',{5},{6},'{7}' UNION ALL", item.SurveyTroubleID, item.SurveyOptionID == 0 ? "NULL" : item.SurveyOptionID + "", item.Content, CreateDate, UserName, UserID == 0 ? "NULL" : UserID + "", UserType == -1 ? "NULL" : UserType + "", UserCode);
                    }

                    CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;

                    var AnswerSql = stringBuilderSql.ToString();
                    AnswerSql = AnswerSql.Remove(AnswerSql.Length - " UNION ALL".Length);
                    commonModel.SqlExecute(AnswerSql);
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Error = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 查询调查问卷回答平均分
        /// </summary>
        /// <param name="SMID"></param>
        /// <returns></returns>
        public int GetAverageBySMID(int SMID)
        {
            try
            {
                var ZF = List().Where(a => a.SurveyTrouble.SurveyMainID == SMID).Select(a => a.SurveyOption.Fraction).Sum();
                var ucnt = List().Where(a => a.SurveyTrouble.SurveyMainID == SMID).GroupBy(a => a.UserCode).Count();
                var Average = ZF / ucnt;
                return Average;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 查询调查题目回答平均分
        /// </summary>
        /// <param name="TID"></param>
        /// <returns></returns>
        public int GetAverageByTID(int TID)
        {
            try
            {
                var ZF = List().Where(a => a.SurveyTroubleID == TID).Select(a => a.SurveyOption.Fraction).Sum();
                var ucnt = List().Where(a => a.SurveyTroubleID == TID).GroupBy(a => a.UserCode).Count();
                var Average = ZF / ucnt;
                return Average;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据选项ID 获取回答次数
        /// </summary>
        /// <param name="OID"></param>
        /// <returns></returns>
        public int GetAverageCntByOID(int OID)
        {
            var cnt = List().Where(a => a.SurveyOptionID == OID).Count();
            return cnt;
        }

        /// <summary>
        /// 获取选项回答百分比
        /// </summary>
        /// <param name="OID"></param>
        /// <param name="TID"></param>
        /// <returns></returns>
        public double GetAveragePercentage(int TID, int OID)
        {
            //回答次数
            var cnt = List().Where(a => a.SurveyOptionID == OID).Count();
            if (cnt == 0)
            {
                return 0;
            }
            //总回答人数
            var Usercnt = List().Where(a => a.SurveyTroubleID == TID).Count();
            try
            {
                var Pervcentage = double.Parse((((cnt * 0.1) / Usercnt) * 1000).ToString("0.00"));
                return Pervcentage;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据调查ID 获取回答人列表
        /// </summary>
        /// <param name="SMID"></param>
        /// <returns></returns>
        public IQueryable<SurveyAnswer> GetAnswerUserList(int SMID, int AccountMainID)
        {
            var list = List().Where(a => a.SurveyTrouble.SurveyMainID == SMID && a.SurveyTrouble.SurveyMain.AccountMainID == AccountMainID).GroupBy(a => a.UserCode).Select(a => a.FirstOrDefault()).OrderByDescending(a => a.ID);
            return list;
        }

        /// <summary>
        /// 根据UserCode获取回答信息
        /// </summary>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public IQueryable<SurveyAnswer> GetAnswerInfoByUserCode(string UserCode)
        {
            var list = List().Where(a=>a.UserCode==UserCode);
            return list;
        }

        /// <summary>
        /// 根据Ucode 获取提交人评分
        /// </summary>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public int GetAnswerUserScore(string UserCode)
        {
            try
            {
                var Ucnt = List().Where(a => a.UserCode == UserCode).Select(a => a.SurveyOption.Fraction).Sum();
                return Ucnt;
            }
            catch
            {
                return 0;
            }
        }

    }
}
