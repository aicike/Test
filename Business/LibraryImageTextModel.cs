﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Injection;
using Interface;
using Injection.Transaction;
using System.Web;
using System.IO;
using System.Drawing;
using Common;

namespace Business
{
    public class LibraryImageTextModel : BaseModel<LibraryImageText>, ILibraryImageTextModel
    {
        public IQueryable<LibraryImageText> GetLibraryList(int accountMainID)
        {
            return List(true).Where(a => a.AccountMainID == accountMainID && a.LibraryImageTextParentID.HasValue == false);
        }

        [Transaction]
        public Result Add(LibraryImageText libraryImageText, System.Web.HttpPostedFileBase coverImagePathFile)
        {
            try
            {
                //保存封面图片
                CommonModel com = new CommonModel();
                var LastName = com.CreateRandom("", 5) + coverImagePathFile.FileName.GetFileSuffix();
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                var imageName = string.Format("{0}_{1}", token, LastName);
                var imagePath = string.Format("{0}{1}", string.Format(SystemConst.Business.PathFileLibrary, libraryImageText.AccountMainID), imageName);
                var savePath = HttpContext.Current.Server.MapPath(imagePath);

  

                int dataLengthToRead = (int)coverImagePathFile.InputStream.Length;//获取下载的文件总大小
                byte[] buffer = new byte[dataLengthToRead];


                int r = coverImagePathFile.InputStream.Read(buffer, 0, dataLengthToRead);//本次实际读取到字节的个数
                Stream tream = new MemoryStream(buffer);
                Image img = Image.FromStream(tream);


                Tool.SuperGetPicThumbnail(img, savePath, 70, 800, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
        


                libraryImageText.ImagePath = imagePath;

                var libraryImgModel = Factory.Get<ILibraryImageModel>(SystemConst.IOC_Model.LibraryImageModel);
                LibraryImage entity = new LibraryImage();
                entity.AccountMainID = libraryImageText.AccountMainID;
                entity.FileName = libraryImageText.Title;
                entity.FilePath = imagePath;
                libraryImgModel.Add(entity);

                return base.Add(libraryImageText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [Transaction]
        public Result Add(LibraryImageText libraryImageText)
        {
            try
            {
                //保存封面图片
                CommonModel com = new CommonModel();
                var LastName = com.CreateRandom("", 5) + libraryImageText.ImagePath.GetFileSuffix();
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                var imageName = string.Format("{0}_{1}", token, LastName);
                var imagePath = string.Format("{0}{1}", string.Format(SystemConst.Business.PathFileLibrary, libraryImageText.AccountMainID), imageName);
                var savePath = HttpContext.Current.Server.MapPath(imagePath);


                var lsImgPath = libraryImageText.ImagePath;
                var lsImaFilePath = HttpContext.Current.Server.MapPath(lsImgPath);


                Tool.SuperGetPicThumbnail(lsImaFilePath, savePath, 70, 800, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);



                libraryImageText.ImagePath = imagePath;

                var libraryImgModel = Factory.Get<ILibraryImageModel>(SystemConst.IOC_Model.LibraryImageModel);
                LibraryImage entity = new LibraryImage();
                entity.AccountMainID = libraryImageText.AccountMainID;
                entity.FileName = libraryImageText.Title;
                entity.FilePath = imagePath;
                libraryImgModel.Add(entity);

                return base.Add(libraryImageText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [Transaction]
        public Result Edit(LibraryImageText libraryImageText, HttpPostedFileBase coverImagePathFile)
        {
            try
            {
                if (coverImagePathFile != null)
                {
                    //删除原封面图片
                    var rawImagePath = HttpContext.Current.Server.MapPath(libraryImageText.ImagePath);
                    if (File.Exists(rawImagePath))
                    {
                        File.Delete(rawImagePath);
                    }

                    //保存封面图片
                    CommonModel com = new CommonModel();
                    var LastName = com.CreateRandom("", 5) + coverImagePathFile.FileName.GetFileSuffix();
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imagePath = string.Format("{0}{1}", string.Format(SystemConst.Business.PathFileLibrary, libraryImageText.AccountMainID), imageName);
                    var savePath = HttpContext.Current.Server.MapPath(imagePath);
   


                    int dataLengthToRead = (int)coverImagePathFile.InputStream.Length;//获取下载的文件总大小
                    byte[] buffer = new byte[dataLengthToRead];


                    int r = coverImagePathFile.InputStream.Read(buffer, 0, dataLengthToRead);//本次实际读取到字节的个数
                    Stream tream = new MemoryStream(buffer);
                    Image img = Image.FromStream(tream);


                    Tool.SuperGetPicThumbnail(img, savePath, 70, 800, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
        


                    libraryImageText.ImagePath = imagePath;
                }
                return base.Edit(libraryImageText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        [Transaction]
        public Result Edit(LibraryImageText libraryImageText )
        {
            try
            {
                var Ylivary = this.Get(libraryImageText.ID);
                if (Ylivary.ImagePath != libraryImageText.ImagePath)
                {
                    //删除原封面图片
                    var rawImagePath = HttpContext.Current.Server.MapPath(Ylivary.ImagePath);
                    if (File.Exists(rawImagePath))
                    {
                        File.Delete(rawImagePath);
                    }

                    //保存封面图片
                    CommonModel com = new CommonModel();
                    var LastName = com.CreateRandom("", 5) + libraryImageText.ImagePath.GetFileSuffix();
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imagePath = string.Format("{0}{1}", string.Format(SystemConst.Business.PathFileLibrary, libraryImageText.AccountMainID), imageName);
                    var savePath = HttpContext.Current.Server.MapPath(imagePath);


                    var lsImgPath = libraryImageText.ImagePath;
                    var lsImaFilePath = HttpContext.Current.Server.MapPath(lsImgPath);


                    Tool.SuperGetPicThumbnail(lsImaFilePath, savePath, 70, 800, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);



                    libraryImageText.ImagePath = imagePath;
                }
                return base.Edit(libraryImageText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public Result Delete(int id, int accountMainID)
        {
            var result = new Result();
            var entity = Get(id);
            if (entity.AccountMainID != accountMainID)
            {
                result.Error = SystemConst.Notice.NotAuthorized;
                return result;
            }

            //删除原封面图片
            var rawImagePath = HttpContext.Current.Server.MapPath(entity.ImagePath);
            if (File.Exists(rawImagePath))
            {
                File.Delete(rawImagePath);
            }
            return base.Delete(id);
        }

        /// <summary>
        /// 多图文添加
        /// </summary>
        [Transaction]
        public Result AddMore(LibraryImageText libraryImageText)
        {
            return base.Add(libraryImageText);
        }

        /// <summary>
        /// 多图文修改
        /// </summary>
        [Transaction]
        public Result EditMore(LibraryImageText libraryImageText, List<LibraryImageText> sublist)
        {
            base.SqlExecute("DELETE LibraryImageText WHERE LibraryImageTextParentID=" + libraryImageText.ID);
            var result = base.Edit(libraryImageText);
            if (result.HasError)
            {
                return result;
            }
            if (sublist != null && sublist.Count > 0)
            {
                try
                {
                    StringBuilder stringBuilderSql = new StringBuilder("INSERT INTO dbo.LibraryImageText( SystemStatus ,Title ,ImagePath ,Summary ,Content ,LibraryImageTextParentID ,AccountMainID)");
                    foreach (var item in sublist)
                    {
                        stringBuilderSql.AppendFormat(" SELECT 0,'{0}','{1}','{2}','{3}',{4},{5} UNION ALL", item.Title, item.ImagePath, item.Summary, item.Content, item.LibraryImageTextParentID, item.AccountMainID);
                    }
                    var sql = stringBuilderSql.ToString();
                    sql = sql.Remove(sql.Length - " UNION ALL".Length);
                    base.SqlExecute(sql);
                }
                catch (Exception ex)
                {
                    result.Error = ex.Message;
                }
            }
            return result;
        }
    }
}
