using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using System.Web;
using System.IO;

namespace Business
{
    public class ProductModel : BaseModel<Product>, IProductModel
    {
        public IQueryable<Product> GetList(int AccountMainID)
        {
            var ProductList = List().Where(a => a.AccountMainID == AccountMainID);

            return ProductList;
        }

        [Transaction]
        public Result AddInfo(Product product, System.Web.HttpPostedFileBase HousTypeImagePathFile)
        {
            var result = base.Add(product);
            if (result.HasError == false && HousTypeImagePathFile != null)
            {
                try
                {
                    var path = string.Format(SystemConst.Business.PathBase, product.AccountMainID);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var imageName = string.Format("{0}_{1}", token, HousTypeImagePathFile.FileName);
                    var imagePath = string.Format("{0}\\{1}", path, imageName);
                    var imagePath2 = HttpContext.Current.Server.MapPath(imagePath);
                    HousTypeImagePathFile.SaveAs(imagePath2);
                    product.imgFilePath = imagePath;

                    result = Edit(product);
                }
                catch (Exception ex)
                {
                    result.Error = ex.Message;
                }
            }

            return result;

        }


        public Result EditInfo(Product product, HttpPostedFileBase HousTypeImagePathFile)
        {
            var result = base.Edit(product);
            if (result.HasError == false && HousTypeImagePathFile != null)
            {
                try
                {
                    string oldImgpath = product.imgFilePath;
                    var path = string.Format(SystemConst.Business.PathBase, product.AccountMainID);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var imageName = string.Format("{0}_{1}", token, HousTypeImagePathFile.FileName);
                    var imagePath = string.Format("{0}\\{1}", path, imageName);
                    var imagePath2 = HttpContext.Current.Server.MapPath(imagePath);
                    HousTypeImagePathFile.SaveAs(imagePath2);
                    product.imgFilePath = imagePath;


                    //删除原文件
                    if (oldImgpath != "~/Images/nopicture_icon.png")
                    {
                        var file = HttpContext.Current.Server.MapPath(oldImgpath);
                        if (File.Exists(file))
                        {
                            File.Delete(file);
                        }
                    }
                    result = Edit(product);
                }
                catch (Exception ex)
                {
                    result.Error = ex.Message;
                }
            }

            return result;
        }


        public Result DeleteInfo(int id, int AccountMainID)
        {
            Result result = new Result();
            string oldImgpath =base.Get(id).imgFilePath;
            //删除原文件
            if (oldImgpath != "~/Images/nopicture_icon.png")
            {
                var file = HttpContext.Current.Server.MapPath(oldImgpath);
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            string sql = string.Format("delete Product where ID={0} and AccountMainID={1}", id, AccountMainID);
            int cnt = base.SqlExecute(sql);
            if (cnt > 0)
            {
                result.HasError = false;
            }
            else
            {
                result.HasError = true;
                result.Error = "删除失败 请稍后再试！";
            }
            return result;
        }
    }
}
