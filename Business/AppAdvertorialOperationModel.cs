using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;

namespace Business
{
    public class AppAdvertorialOperationModel : BaseModel<AppAdvertorialOperation>, IAppAdvertorialOperationModel
    {
        /// <summary>
        /// 添加资讯操作数据数据
        /// </summary>
        /// <param name="aao"></param>
        /// <returns></returns>
        public Result AddOperation(AppAdvertorialOperation aao)
        {
            Result result = new Result();
            return base.Add(aao);
        }
    }
}
