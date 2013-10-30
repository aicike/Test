using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Poco.WebAPI_Poco;

namespace Web.Controllers
{
    public class WebRequest_ProductController : Controller
    {
        /// <summary>
        /// 获取一级分类，及分类下产品
        /// </summary>
        /// <param name="amid"></param>
        /// <returns></returns>
        public string Get1levelClass(int amid)
        {
            var classModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);
            var classList = classModel.Get1levelClass(amid);
            List<App_Class> list = new List<App_Class>();
            foreach (var item in classList)
            {
                App_Class appClass = new App_Class();
                appClass.ID = item.ID;
                appClass.Name = item.Name;
                appClass.ParentID = item.ParentID;
                List<App_Product> productList = new List<App_Product>();
                if (item.Product != null)
                {
                    foreach (var p in item.Product)
                    {
                        App_Product product = new App_Product();
                        product.ID = p.ID;
                        product.imgFilePath = SystemConst.WebUrlIP + p.imgFilePath.Replace("~", "");
                        product.Introduction = p.Introduction;
                        product.Specification = p.Specification;
                        product.Price = p.Price;
                        product.Name = p.Name;
                        productList.Add(product);
                    }
                }
                appClass.Product = productList;
                list.Add(appClass);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 获取产品详细
        /// </summary>
        /// <param name="pid"></param>
        public string GetProduct(int pid)
        {
            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            var p= productModel.Get(pid);
            App_Product product = new App_Product();
            product.ID = p.ID;
            product.imgFilePath = SystemConst.WebUrlIP + p.imgFilePath.Replace("~", "");
            product.Introduction = p.Introduction;
            product.Specification = p.Specification;
            product.Price = p.Price;
            product.Name = p.Name;
            return Newtonsoft.Json.JsonConvert.SerializeObject(product);
        }
    }
}
