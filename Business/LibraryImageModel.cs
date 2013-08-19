﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Injection;
using Interface;
using System.Web;
using Injection.Transaction;
using System.IO;
using Common;

namespace Business
{
    public class LibraryImageModel : BaseModel<LibraryImage>, ILibraryImageModel
    {
        public IQueryable<LibraryImage> GetLibraryList(int accountMainID)
        {
            return List().Where(a => a.AccountMainID == accountMainID);
        }

        [Transaction]
        public Result Upload(LibraryImage entity, System.Web.HttpPostedFileBase image)
        {
            var token = DateTime.Now.ToString("yyyyMMddHHmmss");

            var fileName = string.Format("{0}_{1}", token, image.FileName);
            var filePath = string.Format("~/File/{0}/FileLibrary/{1}", entity.AccountMainID, fileName);
            var fileThumbnailPath = string.Format("~/File/{0}/FileLibrary/_{1}", entity.AccountMainID, fileName);
            var pathIO = HttpContext.Current.Server.MapPath(filePath);
            var thumbnailPathIO = HttpContext.Current.Server.MapPath(fileThumbnailPath);
            image.SaveAs(pathIO);

            //缩略图
            if (Tool.Thumbnail(pathIO, thumbnailPathIO, Convert.ToInt32(SystemConst.Business.ThumbnailImage_Width)))
            {
                //删除原头像
                if (File.Exists(pathIO))
                {
                    File.Delete(pathIO);
                }
                entity.FilePath = fileThumbnailPath;
            }
            else
            {
                entity.FilePath = filePath;
            }
            entity.FileName = image.FileName.Substring(0, image.FileName.LastIndexOf('.'));
            return base.Add(entity);
        }

        [Transaction]
        public new Result Delete(int id)
        {
            var result = new Result();
            //先删除文件
            var entity = Get(id);
            if (entity == null)
            {
                result.Error = SystemConst.Notice.NotAuthorized;
            }
            var pathIO = HttpContext.Current.Server.MapPath(entity.FilePath);
            if (File.Exists(pathIO))
            {
                File.Delete(pathIO);
            }
            return base.Delete(id);
        }

        public Result ReName(int id, string name)
        {
            var result = new Result();
            var entity = Get(id);
            if (entity == null)
            {
                result.Error = SystemConst.Notice.NotAuthorized;
            }
            entity.FileName = name;
            return base.Edit(entity);
        }
    }
}
