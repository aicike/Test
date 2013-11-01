using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using System.Web;
using System.IO;
using System.Data.Entity;

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
    }
}
