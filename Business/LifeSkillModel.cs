using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using System.Data.Entity;
using System.Web;
using Injection.Transaction;
using Common;
using System.IO;

namespace Business
{
    /// <summary>
    /// 生活技巧
    /// </summary>
    public class LifeSkillModel : BaseModel<LifeSkill>, ILifeSkillModel
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<LifeSkill> GetList()
        {
            var list = List().OrderByDescending(a=>a.IssueDate);
            return list;
        }

        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public LifeSkill GetInfo(int LID)
        {
            return List().Where(a => a.ID == LID).AsNoTracking().FirstOrDefault();
        }


        /// <summary>
        /// 更改阅读次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void BrowseCntADD(int id)
        {
            string sql = "update LifeSkill set BrowseCnt = (BrowseCnt+1) where id = " + id;
            base.SqlExecute(sql);
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="Release"></param>
        /// <returns></returns>
        public Result UpSatausByID(int RID, bool Release)
        {
            string sql = string.Format("update LifeSkill set IsRelease='{0}' where ID={1}", Release, RID);
            int cnt = base.SqlExecute(sql);
            Result result = new Result();

            if (cnt <= 0)
            {
                result.HasError = true;
            }
            return result;
        }

        /// <summary>
        /// 添加生活技巧
        /// </summary>
        /// <param name="recipes"></param>
        /// <param name="w">当前截图宽度</param>
        /// <param name="h">当前截图高度</param>
        /// <param name="x1">当前截图x坐标</param>
        /// <param name="y1">当前截图y坐标</param>
        /// <param name="Twidth">当前截图显示宽度</param>
        /// <param name="Theight">当前截图显示高度</param>
        /// <returns></returns>
        [Transaction]
        public Result AddLifeSkill(LifeSkill lifeskill, int w, int h, int x1, int y1, int tw, int th)
        {
            Result result = new Result();

            try
            {
                var path = SystemConst.Business.PlatformFile;
                var accountPath = HttpContext.Current.Server.MapPath(path);
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                CommonModel com = new CommonModel();
                var LastName = com.CreateRandom("", 5) + lifeskill.MainImagPath.GetFileSuffix();
                var imageName = string.Format("{0}_{1}", token, LastName);
                var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                var imageName2 = string.Format("{0}Y_{1}", token, LastName);
                var imagePath2 = string.Format("{0}\\{1}", accountPath, imageName2);
                var imageminiName = string.Format("{0}_{1}_{2}", token, "mini", LastName);
                var imageminiPath = string.Format("{0}\\{1}", accountPath, imageminiName);
                var imageshowName = string.Format("{0}_{1}_{2}", token, "show", LastName);
                var imageshowPath = string.Format("{0}\\{1}", accountPath, imageshowName);

                var lsImgPath = lifeskill.MainImagPath;

                var lsImaFilePath = HttpContext.Current.Server.MapPath(lsImgPath);
                Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);


                Tool.SuperGetPicThumbnailJT(lsImaFilePath, imagePath2, 70, w, h, x1, y1, tw, th, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                Tool.SuperGetPicThumbnail(imagePath2, imageshowPath, 70, 480, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                if (File.Exists(imagePath2))
                {
                    File.Delete(imagePath2);
                }

                //缩略图mini
                Tool.SuperGetPicThumbnail(imageshowPath, imageminiPath, 70, 200, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                lifeskill.MainImagPath = path + imageName;
                lifeskill.AppShowImagePath = path + imageshowName;
                lifeskill.MinImagePath = path + imageminiName;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            result = base.Add(lifeskill);

            return result;
        }

        /// <summary>
        /// 修改生活技巧
        /// </summary>
        /// <param name="recipes"></param>
        /// <param name="w">当前截图宽度</param>
        /// <param name="h">当前截图高度</param>
        /// <param name="x1">当前截图x坐标</param>
        /// <param name="y1">当前截图y坐标</param>
        /// <param name="Twidth">当前截图显示宽度</param>
        /// <param name="Theight">当前截图显示高度</param>
        /// <returns></returns>
        [Transaction]
        public Result EditLifeSkill(LifeSkill lifeskill, int w, int h, int x1, int y1, int tw, int th)
        {
            var appadvertorials = this.GetInfo(lifeskill.ID);

            if (appadvertorials.MainImagPath != lifeskill.MainImagPath)
            {
                CommonModel com = new CommonModel();
                var LastName = com.CreateRandom("", 5) + lifeskill.MainImagPath.GetFileSuffix();


                var path = SystemConst.Business.PlatformFile;
                var accountPath = HttpContext.Current.Server.MapPath(path);
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");

                var imageName = string.Format("{0}_{1}", token, LastName);
                var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                var imageName2 = string.Format("{0}Y_{1}", token, LastName);
                var imagePath2 = string.Format("{0}\\{1}", accountPath, imageName2);
                var imageminiName = string.Format("{0}_{1}_{2}", token, "mini", LastName);
                var imageminiPath = string.Format("{0}\\{1}", accountPath, imageminiName);
                var imageshowName = string.Format("{0}_{1}_{2}", token, "show", LastName);
                var imageshowPath = string.Format("{0}\\{1}", accountPath, imageshowName);

                try
                {

                    var lsImgPath = lifeskill.MainImagPath;
                    var lsImaFilePath = HttpContext.Current.Server.MapPath(lsImgPath);

                    Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);


                    Tool.SuperGetPicThumbnailJT(lsImaFilePath, imagePath2, 70, w, h, x1, y1, tw, th, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    Tool.SuperGetPicThumbnail(imagePath2, imageshowPath, 70, 480, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    if (File.Exists(imagePath2))
                    {
                        File.Delete(imagePath2);
                    }

                    //缩略图mini
                    Tool.SuperGetPicThumbnail(imageshowPath, imageminiPath, 70, 200, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);


                    string path2 = HttpContext.Current.Server.MapPath(appadvertorials.MinImagePath);
                    if (File.Exists(path2))
                    {
                        File.Delete(path2);
                    }
                    path2 = HttpContext.Current.Server.MapPath(appadvertorials.MainImagPath);
                    if (File.Exists(path2))
                    {
                        File.Delete(path2);
                    }
                    path2 = HttpContext.Current.Server.MapPath(appadvertorials.AppShowImagePath);
                    if (File.Exists(path2))
                    {
                        File.Delete(path2);
                    }

                    lifeskill.MainImagPath = path + imageName;
                    lifeskill.AppShowImagePath = path + imageshowName;
                    lifeskill.MinImagePath = path + imageminiName;
                }
                catch
                {
                    throw;
                }
            }
            Result result = base.Edit(lifeskill);

            return result;
        }




        [Transaction]
        public Result DelLifeSkill(int ID)
        {
            var Recipes = base.Get(ID);


            string path = HttpContext.Current.Server.MapPath(Recipes.MinImagePath);

            string path2 = HttpContext.Current.Server.MapPath(Recipes.MainImagPath);

            string path3 = HttpContext.Current.Server.MapPath(Recipes.AppShowImagePath);


            var result = base.CompleteDelete(ID);
            if (!result.HasError)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                if (File.Exists(path2))
                {
                    File.Delete(path2);
                }

                if (File.Exists(path3))
                {
                    File.Delete(path3);
                }
            }
            return result;
        }



    }
}
