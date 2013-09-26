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
using WMPLib;
namespace Business
{
    public class LibraryVoiceModel : BaseModel<LibraryVoice>, ILibraryVoiceModel
    {
        public Result Upload(LibraryVoice entity, System.Web.HttpPostedFileBase voice)
        {
            var token = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fileName = string.Format("{0}{1}", token, voice.FileName.Substring(voice.FileName.LastIndexOf(".")));
            var filePath = string.Format("~/File/{0}/FileLibrary/{1}", entity.AccountMainID, fileName);
            var pathIO = HttpContext.Current.Server.MapPath(filePath);
            voice.SaveAs(pathIO);

            string mp3path =pathIO;
            try
            {
                
                CommonModel cm = new CommonModel();
                string hz = filePath.Substring(filePath.LastIndexOf('.'),(filePath.Length-filePath.LastIndexOf('.')));
                if (hz.ToUpper() != ".MP3")
                {
                    //转换
                    mp3path = cm.CreateMp3(pathIO,"mp3");
                }
               
                //获取时间
                string FileTime = cm.GetFileTimeLength(mp3path);
                entity.FileMp3Path = filePath.Substring(0, filePath.LastIndexOf('.')) + ".mp3";
                entity.FileLength = FileTime;
            }
            catch { }


            entity.FileName = voice.FileName.Substring(0, voice.FileName.LastIndexOf('.'));
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
            var mp3pagthIO = HttpContext.Current.Server.MapPath(entity.FileMp3Path);
            if (File.Exists(mp3pagthIO))
            {
                File.Delete(mp3pagthIO);
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

        public IQueryable<LibraryVoice> GetLibraryList(int accountMainID)
        {
            return List().Where(a => a.AccountMainID == accountMainID);
        }
    }
}
