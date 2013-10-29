using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web;

namespace Interface
{
    public interface IProductModel : IBaseModel<Product>
    {

        IQueryable<Product> GetList(int AccountMainID);

        Result AddInfo(Product product, HttpPostedFileBase HousTypeImagePathFile);

        Result EditInfo(Product product, HttpPostedFileBase HousTypeImagePathFile);

        Result DeleteInfo(int id,int AccountMainID);

    }
}
