using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Controllers;
using System.Data;
using Common;
using Business;
using System.IO;
using System.Text;

namespace Web.Controllers
{
    public class ParkingFeeController : ManageAccountController
    {
        //
        // GET: /ParkingFee/
        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="id">分页</param>
        /// <param name="Date">缴费月份</param>
        /// <param name="fhsx">房号缩写</param>
        /// <param name="IsPay">是否已经交费</param>
        /// <returns></returns>
        public ActionResult Index(int? id, string Date, string fhsx, string IsPay)
        {
            if (id.HasValue)
            {
                ViewBag.pageid = id;
            }
            else
            {
                ViewBag.pageid = 1;
            }
            if (TempData["IsError"] != null)
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
            if (!string.IsNullOrEmpty(fhsx))
            {
                ViewBag.fhsx = fhsx;
            }
            else
            {
                ViewBag.fhsx = "";
            }
            if (!string.IsNullOrEmpty(IsPay))
            {
                ViewBag.IsPay = IsPay;
            }
            else
            {
                ViewBag.IsPay = "";
            }
            var parkingpropertyfeemodel = Factory.Get<IParkingFeeModel>(SystemConst.IOC_Model.ParkingFeeModel);
            var propertyfee = parkingpropertyfeemodel.GetParkingFeeInfo(LoginAccount.CurrentAccountMainID, Date, fhsx, IsPay ).ToPagedList(id ?? 1, 50);

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "物业管理-停车费管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            return View(propertyfee);
        }


        [HttpPost]
        public ActionResult ImportExcel(System.Web.HttpPostedFileBase ImExcel, int id, int Month)
        {
            var parkingpropertyfeemodel = Factory.Get<IParkingFeeModel>(SystemConst.IOC_Model.ParkingFeeModel);
            Result result = new Result();
            DataTable dttable = new DataTable();
            dttable.Columns.Add("ID", typeof(Int32));
            dttable.Columns.Add("SystemStatus", typeof(Int32));
            dttable.Columns.Add("AccountMainID", typeof(Int32));
            dttable.Columns.Add("单元");
            dttable.Columns.Add("房号");
            dttable.Columns.Add("缴费月份");
            dttable.Columns.Add("停车费", typeof(double));
            dttable.Columns.Add("是否已缴费（是/否）");
            dttable.Columns.Add("importDate", typeof(DateTime));
            dttable.Columns.Add("车牌号");
            dttable.Columns.Add("楼号");
            dttable.Columns.Add("房号(缩写)");
            dttable.Columns.Add("停车类型");
            result = Tool.GetXLSXInfo(ImExcel, dttable,"1");
            if (result.HasError)
            {
                //导入出错
                TempData["IsError"]  = 1;
                TempData["ErrorStr"] = result.Error;
                return RedirectToAction("Index", "ParkingFee", new { id = id });
                //return JavaScript("window.location.href='" + Url.Action("Index", "PropertyFeeInfo", new { IsError = 1, ErrorStr = result.Error }) + "'");
            }
            else
            {
                DataTable dt = result.Entity as DataTable;
                if (dt.Columns.Count > dttable.Columns.Count)
                {
                    TempData["IsError"] = 1;
                    TempData["ErrorStr"] = "请选择正确的模板";
                    return RedirectToAction("Index", "ParkingFee", new { id = id });
                    //return JavaScript("window.location.href='" + Url.Action("Index", "PropertyFeeInfo", new { IsError = 1, ErrorStr ="请选择正确的模板"}) + "'");
                }

                if (dt.Rows.Count <= 0)
                {
                    TempData["IsError"] = 1;
                    TempData["ErrorStr"] = "模板中没有数据";
                    return RedirectToAction("Index", "PropertyFeeInfo", new { id = id });
                }
                var DBlist = parkingpropertyfeemodel.GetAllByPayDay(LoginAccount.CurrentAccountMainID, dt.Rows[0]["缴费月份"].ToString());
                foreach (DataRow  Row  in dt.Rows)
                {
                    
                    try
                    {
                        string sx = Row["房号(缩写)"].ToString();
                        var sxlist = sx.Split('-').Length;
                        if (sxlist < 2)
                        {
                            TempData["IsError"] = 1;
                            TempData["ErrorStr"] = "房号(缩写)格式不正确";
                            return RedirectToAction("Index", "ParkingFee", new { id = id });
                        }
                        if (string.IsNullOrEmpty(Row["房号(缩写)"].ToString()))
                        {
                            TempData["IsError"] = 1;
                            TempData["ErrorStr"] = "房号(缩写)不能为空";
                            return RedirectToAction("Index", "ParkingFee", new { id = id });
                            //return JavaScript("window.location.href='" + Url.Action("Index", "PropertyFeeInfo", new { IsError = 1, ErrorStr = "房号不能为空" }) + "'");
                        }
                    }
                    catch
                    {
                        TempData["IsError"] = 1;
                        TempData["ErrorStr"] = "房号(缩写)不能为空";
                        return RedirectToAction("Index", "ParkingFee", new { id = id });
                        //return JavaScript("window.location.href='" + Url.Action("Index", "PropertyFeeInfo", new { IsError = 1, ErrorStr = "房号不能为空" }) + "'");
                    }
                  
                    try
                    {
                        if (string.IsNullOrEmpty(Row["是否已缴费（是/否）"].ToString()))
                        {
                            TempData["IsError"] = 1;
                            TempData["ErrorStr"] = "是否已缴费不能为空";
                            return RedirectToAction("Index", "ParkingFee", new { id = id });
                            //return JavaScript("window.location.href='" + Url.Action("Index", "PropertyFeeInfo", new { IsError = 1, ErrorStr = "单元不能为空" }) + "'");
                        }
                        else {
                            if (Row["是否已缴费（是/否）"].ToString() == "是")
                            {
                                Row["是否已缴费（是/否）"] = "True";
                            }
                            else {
                                Row["是否已缴费（是/否）"] = "False";
                            }
                        }
                    }
                    catch
                    {
                        TempData["IsError"] = 1;
                        TempData["ErrorStr"] = "是否已缴费不能为空";
                        return RedirectToAction("Index", "ParkingFee", new { id = id });
                        //return JavaScript("window.location.href='" + Url.Action("Index", "PropertyFeeInfo", new { IsError = 1, ErrorStr = "单元不能为空" }) + "'");
                    }
                    Row["importDate"] = DateTime.Now;
                    Row["AccountMainID"] = LoginAccount.CurrentAccountMainID;
                    Row["SystemStatus"] = 0;

                    try
                    {
                        if (string.IsNullOrEmpty(Row["缴费月份"].ToString()))
                        {
                            TempData["IsError"] = 1;
                            TempData["ErrorStr"] = "缴费月份不能为空";
                            return RedirectToAction("Index", "ParkingFee", new { id = id });
                        }
                        else
                        {
                            var JFmonth = Convert.ToDateTime(Row["缴费月份"]);
                            if (Month != JFmonth.Month)
                            {
                                TempData["IsError"] = 1;
                                TempData["ErrorStr"] = "只能导入与选择月份相同的“缴费月份”数据。";
                                return RedirectToAction("Index", "ParkingFee", new { id = id });
                            }
                        }
                    }
                    catch
                    {
                        TempData["IsError"] = 1;
                        TempData["ErrorStr"] = "缴费月份不能为空";
                        return RedirectToAction("Index", "ParkingFee", new { id = id });
                    }


                    #region 唯一验证
                    int cnts = 0;
                    foreach (DataRow Row2 in dt.Rows)
                    {
                        try
                        {
                            string sx = Row["房号(缩写)"].ToString();
                           

                            string sx2 = Row2["房号(缩写)"].ToString();

                            if (sx == sx2)
                            {
                                cnts = cnts + 1;
                                if (cnts > 1)
                                {
                                    TempData["IsError"] = 1;
                                    TempData["ErrorStr"] = "模板中有重复数据，请删除后再试";
                                    return RedirectToAction("Index", "ParkingFee", new { id = id });
                                }
                            }
                            foreach (ParkingFee pfi in DBlist)
                            {
                                if (sx == pfi.Abbreviation)
                                {
                                    TempData["IsError"] = 1;
                                    TempData["ErrorStr"] = " 房号(缩写)：" + sx + "中的的数据已存在。请删除后再次导入。";
                                    return RedirectToAction("Index", "ParkingFee", new { id = id });
                                }
                            }
                            
                        }
                        catch
                        {
                            TempData["IsError"] = 1;
                            TempData["ErrorStr"] = "模板中 房号(缩写)不能为空。";
                            return RedirectToAction("Index", "PropertyFeeInfo", new { id = id });
                        }
                    }
                    #endregion
                    string fhsx = Row["房号(缩写)"].ToString();
                    var fhsxlist = fhsx.Split('-');
                    Row["楼号"] = fhsxlist[0];
                    Row["单元"] = fhsxlist[1];
                    Row["房号"] = fhsxlist[2];
                }
                CommonModel com = new CommonModel();
                result = com.CopyDataTableToDB(dt, "ParkingFee");
                if (result.HasError)
                {
                    TempData["IsError"] = 1;
                    TempData["ErrorStr"] = result.Error;
                    return RedirectToAction("Index", "ParkingFee", new { id = id });
                }
            }

            TempData["IsError"] = 2;
            TempData["ErrorStr"] = "导入成功";
            return RedirectToAction("Index", "ParkingFee", new { id = id });
            //return  JavaScript("window.location.href='" + Url.Action("Index", "PropertyFeeInfo", new { IsError = 2, ErrorStr = "导入成功" }) + "'");
        }

        [HttpPost]
        public ActionResult DellInfo()
        {
            var parkingpropertyfeemodel = Factory.Get<IParkingFeeModel>(SystemConst.IOC_Model.ParkingFeeModel);
            string ids = Request.Form["ckbOK"];
            var result = parkingpropertyfeemodel.delAll(ids, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                TempData["IsError"] = 1;
                TempData["ErrorStr"] = "删除失败 稍后再试";
            }
            else
            {
                TempData["IsError"] = 3;
                TempData["ErrorStr"] = "删除成功";
            }
            return RedirectToAction("Index", "ParkingFee");
        }

        public ActionResult Add()
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "物业管理-停车费添加-物业费管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            if (TempData["IsError"] != null)
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
            return View();
        }

        [HttpPost]
        public ActionResult Add(ParkingFee parkingfee)
        {
            var parkingpropertyfeemodel = Factory.Get<IParkingFeeModel>(SystemConst.IOC_Model.ParkingFeeModel);
            parkingfee.Abbreviation = parkingfee.BuildingNum + "-" + parkingfee.Unit + "-" + parkingfee.RoomNumber;
            parkingfee.importDate = DateTime.Now;
            parkingfee.AccountMainID = LoginAccount.CurrentAccountMainID;
            var result = parkingpropertyfeemodel.DBImportCheck(LoginAccount.CurrentAccountMainID, parkingfee.PayDate, parkingfee.BuildingNum, parkingfee.Unit, parkingfee.RoomNumber);
            if (result.HasError)
            {
                TempData["IsError"] = 1;
                TempData["ErrorStr"] = result.Error;
                return RedirectToAction("Add", "ParkingFee");
            }
            result = parkingpropertyfeemodel.Add(parkingfee);
            if (result.HasError)
            {
                TempData["IsError"] = 1;
                TempData["ErrorStr"] = result.Error;
                return RedirectToAction("Add", "ParkingFee");
            }
            return RedirectToAction("Index", "ParkingFee");
        }

        /// <summary>
        /// 导出模板
        /// </summary>
        /// <returns></returns>
        public void OutExcel()
        {
            var parkingpropertyfeemodel = Factory.Get<IParkingFeeModel>(SystemConst.IOC_Model.ParkingFeeModel);
            List<DataTable> dts = new List<DataTable>();
            var dt = parkingpropertyfeemodel.getDtInfo(LoginAccount.CurrentAccountMainID);
            dts.Add(dt);

            CommonModel com = new CommonModel();
            string fileName =  "物业停车费导入模板" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".xlsx";
            string file = Server.MapPath("/File/Excel/") + fileName;
            com.Export(dts, "物业停车费导入模板", file);



            FileStream fs = new FileStream(file, FileMode.Open);

            byte[] bytes = new byte[(int)fs.Length];

            fs.Read(bytes, 0, bytes.Length);

            fs.Close();

            Response.ContentType = "application/octet-stream";

            //通知浏览器下载文件而不是打开 

            Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));

            Response.BinaryWrite(bytes);

            Response.Flush();

            Response.End();
            System.IO.File.Delete(file);
            //Directory.Delete(file);
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <returns></returns>
        public void OutExcel2(string DcDate, string Dcfhsx)
        {
            var parkingpropertyfeemodel = Factory.Get<IParkingFeeModel>(SystemConst.IOC_Model.ParkingFeeModel);
            List<DataTable> dts = new List<DataTable>();
            var dt = parkingpropertyfeemodel.getDtHistoryInfo(LoginAccount.CurrentAccountMainID,DcDate,Dcfhsx);
            dts.Add(dt);

            CommonModel com = new CommonModel();
            string fileName = "物业停车费历史数据" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".xlsx";
            string file = Server.MapPath("/File/Excel/") + fileName;
            com.Export(dts, "物业停车费历史数据", file);



            FileStream fs = new FileStream(file, FileMode.Open);

            byte[] bytes = new byte[(int)fs.Length];

            fs.Read(bytes, 0, bytes.Length);

            fs.Close();

            Response.ContentType = "application/octet-stream";

            //通知浏览器下载文件而不是打开 

            Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));

            Response.BinaryWrite(bytes);

            Response.Flush();

            Response.End();
            System.IO.File.Delete(file);
            //Directory.Delete(file);
        }
    }
}
