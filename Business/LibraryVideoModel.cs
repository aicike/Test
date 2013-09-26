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

namespace Business
{
    public class LibraryVideoModel : BaseModel<LibraryVideo>, ILibraryVideoModel
    {
        public IQueryable<LibraryVideo> GetLibraryList(int accountMainID)
        {
            return List().Where(a => a.AccountMainID == accountMainID);
        }

        public Result Upload(LibraryVideo entity, System.Web.HttpPostedFileBase video)
        {
            var token = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fileName = string.Format("{0}{1}", token, video.FileName.Substring(video.FileName.LastIndexOf(".")));
            var filePath = string.Format("~/File/{0}/FileLibrary/{1}", entity.AccountMainID, fileName);
            var pathIO = HttpContext.Current.Server.MapPath(filePath);
            video.SaveAs(pathIO);

            try
            {
                //获取时间
                CommonModel cm = new CommonModel();
                string FileTime = cm.GetFileTimeLength(pathIO);
                entity.FileLength = FileTime;
            }
            catch { }



            entity.FileName = video.FileName.Substring(0, video.FileName.LastIndexOf('.'));
            entity.FilePath = filePath;
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
