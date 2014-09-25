using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;
using Injection;
using System.Data;

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
        /// <param name="fhsx">房号缩写</param>
        /// <param name="IsPay">是否已经交费</param>
        /// <returns></returns>
        public IQueryable<ParkingFee> GetParkingFeeInfo(int AMID, string Date, string fhsx, string IsPay)
        {
            var list = List().Where(a => a.AccountMainID == AMID);
            if (!string.IsNullOrEmpty(Date))
            {
                string year = Date.Split('-')[0];
                string day = Date.Split('-')[1];
                string Date2 = "";
                if (day.Length > 1)
                {
                    if (day.Substring(0, 1) == "0")
                    {
                        Date2 = year + "-" + day.Substring(1, 1);
                    }
                    else
                    {
                        Date2 = Date;
                    }
                }
                else
                {
                    Date2 = year + "-0" + day.Substring(1, 1);
                }
                list = list.Where(a => a.PayDate == Date || a.PayDate == Date2);
            }
            if (!string.IsNullOrEmpty(fhsx))
            {
                list = list.Where(a => a.Abbreviation.Contains(fhsx.Trim()));
            }
            if (!string.IsNullOrEmpty(IsPay))
            {
                if (IsPay != "0")
                {
                    var pay = bool.Parse(IsPay);
                    list = list.Where(a => a.IsPay == pay);
                }
            }
            //if (!string.IsNullOrEmpty(OwnerName))
            //{
            //    list = list.Where(a => a.OwnerName.Contains(OwnerName.Trim()));
            //}
            //if (!string.IsNullOrEmpty(OwnerPhone))
            //{
            //    list = list.Where(a => a.OwnerPhone.Contains(OwnerPhone.Trim()));
            //}
            return list;
        }

        /// <summary>
        /// 根据电话 年份 获取停车费
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="RoomNumber"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public List<ParkingFee> GetPropertyFeeInfo(int AMID, string PhoneNum, int Year)
        {
            var property_userModel = Factory.Get<IProperty_UserModel>(SystemConst.IOC_Model.Property_UserModel);
            var property_house = property_userModel.getHoust_ByPhone(PhoneNum, AMID).Property_House;

            string year = Year.ToString();
            var list = List().Where(a => a.AccountMainID == AMID && a.PayDate.Contains(year) && a.BuildingNum == property_house.BuildingNum && a.RoomNumber == property_house.RoomNumber & a.Unit == property_house.CellNum).ToList(); //&& a.OwnerPhone == PhoneNum
            return list;
        }

        /// <summary>
        /// 根据房号 年份 获取停车费
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="RoomNumber"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public List<ParkingFee> GetPropertyFeeInfo_2(int AMID, string FHSX, int Year)
        {

            string year = Year.ToString();
            var list = List().Where(a => a.AccountMainID == AMID && a.PayDate.Contains(year) && a.Abbreviation == FHSX).ToList(); //&& a.OwnerPhone == PhoneNum
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
        public Result DBImportCheck(int AMID, string PayDate, string LH, string DY, string FH, string carnumber)
        {
            Result result = new Result();
            string sql = string.Format("select count(ID) from ParkingFee where AccountMainID={0} and PayDate='{1}' and BuildingNum='{2}'and Unit='{3}'and RoomNumber='{4}' and plates='{5}'",
                                      AMID, PayDate, LH, DY, FH, carnumber);
            var cnt = Context.Database.SqlQuery<int>(sql).FirstOrDefault();
            if (cnt > 0)
            {
                result.HasError = true;
                result.Error = " 楼号：" + LH + " 单元：" + DY + " 房号：" + FH + "车牌号：" + carnumber + " 在" + PayDate + "中的的数据已存在。请删除后再次导入。";
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

        /// <summary>
        /// 获取导出模板内容
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public DataTable getDtInfo(int AMID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("房号(缩写)");
            dt.Columns.Add("缴费月份");
            dt.Columns.Add("车牌号");
            dt.Columns.Add("停车费");
            dt.Columns.Add("停车类型");
            dt.Columns.Add("是否已缴费（是/否）");
            var property_houseModel = Factory.Get<IProperty_HouseModel>(SystemConst.IOC_Model.Property_HouseModel);
            var Property_House = property_houseModel.List().Where(a => a.AccountMainID == AMID).OrderBy(a => a.BuildingNum).OrderBy(a => a.CellNum).OrderBy(a => a.RoomNumber);
            var i = 0;
            foreach (var item in Property_House)
            {
                DataRow row = dt.NewRow();
                row["房号(缩写)"] = item.BuildingNum + "-" + item.CellNum + "-" + item.RoomNumber;
                if (i == 0)
                {
                    row["缴费月份"] = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString();
                    row["车牌号"] = "陕A 12345";
                    row["停车费"] = "300.00";
                    row["停车类型"] = "地上，地下，其他";
                    row["是否已缴费（是/否）"] = "是";

                }
                if (i == 0)
                {
                    row["缴费月份"] = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString();
                    row["车牌号"] = "陕A 54321";
                    row["停车费"] = "300.00";
                    row["停车类型"] = "地上，地下，其他";
                    row["是否已缴费（是/否）"] = "否";

                }
                dt.Rows.Add(row);
                i++;
            }
            return dt;
        }

        /// <summary>
        /// 获取导出历史内容
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public DataTable getDtHistoryInfo(int AMID, string DcDate, string Dcfhsx)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("房号(缩写)");
            dt.Columns.Add("缴费月份");
            dt.Columns.Add("车牌号");
            dt.Columns.Add("停车费");
            dt.Columns.Add("停车类型");
            dt.Columns.Add("是否已缴费（是/否）");
            var Propertyfee = this.List().Where(a => a.AccountMainID == AMID);
            if (DcDate != null && DcDate != "")
            {
                string year = DcDate.Split('-')[0];
                string day = DcDate.Split('-')[1];
                string Date2 = "";
                if (day.Length > 1)
                {
                    if (day.Substring(0, 1) == "0")
                    {
                        Date2 = year + "-" + day.Substring(1, 1);
                    }
                    else
                    {
                        Date2 = DcDate;
                    }
                }
                else
                {
                    Date2 = year + "-0" + day.Substring(1, 1);
                }
                Propertyfee = Propertyfee.Where(a => a.PayDate == DcDate || a.PayDate == Date2);
            }
            if (Dcfhsx != null && Dcfhsx != "")
            {
                Propertyfee = Propertyfee.Where(a => a.Abbreviation.Contains(Dcfhsx));
            }
            Propertyfee = Propertyfee.OrderBy(a => a.BuildingNum).OrderBy(a => a.Unit).OrderBy(a => a.RoomNumber);
            foreach (var item in Propertyfee)
            {
                DataRow row = dt.NewRow();
                row["房号(缩写)"] = item.BuildingNum + "-" + item.Unit + "-" + item.RoomNumber;

                row["缴费月份"] = item.PayDate;
                row["车牌号"] = item.plates;
                row["停车费"] = item.ParkingFees;
                row["停车类型"] = item.ParkingType;
                row["是否已缴费（是/否）"] = item.IsPay ? "是" : "否";

                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
