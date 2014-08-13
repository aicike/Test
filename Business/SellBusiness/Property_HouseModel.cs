using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using Injection;

namespace Business
{
    public class Property_HouseModel : BaseModel<Property_House>, IProperty_HouseModel
    {
        [Transaction]
        public new Result Delete(int id)
        {
            var result = base.Delete(id);
            if (!result.HasError)
            {
                var propertyUserModel = Factory.Get<IProperty_UserModel>(SystemConst.IOC_Model.Property_UserModel);
                result = propertyUserModel.DeleteByPropertyHouseID(id);
            }
            return result;
        }
    }
}
