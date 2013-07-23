using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Controllers;
using Poco.Enum;

namespace Web.Controllers
{
    public class LibraryController : ManageAccountController
    {
        protected List<LibraryType> LibraryType()
        {
            var accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var accountMainLibraryInfo = accountMainModel.GetAccountMainLibraryInfo(LoginAccount.CurrentAccountMainID);
            List<LibraryType> type = new List<Poco.LibraryType>();
            type.Add(new LibraryType() { ID = 0, Name = "文字", Token = "LibraryText", Count = accountMainLibraryInfo.LibraryTextCount });
            type.Add(new LibraryType() { ID = 1, Name = "图片", Token = "LibraryImage", Count = accountMainLibraryInfo.LibraryImageCount });
            type.Add(new LibraryType() { ID = 2, Name = "视频", Token = "LibraryVideo", Count = accountMainLibraryInfo.LibraryVideoCount });
            type.Add(new LibraryType() { ID = 3, Name = "语音", Token = "LibraryVoice", Count = accountMainLibraryInfo.LibraryVoiceCount });
            type.Add(new LibraryType() { ID = 4, Name = "图文", Token = "LibraryImageText", Count = accountMainLibraryInfo.LibraryImageTextCount });
            return type;
        }
    }
}
