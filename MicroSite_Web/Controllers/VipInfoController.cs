using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Poco;
using Injection;
using Interface;
using Business;
using System.IO;
using Common;

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


            var PrefixModel = Factory.Get<ICardPrefixModel>(SystemConst.IOC_Model.CardPrefixModel);
            var frefixlist = PrefixModel.getList(LoginAccount.CurrentAccountMainID).ToList();
            ViewBag.Frefix = frefixlist;

            return View();
        }
        [HttpPost]
        public ActionResult Add(string phoneNum, int Prefix, string CardNumber)
        {

            var CardModel = Factory.Get<ICardInfoModel>(SystemConst.IOC_Model.CardInfoModel);
            var vipModel = Factory.Get<IVipInfoModel>(SystemConst.IOC_Model.VipInfoModel);

            var cardinfo = CardModel.GetCardInfoBy(CardNumber.Trim(), Prefix, LoginAccount.CurrentAccountMainID);
            if (cardinfo == null)
            {
                return JavaScript(" isCommit = true;" + AlertJS_NoTag(new Dialog("该卡不存在")));
            }
            else
            {
                if (cardinfo.Status == 0)
                {
                    return JavaScript(" isCommit = true;" + AlertJS_NoTag(new Dialog("该卡已经被冻结，不能绑定！")));
                }
                int[] CardIDs = new int[] { cardinfo.ID };
                if (vipModel.ckbIsbind(CardIDs))
                {
                    return JavaScript(" isCommit = true;" + AlertJS_NoTag(new Dialog("该卡已经被使用！")));
                }
            }



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
                VIPInfo vipinfo = new VIPInfo();
                vipinfo.CardInfoID = cardinfo.ID;
                vipinfo.score = 0;
                vipinfo.UserID = ID;
                vipinfo.CreateDate = DateTime.Now;
                vipinfo.Status = 1;
                vipinfo.AccountMainID = LoginAccount.CurrentAccountMainID;
                vipModel.Add(vipinfo);
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "VipInfo", new { HostName = LoginAccount.HostName }) + "'");
        }

        public ActionResult Edit(int id)
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "会员管理-修改会员", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.HostName = LoginAccount.HostName;

            var vipModel = Factory.Get<IVipInfoModel>(SystemConst.IOC_Model.VipInfoModel);
            var vipinfo = vipModel.Get(id);

            var PrefixModel = Factory.Get<ICardPrefixModel>(SystemConst.IOC_Model.CardPrefixModel);
            var frefixlist = PrefixModel.getList(LoginAccount.CurrentAccountMainID).ToList();
            ViewBag.Frefix = frefixlist;


            return View(vipinfo);
        }
        [HttpPost]
        public ActionResult Edit(VIPInfo vipinfo, int selPrefix)
        {

            var CardModel = Factory.Get<ICardInfoModel>(SystemConst.IOC_Model.CardInfoModel);



            var cardinfo = CardModel.getListEQ(LoginAccount.CurrentAccountMainID, vipinfo.CardInfo.CardNum.Trim(), selPrefix);
            if (cardinfo.Count() > 0)
            {
                var cardY = CardModel.Get(vipinfo.CardInfoID);
                var card = cardinfo.FirstOrDefault();
                var CardID = card.ID;
                if (cardY.CardNum == card.CardNum && cardY.CardPrefixID == card.CardPrefixID)
                {
                    return JavaScript("window.location.href='" + Url.Action("Index", "VipInfo", new { HostName = LoginAccount.HostName }) + "'");
                }
                else
                {
                    if (card.Status == 0)
                    {
                        return JavaScript(" isCommit = true;" + AlertJS_NoTag(new Dialog("该卡已经被冻结，不能绑定！")));
                    }
                    var vipModel = Factory.Get<IVipInfoModel>(SystemConst.IOC_Model.VipInfoModel);
                    int[] CardIDs = new int[] { CardID };
                    if (vipModel.ckbIsbind(CardIDs))
                    {
                        return JavaScript(" isCommit = true;" + AlertJS_NoTag(new Dialog("该卡已经被使用！")));
                    }
                    else
                    {
                        var result = vipModel.EditCID(vipinfo.ID, CardID, LoginAccount.CurrentAccountMainID);
                        if (result.HasError)
                        {
                            return JavaScript(" isCommit = true;" + AlertJS_NoTag(new Dialog(result.Error)));
                        }

                        return JavaScript("window.location.href='" + Url.Action("Index", "VipInfo", new { HostName = LoginAccount.HostName }) + "'");
                    }
                }

            }
            else
            {
                return JavaScript(" isCommit = true;" + AlertJS_NoTag(new Dialog("该卡不存在")));
            }





        }

        public ActionResult Delete()
        {

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isOK">1 充值成功</param>
        /// <returns></returns>
        public ActionResult Recharge(int? isOK)
        {
            if (isOK.HasValue)
            {
                ViewBag.IsOK = isOK;
            }
            else
            {
                ViewBag.IsOK = 0;
            }


            var PrefixModel = Factory.Get<ICardPrefixModel>(SystemConst.IOC_Model.CardPrefixModel);
            var frefixlist = PrefixModel.getList(LoginAccount.CurrentAccountMainID).ToList();
            ViewBag.Frefix = frefixlist;


            return View();
        }

        [HttpPost]
        public ActionResult Recharge(CardInfo cardInfo, int CardID)
        {
            var CardModel = Factory.Get<ICardInfoModel>(SystemConst.IOC_Model.CardInfoModel);
            var result = CardModel.Recharge(cardInfo.Balance, CardID, LoginAccount.CurrentAccountMainID,LoginAccount.ID);
            if (result.HasError)
            {
                return JavaScript(" isCommit = true;" + AlertJS_NoTag(new Dialog("充值失败 请稍后再试！")));
            }
            return JavaScript("window.location.href='" + Url.Action("Recharge", "VipInfo", new { HostName = LoginAccount.HostName, isOK = 1 }) + "'");
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns>json  0 不存在 1未绑定 2卡片冻结</returns>
        [HttpPost]
        [AllowCheckPermissions(false)]
        public string SelVipInfo(string cardNum, int Prefix)
        {
            var CardModel = Factory.Get<ICardInfoModel>(SystemConst.IOC_Model.CardInfoModel);
            var card = CardModel.GetCardInfoBy(cardNum, Prefix, LoginAccount.CurrentAccountMainID);
            if (card != null)
            {
                if (card.Status == 0)
                {
                    //卡片冻结
                    return "2";
                }
                var vipModel = Factory.Get<IVipInfoModel>(SystemConst.IOC_Model.VipInfoModel);
                var vipinfo = vipModel.GetInfoBYCardID(card.ID, LoginAccount.CurrentAccountMainID);
                if (vipinfo != null)
                {
                    _B_VIPInfo b_vip = new _B_VIPInfo();
                    b_vip.ID = vipinfo.ID;
                    b_vip.UserID = vipinfo.UserID ?? 0;
                    b_vip.UserName = vipinfo.User.UserLoginInfo.Name;
                    b_vip.UserPhone = vipinfo.User.UserLoginInfo.Phone;
                    b_vip.CardID = card.ID;

                    return Newtonsoft.Json.JsonConvert.SerializeObject(b_vip);
                }
                else
                {
                    //未绑定
                    return "1";
                }
            }
            else
            {
                //不存在
                return "0";
            }


        }

        //二维码
        [AllowCheckPermissions(false)]
        public void QrCode(string cardNums)
        {
            string card = cardNums + "." + LoginAccount.CurrentAccountMainID;
            string number = DESEncrypt.Encrypt(card);
            QrCodeModel model = new QrCodeModel();

            MemoryStream ms = model.Get_Android_DownloadUrl(number);
            if (null != ms)
            {
                Response.BinaryWrite(ms.ToArray());
            }
        }



    }
}
