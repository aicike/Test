using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using Common;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Business
{
    public class AppAdvertorialModel : BaseModel<AppAdvertorial>, IAppAdvertorialModel
    {

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public IQueryable<AppAdvertorial> GetList(int AccountMainID, int AdverTorialType)
        {
            var appadverlist = List().Where(a => a.AccountMainID == AccountMainID && a.EnumAdvertorialUType == AdverTorialType).OrderByDescending(a => a.stick).ThenByDescending(a => a.Sort).ThenByDescending(a => a.IssueDate);
            return appadverlist;
        }

        [Transaction]
        public Result AddAppAdvertorial(AppAdvertorial appadvertorial, System.Web.HttpPostedFileBase HousShowImagePathFile, int w, int h, int x1, int y1, int tw, int th)
        {
            Result result = new Result();

            try
            {

                CommonModel com = new CommonModel();
                var LastName = com.CreateRandom("", 5) + HousShowImagePathFile.FileName.GetFileSuffix();
                var path = string.Format(SystemConst.Business.PathBase, appadvertorial.AccountMainID);
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


                int dataLengthToRead = (int)HousShowImagePathFile.InputStream.Length;//获取下载的文件总大小
                byte[] buffer = new byte[dataLengthToRead];


                int r = HousShowImagePathFile.InputStream.Read(buffer, 0, dataLengthToRead);//本次实际读取到字节的个数
                Stream tream = new MemoryStream(buffer);
                Image img = Image.FromStream(tream);

                Tool.SuperGetPicThumbnail(img, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                img = Image.FromStream(tream);
                Tool.SuperGetPicThumbnailJT(img, imagePath2, 70, w, h, x1, y1, tw, th, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                Tool.SuperGetPicThumbnail(imagePath2, imageshowPath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                if (File.Exists(imagePath2))
                {
                    File.Delete(imagePath2);
                }

                //缩略图mini
                Tool.SuperGetPicThumbnail(imageshowPath, imageminiPath, 70, 120, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                appadvertorial.MainImagPath = path + imageName;
                appadvertorial.AppShowImagePath = path + imageshowName;
                appadvertorial.MinImagePath = path + imageminiName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            result = base.Add(appadvertorial);
            if (appadvertorial.stick == 1)
            {
                int cnt = EditAppAdvertorialStick(appadvertorial.ID, appadvertorial.stick, appadvertorial.AccountMainID, appadvertorial.Sort, appadvertorial.EnumAdvertorialUType);
                if (cnt <= 0)
                {
                    result.HasError = true;
                    result.Error = "添加失败 请稍后再试！";
                }
            }
            return result;
        }




        [Transaction]
        public Result AddAppAdvertorial(AppAdvertorial appadvertorial, int w, int h, int x1, int y1, int tw, int th)
        {
            Result result = new Result();

            try
            {

                var path = string.Format(SystemConst.Business.PathBase, appadvertorial.AccountMainID);
                var accountPath = HttpContext.Current.Server.MapPath(path);
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                CommonModel com = new CommonModel();
                var LastName = com.CreateRandom("", 5) + appadvertorial.MainImagPath.GetFileSuffix();


                var imageName = string.Format("{0}_{1}", token, LastName);
                var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                var imageName2 = string.Format("{0}Y_{1}", token, LastName);
                var imagePath2 = string.Format("{0}\\{1}", accountPath, imageName2);
                var imageminiName = string.Format("{0}_{1}_{2}", token, "mini", LastName);
                var imageminiPath = string.Format("{0}\\{1}", accountPath, imageminiName);
                var imageshowName = string.Format("{0}_{1}_{2}", token, "show", LastName);
                var imageshowPath = string.Format("{0}\\{1}", accountPath, imageshowName);


                var lsImgPath = appadvertorial.MainImagPath;
                var lsImaFilePath = HttpContext.Current.Server.MapPath(lsImgPath);


                Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);


                Tool.SuperGetPicThumbnailJT(lsImaFilePath, imagePath2, 70, w, h, x1, y1, tw, th, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                Tool.SuperGetPicThumbnail(imagePath2, imageshowPath, 70, 480, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                if (File.Exists(imagePath2))
                {
                    File.Delete(imagePath2);
                }

                //缩略图mini
                Tool.SuperGetPicThumbnail(imageshowPath, imageminiPath, 70, 120, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                appadvertorial.MainImagPath = path + imageName;
                appadvertorial.AppShowImagePath = path + imageshowName;
                appadvertorial.MinImagePath = path + imageminiName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            result = base.Add(appadvertorial);
            if (appadvertorial.stick == 1)
            {
                int cnt = EditAppAdvertorialStick(appadvertorial.ID, appadvertorial.stick, appadvertorial.AccountMainID, appadvertorial.Sort, appadvertorial.EnumAdvertorialUType);
                if (cnt <= 0)
                {
                    result.HasError = true;
                    result.Error = "添加失败 请稍后再试！";
                }
            }
            return result;
        }



        [Transaction]
        public Result DelAppAdvertorial(int ID, int AdverTorialType)
        {
            var appadivertorial = base.Get(ID);
            if (appadivertorial.MinImagePath.Substring(appadivertorial.MinImagePath.LastIndexOf('/')) != "/Survey.png" && appadivertorial.MinImagePath.Substring(appadivertorial.MinImagePath.LastIndexOf('/')) != "/ActivityInfo.png")
            {
                string path = HttpContext.Current.Server.MapPath(appadivertorial.MinImagePath);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                path = HttpContext.Current.Server.MapPath(appadivertorial.MainImagPath);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                path = HttpContext.Current.Server.MapPath(appadivertorial.AppShowImagePath);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            if (appadivertorial.stick == 1)
            {
                string sql = string.Format("update AppAdvertorial set Sort=(Sort-1) where AccountMainID = {0} and EnumAdvertorialUType={1} and stick=1 and sort>{2}", appadivertorial.AccountMainID, AdverTorialType, appadivertorial.Sort);
                base.SqlExecute(sql);
            }

            return base.CompleteDelete(ID);
        }


        [Transaction]
        public Result EditAppAdvertorial(AppAdvertorial appadvertorial, HttpPostedFileBase HousShowImagePathFile, int w, int h, int x1, int y1, int tw, int th)
        {
            var appadvertorials = this.Get(appadvertorial.ID);
            if (HousShowImagePathFile != null)
            {

                CommonModel com = new CommonModel();
                var LastName = com.CreateRandom("", 5) + HousShowImagePathFile.FileName.GetFileSuffix();
                var path = string.Format(SystemConst.Business.PathBase, appadvertorial.AccountMainID);
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
                HousShowImagePathFile.SaveAs(imagePath);
                try
                {

                    int dataLengthToRead = (int)HousShowImagePathFile.InputStream.Length;//获取下载的文件总大小
                    byte[] buffer = new byte[dataLengthToRead];


                    int r = HousShowImagePathFile.InputStream.Read(buffer, 0, dataLengthToRead);//本次实际读取到字节的个数
                    Stream tream = new MemoryStream(buffer);
                    Image img = Image.FromStream(tream);

                    Tool.SuperGetPicThumbnail(img, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    img = Image.FromStream(tream);
                    Tool.SuperGetPicThumbnailJT(img, imagePath2, 70, w, h, x1, y1, tw, th, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    Tool.SuperGetPicThumbnail(imagePath2, imageshowPath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    if (File.Exists(imagePath2))
                    {
                        File.Delete(imagePath2);
                    }

                    //缩略图mini
                    Tool.SuperGetPicThumbnail(imageshowPath, imageminiPath, 70, 120, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    if (appadvertorials.MinImagePath.Substring(appadvertorials.MinImagePath.LastIndexOf('/')) != "/Survey.png" && appadvertorials.MinImagePath.Substring(appadvertorials.MinImagePath.LastIndexOf('/')) != "/ActivityInfo.png")
                    {

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
                    }
                    appadvertorial.MainImagPath = path + imageName;
                    appadvertorial.AppShowImagePath = path + imageshowName;
                    appadvertorial.MinImagePath = path + imageminiName;
                }
                catch
                {
                    throw;
                }
            }
            Result result = base.Edit(appadvertorial);
            if (appadvertorial.stick != appadvertorials.stick)
            {

                int cnt = EditAppAdvertorialStick(appadvertorial.ID, appadvertorial.stick, appadvertorial.AccountMainID, appadvertorial.Sort, appadvertorial.EnumAdvertorialUType);
                if (cnt <= 0)
                {
                    result.HasError = true;
                    result.Error = "修改失败 请稍后再试！";
                }
            }
            return result;
        }


        [Transaction]
        public Result EditAppAdvertorial(AppAdvertorial appadvertorial, int w, int h, int x1, int y1, int tw, int th)
        {
            var appadvertorials = this.Get(appadvertorial.ID);

            if (appadvertorials.MainImagPath != appadvertorial.MainImagPath)
            {
                CommonModel com = new CommonModel();
                var LastName = com.CreateRandom("", 5) + appadvertorial.MainImagPath.GetFileSuffix();


                var path = string.Format(SystemConst.Business.PathBase, appadvertorial.AccountMainID);
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

                    var lsImgPath = appadvertorial.MainImagPath;
                    var lsImaFilePath = HttpContext.Current.Server.MapPath(lsImgPath);

                    Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);


                    Tool.SuperGetPicThumbnailJT(lsImaFilePath, imagePath2, 70, w, h, x1, y1, tw, th, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    Tool.SuperGetPicThumbnail(imagePath2, imageshowPath, 70, 480, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    if (File.Exists(imagePath2))
                    {
                        File.Delete(imagePath2);
                    }

                    //缩略图mini
                    Tool.SuperGetPicThumbnail(imageshowPath, imageminiPath, 70, 120, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

                    if (appadvertorials.MinImagePath.Substring(appadvertorials.MinImagePath.LastIndexOf('/')) != "/Survey.png" && appadvertorials.MinImagePath.Substring(appadvertorials.MinImagePath.LastIndexOf('/')) != "/ActivityInfo.png")
                    {

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

                    }
                    appadvertorial.MainImagPath = path + imageName;
                    appadvertorial.AppShowImagePath = path + imageshowName;
                    appadvertorial.MinImagePath = path + imageminiName;
                }
                catch
                {
                    throw;
                }
            }
            Result result = base.Edit(appadvertorial);
            if (appadvertorial.stick != appadvertorials.stick)
            {

                int cnt = EditAppAdvertorialStick(appadvertorial.ID, appadvertorial.stick, appadvertorial.AccountMainID, appadvertorial.Sort, appadvertorial.EnumAdvertorialUType);
                if (cnt <= 0)
                {
                    result.HasError = true;
                    result.Error = "修改失败 请稍后再试！";
                }
            }
            return result;
        }





        //修改置顶 isok 1 置顶 0 取消
        [Transaction]
        public int EditAppAdvertorialStick(int ID, int isok, int accoutMainID, int Sort, int AdverTorialType)
        {
            string sql = "";
            if (isok == 1)
            {
                var appadverSort = List().Where(a => a.AccountMainID == accoutMainID && a.EnumAdvertorialUType == AdverTorialType).Max(a => a.Sort);
                string sort = (appadverSort + 1).ToString();
                sql = string.Format("update AppAdvertorial set stick = {0}, sort={1} where id ={2}", isok, sort, ID);
            }
            else
            {
                sql = string.Format("update AppAdvertorial set stick = {0}, sort=0 where id ={1}", isok, ID);
                string sql2 = string.Format("update AppAdvertorial set Sort = (Sort-1) where stick = 1 and Sort>{0} and accountMainID={1} and EnumAdvertorialUType={2}", Sort, accoutMainID, AdverTorialType);
                base.SqlExecute(sql2);
            }

            return base.SqlExecute(sql);
        }

        //排序 type 1 向上 0 向下
        public int EditAppAdvertorialSort(int ID, int AccountMainID, int Sort, int type, int AdverTorialType)
        {
            int cnt = List().Where(a => a.AccountMainID == AccountMainID && a.stick == 1).Count();
            if ((Sort == 1 && type == 0) || (Sort == cnt && type == 1))
            {
                return 0;
            }
            else
            {


                string sql = "";
                if (type == 1) //向上
                {
                    sql = string.Format(@"update AppAdvertorial set Sort = (Sort-1) where accountMainID={1}  and EnumAdvertorialUType={3} and Sort=({2}+1) and stick=1 
                                    update AppAdvertorial set Sort = (Sort+1) where ID={0} ", ID, AccountMainID, Sort, AdverTorialType);
                }
                else
                {
                    sql = string.Format(@"update AppAdvertorial set Sort = (Sort+1) where accountMainID={1}  and EnumAdvertorialUType={3} and Sort=({2}-1) and stick=1 
                                    update AppAdvertorial set Sort = (Sort-1) where ID={0} ", ID, AccountMainID, Sort, AdverTorialType);
                }


                return base.SqlExecute(sql);
            }

        }
    }
}
