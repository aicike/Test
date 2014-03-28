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
using System.Resources;
using System.Configuration;
using System.Text;

namespace Web.Controllers
{
    public class DefaultController : BaseController
    {
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
            var AdvertorialOperModel = Factory.Get<IAppAdvertorialOperationModel>(SystemConst.IOC_Model.AppAdvertorialOperationModel);

            //查询数据
            var advertorial = AdvertorialModel.Get(id);
            //添加数据到资讯操作表
            if (imtimely_userid.HasValue)
            {
                AppAdvertorialOperation aao = new AppAdvertorialOperation();
                aao.AppAdvertorialID = id;
                aao.EnumAdvertorialUType = advertorial.EnumAdvertorialUType;
                aao.ForwardCnt = 0;
                aao.ForwardFriendCnt = 0;
                aao.ForwardWeiboCnt = 0;
                aao.ForwardWeiXinCnt = 0;
                aao.UserID = imtimely_userid.Value;
                AdvertorialOperModel.AddOperation(aao);
            }
            if (advertorial.EnumAdverTorialType == (int)EnumAdverTorialType.url)
            {
                string url = advertorial.ContentURL;

                //用户ID
                if (imtimely_userid.HasValue)
                {

                    if (url.Contains('?'))
                    {
                        url = url.Replace("?", "?imtimely_userid=" + imtimely_userid + "&imtimely_Apptype=" + advertorial.EnumAdvertorialUType + "&ADverID=" + id + "&");
                    }
                    else
                    {
                        url = url + "?imtimely_userid=" + imtimely_userid + "&imtimely_Apptype=" + advertorial.EnumAdvertorialUType + "&ADverID=" + id;
                    }
                }
                else
                {
                    url = url.Replace("?", "?imtimely_Apptype=" + advertorial.EnumAdvertorialUType + "&ADverID=" + id + "&");
                }

                //imtimely_userid  用户ID
                //imtimely_Apptype 用户端类型 0用户端 1销售端 EnumAdvertorialUType

                return Content("<script>window.location.href='" + url + "' </script>");
            }
            else
            {

                var AccountserverModel = Factory.Get<IAccountMain_ServiceModel>(SystemConst.IOC_Model.AccountMain_ServiceModel);
                var hasAPP = AccountserverModel.CheckService(EnumService.House_Service, advertorial.AccountMainID);
                ViewBag.HasAPP = hasAPP;
                if (hasAPP)
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
                }
                //更改浏览次数
                AdvertorialModel.BrowseCntADD(id);
                //更改浏览次数(报表)
                var BrowseModel = Factory.Get<IAppAdvertorialBrowseModel>(SystemConst.IOC_Model.AppAdvertorialBrowseModel);
                BrowseModel.AddOrUpdBrowse(id, EnumBrowseAdvertorialType.Information);
                return View(advertorial);
            }
        }


        #endregion
        /// <summary>
        /// web端 二维码扫描界面 （用户端与销售端）
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public ActionResult AppQrCode(int AMID)
        {
            ViewBag.AMID = AMID;
            return View();
        }
        /// <summary>
        /// 手机用户端扫描界面
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public ActionResult QrCode(int AMID)
        {
            ViewBag.AMID = AMID;
            return View();
        }

        /// <summary>
        /// 创建二维码用户端
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
        /// 创建二维码销售端
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
        /// 二维码用户端下载界面
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
        /// 二维码销售端下载界面
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
        public ActionResult Questionnaire(string surveyMainID_token, int ADverID, int? imtimely_userid, int? imtimely_Apptype, int? isok)
        {
            int surveyMainID = surveyMainID_token.TokenDecrypt();
            if (isok.HasValue)
            {
                // 1 提交成功 2提交失败
                ViewBag.isok = isok;
            }
            if (imtimely_userid.HasValue)
            {
                ViewBag.UID = imtimely_userid;
                //获取用户信息
                if (imtimely_Apptype.HasValue)
                {
                    // 0用户端，1销售端
                    if (imtimely_Apptype.Value == 0)
                    {
                        var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
                        var user = userModel.Get(imtimely_userid.Value);
                        ViewBag.UName = user.UserLoginInfo.Name;
                        ViewBag.UPhone = user.UserLoginInfo.Phone;
                        ViewBag.UEmail = user.UserLoginInfo.Email;
                    }
                    else if (imtimely_Apptype.Value == 1)
                    {
                        var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
                        var account = accountModel.Get(imtimely_userid.Value);
                        ViewBag.UName = account.Name;
                        ViewBag.UPhone = account.Phone;
                        ViewBag.UEmail = account.Email;
                    }
                }


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
            ViewBag.ADverID = ADverID;

            var AdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            //更改浏览次数
            AdvertorialModel.BrowseCntADD(ADverID);
            //更改浏览次数(报表)
            var BrowseModel = Factory.Get<IAppAdvertorialBrowseModel>(SystemConst.IOC_Model.AppAdvertorialBrowseModel);
            BrowseModel.AddOrUpdBrowse(surveyMainID, EnumBrowseAdvertorialType.SurveyMain);
            return View(main);
        }

        /// <summary>
        /// 提交调查问卷
        /// </summary>
        /// <param name="surveyMainID"></param>
        /// <param name="Utype"> 0用户端，1销售端</param>
        /// <param name="isok">1 提交成功 2提交失败</param>
        /// <param name="Answer">json</param>
        /// <param name="IsRegistered">是否为记名调查</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddQuestionnaire(int surveyMainID, int ADverID, int? UID, int? Utype, string Answer, bool IsRegistered)
        {
            List<SurveyAnswer> SA = null;
            SA = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SurveyAnswer>>(Answer);
            int? SAUID = null;
            if (IsRegistered)
            {
                var AnswerUserModel = Factory.Get<ISurveyAnswerUserModel>(SystemConst.IOC_Model.SurveyAnswerUserModel);
                SurveyAnswerUser sau = new SurveyAnswerUser();
                sau.UserComPany = Request.Form["userCompany"];
                sau.UserEmail = Request.Form["userEmail"];
                sau.UserName = Request.Form["userName"];
                sau.UserPhone = Request.Form["userPhone"];
                sau.UserPosition = Request.Form["userPosition"];
                sau.QQ_Weixin = Request.Form["userQQ"];
                AnswerUserModel.Add(sau);
                SAUID = sau.ID;

            }
            var AnswerModel = Factory.Get<ISurveyAnswerModel>(SystemConst.IOC_Model.SurveyAnswerModel);
            var result = AnswerModel.InsertAnswer(SA, UID, Utype, SAUID);
            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }
            return RedirectToAction("Questionnaire", "Default", new { surveyMainID_token = surveyMainID.TokenEncrypt(true), ADverID = ADverID, isok = 1 });
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
        public ActionResult ActivityInfo(string ActivityID_token, int ADverID, int? imtimely_userid, int? imtimely_Apptype, int? isok)
        {
            int ActivityID = ActivityID_token.TokenDecrypt();
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
                Result result = IActivityInfoParticipatorModelModel.GetUserIsSignUP(imtimely_userid.Value, imtimely_Apptype.Value, ActivityID);
                if (result.HasError)
                {
                    ViewBag.isSignUP = "true";
                }
                else
                {
                    ViewBag.isSignUP = "false";
                    //获取用户信息
                    if (imtimely_Apptype.HasValue)
                    {
                        // 0用户端，1销售端
                        if (imtimely_Apptype.Value == 0)
                        {
                            var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
                            var user = userModel.Get(imtimely_userid.Value);
                            ViewBag.UName = user.UserLoginInfo.Name;
                            ViewBag.UPhone = user.UserLoginInfo.Phone;
                            ViewBag.UEmail = user.UserLoginInfo.Email;
                        }
                        else if (imtimely_Apptype.Value == 1)
                        {
                            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
                            var account = accountModel.Get(imtimely_userid.Value);
                            ViewBag.UName = account.Name;
                            ViewBag.UPhone = account.Phone;
                            ViewBag.UEmail = account.Email;
                        }
                    }
                }
            }
            if (imtimely_Apptype.HasValue)
            {
                // 0用户端，1销售端
                ViewBag.Utype = imtimely_Apptype;
            }
            //判断报名是否结束

            if (DateTime.Now > Activity.EnrollEndDate)
            {
                ViewBag.IsJS = "true";
            }
            else
            {
                ViewBag.IsJS = "false";
            }
            //判断名额是否已满
            if (Activity.ActivityInfoParticipators.Count() >= Activity.MaxCount)
            {
                ViewBag.IsMaxCnt = "true";
            }
            else
            {
                ViewBag.IsMaxCnt = "false";
            }

            var AccountserverModel = Factory.Get<IAccountMain_ServiceModel>(SystemConst.IOC_Model.AccountMain_ServiceModel);
            var hasAPP = AccountserverModel.CheckService(EnumService.House_Service, Activity.AccountMainID);
            ViewBag.HasAPP = hasAPP;
            if (hasAPP)
            {
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
            }
            ViewBag.ADverID = ADverID;
            var AdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            //更改浏览次数
            AdvertorialModel.BrowseCntADD(ADverID);
            //更改浏览次数(报表)
            var BrowseModel = Factory.Get<IAppAdvertorialBrowseModel>(SystemConst.IOC_Model.AppAdvertorialBrowseModel);
            BrowseModel.AddOrUpdBrowse(ActivityID, EnumBrowseAdvertorialType.ActivityInfo);
            return View(Activity);
        }
        /// <summary>
        /// 校验是否报名
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="phone"></param>
        /// <returns>1 电话存在 2 邮箱存在</returns>
        [HttpPost]
        public string CheckISBM(int AID, string phone, string Email)
        {
            var IActivityInfoParticipatorModelModel = Factory.Get<IActivityInfoParticipatorModel>(SystemConst.IOC_Model.ActivityInfoParticipatorModel);
            var result = IActivityInfoParticipatorModelModel.GetUserIsSignUP2(phone, AID);
            if (result.HasError)
            {
                return "1";
            }
            var result2 = IActivityInfoParticipatorModelModel.GetUserIsSignUP3(Email, AID);
            if (result2.HasError)
            {
                return "2";
            }
            return "False";
        }

        [HttpPost]
        public ActionResult AddActivityInfo(int ActivityID, int ADverID, int? UID, int? Utype)
        {
            ActivityInfoParticipator aip = new ActivityInfoParticipator();
            aip.ActivityInfoID = ActivityID;
            aip.JoinDateTime = DateTime.Now;
            aip.Name = Request.Form["userName"];
            aip.Phone = Request.Form["userPhone"];
            aip.Email = Request.Form["userEmail"];
            aip.Company = "";
            aip.Position = "";
            if (UID.HasValue)
            {
                aip.UserID = UID.Value;
            }
            if (Utype.HasValue)
            {
                aip.EnumAdvertorialUType = Utype.Value;
            }
            var IActivityInfoParticipatorModelModel = Factory.Get<IActivityInfoParticipatorModel>(SystemConst.IOC_Model.ActivityInfoParticipatorModel);
            var result = IActivityInfoParticipatorModelModel.Add(aip);
            if (result.HasError == false)
            {
                try
                {
                    System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(() =>
                    {
                        var activitymodel = Factory.Get<IActivityInfoModel>(SystemConst.IOC_Model.ActivityInfoModel);
                        var activity = activitymodel.Get(ActivityID);
                        string webUrl = ConfigurationManager.AppSettings["WebUrl"].ToString();
                        string content = string.Format("您好，{0}先生/女士。您参与的活动\"{1}\" 于明日{2}开始。详细地址：{3}/default/ActivityInfo?ActivityID={4}  【{5}】"
                                                            , aip.Name, activity.Title, activity.ActivityStratDate.Substring(activity.ActivityStratDate.LastIndexOf(' ')), webUrl, activity.ID, activity.AccountMain.Name);
                        SMS_Model sms = new SMS_Model();
                        sms.SendSMS(aip.Phone, content);
                    });
                    t.Start();
                }
                catch { }
            }
            return RedirectToAction("ActivityInfo", "Default", new { ActivityID_token = ActivityID.TokenEncrypt(true), ADverID = ADverID, isok = 1 });
        }

        /// <summary>
        /// 大转盘抽奖
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="id">资讯ID，活动ID，调查ID</param>
        /// <returns></returns>
        public ActionResult LotteryDish(int type, int id)
        {
            EnumBrowseAdvertorialType advertorialType = (EnumBrowseAdvertorialType)type;
            int lottery_dishID=0;
            switch (advertorialType)
            {
                case EnumBrowseAdvertorialType.Information:
                    break;
                case EnumBrowseAdvertorialType.ActivityInfo:
                    var activityModel = Factory.Get<IActivityInfoModel>(SystemConst.IOC_Model.ActivityInfoModel);
                    var activity= activityModel.Get(id);
                    lottery_dishID = activity.Lottery_dishID.Value;
                    break;
                case EnumBrowseAdvertorialType.SurveyMain:
                    break;
            }
            var model = Factory.Get<ILottery_dishModel>(SystemConst.IOC_Model.Lottery_dishModel);
            var entity = model.Get(lottery_dishID);
            var list = entity.Lottery_dish_details.ToList();
            //名称
            StringBuilder names = new StringBuilder();
            foreach (var item in list)
            {
                names.AppendFormat("\"{0}\",", item.Name);
            }
            var name_value = names.Remove(names.Length - 1, 1).ToString();
            ViewBag.Name = name_value;
            //颜色
            string[] color = new string[] { "#6D7278", "#B55610", "#349933", "#CC3333", "#2C3144", "#B12E3D", "#FFE44C", "#41547F", "#ff0000", "#FFE44C", "#41547F", "#ff0000" };
            StringBuilder colors = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                colors.AppendFormat("\"{0}\",", color[i]);
            }
            var color_value = colors.Remove(colors.Length - 1, 1).ToString();
            ViewBag.Color = color_value;
            //图片
            StringBuilder img = new StringBuilder();
            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item.Image) == false)
                {
                    img.AppendFormat("\"{0}\",", Url.Content(item.Image));
                }
                else
                {
                    img.AppendFormat("\"{0}\",", "");
                }
            }
            var img_value = img.Remove(img.Length - 1, 1).ToString();
            ViewBag.Img = img_value;

            #region 中奖率情况

            ////中奖情况计算
            Random r = new Random();
            Lottery_dish_detail detail = model.ControllerRandomExtract(list, r, 1)[0];
            int index = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID == detail.ID)
                {
                    index = i;
                    break;
                }
            }
            //中奖奖品
            ViewBag.WinningIndex = index + 1;
            ViewBag.Winning = detail;

            //保存中奖情况
            #endregion
            return View(entity);
        }


        //---------------活动签到-----------------------------------------
        public ActionResult ActivitySignIn(int ActivityID, int? isok)
        {
            ViewBag.ActivityID = ActivityID;
            var ActivityModel = Factory.Get<IActivityInfoModel>(SystemConst.IOC_Model.ActivityInfoModel);
            var Activity = ActivityModel.Get(ActivityID);
            if (isok.HasValue)
            {
                // 1 提交成功 2提交失败
                ViewBag.isok = isok;
            }

            return View(Activity);
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="ActivityID"></param>
        /// <param name="isok"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ActivitySignIn(int ActivityID)
        {
            ActivityInfoSignIn ais = new ActivityInfoSignIn();
            ais.ActivityInfoID = ActivityID;
            ais.JoinDateTime = DateTime.Now;
            ais.Name = Request.Form["userName"];
            ais.Phone = Request.Form["userPhone"];
            ais.Email = Request.Form["userEmail"];
            ais.Company = Request.Form["userCompany"];
            ais.Position = Request.Form["userPosition"];

            var ActivitySignInModel = Factory.Get<IActivityInfoSignInModel>(SystemConst.IOC_Model.ActivityInfoSignInModel);
            ActivitySignInModel.Add(ais);

            return RedirectToAction("ActivitySignIn", "Default", new { ActivityID_token = ActivityID.TokenEncrypt(true), isok = 1 });
        }

        public ActionResult ceshi()
        {
            int count;
            String[] userLang = Request.UserLanguages;

            for (count = 0; count < userLang.Length; count++)
            {
                Response.Write("User Language " + count + ": " + userLang[count] + "<br>");
            }
            return View();
        }



    }
}
