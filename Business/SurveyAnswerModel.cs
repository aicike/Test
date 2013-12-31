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
    }
}
