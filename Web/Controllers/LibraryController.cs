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
        [AllowCheckPermissions(false)]
        public ActionResult Manage()
        {
            var libraryImageModel = Factory.Get<ILibraryImageModel>(SystemConst.IOC_Model.LibraryImageModel);
            var libraryImageTextModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            var libraryVideoModel = Factory.Get<ILibraryVideoModel>(SystemConst.IOC_Model.LibraryVideoModel);
            var libraryVoiceModel = Factory.Get<ILibraryVoiceModel>(SystemConst.IOC_Model.LibraryVoiceModel);
            //图片
            var imageList = libraryImageModel.GetLibraryList(LoginAccount.CurrentAccountMainID).ToList();
            ViewBag.ImageList = imageList;
            //语音
            var voiceList = libraryVoiceModel.GetLibraryList(LoginAccount.CurrentAccountMainID).ToList();
            ViewBag.VoiceList = voiceList;
            //视频
            var videoList = libraryVideoModel.GetLibraryList(LoginAccount.CurrentAccountMainID).ToList();
            ViewBag.VideoList = videoList;

            return View();
        }

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
