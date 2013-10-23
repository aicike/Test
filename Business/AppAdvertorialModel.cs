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
        public IQueryable<AppAdvertorial> GetList(int AccountMainID)
        {
            var appadverlist = List().Where(a => a.AccountMainID == AccountMainID).OrderByDescending(a => a.stick).ThenByDescending(a => a.Sort).ThenByDescending(a => a.IssueDate);
            return appadverlist;
        }

        [Transaction]
        public Result AddAppAdvertorial(AppAdvertorial appadvertorial, System.Web.HttpPostedFileBase HousShowImagePathFile, int w, int h, int x1, int y1, int tw, int th)
        {
            Result result = new Result();
            
            try
            {
                var path = string.Format(SystemConst.Business.PathBase, appadvertorial.AccountMainID);
                var accountPath = HttpContext.Current.Server.MapPath(path);
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");

                var imageName = string.Format("{0}_{1}", token, HousShowImagePathFile.FileName);
                var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                var imageminiName = string.Format("{0}_{1}_{2}", token, "mini", HousShowImagePathFile.FileName);
                var imageminiPath = string.Format("{0}\\{1}", accountPath, imageminiName);
                var imageshowName = string.Format("{0}_{1}_{2}", token, "show", HousShowImagePathFile.FileName);
                var imageshowPath = string.Format("{0}\\{1}", accountPath, imageshowName);
                HousShowImagePathFile.SaveAs(imagePath);


                if (w > 0)
                {
                    
                        int ToWidth = w;
                        int ToHeight = h;
                        int ToX1 = x1;
                        int ToY1 = y1;

                        Bitmap sourceBitmap = new Bitmap(imagePath);

                        int YW = sourceBitmap.Width;
                        int YH = sourceBitmap.Height;

                        if (YH != th)
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

                        resultBitmap.Save(imageshowPath, sourceBitmap.RawFormat);
                        resultBitmap.Dispose();
                        sourceBitmap.Dispose();
                        
                   
                  
                }

                //缩略图mini
                if (Tool.Thumbnail(imageshowPath, imageminiPath, 120, 60))
                {
                    //account.HeadImagePath = path + imageThumbnailName;

                }
                appadvertorial.MainImagPath =path + imageName;
                appadvertorial.AppShowImagePath =path + imageshowName;
                appadvertorial.MinImagePath = path + imageminiName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            result = base.Add(appadvertorial);
            if (appadvertorial.stick == 1)
            {
                int cnt = EditAppAdvertorialStick(appadvertorial.ID, appadvertorial.stick, appadvertorial.AccountMainID, appadvertorial.Sort);
                if (cnt <= 0)
                {
                    result.HasError = true;
                    result.Error = "添加失败 请稍后再试！";
                }
            }
            return result;
        }

        [Transaction]
        public Result DelAppAdvertorial(int ID)
        {
            var appadivertorial = base.Get(ID);
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
            if (appadivertorial.stick == 1)
            {
                string sql = string.Format("update AppAdvertorial set Sort=(Sort-1) where AccountMainID = {0} and stick=1 and sort>{1}", appadivertorial.AccountMainID, appadivertorial.Sort);
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
        
                var path = string.Format(SystemConst.Business.PathBase, appadvertorial.AccountMainID);
                var accountPath = HttpContext.Current.Server.MapPath(path);
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");

                var imageName = string.Format("{0}_{1}", token, HousShowImagePathFile.FileName);
                var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                var imageminiName = string.Format("{0}_{1}_{2}", token, "mini", HousShowImagePathFile.FileName);
                var imageminiPath = string.Format("{0}\\{1}", accountPath, imageminiName);
                var imageshowName = string.Format("{0}_{1}_{2}", token, "show", HousShowImagePathFile.FileName);
                var imageshowPath = string.Format("{0}\\{1}", accountPath, imageshowName);
                HousShowImagePathFile.SaveAs(imagePath);
                try
                {
                    if (w > 0)
                    {

                        int ToWidth = w;
                        int ToHeight = h;
                        int ToX1 = x1;
                        int ToY1 = y1;

                        Bitmap sourceBitmap = new Bitmap(imagePath);

                        int YW = sourceBitmap.Width;
                        int YH = sourceBitmap.Height;

                        if (YH != th)
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

                        resultBitmap.Save(imageshowPath, sourceBitmap.RawFormat);
                        resultBitmap.Dispose();
                        sourceBitmap.Dispose();



                    }



                    //缩略图mini
                    if (Tool.Thumbnail(imageshowPath, imageminiPath, 120, 60))
                    {
                        //account.HeadImagePath = path + imageThumbnailName;

                    }
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

                    appadvertorial.MainImagPath = path + imageName;
                    appadvertorial.AppShowImagePath = path + imageshowName;
                    appadvertorial.MinImagePath = path + imageminiName;
                }
                catch
                {
                    throw;
                }
            }
            Result result= base.Edit(appadvertorial);
            if (appadvertorial.stick != appadvertorials.stick)
            {

                int cnt = EditAppAdvertorialStick(appadvertorial.ID, appadvertorial.stick, appadvertorial.AccountMainID, appadvertorial.Sort);
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
        public int EditAppAdvertorialStick(int ID, int isok, int accoutMainID, int Sort)
        {
            string sql = "";
            if (isok == 1)
            {
                var appadverSort = List().Where(a => a.AccountMainID == accoutMainID).Max(a => a.Sort);
                string sort = (appadverSort + 1).ToString();
                sql = string.Format("update AppAdvertorial set stick = {0}, sort={1} where id ={2}", isok, sort, ID);
            }
            else
            {
                sql = string.Format("update AppAdvertorial set stick = {0}, sort=0 where id ={1}", isok, ID);
                string sql2 = string.Format("update AppAdvertorial set Sort = (Sort-1) where stick = 1 and Sort>{0} and accountMainID={1}", Sort, accoutMainID);
                base.SqlExecute(sql2);
            }

            return base.SqlExecute(sql);
        }

        //排序 type 1 向上 0 向下
        public int EditAppAdvertorialSort(int ID, int AccountMainID, int Sort, int type)
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
                    sql = string.Format(@"update AppAdvertorial set Sort = (Sort-1) where accountMainID={1} and Sort=({2}+1) and stick=1 
                                    update AppAdvertorial set Sort = (Sort+1) where ID={0} ", ID, AccountMainID, Sort);
                }
                else
                {
                    sql = string.Format(@"update AppAdvertorial set Sort = (Sort+1) where accountMainID={1} and Sort=({2}-1) and stick=1 
                                    update AppAdvertorial set Sort = (Sort-1) where ID={0} ", ID, AccountMainID, Sort);
                }


                return base.SqlExecute(sql);
            }

        }
    }
}
