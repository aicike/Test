using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;

namespace Web.Areas.System.Controllers
{
    /// <summary>
    /// 生活技巧
    /// </summary>
    public class LifeSkillController : Controller
    {
        //
        // GET: /System/LifeSkill/

        public ActionResult Index(int ?id,int ?Error)
        {
            var LifeSkillModel = Factory.Get<ILifeSkillModel>(SystemConst.IOC_Model.LifeSkillModel);
            var LifeSkill = LifeSkillModel.GetList().ToPagedList(id ?? 1, 15);
            if (Error.HasValue)
            {
                ViewBag.Error = Error.Value;
            }
            else
            {
                ViewBag.Error = 0;
            }
            return View(LifeSkill);
        }

        public ActionResult Add(int? Error)
        {
            if (Error.HasValue)
            {
                ViewBag.Error = Error.Value;
            }
            else
            {
                ViewBag.Error = 0;
            }
            return View();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="recipes"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="tw"></param>
        /// <param name="th"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(LifeSkill lifeskill, int w, int h, int x1, int y1, int tw, int th)
        {
            if (w <= 0)
            {
                return RedirectToAction("Add", "LifeSkill", new { Error = 1 });
            }
            var LifeSkillModel = Factory.Get<ILifeSkillModel>(SystemConst.IOC_Model.LifeSkillModel);
            lifeskill.IssueDate = DateTime.Now;
            lifeskill.BrowseCnt = 0;
            LifeSkillModel.AddLifeSkill(lifeskill, w, h, x1, y1, tw, th);
            return RedirectToAction("Index", "LifeSkill");

        }


        public ActionResult Delete(int LID)
        {
            var LifeSkillModel = Factory.Get<ILifeSkillModel>(SystemConst.IOC_Model.LifeSkillModel);
            LifeSkillModel.DelLifeSkill(LID);
            return JavaScript("window.location.href='" + Url.Action("Index", "LifeSkill") + "'");
        }



        public ActionResult Edit(int LID, int? Error)
        {
            var LifeSkillModel = Factory.Get<ILifeSkillModel>(SystemConst.IOC_Model.LifeSkillModel);
            var LifeSkill = LifeSkillModel.GetInfo(LID);
            if (Error.HasValue)
            {
                ViewBag.Error = Error.Value;
            }
            else
            {
                ViewBag.Error = 0;
            }
            return View(LifeSkill);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(LifeSkill lifeskill, int w, int h, int x1, int y1, int tw, int th)
        {
            
            var LifeSkillModel = Factory.Get<ILifeSkillModel>(SystemConst.IOC_Model.LifeSkillModel);
            var LifeSkill = LifeSkillModel.GetInfo(lifeskill.ID);
            if (LifeSkill.MainImagPath != lifeskill.MainImagPath)
            {
                if (w <= 0)
                {
                    return RedirectToAction("Edit", "LifeSkill", new { Error = 1 });
                }
            }
            LifeSkillModel.EditLifeSkill(lifeskill, w, h, x1, y1, tw, th);
            return RedirectToAction("Index", "LifeSkill");
        }







        /// <summary>
        /// 更改发布状态
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="Release"></param>
        /// <returns></returns>
        public string UpSatausByID(int LID, bool Release)
        {
            var LifeSkillModel = Factory.Get<ILifeSkillModel>(SystemConst.IOC_Model.LifeSkillModel);
            LifeSkillModel.UpSatausByID(LID, Release);
            if (Release)
            {
                return "已发布";
            }
            else
            {
                return "未发布";
            }
        }
    }
}
