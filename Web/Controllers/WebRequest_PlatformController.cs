using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;

namespace Web.Controllers
{
    public class WebRequest_PlatformController : Controller
    {
        #region------------------每日食谱----------------------
        /// <summary>
        /// 每日食谱显示列表
        /// </summary>
        /// <param name="AccountID">售楼部ID</param>
        /// <param name="ID">显示开始ID 第一次打开传0</param>
        /// <param name="ListCnt">返回列表的条数</param>
        /// <returns></returns>
        [AllowAnonymous]
        public string GetRecipesList(int AMID, int ID, int ListCnt)
        {
            var RecipesModel = Factory.Get<IRecipesModel>(SystemConst.IOC_Model.RecipesModel);
            var list = RecipesModel.GetList();
            PagedList<Recipes> RListImg = null;
            if (ID == 0)
            {
                
                RListImg = list.ToPagedList(1, ListCnt);
            }
            else
            {
                RListImg = list.Where(a => a.ID < ID).ToPagedList(1, ListCnt);
            }
            List<_B_Advertorial> ListShow = new List<_B_Advertorial>();
            if (RListImg != null)
            {
                foreach (var item in RListImg)
                {
                    _B_Advertorial ADVERTORIAL = new _B_Advertorial();
                    ADVERTORIAL.I = item.ID;
                    ADVERTORIAL.T = item.Title;
                    ADVERTORIAL.P = item.Depict;
                    ADVERTORIAL.D = item.IssueDate.ToString("yyyy-MM-dd");
                    ADVERTORIAL.S = SystemConst.WebUrlIP + Url.Content(item.AppShowImagePath ?? "");
                    ADVERTORIAL.F = SystemConst.WebUrlIP + Url.Content(item.AppShowImagePath ?? "");
                    ADVERTORIAL.URL = SystemConst.WebUrlIP + "/Default/News?id_token=" + item.ID.TokenEncrypt();
                    ListShow.Add(ADVERTORIAL);
                }
            }


            return Newtonsoft.Json.JsonConvert.SerializeObject(ListShow);
        }

        #endregion

        #region------------------生活技巧----------------------
        /// <summary>
        /// 生活技巧显示列表
        /// </summary>
        /// <param name="AccountID">售楼部ID</param>
        /// <param name="ID">显示开始ID 第一次打开传0</param>
        /// <param name="ListCnt">返回列表的条数</param>
        /// <returns></returns>
        [AllowAnonymous]
        public string GetLifeSkillList(int AMID, int ID, int ListCnt)
        {
            var LifeSkillModel = Factory.Get<ILifeSkillModel>(SystemConst.IOC_Model.LifeSkillModel);
            var list = LifeSkillModel.GetList();
            PagedList<LifeSkill> RListImg = null;
            if (ID == 0)
            {

                RListImg = list.ToPagedList(1, ListCnt);
            }
            else
            {
                RListImg = list.Where(a => a.ID < ID).ToPagedList(1, ListCnt);
            }
            List<_B_Advertorial> ListShow = new List<_B_Advertorial>();
            if (RListImg != null)
            {
                foreach (var item in RListImg)
                {
                    _B_Advertorial ADVERTORIAL = new _B_Advertorial();
                    ADVERTORIAL.I = item.ID;
                    ADVERTORIAL.T = item.Title;
                    ADVERTORIAL.P = item.Depict;
                    ADVERTORIAL.D = item.IssueDate.ToString("yyyy-MM-dd");
                    ADVERTORIAL.S = SystemConst.WebUrlIP + Url.Content(item.AppShowImagePath ?? "");
                    ADVERTORIAL.F = SystemConst.WebUrlIP + Url.Content(item.AppShowImagePath ?? "");
                    ADVERTORIAL.URL = SystemConst.WebUrlIP + "/Default/News?id_token=" + item.ID.TokenEncrypt();
                    ListShow.Add(ADVERTORIAL);
                }
            }


            return Newtonsoft.Json.JsonConvert.SerializeObject(ListShow);
        }

        #endregion
        
    }
}
