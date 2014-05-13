using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class ExpressCollectionModel : BaseModel<ExpressCollection>, IExpressCollectionModel
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public IQueryable<ExpressCollection> GetListByAMID(int AMID)
        {
            var list = List().Where(a=>a.AccountMainID==AMID);
            return list;
        }

        /// <summary>
        /// 获取列表(条件查询)
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="OddNumber"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public IQueryable<ExpressCollection> GetListByAMID(int AMID ,string OddNumber, string phone)
        {
            var list = List().Where(a=>a.AccountMainID==AMID);
            if (!string.IsNullOrEmpty(OddNumber))
            {
                list = list.Where(a => a.OddNumber.Contains(OddNumber.Trim()));
            }
            if (!string.IsNullOrEmpty(phone))
            {
                list = list.Where(a => a.Phone.Contains(phone.Trim()));
            }
            return list;
        }

        /// <summary>
        /// 根据ID 获取详细信息
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ExpressCollection GetInfoBuID(int AMID, int ID)
        {
            var item = List().Where(a => a.AccountMainID == AMID && a.ID == ID).FirstOrDefault();
            return item;
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="AMID"></param>
        /// <param name="EnumExpressStatus"></param>
        /// <returns></returns>
        public Result UpdStatus(int ID, int AMID, int EnumExpressStatus)
        {
            Result result = new Result();
            string sql = string.Format("update ExpressCollection set EnumExpressStatus = {0} where ID={1} and AccountMainID={2}",EnumExpressStatus,ID,AMID);
            int cnt = base.SqlExecute(sql);
            if (cnt > 0)
            {

            }
            else {
                result.HasError = true;
            }
            return result;
        }

        /// <summary>
        /// 根据单号或电话查询快递
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="OddNumber"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public IQueryable<ExpressCollection> GetExpress(int AMID,string OddNumber, string Phone)
        {
            if (!string.IsNullOrEmpty(OddNumber) || !string.IsNullOrEmpty(Phone))
            {
                var list = List().Where(a => a.AccountMainID == AMID);
                if (!string.IsNullOrEmpty(OddNumber))
                {
                    list = list.Where(a => a.OddNumber == OddNumber.Trim());
                }
                if (!string.IsNullOrEmpty(Phone))
                {
                    list = list.Where(a => a.Phone == Phone.Trim());
                }
                return list;
            }
            else {
                return null;
            }

        }

    }
}
