using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Poco;
using Injection;
using Interface;

namespace Web.Controllers
{
    public class PaymentController : ManageAccountController
    {
        //
        // GET: /Payment/

        /// <summary>
        /// 支付配置界面
        /// </summary>
        /// <param name="Error">1:成功 2：失败</param>
        /// <param name="Type">1：支付宝</param>
        /// <returns>支付宝信息</returns>
        public ActionResult Index(int? Error, int? Type)
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "设置-支付配置", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            var accountmainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var accountmain = accountmainModel.Get(LoginAccount.CurrentAccountMainID);
            //是否启用支付宝
            ViewBag.IsusePay = accountmain.IsusePay;

            //支付宝信息
            var paymentModel = Factory.Get<IPaymentModel>(SystemConst.IOC_Model.PaymentModel);
            var payment = paymentModel.GetInfoByAMID(LoginAccount.CurrentAccountMainID);

            if (Error.HasValue)
            {
                ViewBag.Error = Error.Value;
            }
            else
            {
                ViewBag.Error = "0";     
            }
            if (Type.HasValue)
            {
                ViewBag.Type = Type.Value;
            }
            else
            {
                ViewBag.Type = "0";
            }
           
            return View(payment);
        }

        /// <summary>
        /// 保存支付宝信息
        /// </summary>
        /// <returns></returns>
        public ActionResult PreservePay(PayInfo payInfo, bool IsusePay)
        {
            var accountmainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            //支付宝信息
            var paymentModel = Factory.Get<IPaymentModel>(SystemConst.IOC_Model.PaymentModel);
            //修改支付宝状态
            var result = accountmainModel.UpdPayIsUse(LoginAccount.CurrentAccountMainID, IsusePay);
            if (!result.HasError)
            {
                payInfo.AccountMainID = LoginAccount.CurrentAccountMainID;
                var payment = paymentModel.GetInfoByAMID(LoginAccount.CurrentAccountMainID);
                if (payment != null)
                {
                    paymentModel.Edit(payInfo);
                }
                else
                {
                    paymentModel.Add(payInfo);
                }
                return RedirectToAction("Index", "Payment", new { Error = 1, Type = 1 });
            }
            else
            {
                return RedirectToAction("Index", "Payment", new { Error = 2, Type = 1 });
            }
        }

    }
}
