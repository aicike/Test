using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco.MerchantPoco;
using Poco;

namespace Interface.MerchantInterface
{
    public interface IM_UnlockModel : IBaseModel<M_Unlock>
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="MID"></param>
        /// <returns></returns>
        IQueryable<M_Unlock> GetListByMID(int MID);

        /// <summary>
       /// 添加信息
       /// </summary>
       /// <param name="m_unlock"></param>
       /// <param name="communityIDs"></param>
       /// <returns></returns>
        Result AddInfo(M_Unlock m_unlock, int[] communityIDs);

         /// <summary>
        /// 根据ID获取详细信息
        /// </summary>
        /// <param name="MID"></param>
        /// <param name="UID"></param>
        /// <returns></returns>
        M_Unlock GetInfoByID(int MID, int UID);

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="m_unlock"></param>
        /// <param name="communityIDs"></param>
        /// <returns></returns>
        Result EditInfo(M_Unlock m_unlock, int[] communityIDs);

         /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="MID"></param>
        /// <returns></returns>
        Result DeleteInfo(int UID, int MID);
    }
}
