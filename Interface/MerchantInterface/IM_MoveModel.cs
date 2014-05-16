using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco.MerchantPoco;
using Poco;

namespace Interface.MerchantInterface
{
    public interface IM_MoveModel : IBaseModel<M_Move>
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="MID"></param>
        /// <returns></returns>
        IQueryable<M_Move> GetListByMID(int MID);

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="m_unlock"></param>
        /// <param name="communityIDs"></param>
        /// <returns></returns>
        Result AddInfo(M_Move m_move, int[] communityIDs);
        /// <summary>
        /// 根据ID获取详细信息
        /// </summary>
        /// <param name="MID"></param>
        /// <param name="UID"></param>
        /// <returns></returns>
        M_Move GetInfoByID(int MID, int UID);
        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="m_unlock"></param>
        /// <param name="communityIDs"></param>
        /// <returns></returns>
        Result EditInfo(M_Move m_move, int[] communityIDs);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="MID"></param>
        /// <returns></returns>
        Result DeleteInfo(int UID, int MID);
    }
}
