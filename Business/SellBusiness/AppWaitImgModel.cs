using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using System.Web;
using System.IO;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using Common;

namespace Business
{
    public class AppWaitImgModel : BaseModel<AppWaitImg>, IAppWaitImgModel
    {
        public AppWaitImg getAppWaitImg(int AccountMainID)
        {
            return List().Where(a => a.AccountMainID == AccountMainID).AsNoTracking().FirstOrDefault();
        }

        public Result UpAppWaitImg(AppWaitImg appWaitImg, System.Web.HttpPostedFileBase HousShowImagePathFile)
        {
            AppWaitImg await = getAppWaitImg(appWaitImg.AccountMainID);
            Result result = new Result();
            var path = string.Format(SystemConst.Business.PathBase, appWaitImg.AccountMainID);
            var accountPath = HttpContext.Current.Server.MapPath(path);
            var token = DateTime.Now.ToString("yyyyMMddHHmmss");
            var imageName = string.Format("{0}_{1}", token, HousShowImagePathFile.FileName.Substring(HousShowImagePathFile.FileName.LastIndexOf('.')));
            var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
            HousShowImagePathFile.SaveAs(imagePath);

            appWaitImg.ImgPath = path + imageName;
            if (await != null) //修改
            {
                string path2 = HttpContext.Current.Server.MapPath(await.ImgPath);
                if (File.Exists(path2))
                {
                    File.Delete(path2);
                }
                appWaitImg.ID = await.ID;
                result = base.Edit(appWaitImg);
            }
            else //添加
            {
                result = base.Add(appWaitImg);

            }

            return result;
        }

        public int DelAppWaitImg(int AccountMainID)
        {
            AppWaitImg await = getAppWaitImg(AccountMainID);
            if (await != null)
            {
                string path2 = HttpContext.Current.Server.MapPath(await.ImgPath);
                if (File.Exists(path2))
                {
                    File.Delete(path2);
                }
                string sql = "delete AppWaitImg where AccountMainID = " + AccountMainID;

                return base.SqlExecute(sql);
            }
            return 1;
        }


        public Result UpAppWaitImg(AppWaitImg appWaitImg, HttpPostedFileBase HousShowImagePathFile, int w, int h, int x1, int y1, int tw, int th)
        {
            AppWaitImg await = getAppWaitImg(appWaitImg.AccountMainID);
            Result result = new Result();
            var path = string.Format(SystemConst.Business.PathBase, appWaitImg.AccountMainID);
            var accountPath = HttpContext.Current.Server.MapPath(path);
            var token = DateTime.Now.ToString("yyyyMMddHHmmss");
            var imageName = string.Format("{0}_{1}", token, HousShowImagePathFile.FileName.Substring(HousShowImagePathFile.FileName.LastIndexOf('.')));
            var imageName2 = string.Format("{0}j_{1}", token, HousShowImagePathFile.FileName.Substring(HousShowImagePathFile.FileName.LastIndexOf('.')));
            var imageName3 = string.Format("{0}y_{1}", token, HousShowImagePathFile.FileName.Substring(HousShowImagePathFile.FileName.LastIndexOf('.')));
            var imageName4 = string.Format("{0}z_{1}", token, HousShowImagePathFile.FileName.Substring(HousShowImagePathFile.FileName.LastIndexOf('.')));
            var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
            var imagePath2 = string.Format("{0}\\{1}", accountPath, imageName2);
            var imagePath3 = string.Format("{0}\\{1}", accountPath, imageName3);
            var imagePath4 = string.Format("{0}\\{1}", accountPath, imageName4);



            int dataLengthToRead = (int)HousShowImagePathFile.InputStream.Length;//获取下载的文件总大小
            byte[] buffer = new byte[dataLengthToRead];


            int r = HousShowImagePathFile.InputStream.Read(buffer, 0, dataLengthToRead);//本次实际读取到字节的个数
            Stream tream = new MemoryStream(buffer);
            Image img = Image.FromStream(tream);

            //Tool.SuperGetPicThumbnail(img, imagePath,70,960,0,System.Drawing.Drawing2D.SmoothingMode.HighQuality,System.Drawing.Drawing2D.CompositingQuality.Default,System.Drawing.Drawing2D.InterpolationMode.High);
            if (w > 0)
            {
                Tool.SuperGetPicThumbnailJT(img, imagePath, 70, w, h, x1, y1, tw, th, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);

                Tool.SuperGetPicThumbnail(imagePath, imagePath2, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                appWaitImg.ImgPath = path + imageName2;
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
            else
            {
                Tool.SuperGetPicThumbnail(img, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                appWaitImg.ImgPath = path + imageName;
            }
          

           





            if (await != null) //修改
            {
                string path2 = HttpContext.Current.Server.MapPath(await.ImgPath);
                if (File.Exists(path2))
                {
                    File.Delete(path2);
                }

                appWaitImg.ID = await.ID;
                result = base.Edit(appWaitImg);
            }
            else //添加
            {
                result = base.Add(appWaitImg);

            }

            return result;
        }



        public Result UpAppWaitImg(AppWaitImg appWaitImg, int w, int h, int x1, int y1, int tw, int th)
        {
            AppWaitImg await = getAppWaitImg(appWaitImg.AccountMainID);
            Result result = new Result();
            var path = string.Format(SystemConst.Business.PathBase, appWaitImg.AccountMainID);
            var accountPath = HttpContext.Current.Server.MapPath(path);
            var token = DateTime.Now.ToString("yyyyMMddHHmmss");
            CommonModel com = new CommonModel();
            var LastName = com.CreateRandom("", 5) + appWaitImg.ImgPath.GetFileSuffix();

            var imageName = string.Format("{0}_{1}", token, LastName);
            var imageName2 = string.Format("{0}j_{1}", token, LastName);
            var imageName3 = string.Format("{0}y_{1}", token, LastName);
            var imageName4 = string.Format("{0}z_{1}", token, LastName);
            var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
            var imagePath2 = string.Format("{0}\\{1}", accountPath, imageName2);
            var imagePath3 = string.Format("{0}\\{1}", accountPath, imageName3);
            var imagePath4 = string.Format("{0}\\{1}", accountPath, imageName4);

            var lsImgPath = appWaitImg.ImgPath;
            var lsImaFilePath = HttpContext.Current.Server.MapPath(lsImgPath);

            //Tool.SuperGetPicThumbnail(img, imagePath,70,960,0,System.Drawing.Drawing2D.SmoothingMode.HighQuality,System.Drawing.Drawing2D.CompositingQuality.Default,System.Drawing.Drawing2D.InterpolationMode.High);
            if (w > 0)
            {
                Tool.SuperGetPicThumbnailJT(lsImaFilePath, imagePath, 55, w, h, x1, y1, tw, th, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);

                Tool.SuperGetPicThumbnail(imagePath, imagePath2, 55, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                appWaitImg.ImgPath = path + imageName2;
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
            else
            {
                Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 55, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                appWaitImg.ImgPath = path + imageName;
            }




            if (await != null) //修改
            {
                string path2 = HttpContext.Current.Server.MapPath(await.ImgPath);
                if (File.Exists(path2))
                {
                    File.Delete(path2);
                }

                appWaitImg.ID = await.ID;
                result = base.Edit(appWaitImg);
            }
            else //添加
            {
                result = base.Add(appWaitImg);

            }

            return result;
        }
    }
}
