using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection;
using Injection.Transaction;
using System.Web;
using Common;
using System.IO;

namespace Business
{
    public class ProductImgModel : BaseModel<ProductImg>, IProductImgModel
    {

        /// <summary>
        /// 添加图片地址信息
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        [Transaction]
        public Result AddImgInfo(int ProductID, List<string> paths, int AMID)
        {
            Result result = new Result();
            try
            {
                CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                StringBuilder stringBuilderSql = new StringBuilder("insert into ProductImg(SystemStatus,ProductID,PImgOriginal,PImgMini) ");
                foreach (var item in paths)
                {
                    CommonModel com = new CommonModel();
                    var LastName = com.CreateRandom("", 5) + item.GetFileSuffix();
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var path = string.Format(SystemConst.Business.PathBase, AMID);
                    var accountPath = "";

                    //集成微网站
                    if (SystemConst.IsIntegrationWebProject)
                    {
                        accountPath = string.Format(SystemConst.IntegrationPathBase, AMID);
                    }
                    //不是集成微网站
                    else
                    {
                        accountPath = HttpContext.Current.Server.MapPath(path);
                    }
                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imageName2 = string.Format("{0}_M_{1}", token, LastName);
                    var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                    var imagePath2 = string.Format("{0}\\{1}", accountPath, imageName2);

                    var lsImaFilePath = HttpContext.Current.Server.MapPath(item);

                    Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                    var imgPath = path + imageName;

                    Tool.SuperGetPicThumbnail(imagePath, imagePath2, 70, 320, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                    var imgPathMini = path + imageName2;

                    stringBuilderSql.AppendFormat(" SELECT 0,{0},'{1}','{2}' UNION ALL", ProductID, imgPath, imgPathMini);
                }
                var OptionSql = stringBuilderSql.ToString();
                OptionSql = OptionSql.Remove(OptionSql.Length - " UNION ALL".Length);
                commonModel.SqlExecute(OptionSql);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Error = "上传失败，请稍后再试";
            }

            return result;
        }


        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public Result DelImgInfo(int ProductID, int AMID)
        {
            Result result = new Result();

            try
            {
                var list = List().Where(a => a.Product.AccountMainID == AMID && a.ProductID == ProductID);
                foreach (var item in list)
                {
                    var file = "";
                    var minifile = "";

                    //集成微网站
                    if (SystemConst.IsIntegrationWebProject)
                    {
                        file = string.Format(SystemConst.IntegrationPathBase, AMID) + item.PImgOriginal.Substring(item.PImgOriginal.LastIndexOf('/') + 1);
                        minifile = string.Format(SystemConst.IntegrationPathBase, AMID) + item.PImgMini.Substring(item.PImgMini.LastIndexOf('/') + 1);
                    }
                    //不是集成微网站
                    else
                    {
                        file = HttpContext.Current.Server.MapPath(item.PImgOriginal);
                        minifile = HttpContext.Current.Server.MapPath(item.PImgMini);

                    }

                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                    if (File.Exists(minifile))
                    {
                        File.Delete(minifile);
                    }
                }
                string sql = string.Format("delete ProductImg where ProductID={0} ", ProductID);
                int cnt = base.SqlExecute(sql);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Error = "删除失败，请稍后再试";
            }
            return result;
        }

        /// <summary>
        /// 修改图片地址信息
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        [Transaction]
        public Result EditImgInfo(int ProductID, List<string> paths, int AMID)
        {
            Result result = new Result();
            try
            {
                CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                StringBuilder stringBuilderSql = new StringBuilder("insert into ProductImg(SystemStatus,ProductID,PImgOriginal,PImgMini) ");
                foreach (var item in paths)
                {
                    CommonModel com = new CommonModel();
                    var LastName = com.CreateRandom("", 5) + item.GetFileSuffix();
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var path = string.Format(SystemConst.Business.PathBase, AMID);

                    var accountPath = "";
                    var lsImaFilePath = "";

                    //集成微网站
                    if (SystemConst.IsIntegrationWebProject)
                    {
                        accountPath = string.Format(SystemConst.IntegrationPathBase, AMID);
                        if (item.Contains("Temporary"))
                        {
                            lsImaFilePath = HttpContext.Current.Server.MapPath(item);
                        }
                        else
                        {
                            lsImaFilePath = accountPath + item.Substring(item.LastIndexOf('/') + 1);
                        }
                    }
                    //不是集成微网站
                    else
                    {
                        accountPath = HttpContext.Current.Server.MapPath(path);
                        lsImaFilePath = HttpContext.Current.Server.MapPath(item);
                    }




                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imageName2 = string.Format("{0}_M_{1}", token, LastName);
                    var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                    var imagePath2 = string.Format("{0}\\{1}", accountPath, imageName2);



                    Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                    var imgPath = path + imageName;

                    Tool.SuperGetPicThumbnail(imagePath, imagePath2, 70, 320, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                    var imgPathMini = path + imageName2;

                    stringBuilderSql.AppendFormat(" SELECT 0,{0},'{1}','{2}' UNION ALL", ProductID, imgPath, imgPathMini);
                }
                var OptionSql = stringBuilderSql.ToString();
                OptionSql = OptionSql.Remove(OptionSql.Length - " UNION ALL".Length);


                var list = List().Where(a => a.Product.AccountMainID == AMID && a.ProductID == ProductID);
                foreach (var item in list)
                {

                    var file = "";
                    var minifile = "";

                    //集成微网站
                    if (SystemConst.IsIntegrationWebProject)
                    {
                        file = string.Format(SystemConst.IntegrationPathBase, AMID) + item.PImgOriginal.Substring(item.PImgOriginal.LastIndexOf('/') + 1);
                        minifile = string.Format(SystemConst.IntegrationPathBase, AMID) + item.PImgMini.Substring(item.PImgMini.LastIndexOf('/') + 1);
                    }
                    //不是集成微网站
                    else
                    {
                        file = HttpContext.Current.Server.MapPath(item.PImgOriginal);
                        minifile = HttpContext.Current.Server.MapPath(item.PImgMini);

                    }
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                    if (File.Exists(minifile))
                    {
                        File.Delete(minifile);
                    }
                }
                string sql = string.Format("delete ProductImg where ProductID={0} ", ProductID);
                int cnt = base.SqlExecute(sql);

                commonModel.SqlExecute(OptionSql);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Error = "上传失败，请稍后再试";
            }

            return result;
        }
    }
}
