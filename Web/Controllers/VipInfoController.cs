using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Poco;
using Injection;
using Interface;

namespace Web.Controllers
{
    public class VipInfoController : ManageAccountController
    {
        //
        // GET: /VipMessage/

        public ActionResult Index(int? id, string cardNum, string phoneNum)
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "会员管理-会员列表", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.HostName = LoginAccount.HostName;

            if (!string.IsNullOrEmpty(cardNum))
            {
                ViewBag.cardNum = cardNum;
            }
            else
            {
                ViewBag.cardNum = "";
            }
            if (!string.IsNullOrEmpty(phoneNum))
            {
                ViewBag.phoneNum = phoneNum;
            }
            else
            {
                ViewBag.phoneNum = "";
            }

            var vipModel = Factory.Get<IVipInfoModel>(SystemConst.IOC_Model.VipInfoModel);
            var viplist = vipModel.getList(LoginAccount.CurrentAccountMainID, cardNum, phoneNum).ToPagedList(id ?? 1, 15);

            return View(viplist);
        }

        public ActionResult Add()
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "会员管理-添加会员", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.HostName = LoginAccount.HostName;
            return View();
        }
        [HttpPost]
        public ActionResult Add(VIPInfo vipinfo, string phoneNum)
        {
            var vipModel = Factory.Get<IVipInfoModel>(SystemConst.IOC_Model.VipInfoModel);
            var ID = vipModel.CheckPhoneGetID(phoneNum, LoginAccount.CurrentAccountMainID);
            if (ID == 0)
            {
                return JavaScript(" isCommit = true;" + AlertJS_NoTag(new Dialog("该手机号已经注册为会员")));
            }
            else if (ID == -1)
            {
                return JavaScript(" isCommit = true;" + AlertJS_NoTag(new Dialog("该手机号还未注册用户")));
            }
            else
            {
                vipinfo.UserID = ID;
                vipinfo.CreateDate = DateTime.Now;
                vipinfo.Status = 1;
                vipinfo.AccountMainID = LoginAccount.CurrentAccountMainID;
                vipModel.Add(vipinfo);
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "VipMessage", new { HostName = LoginAccount.HostName }) + "'");
        }

        public ActionResult Edit(int id)
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "会员管理-修改会员", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.HostName = LoginAccount.HostName;

            var vipModel = Factory.Get<IVipInfoModel>(SystemConst.IOC_Model.VipInfoModel);
            var vipinfo = vipModel.Get(id);


            return View(vipinfo);
        }
        [HttpPost]
        public ActionResult Edit(VIPInfo vipinfo)
        {
            var vipModel = Factory.Get<IVipInfoModel>(SystemConst.IOC_Model.VipInfoModel);
            vipModel.Edit(vipinfo);
            return JavaScript("window.location.href='" + Url.Action("Index", "VipMessage", new { HostName = LoginAccount.HostName }) + "'");
        }

        public ActionResult Delete()
        {

            return View();
        }


        public ActionResult Recharge()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Recharge(VIPInfo vipinfo)
        {

            return View();
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string SelVipInfo(string cardNum)
        {
            var vipModel = Factory.Get<IVipInfoModel>(SystemConst.IOC_Model.VipInfoModel);
            var vipinfo = vipModel.getByCardNum(cardNum,LoginAccount.CurrentAccountMainID);
            return Newtonsoft.Json.JsonConvert.SerializeObject(vipinfo);
        }

    }
}
