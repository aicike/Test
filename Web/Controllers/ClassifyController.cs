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
    public class ClassifyController : ManageAccountController
    {
        //
        // GET: /Classify/

        public ActionResult Index()
        {
            var classModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);
            var classify = classModel.GetLastClass(0, LoginAccount.CurrentAccountMainID);
            string TreeStr = "";
            foreach (var item in classify)
            {
                TreeStr += string.Format("<li><a id='{0}' onclick=\"ckSet({0},{3},'{4}',{5},'{6}','{7}')\" >{1}</a>{2}</li>", item.ID, item.Name, GetLastClass(item.ID), item.Level, item.Name, item.ParentID, item.ImgPath, string.IsNullOrEmpty(item.ImgPath) ? "" : Url.Content(item.ImgPath));
            }
            ViewBag.TreeView = TreeStr;
            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "类别管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }

        //获取下级分类
        public string GetLastClass(int ID)
        {
            var classModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);
            var classify = classModel.GetLastClass(ID, LoginAccount.CurrentAccountMainID);
            string TreeStr = "<ul>";
            foreach (var item in classify)
            {
                TreeStr += string.Format("<li><a id='{0}' onclick=\"ckSet({0},{3},'{4}',{5},'{6}','{7}')\" >{1}</a>{2}</li>", item.ID, item.Name, GetLastClass(item.ID), item.Level, item.Name, item.ParentID, item.ImgPath, string.IsNullOrEmpty(item.ImgPath) ? "" : Url.Content(item.ImgPath));
            }
            TreeStr += "</ul>";
            if (classify.Count() <= 0)
            {
                TreeStr = "";
            }
            return TreeStr;
        }

        //

        public ActionResult Add(int PID, int Level, string AddCname, string imgpath1)
        {
            var classModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);
            var cnt = classModel.AddClass(PID, Level, LoginAccount.CurrentAccountMainID, AddCname, imgpath1);
            return RedirectToAction("Index", "Classify", new { HostName = LoginAccount.HostName });
        }

        public ActionResult Edit(int CID, string EditCname, int PID, string imgpath2)
        {
            var classModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);
            var cnt = classModel.UpdClass(PID, CID, EditCname, LoginAccount.CurrentAccountMainID, imgpath2);

            return RedirectToAction("Index", "Classify", new { HostName = LoginAccount.HostName });
        }

        public ActionResult Delete(int CID)
        {
            var classModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);
            if (classModel.GetIsMainNode(CID))
            {
                if (CkDelete(CID))
                {
                    Result result = classModel.DelClassify(CID,LoginAccount.CurrentAccountMainID);
                    if (result.HasError)
                    {
                        return Alert(new Dialog("此节点 已被引用 不能删除"));
                    }
                }
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "Classify") + "'");
        }

        //选择子类
        [AllowCheckPermissions(false)]
        public ActionResult ShowParent(int id)
        {
            var classModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);

            var classify = classModel.GetLastClass(0, LoginAccount.CurrentAccountMainID);
            string TreeStr = "";
            foreach (var item in classify)
            {
                TreeStr += string.Format("<li><input type='radio' name='sel' id='{0}' title='{3}' /><a  id='a{0}'  onclick='CkSel({0})' >{1}</a>{2}</li>", item.ID, item.Name, ShowGetLastClass(item.ID, id), item.Name);
            }
            ViewBag.TreeView = TreeStr;

            return View();
        }

        //选择子类获取下级分类

        public string ShowGetLastClass(int ID, int NoID)
        {
            var classModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);
            var classify = classModel.GetLastClass(ID, LoginAccount.CurrentAccountMainID, NoID);
            string TreeStr = "<ul>";
            foreach (var item in classify)
            {
                TreeStr += string.Format("<li><input type='radio' name='sel' id='{0}' title='{3}' /><a  id='a{0}'  onclick='CkSel({0})' >{1}</a>{2}</li>", item.ID, item.Name, ShowGetLastClass(item.ID, NoID), item.Name);
            }
            TreeStr += "</ul>";
            if (classify.Count() <= 0)
            {
                TreeStr = "";
            }
            return TreeStr;
        }



        [HttpPost]
        [AllowCheckPermissions(false)]
        public string GetPname(int pid)
        {
            string str = "";
            Classify cf = new Classify();

            cf.Name = "此节点为顶级节点";
            var classModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);
            var classify = classModel.Get(pid);
            if (classify != null)
            {
                cf.Name = classify.Name;
            }
            str = Newtonsoft.Json.JsonConvert.SerializeObject(cf);
            return str;
        }

        [HttpPost]
        [AllowCheckPermissions(false)]
        public bool CkDelete(int ID)
        {
            bool isOk = true;
            var classModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);
            var classify = classModel.GetClassByPID(ID);
            if (classify.Count() > 0)
            {
                isOk = false;
            }
            return isOk;
        }
    }
}
