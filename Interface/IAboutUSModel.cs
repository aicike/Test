using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IAboutUSModel : IBaseModel<AboutUS>
    {
        /// <summary>
        /// 获取关于我们
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        AboutUS GetAboutUS(int AMID);
    }
}
