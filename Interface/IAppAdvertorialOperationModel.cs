using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IAppAdvertorialOperationModel : IBaseModel<AppAdvertorialOperation>
    {
        
        /// <summary>
        /// 添加资讯操作数据数据
        /// </summary>
        /// <param name="aao"></param>
        /// <returns></returns>
        Result AddOperation(AppAdvertorialOperation aao);
    }
}
