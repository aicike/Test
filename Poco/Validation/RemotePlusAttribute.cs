﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property)]
    [Obsolete("该方法已经失效，请修改。",false)]
    public class RemotePlusAttribute : Attribute//: RemoteAttribute
    {
        public string ErrorMessage { get; set; }
        public RemotePlusAttribute(string action, string controller, string area, string routeName = null)
        //: base(action, controller, area)
        {
            //if (!string.IsNullOrEmpty(routeName))
            //{
            //    this.RouteName = routeName;
            //}
        }
    }
}
