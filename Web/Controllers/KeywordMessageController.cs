using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interface;
using Poco;
using Injection;
using Controllers;
using System.Text;

namespace Web.Controllers
{
    public class KeywordMessageController : ManageAccountController
    {
        public ActionResult Index()
        {
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            var list = autoMessage_KeywordModel.List(LoginAccount.CurrentAccountMainID).Where(a => a.ParentAutoMessage_KeywordID.HasValue == false).OrderBy(a => a.ID).ToList();

            StringBuilder sb = new StringBuilder();
            sb.Append("<ul id='browser'>");
            foreach (var item in list)
            {
                sb.AppendFormat("<li><div class='keyLiTitle'>({0})&nbsp;&nbsp;<label>{1}</label>&nbsp;&nbsp;<a class='btnAddSub' fullRuleNo='{0}' ruleNo='{4}' parentID='{2}'>[添加子项]</a>&nbsp;<a class='btnEdit' keyID='{2}' fullRuleNo='{0}' ruleNo='{4}' parentID='{2}'>[修改]</a>&nbsp;<a onclick='return deleteItem({2})'>[删除]</a></div>{3}</li>",
                    item.FullRuleNo, item.RuleName.Show(10, "..."), item.ID, GetHtml(item.AutoMessage_KeywordsKeyword), item.RuleNo);
            }
            sb.Append("</ul>");
            ViewBag.Tree = sb.ToString();


            var accountMainHousesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var projectList = accountMainHousesModel.GetList(LoginAccount.CurrentAccountMainID).OrderBy(a => a.ID).ToList();
            var selectListProjects = new SelectList(projectList, "ID", "HName");
            List<SelectListItem> newProjectList = new List<SelectListItem>();
            newProjectList.Add(new SelectListItem { Text = "请选择", Value = "0", Selected = true });
            newProjectList.AddRange(selectListProjects);
            ViewData["Project"] = newProjectList;
            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "设置-关键词自动回复", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(list);
        }

        private string GetHtml(ICollection<AutoMessage_Keyword> entitys)
        {
            if (entitys.Count == 0)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>");
            foreach (var item in entitys)
            {
                sb.AppendFormat("<li><div class='keyLiTitle'>({0})&nbsp;&nbsp;<label>{1}</label>&nbsp;&nbsp;<a class='btnAddSub' fullRuleNo='{0}' ruleNo='{4}' parentID='{2}'>[添加子项]</a>&nbsp;<a class='btnEdit' keyID='{2}' fullRuleNo='{0}' ruleNo='{4}' parentID='{2}'>[修改]</a>&nbsp;<a onclick='return deleteItem({2})'>[删除]</a></div>{3}</li>",
                    item.FullRuleNo, item.RuleName.Show(10, "..."), item.ID, GetHtml(item.AutoMessage_KeywordsKeyword), item.RuleNo);
            }
            sb.Append("</ul>");
            return sb.ToString();
        }

        [HttpPost]
        [ValidateInput(false)]
        public string Add(string ruleName, int ruleNo, string fullRuleNo, int? parentID, string keys, string messageFileIDs, string messageImageTextIDs, int projectID, bool isFirstAutoMsg, string files)
        {
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            AutoMessage_Keyword msg = new AutoMessage_Keyword();
            msg.RuleName = ruleName;
            msg.RuleNo = ruleNo;
            msg.FullRuleNo = fullRuleNo;
            msg.ParentAutoMessage_KeywordID = parentID;
            msg.AccountMainHousesID = projectID;
            msg.IsFistAutoMessage = isFirstAutoMsg;
            List<Files> fileList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Files>>(files);
            var result = autoMessage_KeywordModel.Add(msg, keys, messageFileIDs, messageImageTextIDs, LoginAccount.CurrentAccountMainID, fileList);
            if (result.HasError)
            {
                return AlertJS_NoTag(new Dialog(result.Error));
            }
            return "window.location.href='" + Url.Action("Index", "KeywordMessage", new { HostName = LoginAccount.HostName }) + "'";
        }

        [HttpPost]
        [ValidateInput(false)]
        public string Edit(int keyID, string ruleName, string keys, string messageFileIDs, string messageImageTextIDs, int projectID, bool isFirstAutoMsg, string files)
        {
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            List<Files> fileList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Files>>(files);
            var result = autoMessage_KeywordModel.Edit(keyID, ruleName, projectID, keys, messageFileIDs, messageImageTextIDs, LoginAccount.CurrentAccountMainID, isFirstAutoMsg, fileList);
            if (result.HasError)
            {
                return AlertJS_NoTag(new Dialog(result.Error));
            }
            return "window.location.href='" + Url.Action("Index", "KeywordMessage", new { HostName = LoginAccount.HostName }) + "'";
        }

        [AllowCheckPermissions(false)]
        public string GetRuleNoString(string fullRuleNo)
        {
            string ruleNo = null;
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            if (string.IsNullOrEmpty(fullRuleNo) == false)
            {
                ruleNo = autoMessage_KeywordModel.GetFullRuleNo(LoginAccount.CurrentAccountMainID, fullRuleNo);
            }
            else
            {
                ruleNo = autoMessage_KeywordModel.GetRuleNo(LoginAccount.CurrentAccountMainID, null).ToString();
            }
            return ruleNo;
        }

        [AllowCheckPermissions(false)]
        public ActionResult Get(int id)
        {
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            var entity = autoMessage_KeywordModel.GetByID_AccountMainID(id, LoginAccount.ID);
            List<Files> files = new List<Files>();
            if (entity.KeywordAutoMessages.Count > 0)
            {
                var libraryImageModel = Factory.Get<ILibraryImageModel>(SystemConst.IOC_Model.LibraryImageModel);
                var libraryImageTextModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
                var libraryVideoModel = Factory.Get<ILibraryVideoModel>(SystemConst.IOC_Model.LibraryVideoModel);
                var libraryVoiceModel = Factory.Get<ILibraryVoiceModel>(SystemConst.IOC_Model.LibraryVoiceModel);
                foreach (var item in entity.KeywordAutoMessages)
                {
                    Files file = new Files();
                    file.id = item.MessageID;
                    switch (item.EnumMessageType.Token)
                    {
                        case "Text":
                            file.type = "LibraryText";
                            file.content = item.TextReply;
                            break;
                        case "Image":
                            file.type = "LibraryImage";
                            var img = libraryImageModel.Get(item.MessageID);
                            if (img == null) {
                                continue;
                            }
                            file.url = Url.Content(img.FilePath);
                            file.fileTitle = img.FileName;
                            break;
                        case "Video":
                            file.type = "LibraryVideo";
                            var video = libraryVideoModel.Get(item.MessageID);
                            if (video == null)
                            {
                                continue;
                            }
                            file.url = Url.Content(video.FilePath);
                            file.fileTitle = video.FileName;
                            break;
                        case "Voice":
                            file.type = "LibraryVoice";
                            var voice = libraryVoiceModel.Get(item.MessageID);
                            if (voice == null)
                            {
                                continue;
                            }
                            file.url = Url.Content(voice.FilePath);
                            file.fileTitle = voice.FileName;
                            break;
                        case "ImageText":
                            file.type = "LibraryImageText";
                            var imageText = libraryImageTextModel.Get(item.MessageID);
                            file.url = Url.Content(imageText.ImagePath);
                            file.fileTitle = imageText.Title;
                            break;
                    }
                    files.Add(file);
                }
            }

            var json = new
            {
                ID = entity.ID,
                RuleName = entity.RuleName,
                RuleNo = entity.RuleNo,
                FullRuleNo = entity.FullRuleNo,
                ProjectID = entity.AccountMainHousesID,
                IsFistAutoMessage = entity.IsFistAutoMessage,
                Keywords = entity.Keywords.Select(a => a.Token).ToList().ConvertToString(","),
                KeywordAutoMessages = files.ObjectToJson("Files")
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            var result = autoMessage_KeywordModel.Delete(id, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "KeywordMessage", new { HostName = LoginAccount.HostName }) + "'");
        }
    }
}
