using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;
using Injection;
using Interface;
using Poco;

namespace Web.Areas.System.Controllers
{
    public class FeedbackController : ManageSystemUserController
    {
        //
        // GET: /System/Feedback/

        public ActionResult Index(int ?id)
        {
            var FeedbackModel = Factory.Get<IFeedbackModel>(SystemConst.IOC_Model.FeedbackModel);
            var feedback = FeedbackModel.List(true).ToPagedList(id ?? 1, 50);
            return View(feedback);
        }

        [AllowCheckPermissions(false)]
        public ActionResult GetInfo(int id)
        {
            var FeedbackModel = Factory.Get<IFeedbackModel>(SystemConst.IOC_Model.FeedbackModel);
            var feedback = FeedbackModel.Get(id);
            return View(feedback);
        }
    }
}
