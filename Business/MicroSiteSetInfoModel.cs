using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class MicroSiteSetInfoModel : BaseModel<MicroSiteSetInfo>, IMicroSiteSetInfoModel
    {
        public MicroSiteSetInfo GetByAccountMainID(int accountMainID)
        {
            var obj = List().Where(a => a.AccountMainID == accountMainID).FirstOrDefault();
            if (obj == null)
            {
                MicroSiteSetInfo setInfo = new MicroSiteSetInfo();
                setInfo.AccountMainID = accountMainID;
                base.Add(setInfo);
                return setInfo;
            }
            return obj;
        }

        public new Result Edit(MicroSiteSetInfo setInfo)
        {
            bool has = List().Any(a => a.ID == setInfo.ID && a.AccountMainID == setInfo.AccountMainID);
            if (has == false)
            {
                Result result = new Result();
                result.Error = "参数错误，无法操作。";
                return result;
            }
            return base.Edit(setInfo);
        }
    }
}
