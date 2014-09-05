using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business.SellBusiness
{
    public class ExpressNoticeModel : BaseModel<ExpressNotice>, IExpressNoticeModel
    {
        /// <summary>
        /// 获取快递通知
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public ExpressNotice getInfoByAMID(int AMID)
        {
            var item = List().Where(a => a.AccountMainID == AMID).FirstOrDefault();
            return item;
        }
    }
}
