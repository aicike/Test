using System;
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
using System.Drawing;

namespace Business
{
    public class LibraryImageModel : BaseModel<LibraryImage>, ILibraryImageModel
    {
        public IQueryable<LibraryImage> GetLibraryList(int accountMainID)
        {
            return List(true).Where(a => a.AccountMainID == accountMainID);
        }



        [Transaction]
        public Result Upload(LibraryImage entity, System.Web.HttpPostedFileBase image)
        {
            var token = DateTime.Now.ToString("yyyyMMddHHmmss");
            CommonModel com = new CommonModel();
            var LastName = com.CreateRandom("", 5) + image.FileName.GetFileSuffix();
            var fileName = string.Format("{0}_{1}", token, LastName);
            var filePath = string.Format("~/File/{0}/FileLibrary/{1}", entity.AccountMainID, fileName);
            var fileThumbnailPath = string.Format("~/File/{0}/FileLibrary/_{1}", entity.AccountMainID, fileName);
            var pathIO = HttpContext.Current.Server.MapPath(filePath);
            var thumbnailPathIO = HttpContext.Current.Server.MapPath(fileThumbnailPath);



            int dataLengthToRead = (int)image.InputStream.Length;//获取下载的文件总大小
            byte[] buffer = new byte[dataLengthToRead];


            int r = image.InputStream.Read(buffer, 0, dataLengthToRead);//本次实际读取到字节的个数
            Stream tream = new MemoryStream(buffer);
            Image img = Image.FromStream(tream);


            Tool.SuperGetPicThumbnail(img, pathIO, 70, 800, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
        
            entity.FilePath = filePath;
           
            entity.FileName = image.FileName.Substring(0, image.FileName.LastIndexOf('.'));
            Result result = base.Add(entity);
            result.Entity = entity.FilePath;
            return result;
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
