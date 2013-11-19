using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Injection;
using Interface;
using Poco;

namespace Business
{
    public class AccountMainGuideModel
    {
        /// <summary>
        /// 获取向导数据
        /// </summary>
        /// <param name="AccountMainID">售楼部</param>
        /// <param name="IsUntreated">是否有向导没完成</param>
        /// <returns></returns>
        public DataTable getUntreated(int AccountMainID, out bool IsUntreated)
        {
            bool i = false;
            //初始化临时表
            DataTable dt = new DataTable();
            dt.Columns.Add("Title");        //显示内容
            dt.Columns.Add("Conntroller");  //控制器名称
            dt.Columns.Add("View");         //视图名称
            dt.Columns.Add("Status");       //状态 1未设置 2 已设置 3 非必须
            //是否设置基础设置
            var AccountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var accountmain = AccountMainModel.Get(AccountMainID);
            if (string.IsNullOrEmpty(accountmain.Lat))
            {
                DataRow row = dt.NewRow();
                row["Title"] = "第一步：在 <span style='font-weight:bold'>设置 </span>-><span style='font-weight:bold'> 基础设置 </span>中设置基础数据！";
                row["Conntroller"] = "BasisSet";
                row["View"] = "Index";
                row["Status"] = "1";
                dt.Rows.Add(row);
                i = true;
            }
            else
            {
                DataRow row = dt.NewRow();
                row["Title"] = "第一步：在 <span style='font-weight:bold'>设置 </span>-><span style='font-weight:bold'> 基础设置 </span>中设置基础数据！";
                row["Conntroller"] = "";
                row["View"] = "";
                row["Status"] = "2";
                dt.Rows.Add(row);
            }
            //是否添加项目
            var AccountHousesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var accountHouses = AccountHousesModel.GetList(AccountMainID);
            if (accountHouses.Count() > 0)
            {
                DataRow row = dt.NewRow();
                row["Title"] = "第二步：在 <span style='font-weight:bold'> 项目管理 </span>中录入项目信息！";
                row["Conntroller"] = "";
                row["View"] = "";
                row["Status"] = "2";
                dt.Rows.Add(row);
            }
            else
            {
                DataRow row = dt.NewRow();
                row["Title"] = "第二步：在 <span style='font-weight:bold'> 项目管理 </span>中录入项目信息！";
                row["Conntroller"] = "HousesMange";
                row["View"] = "Index";
                row["Status"] = "1";
                dt.Rows.Add(row);
                i = true;
            }
            //是否设置关键词自动回复
            var AutoMessageKeyModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            var autoMessagekey = AutoMessageKeyModel.List(AccountMainID);
            if (autoMessagekey.Count() > 0)
            {
                DataRow row = dt.NewRow();
                row["Title"] = "第三步：在 <span style='font-weight:bold'>设置 </span>-><span style='font-weight:bold'> 关键词自动回复 </span>中录入自动答复规则！";
                row["Conntroller"] = "";
                row["View"] = "";
                row["Status"] = "2";
                dt.Rows.Add(row);
            }
            else
            {
                DataRow row = dt.NewRow();
                row["Title"] = "第三步：在 <span style='font-weight:bold'>设置 </span>-><span style='font-weight:bold'> 关键词自动回复 </span>中录入自动答复规则！";
                row["Conntroller"] = "KeywordMessage";
                row["View"] = "Index";
                row["Status"] = "1";
                dt.Rows.Add(row);
                i = true;
            }

            //是否设置安装App自动回复
            //var AutoMessageModel = Factory.Get<IAutoMessage_AddModel>(SystemConst.IOC_Model.AutoMessage_AddModel);
            //var AutoMessage = AutoMessageModel.GetInfo(AccountMainID);
            //if (AutoMessage != null)
            //{
            //    DataRow row = dt.NewRow();
            //    row["Title"] = "第四步：在 <span style='font-weight:bold'>设置 </span>-><span style='font-weight:bold'> 安装App自动回复 </span>中录入安装App后的自动回复！";
            //    row["Conntroller"] = "";
            //    row["View"] = "";
            //    row["Status"] = "2";
            //    dt.Rows.Add(row);
            //}
            //else
            //{
            //    DataRow row = dt.NewRow();
            //    row["Title"] = "第四步：在 <span style='font-weight:bold'>设置 </span>-><span style='font-weight:bold'> 安装App自动回复 </span>中录入安装App后的自动回复！";
            //    row["Conntroller"] = "InstallAppReply";
            //    row["View"] = "Index";
            //    row["Status"] = "1";
            //    dt.Rows.Add(row);
            //    i = true;
            //}
            //是否设置App软文
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var AppAdvertorial = AppAdvertorialModel.GetList(AccountMainID);
            if (AppAdvertorial != null)
            {
                if (AppAdvertorial.Count() > 0)
                {
                    DataRow row = dt.NewRow();
                    row["Title"] = "第四步：在 <span style='font-weight:bold'>设置 </span>-><span style='font-weight:bold'> App动态软文 </span>中录入App动态软文！";
                    row["Conntroller"] = "";
                    row["View"] = "";
                    row["Status"] = "2";
                    dt.Rows.Add(row);
                }
                else
                {
                    DataRow row = dt.NewRow();
                    row["Title"] = "第四步：在 <span style='font-weight:bold'>设置 </span>-><span style='font-weight:bold'> App动态软文 </span>中录入App动态软文！";
                    row["Conntroller"] = "InstallAppReply";
                    row["View"] = "Index";
                    row["Status"] = "1";
                    dt.Rows.Add(row);
                    i = true;
                }
            }
            else
            {

            }


            //添加账号

            DataRow row1 = dt.NewRow();
            row1["Title"] = "第五步：在<span style='font-weight:bold'> 账号管理 </span>中添加账户！";
            row1["Conntroller"] = "Account";
            row1["View"] = "Index";
            row1["Status"] = "3";
            dt.Rows.Add(row1);


            //上传素材
            DataRow row2 = dt.NewRow();
            row2["Title"] = "第五步：在<span style='font-weight:bold'> 素材管理 </span>中完善您的素材库！";
            row2["Conntroller"] = "LibraryImage";
            row2["View"] = "Index";
            row2["Status"] = "3";
            dt.Rows.Add(row2);



            IsUntreated = i;
            return dt;
        }
    }
}
