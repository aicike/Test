using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection;
using System.Data;

namespace Business
{
    public class PropertyFeeInfoModel : BaseModel<PropertyFeeInfo>, IPropertyFeeInfoModel
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
        public IQueryable<PropertyFeeInfo> GetPropertyFeeInfo(int AMID, string Date, string fhsx, string IsPay)
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
            return list;
        }

        /// <summary>
        /// 根据电话 获取年份物业费
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="RoomNumber">电话</param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public List<PropertyFeeInfo> GetPropertyFeeInfo(int AMID, string RoomNumber, string Phone, int Year)
        {
            var property_userModel = Factory.Get<IProperty_UserModel>(SystemConst.IOC_Model.Property_UserModel);
            var property_house = property_userModel.getHoust_ByPhone(RoomNumber, AMID).Property_House;

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
        public List<PropertyFeeInfo> GetPropertyFeeInfo_2(int AMID, string FHSX, int Year)
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
        public PropertyFeeInfo GetInfoByID(int RID, int AMID)
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
        public List<PropertyFeeInfo> GetPropertyFeeByIDS(int[] IDS, int AMID)
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
            var Price = List().Where(a => a.AccountMainID == AMID && IDS.Contains(a.ID)).Sum(a => a.Total);
            if (Price.HasValue)
            {
                return Price.Value;
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
            string sql = "delete PropertyFeeInfo where id in(" + IDS + ") and AccountMainID=" + AMID;
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
        public Result DBImportCheck(int AMID, string PayDate, string LH, string DY, string FH)
        {
            Result result = new Result();
            string sql = string.Format("select count(ID) from PropertyFeeInfo where AccountMainID={0} and PayDate='{1}' and BuildingNum='{2}'and Unit='{3}'and RoomNumber='{4}'",
                                      AMID, PayDate, LH, DY, FH);

            var cnt = Context.Database.SqlQuery<int>(sql).FirstOrDefault();

            if (cnt > 0)
            {
                result.HasError = true;
                result.Error = "楼号：" + LH + " 单元：" + DY + " 房号：" + FH + "在" + PayDate + "中的的数据已存在。请删除后再次导入。";
            }

            return result;
        }

        /// <summary>
        /// 获取一个月的数据
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="PayDate"></param>
        /// <returns></returns>
        public List<PropertyFeeInfo> GetAllByPayDay(int AMID, string PayDate)
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
            dt.Columns.Add("物业管理费");
            dt.Columns.Add("电梯费");
            dt.Columns.Add("水费");
            dt.Columns.Add("卫生费");
            dt.Columns.Add("其他费用");
            dt.Columns.Add("合计");
            dt.Columns.Add("是否已缴费（是/否）");
            dt.Columns.Add("备注");
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
                    row["物业管理费"] = "200.00";
                    row["电梯费"] = "10.00";
                    row["水费"] = "60.00";
                    row["卫生费"] = "20.00";
                    row["其他费用"] = "100.00";
                    row["合计"] = "390.00";
                    row["是否已缴费（是/否）"] = "是";
                    row["备注"] = "其他费用为维修XXX费";

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
            dt.Columns.Add("物业管理费");
            dt.Columns.Add("电梯费");
            dt.Columns.Add("水费");
            dt.Columns.Add("卫生费");
            dt.Columns.Add("其他费用");
            dt.Columns.Add("合计");
            dt.Columns.Add("是否已缴费（是/否）");
            dt.Columns.Add("备注");
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
            Propertyfee=Propertyfee.OrderBy(a => a.BuildingNum).OrderBy(a => a.Unit).OrderBy(a => a.RoomNumber);
            foreach (var item in Propertyfee)
            {
                DataRow row = dt.NewRow();
                row["房号(缩写)"] = item.BuildingNum + "-" + item.Unit + "-" + item.RoomNumber;

                row["缴费月份"] = item.PayDate;
                row["物业管理费"] = item.ManagerFee;
                row["电梯费"] =item.ElevatorFee;
                row["水费"] = item.WaterFee;
                row["卫生费"] = item.HealthFee;
                row["其他费用"] =item.OrterFee;
                row["合计"] = item.Total;
                row["是否已缴费（是/否）"] = item.IsPay ? "是" : "否";
                row["备注"] = item.Remarks;

                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
