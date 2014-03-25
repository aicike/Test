using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;
using Controllers;
using Interface;
using Injection;
using Poco.Enum;

namespace Web.Controllers
{
    public class SurveyMainController : ManageAccountController
    {

        #region -------------调查问卷-------------------------------------------------------------------------------------------

        //
        // GET: /SurveyMain/
        /// <summary>
        /// 查询调查列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult IndexMain(int? id, int? tishi)
        {
            var MainModel = Factory.Get<ISurveyMainModel>(SystemConst.IOC_Model.SurveyMainModel);
            var list = MainModel.SelList(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 15);


            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "调查问卷", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            //提示 1 不能添加 2不能删除
            int ts = 0;
            if (tishi.HasValue)
            {
                ts = tishi.Value;
            }
            ViewBag.TS = ts;

            return View(list);
        }

        /// <summary>
        /// 创建调查
        /// </summary>
        /// <returns></returns>
        public ActionResult AddMain()
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "调查问卷 - 创建调查问卷", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }

        /// <summary>
        /// 创建调查
        /// </summary>
        /// <param name="surveymain"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddMain(SurveyMain surveymain)
        {
            surveymain.CreateDate = DateTime.Now;
            surveymain.AccountID = LoginAccount.ID;
            surveymain.AccountMainID = LoginAccount.CurrentAccountMainID;
            var MainModel = Factory.Get<ISurveyMainModel>(SystemConst.IOC_Model.SurveyMainModel);
            var result = MainModel.Add(surveymain);
            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }

            return JavaScript("window.location.href='" + Url.Action("IndexMain", "SurveyMain", new { HostName = LoginAccount.HostName }) + "'");

        }

        /// <summary>
        /// 修改调查
        /// </summary>
        /// <returns></returns>
        public ActionResult EditMain(int id)
        {

            //验证是否能添加
            var AnswerModel = Factory.Get<ISurveyAnswerModel>(SystemConst.IOC_Model.SurveyAnswerModel);
            if (AnswerModel.GetListBySMID(id, LoginAccount.CurrentAccountMainID).Count() > 0)
            {
                return RedirectToAction("IndexMain", "SurveyMain", new { HostName = LoginAccount.HostName, tishi = 2 });
                //return JavaScript("window.location.href='" + Url.Action("IndexMain", "SurveyMain", new { HostName = LoginAccount.HostName, tishi = 2 }) + "'");
            }


            var MainModel = Factory.Get<ISurveyMainModel>(SystemConst.IOC_Model.SurveyMainModel);
            var SMain = MainModel.GetSurveyMainByID(id, LoginAccount.CurrentAccountMainID);


            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "调查问卷 - 修改调查问卷", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            return View(SMain);
        }

        /// <summary>
        /// 修改调查
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditMain(SurveyMain surveymain)
        {
            surveymain.CreateDate = DateTime.Now;
            surveymain.AccountMainID = LoginAccount.CurrentAccountMainID;
            surveymain.AccountID = LoginAccount.ID;
            var MainModel = Factory.Get<ISurveyMainModel>(SystemConst.IOC_Model.SurveyMainModel);
            var result = MainModel.Edit(surveymain);
            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }
            return JavaScript("window.location.href='" + Url.Action("IndexMain", "SurveyMain", new { HostName = LoginAccount.HostName }) + "'");
        }

        /// <summary>
        /// 删除调查
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteMain(int id)
        {
            var MainModel = Factory.Get<ISurveyMainModel>(SystemConst.IOC_Model.SurveyMainModel);
            var result = MainModel.DelSurveyMain(id, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }

            return JavaScript("window.location.href='" + Url.Action("IndexMain", "SurveyMain", new { HostName = LoginAccount.HostName }) + "'");

        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <returns></returns>
        [AllowCheckPermissions(false)]
        public ActionResult SetMainStatus(int id, int Status)
        {
            var MainModel = Factory.Get<ISurveyMainModel>(SystemConst.IOC_Model.SurveyMainModel);
            var result = MainModel.SetMainStatus(id, LoginAccount.CurrentAccountMainID, Status);
            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }
            return JavaScript("window.location.href='" + Url.Action("IndexMain", "SurveyMain", new { HostName = LoginAccount.HostName }) + "'");

        }



        #endregion


        #region -------------问题录入-------------------------------------------------------------------------------------------


        /// <summary>
        /// 查询调查列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult IndexTrouble(int? id, int SMID, int? tishi)
        {
            var TroubleModel = Factory.Get<ISurveyTroubleModel>(SystemConst.IOC_Model.SurveyTroubleModel);
            var list = TroubleModel.GetListByMainID(SMID, LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 20);

            var MainModel = Factory.Get<ISurveyMainModel>(SystemConst.IOC_Model.SurveyMainModel);
            var main = MainModel.GetSurveyMainByID(SMID, LoginAccount.CurrentAccountMainID);
            ViewBag.ShowTitle = main.SurveyTitle;
            ViewBag.SMID = SMID;


            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "调查问卷 - 调查问卷问题列表", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;



            ViewBag.RawUrl = Url.Action("IndexMain", "SurveyMain");

            //提示 1 不能添加 2不能删除
            int ts = 0;
            if (tishi.HasValue)
            {
                ts = tishi.Value;
            }
            ViewBag.TS = ts;

            return View(list);
        }


        /// <summary>
        /// 录入调查问题
        /// </summary>
        /// <returns></returns>
        public ActionResult AddTrouble(int SMID)
        {
            //验证是否能添加
            var AnswerModel = Factory.Get<ISurveyAnswerModel>(SystemConst.IOC_Model.SurveyAnswerModel);
            if (AnswerModel.GetListBySMID(SMID, LoginAccount.CurrentAccountMainID).Count() > 0)
            {
                return RedirectToAction("IndexTrouble", "SurveyMain", new { HostName = LoginAccount.HostName, SMID = SMID, tishi = 1});
            }

            //基本信息
            var MainModel = Factory.Get<ISurveyMainModel>(SystemConst.IOC_Model.SurveyMainModel);
            var main = MainModel.GetSurveyMainByID(SMID, LoginAccount.CurrentAccountMainID);
            ViewBag.ShowTitle = main.SurveyTitle; //标题
            ViewBag.MainType = main.EnumSurveyMainType; //调查类型 1普通 2评分

            //提号
            var TroubleModel = Factory.Get<ISurveyTroubleModel>(SystemConst.IOC_Model.SurveyTroubleModel);
            var list = TroubleModel.GetListByMainID(SMID, LoginAccount.CurrentAccountMainID);
            ViewBag.Tnumber = list.Count() + 1;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "调查问卷 - 调查问卷问题录入", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            ViewBag.SMID = SMID;

            ViewBag.RawUrl = Url.Action("IndexTrouble", "SurveyMain", new { SMID = SMID });
            return View();
        }

        /// <summary>
        /// 录入调查问题
        /// </summary>
        /// <param name="surveymain"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddTrouble(SurveyTrouble surveytrouble, string Options)
        {
            if (surveytrouble.EnumTroubleType != (int)EnumTroubleType.Txt)
            {
                List<SurveyOption> Bos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SurveyOption>>(Options);
                if (Bos.Count() <= 0)
                {
                    return JavaScript(AlertJS_NoTag(new Dialog("请输入选项")));
                }
                surveytrouble.SurveyOption = new List<SurveyOption>();
                foreach (SurveyOption so in Bos)
                {
                    surveytrouble.SurveyOption.Add(new SurveyOption()
                    {
                        Fraction = so.Fraction,
                        Option = so.Option
                    });
                }


            }
            var TroubleModel = Factory.Get<ISurveyTroubleModel>(SystemConst.IOC_Model.SurveyTroubleModel);
            var result = TroubleModel.Add(surveytrouble);
            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }
            return JavaScript("window.location.href='" + Url.Action("IndexTrouble", "SurveyMain", new { HostName = LoginAccount.HostName, SMID = surveytrouble.SurveyMainID }) + "'");
        }

        /// <summary>
        /// 修改调查问题
        /// </summary>
        /// <returns></returns>
        public ActionResult EditTrouble(int id, int SMID)
        {
            //验证是否能修改
            var AnswerModel = Factory.Get<ISurveyAnswerModel>(SystemConst.IOC_Model.SurveyAnswerModel);
            if (AnswerModel.GetListBySMID(SMID, LoginAccount.CurrentAccountMainID).Count() > 0)
            {
                return RedirectToAction("IndexTrouble", "SurveyMain", new { HostName = LoginAccount.HostName, SMID = SMID, tishi = 2 });
                //return JavaScript("window.location.href='" + Url.Action("IndexTrouble", "SurveyMain", new { HostName = LoginAccount.HostName, SMID = SMID, tishi = 2 }) + "'");
            }

            //基本信息
            var MainModel = Factory.Get<ISurveyMainModel>(SystemConst.IOC_Model.SurveyMainModel);
            var main = MainModel.GetSurveyMainByID(SMID, LoginAccount.CurrentAccountMainID);
            ViewBag.ShowTitle = main.SurveyTitle; //标题
            ViewBag.MainType = main.EnumSurveyMainType; //调查类型 1普通 2评分

            var TroubleModel = Factory.Get<ISurveyTroubleModel>(SystemConst.IOC_Model.SurveyTroubleModel);
            var list = TroubleModel.GetListByMainID(SMID, LoginAccount.CurrentAccountMainID);
            //总题数
            ViewBag.Tcount = list.Count();
            //问题信息
            var Trouble = TroubleModel.GetInfoByID(id, LoginAccount.CurrentAccountMainID);
            //选项信息
            var OptionModel = Factory.Get<ISurveyOptionModel>(SystemConst.IOC_Model.SurveyOptionModel);
            var Option = OptionModel.GetListBYTroubleID(id, LoginAccount.CurrentAccountMainID);
            ViewBag.Options = Option;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "调查问卷 - 调查问卷问题修改", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;


            ViewBag.RawUrl = Url.Action("IndexTrouble", "SurveyMain", new { SMID = SMID });
            return View(Trouble);
        }

        /// <summary>
        /// 修改调查问题
        /// </summary>
        /// <param name="surveymain"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditTrouble(SurveyTrouble surveytrouble, string Options)
        {
            List<SurveyOption> Bos = null;
            if (surveytrouble.EnumTroubleType != (int)EnumTroubleType.Txt)
            {
                Bos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SurveyOption>>(Options);
                if (Bos.Count() <= 0)
                {
                    return JavaScript(AlertJS_NoTag(new Dialog("请输入选项")));
                }
            }
            var TroubleModel = Factory.Get<ISurveyTroubleModel>(SystemConst.IOC_Model.SurveyTroubleModel);
            var result = TroubleModel.EditTroubleOrOption(surveytrouble, Bos, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }

            return JavaScript("window.location.href='" + Url.Action("IndexTrouble", "SurveyMain", new { HostName = LoginAccount.HostName, SMID = surveytrouble.SurveyMainID }) + "'");
        }


        /// <summary>
        /// 删除调查问题
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteTrouble(int Tid, int SMID)
        {
            var TroubleModel = Factory.Get<ISurveyTroubleModel>(SystemConst.IOC_Model.SurveyTroubleModel);
            var result = TroubleModel.DelSurveyTrouble(Tid, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }
            return JavaScript("window.location.href='" + Url.Action("IndexTrouble", "SurveyMain", new { HostName = LoginAccount.HostName, SMID = SMID }) + "'");
        }

        /// <summary>
        /// 显示问题选项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowCheckPermissions(false)]
        public ActionResult ShowOption(int id, int SMID)
        {
            //基本信息
            var MainModel = Factory.Get<ISurveyMainModel>(SystemConst.IOC_Model.SurveyMainModel);
            var main = MainModel.GetSurveyMainByID(SMID, LoginAccount.CurrentAccountMainID);
            ViewBag.MainType = main.EnumSurveyMainType; //调查类型 1普通 2评分

            //题目信息
            var TroubleModel = Factory.Get<ISurveyTroubleModel>(SystemConst.IOC_Model.SurveyTroubleModel);
            var Trouble = TroubleModel.GetInfoByID(id, LoginAccount.CurrentAccountMainID);
            ViewBag.Trouble = Trouble;
            //选项信息
            var OptionModel = Factory.Get<ISurveyOptionModel>(SystemConst.IOC_Model.SurveyOptionModel);
            var Option = OptionModel.GetListBYTroubleID(id, LoginAccount.CurrentAccountMainID);
            return View(Option);
        }


        #endregion


        #region -------------报表 其他列表-------------------------------------------------------------------------------------------


        /// <summary>
        /// 查询调查列表统计
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowCheckPermissions(false)]
        public ActionResult TroubleStatistics(int id)
        {
            //主表
            var MainModel = Factory.Get<ISurveyMainModel>(SystemConst.IOC_Model.SurveyMainModel);
            var main = MainModel.GetSurveyMainByID(id, LoginAccount.CurrentAccountMainID);
            //选项表
            var OptionModel = Factory.Get<ISurveyOptionModel>(SystemConst.IOC_Model.SurveyOptionModel);
            var _b_score = OptionModel.GetSurveyFraction(main.ID);
            //满分
            ViewBag.FullMarks = _b_score.FullMarks;
            //平均分
            ViewBag.Average = _b_score.Average;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "调查问卷 - 调查问卷统计", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            return View(main);
        }

        /// <summary>
        /// 回答用户列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="SMID"></param>
        /// <returns></returns>
        [AllowCheckPermissions(false)]
        public ActionResult SurveyUserList(int? id, int SMID)
        {
            var AnswerModel = Factory.Get<ISurveyAnswerModel>(SystemConst.IOC_Model.SurveyAnswerModel);
            var Answer = AnswerModel.GetAnswerUserList(SMID, LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 25);
            ViewBag.SMID = SMID;
            return View(Answer);
        }

        /// <summary>
        /// 用户回答详细页
        /// </summary>
        /// <param name="SMID"></param>
        /// <param name="Ucode"></param>
        /// <returns></returns>
        [AllowCheckPermissions(false)]
        public ActionResult AnswerInfo(int SMID, string Ucode)
        {
            //主表
            var MainModel = Factory.Get<ISurveyMainModel>(SystemConst.IOC_Model.SurveyMainModel);
            var main = MainModel.GetSurveyMainByID(SMID, LoginAccount.CurrentAccountMainID);
            //回答表
            var AnswerModel = Factory.Get<ISurveyAnswerModel>(SystemConst.IOC_Model.SurveyAnswerModel);
            ViewBag.Answer = AnswerModel.GetAnswerInfoByUserCode(Ucode);
            //选项表
            var OptionModel = Factory.Get<ISurveyOptionModel>(SystemConst.IOC_Model.SurveyOptionModel);
            //满分
            ViewBag.FullMarks = OptionModel.GetSurveySum(main.ID);
            //评分
            ViewBag.Average = AnswerModel.GetAnswerUserScore(Ucode);

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "调查问卷 - 调查问卷信息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(main);
        }

        /// <summary>
        /// 生成资讯 ajax
        /// </summary>
        /// <param name="id"></param>
        /// <param name="client">客户端 EnumAdvertorialUType</param>
        /// <returns>OK 成功 NO失败</returns>
        [AllowCheckPermissions(false)]
        public string SetAppAdverTorial(int id, int client)
        {
            AppAdvertorial appRW = new AppAdvertorial();
            Result result = new Result();
            var AdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            //主表
            var MainModel = Factory.Get<ISurveyMainModel>(SystemConst.IOC_Model.SurveyMainModel);
            var main = MainModel.GetSurveyMainByID(id, LoginAccount.CurrentAccountMainID);
            appRW.Content = "";
            appRW.ContentURL = "http://" + SystemConst.WebUrl + "/Default/Questionnaire?surveyMainID_token=" + id.TokenEncrypt();
            appRW.ShortURL = ("http://" + SystemConst.WebUrl + "/Default/News?id_token=" + id.TokenEncrypt()).ConvertToShortURL();
            appRW.EnumAdverURLType = (int)EnumAdverURLType.Survey;
            appRW.AccountMainID = LoginAccount.CurrentAccountMainID;
            appRW.AppShowImagePath = "~/Images/Survey.png";
            appRW.MainImagPath = "~/Images/Survey.png";
            appRW.MinImagePath = "~/Images/Survey.png";
            appRW.Depict = main.SurveyRemarks;
            appRW.EnumAdverTorialType = 1;
            appRW.EnumAdvertorialUType = client;
            appRW.IssueDate = DateTime.Now;
            appRW.Sort = 0;
            appRW.stick = 0;
            appRW.SystemStatus = 0;
            appRW.Title = main.SurveyTitle;

            result = AdvertorialModel.Add(appRW);
            if (result.HasError == true)
            {
                return "No";
            }
            else
            {
                return "OK";
            }
        }

        #endregion
    }
}
