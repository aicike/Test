using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Injection;
using Interface;
using Injection.Transaction;
using System.Web;

namespace Business
{
    public class LibraryImageTextModel : BaseModel<LibraryImageText>, ILibraryImageTextModel
    {
        public IQueryable<LibraryImageText> GetLibraryList(int accountMainID)
        {
            return List().Where(a => a.AccountMainID == accountMainID);
        }

        [Transaction]
        public Result Add(LibraryImageText libraryImageText, System.Web.HttpPostedFileBase coverImagePathFile)
        {
            try
            {
                //保存封面图片
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                var imageName = string.Format("{0}_{1}", token, coverImagePathFile.FileName);
                var imagePath = string.Format("{0}\\{1}", string.Format(SystemConst.Business.PathBase, libraryImageText.AccountMainID), imageName);
                var savePath = HttpContext.Current.Server.MapPath(imagePath);
                coverImagePathFile.SaveAs(savePath);
                libraryImageText.ImagePath = imagePath;
                return base.Add(libraryImageText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
