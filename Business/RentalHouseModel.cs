using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using System.Web;
using Common;
using System.IO;

namespace Business
{
    /// <summary>
    /// 房屋租赁
    /// </summary>
    public class RentalHouseModel : BaseModel<RentalHouse>, IRentalHouseModel
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public IQueryable<RentalHouse> GetList(int AMID)
        {
            var list = List(true).Where(a => a.AccountMainID == AMID);
            return list;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public RentalHouse GetInfo(int RID,int AMID)
        {
            var item = List().Where(a => a.AccountMainID == AMID&&a.ID==RID).FirstOrDefault();
            return item;
        }


        /// <summary>
        ///  添加数据
        /// </summary>
        /// <param name="rentalhouse"></param>
        /// <param name="w">当前截图宽度</param>
        /// <param name="h">当前截图高度</param>
        /// <param name="x1">当前截图x坐标</param>
        /// <param name="y1">当前截图y坐标</param>
        /// <param name="Twidth">当前截图显示宽度</param>
        /// <param name="Theight">当前截图显示高度</param>
        /// <returns></returns>
        public Result AddInfo(RentalHouse rentalhouse, int w, int h, int x1, int y1, int tw, int th)
        {
            Result result = new Result();

            CommonModel com = new CommonModel();
            var LastName = com.CreateRandom("", 5) + rentalhouse.TitleImage.GetFileSuffix();
            var path = string.Format(SystemConst.Business.PathBase, rentalhouse.AccountMainID);
            var accountPath = HttpContext.Current.Server.MapPath(path);
            var token = DateTime.Now.ToString("yyyyMMddHHmmss");

            var imageName = string.Format("{0}_{1}", token, LastName);
            var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
            var imageName2 = string.Format("{0}Y_{1}", token, LastName);
            var imagePath2 = string.Format("{0}\\{1}", accountPath, imageName2);
            var imageshowName = string.Format("{0}_{1}_{2}", token, "show", LastName);
            var imageshowPath = string.Format("{0}\\{1}", accountPath, imageshowName);


            var lsImgPath = rentalhouse.TitleImage;
            var lsImaFilePath = HttpContext.Current.Server.MapPath(lsImgPath);

            Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);


            Tool.SuperGetPicThumbnailJT(lsImaFilePath, imagePath2, 70, w, h, x1, y1, tw, th, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

            Tool.SuperGetPicThumbnail(imagePath2, imageshowPath, 70, 480, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

            if (File.Exists(imagePath2))
            {
                File.Delete(imagePath2);
            }


            rentalhouse.TitleImage = path + imageName;
            rentalhouse.TitleShowImage = path + imageshowName;

            result = base.Add(rentalhouse);
            return result;
        }

        /// <summary>
        ///  修改数据
        /// </summary>
        /// <param name="rentalhouse"></param>
        /// <param name="w">当前截图宽度</param>
        /// <param name="h">当前截图高度</param>
        /// <param name="x1">当前截图x坐标</param>
        /// <param name="y1">当前截图y坐标</param>
        /// <param name="Twidth">当前截图显示宽度</param>
        /// <param name="Theight">当前截图显示高度</param>
        /// <returns></returns>
        public Result EditInfo(RentalHouse rentalhouse, int w, int h, int x1, int y1, int tw, int th)
        {
            Result result = new Result();
            var rh = this.Get(rentalhouse.ID);
            if (rh.TitleImage != rentalhouse.TitleImage)
            {
                CommonModel com = new CommonModel();
                var LastName = com.CreateRandom("", 5) + rentalhouse.TitleImage.GetFileSuffix();
                var path = string.Format(SystemConst.Business.PathBase, rentalhouse.AccountMainID);
                var accountPath = HttpContext.Current.Server.MapPath(path);
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");


                var imageName = string.Format("{0}_{1}", token, LastName);
                var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                var imageName2 = string.Format("{0}Y_{1}", token, LastName);
                var imagePath2 = string.Format("{0}\\{1}", accountPath, imageName2);
                var imageshowName = string.Format("{0}_{1}_{2}", token, "show", LastName);
                var imageshowPath = string.Format("{0}\\{1}", accountPath, imageshowName);

                try
                {
                    var lsImgPath = rentalhouse.TitleImage;
                    var lsImaFilePath = HttpContext.Current.Server.MapPath(lsImgPath);

                    Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);


                    Tool.SuperGetPicThumbnailJT(lsImaFilePath, imagePath2, 70, w, h, x1, y1, tw, th, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    Tool.SuperGetPicThumbnail(imagePath2, imageshowPath, 70, 480, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    if (File.Exists(imagePath2))
                    {
                        File.Delete(imagePath2);
                    }
                    string rhs = HttpContext.Current.Server.MapPath(rh.TitleImage);
                    if (File.Exists(rhs))
                    {
                        File.Delete(rhs);
                    }
                    rhs = HttpContext.Current.Server.MapPath(rh.TitleShowImage);
                    if (File.Exists(rhs))
                    {
                        File.Delete(rhs);
                    }

                    rentalhouse.TitleImage = path + imageName;
                    rentalhouse.TitleShowImage = path + imageshowName;
                }
                catch
                {
                
                }
              
            }
            result = base.Edit(rentalhouse);
            return result;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public Result Delete(int RID, int AMID)
        {
            Result reuslt = new Result();
            var rh = this.Get(RID);
            string sql = string.Format("delete RentalHouse where ID={0} and AccountMainID={1}",RID,AMID);
            int cnt = base.SqlExecute(sql);
            if (cnt > 0)
            {
                string rhs = HttpContext.Current.Server.MapPath(rh.TitleImage);
                if (File.Exists(rhs))
                {
                    File.Delete(rhs);
                }
                rhs = HttpContext.Current.Server.MapPath(rh.TitleShowImage);
                if (File.Exists(rhs))
                {
                    File.Delete(rhs);
                }

            }
            return reuslt;
        }
    }
}
