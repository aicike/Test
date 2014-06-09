using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco.MerchantPoco;
using Poco;

namespace Interface.MerchantInterface
{
    /// <summary>
    /// 干洗服务
    /// </summary>
    public interface IM_DryCleaningModel : IBaseModel<M_DryCleaning>
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="MID"></param>
        /// <returns></returns>
        IQueryable<M_DryCleaning> GetListByMID(int MID);


        /// <summary>
        /// 根据ID获取详细信息
        /// </summary>
        /// <param name="MID"></param>
        /// <param name="UID"></param>
        /// <returns></returns>
        M_DryCleaning GetInfoByID(int MID, int UID);

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="m_unlock"></param>
        /// <param name="communityIDs"></param>
        /// <returns></returns>
        Result AddInfo(M_DryCleaning m_drycleaning, int[] communityIDs, int x1, int y1, int width, int height, int Twidth, int Theight);

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="m_unlock"></param>
        /// <param name="communityIDs"></param>
        /// <returns></returns>
        Result EditInfo(M_DryCleaning m_drycleaning, int[] communityIDs, int x1, int y1, int width, int height, int Twidth, int Theight);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="MID"></param>
        /// <returns></returns>
        Result DeleteInfo(int DID, int MID);

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="EnumDataStatus"></param>
        /// <returns></returns>
        Result UpdateStatus(int DID, int EnumDataStatus);

        /// <summary>
        /// 修改是否发布
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="push"></param>
        /// <returns></returns>
        Result UpdatePush(int DID, bool push);

        /// <summary>
        /// 查询数据 审核数据
        /// </summary>
        /// <param name="EnumDataStatus"></param>
        /// <param name="CreatDate"></param>
        /// <param name="MName"></param>
        /// <returns></returns>
        IQueryable<M_DryCleaning> GetListByStatus(int? EnumDataStatus, string CreatDate, string MName);

    }
}
