﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface;
using Poco;
using Common;
using System.Data;
using Business;

namespace Web.Controllers
{
    public class PropertyFeeInfoController : ManageAccountController
    {
        //
        // GET: /PropertyFeeInfo/
        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="id">分页</param>
        /// <param name="Date">缴费月份</param>
        /// <param name="Unit">单元</param>
        /// <param name="RoomNumber">房号</param>
        /// <param name="OwnerName">业主姓名</param>
        /// <param name="OwnerPhone">业主电话</param>
        /// <param name="IsError">导入状态 1出错 2成功 </param>
        /// <param name="ErrorStr">消息内容</param>
        /// <returns></returns>
        public ActionResult Index(int? id, string Date, string Unit, string RoomNumber, string OwnerName, string OwnerPhone)
        {
            if (id.HasValue)
            {
                ViewBag.pageid = id;
            }
            else
            {
                ViewBag.pageid = 1;
            }
            if (TempData["IsError"] !=null)
            {
                ViewBag.IsError = TempData["IsError"].ToString();
               
            }
            else
            {
                ViewBag.IsError = 0;
            }
            if (TempData["ErrorStr"] != null)
            {
                ViewBag.ErrorStr = TempData["ErrorStr"].ToString();
            }
            else
            {
                ViewBag.ErrorStr = "";

            }
            if (!string.IsNullOrEmpty(Date))
            {
                ViewBag.Date = Date;
            }
            else
            {
                ViewBag.Date = "";
            }
            if (!string.IsNullOrEmpty(Unit))
            {
                ViewBag.Unit = Unit;
            }
            else
            {
                ViewBag.Unit = "";
            }
            if (!string.IsNullOrEmpty(RoomNumber))
            {
                ViewBag.RoomNumber = RoomNumber;
            }
            else
            {
                ViewBag.RoomNumber = "";
            }
            if (!string.IsNullOrEmpty(OwnerName))
            {
                ViewBag.OwnerName = OwnerName;
            }
            else
            {
                ViewBag.OwnerName = "";
            }
            if (!string.IsNullOrEmpty(OwnerPhone))
            {
                ViewBag.OwnerPhone = OwnerPhone;
            }
            else
            {
                ViewBag.OwnerPhone = "";
            }
            var propertyfeemodel = Factory.Get<IPropertyFeeInfoModel>(SystemConst.IOC_Model.PropertyFeeInfoModel);
            var propertyfee = propertyfeemodel.GetPropertyFeeInfo(LoginAccount.CurrentAccountMainID, Date, Unit, RoomNumber, OwnerName, OwnerPhone).ToPagedList(id ?? 1, 50);

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "项目管理-户型管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            return View(propertyfee);
        }

        [HttpPost]
        public ActionResult ImportExcel(System.Web.HttpPostedFileBase ImExcel,int id)
        {
            Result result = new Result();
            DataTable dttable = new DataTable();
            dttable.Columns.Add("ID", typeof(Int32));
            dttable.Columns.Add("SystemStatus", typeof(Int32));
            dttable.Columns.Add("AccountMainID", typeof(Int32));
            dttable.Columns.Add("业主姓名");
            dttable.Columns.Add("业主电话");
            dttable.Columns.Add("单元");
            dttable.Columns.Add("房号");
            dttable.Columns.Add("缴费月份");
            dttable.Columns.Add("物业管理费", typeof(double));
            dttable.Columns.Add("物业服务费", typeof(double));
            dttable.Columns.Add("固定停车费", typeof(double));
            dttable.Columns.Add("电梯费", typeof(double));
            dttable.Columns.Add("水费", typeof(double));
            dttable.Columns.Add("卫生费", typeof(double));
            dttable.Columns.Add("其他费用", typeof(double));
            dttable.Columns.Add("合计", typeof(double));
            dttable.Columns.Add("备注");
            dttable.Columns.Add("importDate", typeof(DateTime));
            result = Tool.GetXLSXInfo(ImExcel, dttable);
            if (result.HasError)
            {
                //导入出错
                TempData["IsError"]  = 1;
                TempData["ErrorStr"] = result.Error;
                return RedirectToAction("Index", "PropertyFeeInfo", new { id = id});
                //return JavaScript("window.location.href='" + Url.Action("Index", "PropertyFeeInfo", new { IsError = 1, ErrorStr = result.Error }) + "'");
            }
            else
            {
                DataTable dt = result.Entity as DataTable;
                if (dt.Columns.Count > dttable.Columns.Count)
                {
                    TempData["IsError"] = 1;
                    TempData["ErrorStr"] = "请选择正确的模板";
                    return RedirectToAction("Index", "PropertyFeeInfo", new { id = id});
                    //return JavaScript("window.location.href='" + Url.Action("Index", "PropertyFeeInfo", new { IsError = 1, ErrorStr ="请选择正确的模板"}) + "'");
                }
                foreach (DataRow  Row  in dt.Rows)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(Row["业主电话"].ToString()))
                        {
                            TempData["IsError"] = 1;
                            TempData["ErrorStr"] = "业主电话不能为空";
                            return RedirectToAction("Index", "PropertyFeeInfo", new { id = id });
                            //return JavaScript("window.location.href='" + Url.Action("Index", "PropertyFeeInfo", new { IsError = 1, ErrorStr = "业主电话不能为空" }) + "'");
                        }
                    }
                    catch {
                        TempData["IsError"] = 1;
                        TempData["ErrorStr"] = "业主电话不能为空";
                        return RedirectToAction("Index", "PropertyFeeInfo", new { id = id });
                        //return JavaScript("window.location.href='" + Url.Action("Index", "PropertyFeeInfo", new { IsError = 1, ErrorStr = "业主电话不能为空" }) + "'");
                    }
                    try
                    {
                        
                        if (string.IsNullOrEmpty(Row["业主姓名"].ToString()))
                        {
                            TempData["IsError"] = 1;
                            TempData["ErrorStr"] = "业主姓名不能为空";
                            return RedirectToAction("Index", "PropertyFeeInfo", new { id = id });
                            //return JavaScript("window.location.href='" + Url.Action("Index", "PropertyFeeInfo", new { IsError = 1, ErrorStr = "业主姓名不能为空" }) + "'");
                        }
                    }
                    catch
                    {
                        TempData["IsError"] = 1;
                        TempData["ErrorStr"] = "业主姓名不能为空";
                        return RedirectToAction("Index", "PropertyFeeInfo", new { id = id});
                        //return JavaScript("window.location.href='" + Url.Action("Index", "PropertyFeeInfo", new { IsError = 1, ErrorStr = "业主姓名不能为空" }) + "'");
                    }
                    try
                    {
                        if (string.IsNullOrEmpty(Row["房号"].ToString()))
                        {
                            TempData["IsError"] = 1;
                            TempData["ErrorStr"] = "房号不能为空";
                            return RedirectToAction("Index", "PropertyFeeInfo", new { id = id });
                            //return JavaScript("window.location.href='" + Url.Action("Index", "PropertyFeeInfo", new { IsError = 1, ErrorStr = "房号不能为空" }) + "'");
                        }
                    }
                    catch
                    {
                        TempData["IsError"] = 1;
                        TempData["ErrorStr"] = "房号不能为空";
                        return RedirectToAction("Index", "PropertyFeeInfo", new { id = id });
                        //return JavaScript("window.location.href='" + Url.Action("Index", "PropertyFeeInfo", new { IsError = 1, ErrorStr = "房号不能为空" }) + "'");
                    }
                    try
                    {
                        if (string.IsNullOrEmpty(Row["单元"].ToString()))
                        {
                            TempData["IsError"] = 1;
                            TempData["ErrorStr"] = "单元不能为空";
                            return RedirectToAction("Index", "PropertyFeeInfo", new { id = id });
                            //return JavaScript("window.location.href='" + Url.Action("Index", "PropertyFeeInfo", new { IsError = 1, ErrorStr = "单元不能为空" }) + "'");
                        }
                    }
                    catch
                    {
                        TempData["IsError"] = 1;
                        TempData["ErrorStr"] = "单元不能为空";
                        return RedirectToAction("Index", "PropertyFeeInfo", new { id = id});
                        //return JavaScript("window.location.href='" + Url.Action("Index", "PropertyFeeInfo", new { IsError = 1, ErrorStr = "单元不能为空" }) + "'");
                    }
                    Row["importDate"] = DateTime.Now;
                    Row["AccountMainID"] = LoginAccount.CurrentAccountMainID;
                    Row["SystemStatus"] = 0;
                }
                CommonModel com = new CommonModel();
                result = com.CopyDataTableToDB(dt, "PropertyFeeInfo");
                if (result.HasError)
                {
                    TempData["IsError"] = 1;
                    TempData["ErrorStr"] = result.Error;
                    return RedirectToAction("Index", "PropertyFeeInfo", new { id = id });
                }
            }

            TempData["IsError"] = 2;
            TempData["ErrorStr"] = "导入成功";
            return RedirectToAction("Index", "PropertyFeeInfo", new { id = id});
            //return  JavaScript("window.location.href='" + Url.Action("Index", "PropertyFeeInfo", new { IsError = 2, ErrorStr = "导入成功" }) + "'");
        }

    }
}
