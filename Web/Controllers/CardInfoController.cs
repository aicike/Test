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
    public class CardInfoController : ManageAccountController
    {
        //
        // GET: /CardInfo/

        public ActionResult Index(int ? id, string cardNum,string qz)
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "卡片管理-卡片列表", LoginAccount.CurrentAccountMainName, WebTitleRemark);
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
            if (!string.IsNullOrEmpty(qz))
            {
                ViewBag.qz = qz;
            }
            else
            {
                ViewBag.qz = "";
            }

            var CardModel = Factory.Get<ICardInfoModel>(SystemConst.IOC_Model.CardInfoModel);
            var cardlist = CardModel.getList(LoginAccount.CurrentAccountMainID, cardNum, qz).ToPagedList(id ?? 1, 50);

            return View(cardlist);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(CardInfo cardinfo)
        {

            cardinfo.Status = 1;
            cardinfo.AccountMainID = LoginAccount.CurrentAccountMainID;
            cardinfo.CreateDate = DateTime.Now;
            var CardModel = Factory.Get<ICardInfoModel>(SystemConst.IOC_Model.CardInfoModel);
            if (CardModel.ckbCardRepeat(cardinfo.CardNum, cardinfo.CardPrefix, LoginAccount.CurrentAccountMainID))
            {
                return JavaScript(AlertJS_NoTag(new Dialog("卡号重复！")));
            }
            var result = CardModel.Add(cardinfo);
            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "CardInfo", new { HostName = LoginAccount.HostName }) + "'");
        }

         [HttpPost]
        public ActionResult Delete()
        {
            var checkboxMenu = Request["ckbOK"];

            if (!string.IsNullOrEmpty(checkboxMenu))
            {
                int[] CardIDs = checkboxMenu.Split(',').ConvertToIntArray();
               

                var VipModel = Factory.Get<IVipInfoModel>(SystemConst.IOC_Model.VipInfoModel);
                if (VipModel.ckbIsbind(CardIDs))
                {
                    return Alert(new Dialog("您选中的卡号中 有被绑定,不能删除！"));
                }
                else
                {
                    var CardModel = Factory.Get<ICardInfoModel>(SystemConst.IOC_Model.CardInfoModel);
                    var result = CardModel.DelAll(checkboxMenu);
                }

                return JavaScript("window.location.href='" + Url.Action("Index", "CardInfo", new { HostName = LoginAccount.HostName }) + "'");
            }
            else {

                return Alert(new Dialog("没有选中项！"));
            }
            
        }

         public ActionResult SetStatus(int id,int status)
         {
             var CardModel = Factory.Get<ICardInfoModel>(SystemConst.IOC_Model.CardInfoModel);
             if (status == 0)
             {
                 status = 1;
             }
             else
             {
                 status = 0;
             }
             CardModel.SetStatus(id,status);
             return JavaScript("window.location.href='" + Url.Action("Index", "CardInfo", new { HostName = LoginAccount.HostName }) + "'");
         }

    }
}
