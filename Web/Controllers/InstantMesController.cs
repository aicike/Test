using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface;
using Poco;
using Business;

namespace Web.Controllers
{
    public class InstantMesController : ManageAccountController
    {
        //
        // GET: /InstantMes/

        public ActionResult Index(int? id)
        {
            //获取用户分组
            var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
            var groupList = groupModel.GetGroupListByAccountID(LoginAccount.ID, null);
            ViewBag.GroupList = groupList;

            ViewBag.HostName = LoginAccount.HostName;


            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "用户管理-销售消息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            //读取信息
            //var TemporayModel = Factory.Get<ITemporayInstantMesModel>(SystemConst.IOC_Model.TemporayInstantMes);
            //var TemporayList = TemporayModel.GetList(LoginAccount.ID).ToPagedList(id ?? 1, 15); 

            var commonModel = Factory.Get<CommonModel>(SystemConst.IOC_Model.CommonModel);
            var TemporayList = commonModel.getSessionList(LoginAccount.ID, 0,LoginAccount.CurrentAccountMainID);


            return View(TemporayList.ToPagedList(id ?? 1, 15));

        }


        [AllowCheckPermissions(false)]
        public String SendMessage()
        {
            var MessageModel = Factory.Get<IMessageModel>(SystemConst.IOC_Model.MessageModel);
            int count = MessageModel.getUnreadCnt(LoginAccount.ID);
            return count.ToString();
        }
    }
}