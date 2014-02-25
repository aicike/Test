using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;
using Injection;

namespace Business
{
    public class AppAdvertorialOperationModel : BaseModel<AppAdvertorialOperation>, IAppAdvertorialOperationModel
    {
        /// <summary>
        /// 添加资讯操作数据数据
        /// </summary>
        /// <param name="aao"></param>
        /// <returns></returns>
        public Result AddOperation(AppAdvertorialOperation aao)
        {
            Result result = new Result();
            return base.Add(aao);
        }

        /// <summary>
        /// 获取资讯操作信息
        /// </summary>
        /// <param name="AID"></param>
        /// <returns></returns>
        public IQueryable<AppAdvertorialOperation> getListByAID(int AID)
        {
            var list = List().Where(a=>a.AppAdvertorialID==AID);
            return list;
        }

        /// <summary>
        /// 查询资讯用户 用户端
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="type">0 已读用户 1未读用户</param>
        /// <returns></returns>
        public IQueryable<_B_AdvertoriaOperation> getAOlist(int AID, int type)
        {
            var cmd = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            string sql = "";
            //0 已读用户 1未读用户
            if (type == 0)
            {
                sql = string.Format("select b.Name,b.Phone,c.forwardCnt,c.ForwardWeiXinCnt,c.ForwardWeiboCnt,c.ForwardFriendCnt from dbo.[User] a,dbo.UserLoginInfo b, dbo.AppAdvertorialOperation c "
                                    + "where a.userlogininfoid = b.id and a.id= c.userid and a.id in(select userid from AppAdvertorialOperation where appadvertorialid = {0})"
                                    ,AID);
            }
            else
            {

                sql = string.Format("select b.Name,b.Phone,0 as forwardCnt,0 as ForwardWeiXinCnt,0 as ForwardWeiboCnt,0 as ForwardFriendCnt  from dbo.[User] a,dbo.UserLoginInfo b"
                                    + " where a.userlogininfoid = b.id and a.id not in(select userid from AppAdvertorialOperation where appadvertorialid = {0})"
                                    , AID);
            }
            return cmd.SqlQuery<_B_AdvertoriaOperation>(sql);
        }

        /// <summary>
        /// 查询资讯用户 销售端
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="type">0 已读用户 1未读用户</param>
        /// <returns></returns>
        public IQueryable<_B_AdvertoriaOperation> getAOlist_account(int AID, int type)
        {
            var cmd = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            string sql = "";
            //0 已读用户 1未读用户
            if (type == 0)
            {
                sql = string.Format("select a.Name,a.Phone,b.forwardCnt,b.ForwardWeiXinCnt,b.ForwardWeiboCnt,b.ForwardFriendCnt from dbo.Account a, dbo.AppAdvertorialOperation b "
                                    + " where a.id= b.userid  and a.id in(select userid from AppAdvertorialOperation where appadvertorialid = {0})"
                                    , AID);
            }
            else
            {

                sql = string.Format("select b.Name,b.Phone,0 as forwardCnt,0 as ForwardWeiXinCnt,0 as ForwardWeiboCnt,0 as ForwardFriendCnt  from dbo.Account b"
                                    + " where b.id not in(select userid from AppAdvertorialOperation where appadvertorialid = {0})"
                                    , AID);
            }
            return cmd.SqlQuery<_B_AdvertoriaOperation>(sql);
        }
    }
}
