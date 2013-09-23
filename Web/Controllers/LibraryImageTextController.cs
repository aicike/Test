using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using System.Collections;
using System.IO;

namespace Web.Controllers
{
    public class LibraryImageTextController : LibraryController
    {
        public ActionResult Index(int? id)
        {
            var libraryModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            var list = libraryModel.GetLibraryList(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 15);
            ViewBag.LibraryType = LibraryType();
            ViewBag.HostName = LoginAccount.HostName;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "素材管理-图文素材", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(list);
        }

        public ActionResult Add()
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "素材管理-添加单图文", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(LibraryImageText libraryImageText, HttpPostedFileBase CoverImagePathFile)
        {
            libraryImageText.AccountMainID = LoginAccount.CurrentAccountMainID;
            var libraryModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            var result = libraryModel.Add(libraryImageText, CoverImagePathFile);
            if (result.HasError)
            {
                throw new ApplicationException(result.Error);
            }
            return RedirectToAction("Index", "LibraryImageText", new { HostName = LoginAccount.HostName });
        }

        [HttpPost]
        [AllowCheckPermissions(false)]
        public string Upload()
        {
            if (Request.Files.Count > 0)
            {
                var libraryModel = Factory.Get<ILibraryImageModel>(SystemConst.IOC_Model.LibraryImageModel);
                LibraryImage entity = new LibraryImage();
                entity.AccountMainID = LoginAccount.CurrentAccountMainID;
                var result = libraryModel.Upload(entity, Request.Files[0]);
                if (result.HasError)
                {
                    //return AlertJS_NoTag(new Dialog(result.Error));
                }
                else
                {
                    return Url.Content(result.Entity.ToString());
                }
            }
            else
            {
                //return AlertJS_NoTag(new Dialog("未能成功上传文件，请重试。"));
            }
            return "false";
        }

        [HttpGet]
        public ActionResult MoreAdd()
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "素材管理-添加多图文", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public string MoreAdd(string json)
        {
            var it = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageText>(json);
            LibraryImageText lit = new LibraryImageText();
            lit.Title = it.title;
            lit.ImagePath = it.image;
            lit.Summary = it.summary;
            lit.Content = it.body;
            lit.AccountMainID = LoginAccount.CurrentAccountMainID;
            if (it.sub != null)
            {
                lit.LibraryImageTexts = new List<LibraryImageText>();
                foreach (var item in it.sub)
                {
                    lit.LibraryImageTexts.Add(new LibraryImageText()
                    {
                        Title = item.title,
                        Summary = "",
                        ImagePath = item.image,
                        Content = item.body,
                        AccountMainID = LoginAccount.CurrentAccountMainID
                    });
                }
            }
            var libraryModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            var result = libraryModel.AddMore(lit);
            if (result.HasError)
            {
                return AlertJS_NoTag(new Dialog(result.Error));
            }
            else
            {
                return AlertJS_NoTag(new Dialog("保存成功。", Url.Action("Index", "LibraryImageText")));
            }
        }

        [HttpGet]
        [AllowCheckPermissions(false)]
        public ActionResult MoreEdit(int id)
        {
            var libraryModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            var entity = libraryModel.Get(id);
            ImageText it = new ImageText();
            it.title = entity.Title;
            it.image = @Url.Content(entity.ImagePath);
            it.summary = entity.Summary;
            it.body = entity.Content;
            if (entity.LibraryImageTexts != null)
            {
                it.sub = new List<ImageText>();
                foreach (var item in entity.LibraryImageTexts)
                {
                    it.sub.Add(new ImageText()
                    {
                        title = item.Title,
                        image = @Url.Content(item.ImagePath),
                        summary = item.Summary,
                        body = item.Content,
                    });
                }
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(it);
            ViewBag.Json = json;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "素材管理-修改多图文", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(entity);
        }

        [HttpPost]
        [AllowCheckPermissions(false)]
        [ValidateInput(false)]
        public string MoreEdit(int id, string json)
        {
            var libraryModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            var it = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageText>(json);
            LibraryImageText lit = libraryModel.Get(id);
            lit.Title = it.title;
            lit.ImagePath = it.image;
            lit.Summary = it.summary;
            lit.Content = it.body;
            lit.AccountMainID = LoginAccount.CurrentAccountMainID;
            List<LibraryImageText> sublist = new List<LibraryImageText>();
            if (it.sub != null)
            {
                foreach (var item in it.sub)
                {
                    sublist.Add(new LibraryImageText()
                    {
                        LibraryImageTextParentID = id,
                        Title = item.title,
                        Summary = "",
                        ImagePath = item.image,
                        Content = item.body,
                        AccountMainID = LoginAccount.CurrentAccountMainID
                    });
                }
            }
            var result = libraryModel.EditMore(lit, sublist);
            if (result.HasError)
            {
                return AlertJS_NoTag(new Dialog(result.Error));
            }
            else
            {
                return AlertJS_NoTag(new Dialog("保存成功。", Url.Action("Index", "LibraryImageText")));
            }
        }

        public ActionResult Edit(int id, bool more = false)
        {
            if (more == true)
            {
                return RedirectToAction("MoreEdit", "LibraryImageText", new { id = id, HostName = LoginAccount.HostName });
            }
            var libraryModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            var entity = libraryModel.Get(id);
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "素材管理-修改单素材", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(entity);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(LibraryImageText libraryImageText, HttpPostedFileBase CoverImagePathFile)
        {
            var libraryModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            var result = libraryModel.Edit(libraryImageText, CoverImagePathFile);
            if (result.HasError)
            {
                throw new ApplicationException(result.Error);
            }
            return RedirectToAction("Index", "LibraryImageText", new { HostName = LoginAccount.HostName });
        }


        public ActionResult Delete(int id)
        {
            var libraryModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            var result = libraryModel.Delete(id, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "LibraryImageText", new { HostName = LoginAccount.HostName }) + "'");
        }

        [Serializable]
        private class ImageText
        {
            public string title;
            public string image;
            public string summary;
            public string body;
            public List<ImageText> sub;
        }
    }
}
