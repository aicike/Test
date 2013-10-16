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

        int DelAppWaitImg(int AccountMainID);
    }
}
