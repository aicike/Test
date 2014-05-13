using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class CardPrefixModel : BaseModel<CardPrefix>, ICardPrefixModel
    {

        public IQueryable<CardPrefix> getList(int AccountMainID)
        {
            var list = List(true).Where(a => a.AccountMainID == AccountMainID);
            return list;
        }


        public Result CheckName(string PName, int AccountMainID)
        {
            Result result = new Result();
            bool isOk= List().Any(a => a.AccountMainID == AccountMainID&&a.PrefixName==PName.Trim());
            if (isOk)
            {
                result.HasError = true;
                result.Error = "前缀名称重复，添加失败！";
            }
            return result;
        }
    }
}
