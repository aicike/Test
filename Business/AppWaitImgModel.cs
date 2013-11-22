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
                string path2 =  HttpContext.Current.Server.MapPath(await.ImgPath);
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
            HousShowImagePathFile.SaveAs(imagePath);

            appWaitImg.ImgPath = path + imageName;


            if (w > 0)
            {
                //System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(() =>
                //{
                    int ToWidth = w;
                    int ToHeight = h;
                    int ToX1 = x1;
                    int ToY1 = y1;

                    Bitmap sourceBitmap = new Bitmap(imagePath);

                    int YW = sourceBitmap.Width;
                    int YH = sourceBitmap.Height;


                    if (YH != th )
                    {
                        double ratio = double.Parse(YH.ToString()) / double.Parse(th.ToString());
                        //ratio = Math.Round(ratio, 2);
                        ToWidth = (int)(ToWidth * ratio);
                        ToHeight = (int)(ToHeight * ratio);
                        ToX1 = (int)(ToX1 * ratio);
                        ToY1 = (int)(ToY1 * ratio);
                    }
                    Bitmap resultBitmap = new Bitmap(ToWidth, ToHeight);

                    using (Graphics g = Graphics.FromImage(resultBitmap))
                    {
                        Rectangle resultRectangle = new Rectangle(0, 0, ToWidth, ToHeight);
                        Rectangle sourceRectangle = new Rectangle(0 + ToX1, 0 + ToY1, ToWidth, ToHeight);
                        g.DrawImage(sourceBitmap, resultRectangle, sourceRectangle, GraphicsUnit.Pixel);
                    }
                    EncoderParameters ep = new EncoderParameters();

                    resultBitmap.Save(imagePath2, sourceBitmap.RawFormat);
                    resultBitmap.Dispose();
                    sourceBitmap.Dispose();
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    } 
                
                    appWaitImg.ImgPath = path + imageName2;
                    if (Tool.Thumbnail(imagePath2, imagePath3, 640))
                    {
                        if (File.Exists(imagePath2))
                        {
                            File.Delete(imagePath2);
                        }
                        Tool.GetPicThumbnail(imagePath3, imagePath4, 50);

                        appWaitImg.ImgPath = path + imageName4;
                        if (File.Exists(imagePath3))
                        {
                            File.Delete(imagePath3);
                        }
                    }
                //});
                //t.Start();
               
            }
            else
            {
                if (Tool.Thumbnail(imagePath, imagePath3, 640))
                {
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                    

                    Tool.GetPicThumbnail(imagePath3, imagePath4, 50);

                    appWaitImg.ImgPath = path + imageName4;

                    if (File.Exists(imagePath3))
                    {
                        File.Delete(imagePath3);
                    }
                }
                else
                {
                    Tool.GetPicThumbnail(imagePath, imagePath4, 50);

                    appWaitImg.ImgPath = path + imageName4;
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
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
