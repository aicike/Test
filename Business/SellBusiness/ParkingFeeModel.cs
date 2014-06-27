using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;

namespace Business
{
    /// <summary>
    /// 停车费
    /// </summary>
    public class ParkingFeeModel : BaseModel<ParkingFee>, IParkingFeeModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="Date">缴费日期</param>
        /// <param name="Unit">单元</param>
        /// <param name="RoomNumber">房号</param>
        /// <param name="OwnerName">业主姓名</param>
        /// <param name="OwnerPhone">业主电话</param>
        /// <returns></returns>
        public IQueryable<ParkingFee> GetParkingFeeInfo(int AMID, string Date, string Unit, string RoomNumber, string OwnerName, string OwnerPhone)
        {
            var list = List().Where(a => a.AccountMainID == AMID);
            if (!string.IsNullOrEmpty(Date))
            {
                list = list.Where(a => a.PayDate.Contains(Date));
            }
            if (!string.IsNullOrEmpty(Unit))
            {
                list = list.Where(a => a.Unit.Contains(Unit.Trim()));
            }
            if (!string.IsNullOrEmpty(RoomNumber))
            {
                list = list.Where(a => a.RoomNumber.Contains(RoomNumber.Trim()));
            }
            if (!string.IsNullOrEmpty(OwnerName))
            {
                list = list.Where(a => a.OwnerName.Contains(OwnerName.Trim()));
            }
            if (!string.IsNullOrEmpty(OwnerPhone))
            {
                list = list.Where(a => a.OwnerPhone.Contains(OwnerPhone.Trim()));
            }
            return list;
        }

        /// <summary>
        /// 根据房号 年份 获取停车费
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="RoomNumber"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public List<ParkingFee> GetPropertyFeeInfo(int AMID, string PhoneNum, int Year)
        {
            string year = Year.ToString();
            var list = List().Where(a => a.AccountMainID == AMID && a.OwnerPhone == PhoneNum && a.PayDate.Contains(year)).ToList();
            return list;
        }

        /// <summary>
        /// 根据ID 获取详细信息
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public ParkingFee GetInfoByID(int RID, int AMID)
        {
            var item = List().Where(a => a.AccountMainID == AMID && a.ID == RID).FirstOrDefault();
            return item;
        }

        /// <summary>
        /// 根据IDS 获取列表
        /// </summary>
        /// <param name="IDS"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public List<ParkingFee> GetPropertyFeeByIDS(int[] IDS, int AMID)
        {
            var list = List().Where(a => a.AccountMainID == AMID && IDS.Contains(a.ID)).ToList();
            return list;
        }

        /// <summary>
        /// 根据IDS 获取总金额
        /// </summary>
        /// <param name="IDS"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public double GetPriceByIDS(int[] IDS, int AMID)
        {
            var Price = List().Where(a => a.AccountMainID == AMID && IDS.Contains(a.ID)).Sum(a => a.ParkingFees);
            if (Price != null)
            {
                return Price;
            }
            else
            {
                return 0;
            }

        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="IDS"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public Result delAll(string IDS, int AMID)
        {
            string sql = "delete ParkingFee where id in(" + IDS + ") and AccountMainID=" + AMID;
            var cnt = base.SqlExecute(sql);
            Result result = new Result();
            if (cnt <= 0)
            {
                result.HasError = true;
                result.Error = "删除失败 请稍后再试！";
            }
            return result;
        }

        /// <summary>
        /// 数据导入验证
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <param name="LH"></param>
        /// <param name="DY"></param>
        /// <param name="FH"></param>
        /// <returns></returns>
        public Result DBImportCheck(int AMID, string PayDate, string Name, string Phone, string LH, string DY, string FH)
        {
            Result result = new Result();
            string sql = string.Format("select count(ID) from ParkingFee where AccountMainID={0} and PayDate='{1}' and OwnerName='{2}'and OwnerPhone='{3}'and BuildingNum='{4}'and Unit='{5}'and RoomNumber='{6}'",
                                      AMID, PayDate, Name, Phone, LH, DY, FH);
            var cnt = Context.Database.SqlQuery<int>(sql).FirstOrDefault();
            if (cnt > 0)
            {
                result.HasError = true;
                result.Error = "用户：" + Name + " 电话：" + Phone + " 楼号：" + LH + " 单元：" + DY + " 房号：" + FH + "在" + PayDate + "中的的数据已存在。请删除后再次导入。";
            }
            return result;
        }

        /// <summary>
        /// 获取一个月的数据
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="PayDate"></param>
        /// <returns></returns>
        public List<ParkingFee> GetAllByPayDay(int AMID, string PayDate)
        {
            var list = List().Where(a => a.AccountMainID == AMID && a.PayDate == PayDate).ToList();
            return list;
        }
    }
}
