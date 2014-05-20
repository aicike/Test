using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;
using Injection;
using Poco;
using Interface.MerchantInterface;
using Poco.Enum;
using Poco.MerchantPoco;

namespace Web.Areas.Manager.Controllers
{
    public class M_TakeOutController : ManageSystemUserController
    {
        public ActionResult Index(int? id)
        {
            var model = Factory.Get<IM_TakeOutModel>(SystemConst.IOC_Model.M_TakeOutModel);
            var none = (int)EnumDataStatus.None;
            var list = model.List().OrderByDescending(a => a.CreatDate).OrderBy(a => a.EnumDataStatus).Where(a => a.EnumDataStatus != none).ToPagedList(id ?? 1, 15);
            return View(list);
        }

        public ActionResult Detail(int id)
        {
            var model = Factory.Get<IM_TakeOutModel>(SystemConst.IOC_Model.M_TakeOutModel);
            var obj = model.Get(id);
            (obj != null).NotAuthorizedPage();
            return View(obj);
        }

        [HttpPost]
        public ActionResult Detail(M_TakeOut takeout)
        {
            var model = Factory.Get<IM_TakeOutModel>(SystemConst.IOC_Model.M_TakeOutModel);
            var obj = model.Get(takeout.ID);
            (obj != null).NotAuthorizedPage();
            string status = Request.Form["hidStatus"];
            if (status == "0")
            {
                //审核不通过
                obj.EnumDataStatus = (int)EnumDataStatus.Disabled;
            }
            else
            {
                //审核通过
                obj.EnumDataStatus = (int)EnumDataStatus.Enabled;
            }
            var result = model.Edit(obj);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "M_TakeOut", new { Area = "Manager" }) + "'");
        }
    }
}
