using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IAppAdvertorialOperationModel : IBaseModel<AppAdvertorialOperation>
    {
        
        /// <summary>
        /// 添加资讯操作数据数据
        /// </summary>
        /// <param name="aao"></param>
        /// <returns></returns>
        Result AddOperation(AppAdvertorialOperation aao);

         /// <summary>
        /// 获取资讯操作信息
        /// </summary>
        /// <param name="AID"></param>
        /// <returns></returns>
        IQueryable<AppAdvertorialOperation> getListByAID(int AID);


        /// <summary>
        /// 查询资讯用户 用户端
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="type">0 已读用户 1未读用户</param>
        /// <returns></returns>
        IQueryable<_B_AdvertoriaOperation> getAOlist(int AID, int type);

         /// <summary>
        /// 查询资讯用户 销售端
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="type">0 已读用户 1未读用户</param>
        /// <returns></returns>
        IQueryable<_B_AdvertoriaOperation> getAOlist_account(int AID, int type);
    }
}
