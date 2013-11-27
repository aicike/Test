using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web;

namespace Interface
{
    public interface IAppWaitImgModel : IBaseModel<AppWaitImg>
    {
        AppWaitImg getAppWaitImg(int AccountMainID);

        Result UpAppWaitImg(AppWaitImg appWaitImg, HttpPostedFileBase HousShowImagePathFile);

        Result UpAppWaitImg(AppWaitImg appWaitImg, HttpPostedFileBase HousShowImagePathFile, int w, int h, int x1, int y1, int tw, int th);

        Result UpAppWaitImg(AppWaitImg appWaitImg, int w, int h, int x1, int y1, int tw, int th);

        int DelAppWaitImg(int AccountMainID);
    }
}
