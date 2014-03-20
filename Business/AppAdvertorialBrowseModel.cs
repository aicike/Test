using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;
using Poco.Enum;

namespace Business
{
    public class AppAdvertorialBrowseModel : BaseModel<AppAdvertorialBrowse> ,IAppAdvertorialBrowseModel
    {


        public Result AddOrUpdBrowseCnt(int ID,EnumBrowseType EBT)
        {
            Result result = new Result();
            switch (EBT)
            { 
                //资讯
                case EnumBrowseType.Consultation:
                    break;
                //活动
                case EnumBrowseType.Activity:
                    break;
                //调查
                case EnumBrowseType.Survey:
                    break;
            }



            return result;
        }
    }
}
