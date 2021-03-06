﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using System.Web;
using System.IO;
using Injection;

namespace Business
{
    public class ProductModel : BaseModel<Product>, IProductModel
    {
        public IQueryable<Product> GetList(int AccountMainID)
        {
            var ProductList = List(true).Where(a => a.AccountMainID == AccountMainID);

            return ProductList;
        }

        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public Product GetProduct(int PID, int AccountMainID)
        {
            return List().Where(a=>a.ID==PID&a.AccountMainID==AccountMainID).FirstOrDefault();
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
                    var imagePath = string.Format("{0}{1}", path, imageName);
                    var imagePath2 = HttpContext.Current.Server.MapPath(imagePath);
                    HousTypeImagePathFile.SaveAs(imagePath2);
                    //product.imgFilePath = imagePath;

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
                    //string oldImgpath = product.imgFilePath;
                    var path = string.Format(SystemConst.Business.PathBase, product.AccountMainID);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var imageName = string.Format("{0}_{1}", token, HousTypeImagePathFile.FileName);
                    var imagePath = string.Format("{0}{1}", path, imageName);
                    var imagePath2 = HttpContext.Current.Server.MapPath(imagePath);
                    HousTypeImagePathFile.SaveAs(imagePath2);
                    //product.imgFilePath = imagePath;


                    //删除原文件
                    //if (oldImgpath != "~/Images/nopicture_icon.png")
                    //{
                    //    var file = HttpContext.Current.Server.MapPath(oldImgpath);
                    //    if (File.Exists(file))
                    //    {
                    //        File.Delete(file);
                    //    }
                    //}
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
            //string oldImgpath =base.Get(id).imgFilePath;
            //删除原文件
            //if (oldImgpath != "~/Images/nopicture_icon.png")
            //{
            //    var file = HttpContext.Current.Server.MapPath(oldImgpath);
            //    if (File.Exists(file))
            //    {
            //        File.Delete(file);
            //    }
            //}
            var productImgModel = Factory.Get<IProductImgModel>(SystemConst.IOC_Model.ProductImgModel);
            result = productImgModel.DelImgInfo(id, AccountMainID);
           
            if (result.HasError)
            {
                result.HasError = true;
                result.Error = "删除失败 请稍后再试！";
            }
            else
            {
                string sql = string.Format("delete Product where ID={0} and AccountMainID={1}", id, AccountMainID);
                int cnt = base.SqlExecute(sql);
               
            }
            return result;
        }


        /// <summary>
        /// 根据分类ID 获取包含其子分类的所有产品(不包含 下架产品)
        /// </summary>
        /// <param name="TypeID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public IQueryable<Product> GetListByTypeID(int TypeID, int AccountMainID)
        {
            var classifyModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);
            string TypeIDS = TypeID+","+classifyModel.GetTypeSUBID(TypeID, AccountMainID);
            if(TypeIDS!="")
            {

                int []types = TypeIDS.ConvertToIntArray(',');
                var list = List(true).Where(a => types.Contains(a.ClassifyID) && a.Status != (int)Poco.Enum.EnumProductType.OffShelves);
                return list;
            }
            else
            {
               return null;
            }
        }

        /// <summary>
        /// 根据ID 数组获取产品信息
        /// </summary>
        /// <param name="IDS">产品id 数组</param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public IQueryable<Product> GetProductListByIDs(int[] IDS, int AccountMainID)
        {
            return List(true).Where(a => IDS.Contains(a.ID) && a.AccountMainID == AccountMainID);
        }

        /// <summary>
        /// 根据id 与 amid 获取产品信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public Product GetPInfo(int id, int AccountMainID)
        {
            return List().Where(a => a.ID == id && a.AccountMainID == AccountMainID).FirstOrDefault();
        }

        /// <summary>
        /// 修改发布状态
        /// </summary>
        /// <param name="PID">产品ID</param>
        /// <param name="AMID"></param>
        /// <param name="Release">发布状态 true 发布 false 不发布</param>
        /// <returns></returns>
        public Result UPRelease(int PID, int AMID, bool Release)
        {
            Result result = new Result();
            string sql = string.Format("update [dbo].[Product] set IsRelease = '{0}' where id={1} and AccountMainID={2}", Release, PID, AMID);
            if (base.SqlExecute(sql) <= 0)
            {
                result.HasError = true;
                result.Error = "更改发布状态失败";
            }
            
            return result;
        }
    }
}
