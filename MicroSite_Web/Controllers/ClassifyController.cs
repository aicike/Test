﻿using System;
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
                TreeStr += string.Format("<li><a id='{0}' onclick=\"ckSet({0},{3})\" >{1}</a>{2}</li>", item.ID, item.Name, GetLastClass(item.ID), item.ParentID);
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
                TreeStr += string.Format("<li><a id='{0}' onclick=\"ckSet({0},{3})\" >{1}</a>{2}</li>", item.ID, item.Name, GetLastClass(item.ID), item.ParentID);
            }
            TreeStr += "</ul>";
            if (classify.Count() <= 0)
            {
                TreeStr = "";
            }
            return TreeStr;
        }

        //

        public ActionResult Add(int PID, string AddCname, string AddDepict,string AddBackgroundColor, string imgpath1)
        {
            var classModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);
            Classify cl = new Classify();
            cl.ParentID = PID;
            cl.AccountMainID = LoginAccount.CurrentAccountMainID;
            cl.Level = 0;
            cl.Name = AddCname;
            cl.Depict = AddDepict;
            cl.ImgPath = imgpath1;
            cl.Sort = 0;
            cl.Subordinate = "0";
            cl.BackgroundColor = AddBackgroundColor;

            var cnt = classModel.AddClass(cl);
            return RedirectToAction("Index", "Classify", new { HostName = LoginAccount.HostName });
        }

        public ActionResult Edit(int CID, string EditCname, string EditBackgroundColor, string EditDepict, int PID, string imgpath2)
        {
            var classModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);
            Classify cl = new Classify();
            cl.ID = CID;
            cl.ParentID = PID;
            cl.AccountMainID = LoginAccount.CurrentAccountMainID;
            cl.Level = 0;
            cl.Name = EditCname;
            cl.Depict = EditDepict;
            cl.ImgPath = imgpath2;
            cl.Sort = 0;
            cl.Subordinate = "0";
            cl.BackgroundColor = EditBackgroundColor;
            var cnt = classModel.UpdClass(cl);

            return RedirectToAction("Index", "Classify", new { HostName = LoginAccount.HostName });
        }

        public ActionResult Delete(int CID)
        {
            var classModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);
            if (classModel.GetIsMainNode(CID))
            {
                if (CkDelete(CID))
                {
                    Result result = classModel.CompleteDelete(CID);
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


        /// <summary>
        /// AJAX 获取选中分类信息
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCheckPermissions(false)]
        public string GetPname(int ID, int pid)
        {
            _B_Classify bcf = new _B_Classify();
            var classModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);

            if (pid == 0)
            {
                bcf.ParentName = "此节点为顶级节点";
            }
            else
            {
                var Pclassify = classModel.Get(pid);
                bcf.ParentName = Pclassify.Name;
            }
            var classify = classModel.Get(ID);
            bcf.ParentID = pid;
            bcf.ID = ID;
            bcf.Name = classify.Name;
            bcf.Sort = classify.Sort;
            bcf.Depict = classify.Depict;
            bcf.BackgroundColor = classify.BackgroundColor;
            if (string.IsNullOrEmpty(classify.ImgPath))
            {
                bcf.ImgFIlePath = Url.Content("~/Images/nopicture.png");
                bcf.ImgPath = "";
            }
            else
            {
                bcf.ImgFIlePath = Url.Content(classify.ImgPath,true);
                bcf.ImgPath = classify.ImgPath;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(bcf);
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
