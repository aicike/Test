using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Poco.Enum;

namespace Business
{
    public class OrderModel : BaseModel<Order>, IOrderModel
    {
        public IQueryable<Order> GetByAccountMianID(int accountMainID)
        {
            return List().Where(a => a.AccountMainID == accountMainID);
        }

        public IQueryable<Order> GetByAccountID(int accountID)
        {
            return List().Where(a => a.OrderUserID == accountID && a.OrderUserType == (int)EnumClientUserType.Account);
        }

        public IQueryable<Order> GetByAccountID(int accountID, bool orderStatusComplete)
        {
            var list = List().Where(a => a.OrderUserID == accountID && a.OrderUserType == (int)EnumClientUserType.Account);
            if (orderStatusComplete)
            {
                list.Where(a => a.status == (int)EnumOrderStatus.Complete || a.status == (int)EnumOrderStatus.Cancel);
            }
            else
            {
                list.Where(a => a.status != (int)EnumOrderStatus.Complete && a.status != (int)EnumOrderStatus.Cancel);
            }
            return list;
        }


        public IQueryable<Order> GetList(int accountMainID, int daybyday, string orderNum, string PhoneNum, string status)
        {
            var list = List().Where(a => a.AccountMainID == accountMainID);
            DateTime day = DateTime.Now;
            if (status != "all")
            {
                list = list.Where(a => a.status == int.Parse(status));
            }
            switch (daybyday)
            {
                case 0:

                    break;
                case 7:
                    day = DateTime.Now.AddDays(-8);
                    list = list.Where(a => a.OrderDate > day);
                    break;
                case 30:
                    day = DateTime.Now.AddDays(-31);
                    list = list.Where(a => a.OrderDate > day);
                    break;
                case 365:
                    day = DateTime.Now.AddDays(-366);
                    list = list.Where(a => a.OrderDate > day);
                    break;
            }
            if (!string.IsNullOrEmpty(orderNum))
            {
                list.Where(a=>a.OrderNum.Contains(orderNum.Trim()));
            }
            if (!string.IsNullOrEmpty(PhoneNum))
            {
                list.Where(a => a.OrderUserInfo.RPhone.Contains(PhoneNum.Trim()));
            }
            return list;
        }

    }
}
