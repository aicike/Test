using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class AboutUSModel : BaseModel<AboutUS>, IAboutUSModel
    {
        /// <summary>
        /// 获取关于我们
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public AboutUS GetAboutUS(int AMID)
        {
            var item = List().Where(a=>a.AccountMainID==AMID).FirstOrDefault();
            return item;
        }
    }
}
