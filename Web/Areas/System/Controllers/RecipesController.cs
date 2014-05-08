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
    /// 每日食谱
    /// </summary>
    public class RecipesController : Controller
    {
        //
        // GET: /System/Recipes/

        public ActionResult Index(int? id, int? Error)
        {
            var resultModel = Factory.Get<IRecipesModel>(SystemConst.IOC_Model.RecipesModel);
            var result = resultModel.GetList().ToPagedList(id ?? 1, 15);
            if (Error.HasValue)
            {
                ViewBag.Error = Error.Value;
            }
            else {
                ViewBag.Error = 0;
            }
            return View(result);
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
        public ActionResult Add(Recipes recipes, int w, int h, int x1, int y1, int tw, int th)
        {
            if (w <= 0)
            {
                return RedirectToAction("Add", "Recipes", new { Error = 1 });
            }
            var resultModel = Factory.Get<IRecipesModel>(SystemConst.IOC_Model.RecipesModel);
            recipes.IssueDate = DateTime.Now;
            recipes.BrowseCnt = 0;
            resultModel.AddRecipes(recipes, w, h, x1, y1, tw, th);
            return RedirectToAction("Index", "Recipes");

        }

        public ActionResult Edit(int RID,int?Error)
        {
            var resultModel = Factory.Get<IRecipesModel>(SystemConst.IOC_Model.RecipesModel);
            var result = resultModel.GetInfo(RID);
            if (Error.HasValue)
            {
                ViewBag.Error = Error.Value;
            }
            else
            {
                ViewBag.Error = 0;
            }
            return View(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Recipes recipes, int w, int h, int x1, int y1, int tw, int th)
        {
            
            var resultModel = Factory.Get<IRecipesModel>(SystemConst.IOC_Model.RecipesModel);
            var result = resultModel.GetInfo(recipes.ID);
            if (result.MainImagPath != recipes.MainImagPath)
            {
                if (w <= 0)
                {
                    return RedirectToAction("Edit", "Recipes", new { Error = 1 });
                }
            }
            resultModel.EditRecipes(recipes, w, h, x1, y1, tw, th);
            return RedirectToAction("Index", "Recipes");
        }

        public ActionResult Delete(int RID)
        {
            var resultModel = Factory.Get<IRecipesModel>(SystemConst.IOC_Model.RecipesModel);
            resultModel.DelRecipes(RID);
            return JavaScript("window.location.href='" + Url.Action("Index", "Recipes") + "'");
        }

        /// <summary>
        /// 更改发布状态
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="Release"></param>
        /// <returns></returns>
        public string UpSatausByID(int RID, bool Release)
        {
            var resultModel = Factory.Get<IRecipesModel>(SystemConst.IOC_Model.RecipesModel);
            resultModel.UpSatausByID(RID,Release);
            if (Release)
            {
                return "已发布";
            }
            else {
                return "未发布";
            }
        }

    }
}
