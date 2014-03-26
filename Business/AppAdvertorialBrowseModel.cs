using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Poco.Enum;
using System.Data;

namespace Business
{
    public class AppAdvertorialBrowseModel : BaseModel<AppAdvertorialBrowse>, IAppAdvertorialBrowseModel
    {
        /// <summary>
        /// 处理浏览次数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="ebat">EnumBrowseAdvertorialType 类型</param>
        /// <returns></returns>
        public Result AddOrUpdBrowse(int id, EnumBrowseAdvertorialType ebat)
        {
            Result result = new Result();
            IQueryable<AppAdvertorialBrowse> list = null;
            string sql = "";
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            switch (ebat)
            {
                //资讯
                case EnumBrowseAdvertorialType.Information:
                    list = List().Where(a => a.EnumBrowseAdvertorialType == (int)ebat && a.AppAdvertorialID == id && a.BrowseDate.Day == DateTime.Now.Day && a.BrowseDate.Year == DateTime.Now.Year && a.BrowseDate.Month == DateTime.Now.Month);
                    break;
                //活动
                case EnumBrowseAdvertorialType.ActivityInfo:
                    list = List().Where(a => a.EnumBrowseAdvertorialType == (int)ebat && a.ActivityInfoID == id && a.BrowseDate.Day == DateTime.Now.Day && a.BrowseDate.Year == DateTime.Now.Year && a.BrowseDate.Month == DateTime.Now.Month);
                    break;
                //调查
                case EnumBrowseAdvertorialType.SurveyMain:
                    list = List().Where(a => a.EnumBrowseAdvertorialType == (int)ebat && a.SurveyMainID == id && a.BrowseDate.Day == DateTime.Now.Day && a.BrowseDate.Year == DateTime.Now.Year && a.BrowseDate.Month == DateTime.Now.Month);
                    break;
            }
            //更改浏览次数 增加一次
            if (list != null)
            {
                if (list.Count() > 0)
                {
                    sql = "update AppAdvertorialBrowse set BrowseCnt=(BrowseCnt+1) where {0}={1} and  convert(varchar(50),BrowseDate,21) like'%{2}%'";
                    switch (ebat)
                    {
                        //咨讯
                        case EnumBrowseAdvertorialType.Information:
                            sql = string.Format(sql, "AppAdvertorialID", id, date);
                            break;
                        //活动
                        case EnumBrowseAdvertorialType.ActivityInfo:
                            sql = string.Format(sql, "ActivityInfoID", id, date);
                            break;
                        //调查
                        case EnumBrowseAdvertorialType.SurveyMain:
                            sql = string.Format(sql, "SurveyMainID", id, date);
                            break;
                    }
                }
                else
                {
                    sql = "insert into AppAdvertorialBrowse(SystemStatus,BrowseCnt,EnumBrowseAdvertorialType,BrowseDate,{0}) values(0,1,{1},'{2}',{3})";
                    switch (ebat)
                    {
                        //咨讯
                        case EnumBrowseAdvertorialType.Information:
                            sql = string.Format(sql, "AppAdvertorialID", (int)ebat, date, id);
                            break;
                        //活动
                        case EnumBrowseAdvertorialType.ActivityInfo:
                            sql = string.Format(sql, "ActivityInfoID", (int)ebat, date, id);
                            break;
                        //调查
                        case EnumBrowseAdvertorialType.SurveyMain:
                            sql = string.Format(sql, "SurveyMainID", (int)ebat, date, id);
                            break;
                    }
                }
            }
            //第一次浏览
            else
            {
                sql = "insert into AppAdvertorialBrowse(SystemStatus,BrowseCnt,EnumBrowseAdvertorialType,BrowseDate,{0}) values(0,1,{1},{2},{3})";
                switch (ebat)
                {
                    //咨讯
                    case EnumBrowseAdvertorialType.Information:
                        sql = string.Format(sql, "AppAdvertorialID", (int)ebat, date, id);
                        break;
                    //活动
                    case EnumBrowseAdvertorialType.ActivityInfo:
                        sql = string.Format(sql, "ActivityInfoID", (int)ebat, date, id);
                        break;
                    //调查
                    case EnumBrowseAdvertorialType.SurveyMain:
                        sql = string.Format(sql, "SurveyMainID", (int)ebat, date, id);
                        break;
                }
            }
            int cnt = base.SqlExecute(sql);
            if (cnt <= 0)
            {
                result.HasError = true;
                result.Error = "处理浏览记录失败！";
            }
            return result;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="ebat">EnumBrowseAdvertorialType 类型</param>
        /// <returns></returns>
        public Result DelBrowse(int id, EnumBrowseAdvertorialType ebat)
        {
            Result result = new Result();
            string sql = "delete AppAdvertorialBrowse where {0}={1}";
            switch (ebat)
            {
                //咨讯
                case EnumBrowseAdvertorialType.Information:
                    sql = string.Format(sql, "AppAdvertorialID", id);
                    break;
                //活动
                case EnumBrowseAdvertorialType.ActivityInfo:
                    sql = string.Format(sql, "ActivityInfoID", id);
                    break;
                //调查
                case EnumBrowseAdvertorialType.SurveyMain:
                    sql = string.Format(sql, "SurveyMainID", id);
                    break;
            }
            base.SqlExecute(sql);
            //int cnt = base.SqlExecute(sql);
            //if (cnt <= 0)
            //{
            //    result.HasError = true;
            //    result.Error = "删除失败";
            //}
            return result;
        }


        /// <summary>
        /// 获取 活动浏览报表数据 12 天
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable GetReportInfo(int ID, string beginDate, string endDate)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Date");
            dt.Columns.Add("Cnt", typeof(int));
            DateTime beginDT = Convert.ToDateTime(beginDate);
            DateTime endDT = Convert.ToDateTime(endDate);
            for (int i = 0; i < 12; i++)
            {
                DataRow row = dt.NewRow();
                row["Date"] = beginDT.AddDays(i).ToString("yyyy-MM-dd");
                row["Cnt"] = 0;
                dt.Rows.Add(row);
            }

            var list = List().Where(a => a.ActivityInfoID == ID && (a.BrowseDate >= beginDT && a.BrowseDate <= endDT)).OrderBy(a => a.BrowseDate);
            
            foreach (var item in list)
            {
                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    if (item.BrowseDate.ToString("yyyy-MM-dd") == dt.Rows[i]["Date"].ToString())
                    {
                        dt.Rows[i]["Cnt"] = item.BrowseCnt;
                        break;
                    }
                }
            }
            return dt;
        }


    }
}
