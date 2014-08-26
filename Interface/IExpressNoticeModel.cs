using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IExpressNoticeModel : IBaseModel<ExpressNotice>
    {
        /// <summary>
        /// 获取快递通知
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        ExpressNotice getInfoByAMID(int AMID);
    }
}
