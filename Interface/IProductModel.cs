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

        /// <summary>
        /// 根据分类ID 获取包含其子分类的所有产品
        /// </summary>
        /// <param name="TypeID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        IQueryable<Product> GetListByTypeID(int TypeID,int AccountMainID);

        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        Product GetProduct(int PID, int AccountMainID);

        /// <summary>
        /// 根据ID数组 获取产品信息
        /// </summary>
        /// <param name="IDS">产品id 数组</param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        IQueryable<Product> GetProductListByIDs(int[] IDS, int AccountMainID);
       
    }
}
