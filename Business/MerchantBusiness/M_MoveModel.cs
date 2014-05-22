using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco.MerchantPoco;
using Interface.MerchantInterface;
using Injection.Transaction;
using Poco;

namespace Business.MerchantBusiness
{
    public class M_MoveModel : BaseModel<M_Move>, IM_MoveModel
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="MID"></param>
        /// <returns></returns>
        public IQueryable<M_Move> GetListByMID(int MID)
        {
            var list = List().Where(a => a.MerchantID == MID).OrderByDescending(a => a.CreatDate);
            return list;
        }


        /// <summary>
        /// 查询数据 审核数据
        /// </summary>
        /// <param name="EnumDataStatus"></param>
        /// <param name="CreatDate"></param>
        /// <param name="MName"></param>
        /// <returns></returns>
        public IQueryable<M_Move> GetListByStatus(int? EnumDataStatus, string CreatDate, string MName)
        {
            var list = List().Where(a => a.EnumDataStatus != (int)Poco.Enum.EnumDataStatus.None);
            if (EnumDataStatus.HasValue)
            {
                list = list.Where(a => a.EnumDataStatus == EnumDataStatus.Value);
            }
            if (!string.IsNullOrEmpty(CreatDate))
            {
                list = list.Where(a => CreatDate.Contains(a.CreatDate.ToString("yyyy-MM-dd")));
            }
            if (!string.IsNullOrEmpty(MName))
            {
                list = list.Where(a => MName.Contains(a.Merchant.Name));
            }

            return list.OrderByDescending(a => a.CreatDate);
        }




        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="m_unlock"></param>
        /// <param name="communityIDs"></param>
        /// <returns></returns>
        [Transaction]
        public Result AddInfo(M_Move m_move, int[] communityIDs)
        {
            List<M_CommunityMapping> list = new List<M_CommunityMapping>();
            foreach (var item in communityIDs)
            {
                M_CommunityMapping mm = new M_CommunityMapping();
                mm.AccountMainID = item;
                list.Add(mm);
            }
            m_move.M_CommunityMappings = list;
            //保存商户发布信息和小区映射表
            Result result = base.Add(m_move);
            return result;
        }
        /// <summary>
        /// 根据ID获取详细信息
        /// </summary>
        /// <param name="MID"></param>
        /// <param name="UID"></param>
        /// <returns></returns>
        public M_Move GetInfoByID(int MID, int UID)
        {
            var item = List().Where(a => a.MerchantID == MID && a.ID == UID).FirstOrDefault();
            return item;

        }
        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="m_unlock"></param>
        /// <param name="communityIDs"></param>
        /// <returns></returns>
        [Transaction]
        public Result EditInfo(M_Move m_move, int[] communityIDs)
        {
            string sql = "DELETE dbo.M_CommunityMapping WHERE M_MoveID=" + m_move.ID;
            base.SqlExecute(sql);

            Result result = base.Edit(m_move);


            StringBuilder sql_add = new StringBuilder();
            sql_add.Append("INSERT INTO dbo.M_CommunityMapping( SystemStatus , AccountMainID , M_MoveID )");

            List<M_CommunityMapping> list = new List<M_CommunityMapping>();
            for (int i = 0; i < communityIDs.Length; i++)
            {
                if (i + 1 == communityIDs.Length)
                {
                    sql_add.AppendFormat("SELECT 0,{0},{1} ", communityIDs[i], m_move.ID);
                }
                else
                {
                    sql_add.AppendFormat("SELECT 0,{0},{1} UNION ALL ", communityIDs[i], m_move.ID);
                }
            }
            base.SqlExecute(sql_add.ToString());
            return result;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="MID"></param>
        /// <returns></returns>
        [Transaction]
        public Result DeleteInfo(int UID, int MID)
        {
            string sql2 = "DELETE dbo.M_CommunityMapping WHERE M_MoveID=" + UID;
            int cnt = base.SqlExecute(sql2);

            string sql = string.Format("Delete M_Move where id={0} and MerchantID={1}", UID, MID);
            cnt = base.SqlExecute(sql);

            Result result = new Result();
            if (cnt <= 0)
            {
                result.HasError = true;
                result.Error = "删除失败 请稍后再试！";
            }
            return result;

        }


        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="EnumDataStatus"></param>
        /// <returns></returns>
        public Result UpdateStatus(int UID, int EnumDataStatus)
        {
            Result result = new Result();
            string sql = string.Format("update M_Move set EnumDataStatus= {0} where ID = {1}", EnumDataStatus, UID);
            if (EnumDataStatus == (int)Poco.Enum.EnumDataStatus.Disabled)
            {
                sql = string.Format("update M_Move set EnumDataStatus= {0}, IsPublish='False' where ID = {1}", EnumDataStatus, UID);
            }
            int cnt = base.SqlExecute(sql);
            if (cnt <= 0)
            {
                result.HasError = true;
                result.Error = "修改失败 请稍后再试！";
            }
            return result;
        }

        /// <summary>
        /// 修改是否发布
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="push"></param>
        /// <returns></returns>
        public Result UpdatePush(int UID, bool push)
        {
            Result result = new Result();
            string sql = string.Format("update M_Move set IsPublish= '{0}' where ID = {1}", push, UID);

            int cnt = base.SqlExecute(sql);
            if (cnt <= 0)
            {
                result.HasError = true;
                result.Error = "修改失败 请稍后再试！";
            }
            return result;
        }

    }
}
