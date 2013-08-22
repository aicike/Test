using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface;
using Poco;

namespace Web.Controllers
{
    public class InstantMesController : ManageAccountController
    {
        //
        // GET: /InstantMes/

        public ActionResult Index(int ? id)
        {
            //获取用户分组
            var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
            var groupList = groupModel.GetGroupListByAccountID(LoginAccount.ID, null);
            ViewBag.GroupList = groupList;

            ViewBag.HostName = LoginAccount.HostName;


            //读取信息
            var TemporayModel = Factory.Get<ITemporayInstantMesModel>(SystemConst.IOC_Model.TemporayInstantMes);
            var TemporayList = TemporayModel.GetList(LoginAccount.ID).ToPagedList(id ?? 1, 15); ;

            return View(TemporayList);
        }


        [AllowCheckPermissions(false)]
        public String SendMessage()
        {
            var PendModel = Factory.Get<IPendingMessagesModel>(SystemConst.IOC_Model.PendingMessagesModel);
            string count = PendModel.SendMessageCount(LoginAccount.ID);
            return count;
        }
    }
}
