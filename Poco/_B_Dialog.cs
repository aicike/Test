using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poco
{
    public class Dialog
    {
        public Dialog() { }

        public Dialog(string message)
        {
            Message = message;
            btnOk = "关闭";
        }

        //public Dialog(string message, string controller, string action)
        //{
        //    Message = message;
        //    Controller = controller;
        //    Action = action;
        //}

        public Dialog(string message, string url)
        {
            Message = message;
            URL = url;
        }

        public Dialog(Result result, string url, string strOk)
        {
            Message = result.Error;
            URL = url;
            btnOk = strOk;
        }

        #region 高级信息

        public string Controller { get; set; }

        public string Action { get; set; }

        #endregion

        #region 基础信息

        private DialogType dialogType = Poco.DialogType.Ok;

        public string DialogType
        {
            get { return dialogType.ToString(); }
        }

        public string Message { get; set; }

        private string title = "Message";

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private bool resizable = false;

        public bool Resizable
        {
            get { return resizable; }
            set { resizable = value; }
        }

        private int minHeight = 160;

        public int MinHeight
        {
            get { return minHeight; }
            set { minHeight = value; }
        }

        private string btnOk = "确定";

        public string BtnOk
        {
            get { return btnOk; }
            set { btnOk = value; }
        }

        private string btnOkClick = "$(this).dialog('close');";

        public string BtnOkClick
        {
            get { return btnOkClick; }
            set { btnOkClick = value; }
        }

        private string btnCancel = "取消";

        public string BtnCancel
        {
            get { return btnCancel; }
            set { btnCancel = value; }
        }

        private string btnCancelClick = "$(this).dialog('close');";

        public string BtnCancelClick
        {
            get { return btnCancelClick; }
            set { btnCancelClick = value; }
        }

        public string URL { get; set; }

        #endregion
    }
    public enum DialogType
    {
        Ok,
        OkAndCancel
    }
}