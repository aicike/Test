using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Business;
using System.IO;
using Common;
using Poco.Enum;
using System.Security.Policy;

namespace Web.Controllers
{
    public class DefaultController : BaseController
    {
        #region 前台

        public ActionResult Index(int? amid)
        {
            if (SystemConst.IsIntegrationWebProject)
            {
                amid.HasValue.NotAuthorizedPage();
                ViewBag.AMID = amid;
            }
            return View();
        }

        #endregion

        public ActionResult AppView(int id)
        {
            var libraryModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            var entity = libraryModel.Get(id);
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "图文信息", "", WebTitleRemark);
            ViewBag.Title = webTitle;

            return View(entity);
        }

        public ActionResult Calculator()
        {
            return View();
        }

        #region 找回密码

        [HttpGet]

        public ActionResult FindPwd(string code)
        {
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            Result result = userLoginInfoModel.FindPwd_CheckCode(code);
            ViewBag.Result = result;
            ViewBag.Code = code;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "找回密码", "", WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }

        [HttpPost]
        public ActionResult FindPwd(string txtNewPwd, string code)
        {
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            Result result = userLoginInfoModel.FindPwd_ChangePwd(code, txtNewPwd);
            if (result.HasError)
            {
                false.NotAuthorizedPage(result.Error);
            }
            ViewBag.Ok = "true";
            return View();
        }

        public ActionResult ContactUs()
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "联系我们", WebTitleRemark, "");
            ViewBag.Title = webTitle;
            return View();
        }

        /// <summary>
        /// web显示软文
        /// </summary>
        /// <param name="id"></param>
        /// <param name="imtimely_userid">用户端ID</param>
        /// <param name="imtimely_accountid">销售端ID</param>
        /// <returns></returns>
        public ActionResult Advertorial(int id)
        {
            var AdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var advertorial = AdvertorialModel.Get(id);

            return View(advertorial);


        }



        /// <summary>
        /// 显示软文
        /// </summary>
        /// <param name="id"></param>
        /// <param name="imtimely_userid">用户ID</param>
        /// <returns></returns>
        public ActionResult News(string id_token, int? imtimely_userid)
        {
            var id = id_token.TokenDecrypt();
            var AdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var advertorial = AdvertorialModel.Get(id);
            if (advertorial.EnumAdverTorialType == (int)EnumAdverTorialType.url)
            {
                string url = advertorial.ContentURL;

                //用户端ID
                if (imtimely_userid.HasValue)
                {

                    if (url.Contains('?'))
                    {
                        url = url.Replace("?", "?imtimely_userid=" + imtimely_userid + "&imtimely_Apptype=" + advertorial.EnumAdvertorialUType + "&");
                    }
                    else
                    {
                        url = url + "?imtimely_userid=" + imtimely_userid + "&imtimely_Apptype=" + advertorial.EnumAdvertorialUType;
                    }
                }

                //imtimely_userid  用户ID
                //imtimely_Apptype 用户端类型 0用户端 1销售端 EnumAdvertorialUType

                return Content("<script>window.location.href='" + url + "' </script>");
            }
            else
            {

                //用户端软文附加下载地址
                ViewBag.Utype = advertorial.EnumAdvertorialUType;
                if (advertorial.EnumAdvertorialUType == (int)EnumAdvertorialUType.UserEnd)
                {
                    var AccountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
                    var AccountModel = AccountMainModel.Get(advertorial.AccountMainID);
                    ViewBag.AMID = advertorial.AccountMainID;
                    ViewBag.AMName = AccountModel.Name;
                    if (!string.IsNullOrEmpty(AccountModel.AndroidDownloadPath))
                    {
                        ViewBag.AndroidURL = "http://" + SystemConst.WebUrl + Url.Content(AccountModel.AndroidDownloadPath ?? "");
                    }
                    if (!string.IsNullOrEmpty(AccountModel.IOSDownloadPath))
                    {
                        ViewBag.IOSURL = AccountModel.IOSDownloadPath;
                    }

                }
                return View(advertorial);
            }
        }




        #endregion

        public ActionResult AppQrCode(int AMID)
        {
            ViewBag.AMID = AMID;
            return View();
        }

        public ActionResult QrCode(int AMID)
        {
            ViewBag.AMID = AMID;
            return View();
        }

        /// <summary>
        /// 二维码用户端
        /// </summary>
        /// <param name="AMID"></param>
        public void CreateQrCode(int AMID)
        {
            QrCodeModel model = new QrCodeModel();
            string url = "http://" + SystemConst.WebUrl + "/Default/QrCodeSkip?AMID=" + AMID;
            MemoryStream ms = model.Get_Android_DownloadUrl(url);
            if (null != ms)
            {
                Response.BinaryWrite(ms.ToArray());
            }
        }


        /// <summary>
        /// 二维码销售端
        /// </summary>
        /// <param name="AMID"></param>
        public void CreateQrCodeAccount(int AMID)
        {
            QrCodeModel model = new QrCodeModel();
            string url = "http://" + SystemConst.WebUrl + "/Default/QrCodeSkipAccount?AMID=" + AMID;
            MemoryStream ms = model.Get_Android_DownloadUrl(url);
            if (null != ms)
            {
                Response.BinaryWrite(ms.ToArray());
            }
        }

        /// <summary>
        /// 二维码用户端界面
        /// </summary>
        /// <param name="AMID"></param>
        public ActionResult QrCodeSkip(int AMID)
        {
            var AccountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var AccountModel = AccountMainModel.Get(AMID);
            if (AccountModel.AndroidDownloadPath != null)
                AccountModel.AndroidDownloadPath = "http://" + SystemConst.WebUrl + Url.Content(AccountModel.AndroidDownloadPath ?? "");
            return View(AccountModel);
        }

        /// <summary>
        /// 二维码销售端界面
        /// </summary>
        /// <param name="AMID"></param>
        public ActionResult QrCodeSkipAccount(int AMID)
        {
            var AccountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var AccountModel = AccountMainModel.Get(AMID);
            if (AccountModel.AndroidSellDownloadPath != null)
                AccountModel.AndroidSellDownloadPath = "http://" + SystemConst.WebUrl + Url.Content(AccountModel.AndroidSellDownloadPath ?? "");
            return View(AccountModel);
        }




        /// <summary>
        /// 会员二维码
        /// </summary>
        /// <param name="AMID"></param>
        public void GetVIPQrCode(string CardNum)
        {
            string number = DESEncrypt.Encrypt(CardNum);

            QrCodeModel model = new QrCodeModel();

            MemoryStream ms = model.Get_Android_DownloadUrl(number);
            if (null != ms)
            {
                Response.BinaryWrite(ms.ToArray());
            }
        }

        //------------------jquery mobile界面-------------------------------------------------

        /// <summary>
        /// 问题反馈
        /// </summary>
        /// <param name="Type">0用户端，1销售端</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Feedback(int Type, int AccountMainID, int? isok)
        {

            ViewBag.type = Type;
            ViewBag.accountmainid = AccountMainID;
            ViewBag.isok = 0;
            if (isok.HasValue)
            {
                ViewBag.isok = 1;
            }
            return View();
        }

        /// <summary>
        /// 提交问题反馈
        /// </summary>
        [HttpPost]
        public ActionResult AddFeedback()
        {
            try
            {
                Poco.Feedback pb = new Poco.Feedback();

                string Type = Request.Form["Type"];
                if (Type == "0")
                {
                    pb.client = "用户端";
                }
                else
                {
                    pb.client = "销售端";
                }
                pb.AccountMainID = int.Parse(Request.Form["AMID"]);
                pb.CreateDate = DateTime.Now;
                pb.Content = Request.Form["textJY"];
                pb.contact = Request.Form["textDZ"];
                var FeedbackModel = Factory.Get<IFeedbackModel>(SystemConst.IOC_Model.FeedbackModel);
                FeedbackModel.Add(pb);

                return RedirectToAction("Feedback", "Default", new { Type = Type, AccountMainID = pb.AccountMainID, isok = 1 });

            }
            catch (Exception ex)
            {
                return RedirectToAction("Feedback", "Default", new { Type = 0, AccountMainID = 0, isok = 1 });
            }
        }


        /// <summary>
        /// 调查问卷
        /// </summary>
        /// <param name="surveyMainID">调用问卷ID</param>
        /// <param name="UID"></param>
        /// <param name="Utype"> 0用户端，1销售端</param>
        /// <param name="isok">1 提交成功 2提交失败</param>
        /// <param name="isok"></param>
        /// <returns></returns>
        public ActionResult Questionnaire(int surveyMainID, int? imtimely_userid, int? imtimely_Apptype, int? isok)
        {
            if (isok.HasValue)
            {
                // 1 提交成功 2提交失败
                ViewBag.isok = isok;
            }
            if (imtimely_userid.HasValue)
            {
                ViewBag.UID = imtimely_userid;
            }
            if (imtimely_Apptype.HasValue)
            {
                // 0用户端，1销售端
                ViewBag.Utype = imtimely_Apptype;
            }
            ViewBag.smid = surveyMainID;
            //主表
            var MainModel = Factory.Get<ISurveyMainModel>(SystemConst.IOC_Model.SurveyMainModel);
            var main = MainModel.Get(surveyMainID);



            return View(main);
        }

        /// <summary>
        /// 提交调查问卷
        /// </summary>
        /// <param name="surveyMainID"></param>
        /// <param name="Utype"> 0用户端，1销售端</param>
        /// <param name="isok">1 提交成功 2提交失败</param>
        /// <param name="Answer">json</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddQuestionnaire(int surveyMainID, int? UID, int? Utype, string Answer)
        {
            List<SurveyAnswer> SA = null;
            SA = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SurveyAnswer>>(Answer);

            var AnswerModel = Factory.Get<ISurveyAnswerModel>(SystemConst.IOC_Model.SurveyAnswerModel);
            var result = AnswerModel.InsertAnswer(SA, UID, Utype);
            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }
            return RedirectToAction("Questionnaire", "Default", new { surveyMainID = surveyMainID, isok = 1 });
        }


        /// <summary>
        /// 活动展示界面
        /// </summary>
        /// <param name="ActivityID"></param>
        /// <param name="imtimely_userid"></param>
        /// <param name="imtimely_Apptype"></param>
        /// <param name="?"></param>
        /// <param name="isok"></param>
        /// <returns></returns>
        public ActionResult ActivityInfo(int ActivityID, int? imtimely_userid, int? imtimely_Apptype, int? isok)
        {
            ViewBag.ActivityID = ActivityID;
            var ActivityModel = Factory.Get<IActivityInfoModel>(SystemConst.IOC_Model.ActivityInfoModel);
            var Activity = ActivityModel.Get(ActivityID);

            if (isok.HasValue)
            {
                // 1 提交成功 2提交失败
                ViewBag.isok = isok;
            }
            if (imtimely_userid.HasValue)
            {
                ViewBag.UID = imtimely_userid;

                //判断是否报过名
                var IActivityInfoParticipatorModelModel = Factory.Get<IActivityInfoParticipatorModel>(SystemConst.IOC_Model.ActivityInfoParticipatorModel);
                Result result = IActivityInfoParticipatorModelModel.GetUserIsSignUP(imtimely_userid.Value, imtimely_Apptype.Value);
                if (result.HasError)
                {
                    ViewBag.isSignUP = "true";
                }
                else
                {
                    ViewBag.isSignUP = "false";
                }

            }
            if (imtimely_Apptype.HasValue)
            {
                // 0用户端，1销售端
                ViewBag.Utype = imtimely_Apptype;
            }

            //下载APP
            var AccountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var AccountModel = AccountMainModel.Get(Activity.AccountMainID);
            ViewBag.AMID = Activity.AccountMainID;
            ViewBag.AMName = AccountModel.Name;
            if (!string.IsNullOrEmpty(AccountModel.AndroidDownloadPath))
            {
                ViewBag.AndroidURL = "http://" + SystemConst.WebUrl + Url.Content(AccountModel.AndroidDownloadPath ?? "");
            }
            if (!string.IsNullOrEmpty(AccountModel.IOSDownloadPath))
            {
                ViewBag.IOSURL = AccountModel.IOSDownloadPath;
            }


            return View(Activity);
        }

        [HttpPost]
        public ActionResult AddActivityInfo(int ActivityID, int? UID, int? Utype)
        {

            ActivityInfoParticipator aip = new ActivityInfoParticipator();
            aip.ActivityInfoID = ActivityID;
            aip.JoinDateTime = DateTime.Now;
            aip.Name = Request.Form["userName"];
            aip.Phone = Request.Form["userPhone"];
            if (UID.HasValue)
            {
                aip.UserID = UID.Value;
            }
            if (Utype.HasValue)
            {
                aip.EnumAdvertorialUType = Utype.Value;
            }
            var IActivityInfoParticipatorModelModel = Factory.Get<IActivityInfoParticipatorModel>(SystemConst.IOC_Model.ActivityInfoParticipatorModel);
            IActivityInfoParticipatorModelModel.Add(aip);
            return RedirectToAction("ActivityInfo", "Default", new { ActivityID = ActivityID, isok = 1 });
        }

    }
}
