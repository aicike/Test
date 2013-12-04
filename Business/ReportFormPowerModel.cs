using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;
using Injection.Transaction;
using Injection;

namespace Business
{
    public class ReportFormPowerModel : BaseModel<ReportFormPower>, IReportFormPowerModel
    {
        public IQueryable<ReportFormPower> GetReportByAMID(int AMID)
        {
            return List().Where(a => a.AccountMainID == AMID);
        }

        [Transaction]
        public void Edit(int AMID, int[] RIDs)
        {
            CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            //删除原绑定的菜单
            string deleteMenuListSQL = "DELETE dbo.ReportFormPower WHERE AccountMainID=" + AMID;
            commonModel.SqlExecute(deleteMenuListSQL);
            //绑定新菜单
            if (RIDs != null && RIDs.Length > 0)
            {
                StringBuilder addMenuListStringBuilder = new StringBuilder("INSERT dbo.ReportFormPower ( SystemStatus ,AccountMainID , EunmReportID ) ");
                foreach (var item in RIDs)
                {
                    addMenuListStringBuilder.Append(string.Format(" SELECT 0,{0},{1} UNION ALL", AMID, item));
                }
                var addMenuListSQL = addMenuListStringBuilder.ToString();
                addMenuListSQL = addMenuListSQL.Remove(addMenuListSQL.Length - " UNION ALL".Length);
                commonModel.SqlExecute(addMenuListSQL);
            }

        }
    }
}
