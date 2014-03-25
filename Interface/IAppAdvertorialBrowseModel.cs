using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Poco.Enum;

namespace Interface
{
    public interface IAppAdvertorialBrowseModel : IBaseModel<AppAdvertorialBrowse>
    {
         /// <summary>
        /// 处理浏览次数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="ebat">EnumBrowseAdvertorialType 类型</param>
        /// <returns></returns>
        Result AddOrUpdBrowse(int id, EnumBrowseAdvertorialType ebat);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="ebat">EnumBrowseAdvertorialType 类型</param>
        /// <returns></returns>
        Result DelBrowse(int id, EnumBrowseAdvertorialType ebat);
    }
}
