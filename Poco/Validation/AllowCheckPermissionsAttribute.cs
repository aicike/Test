using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowCheckPermissionsAttribute : Attribute
    {
        private bool allowCheckPermissions = true;

        public bool AllowCheckPermissions { get { return allowCheckPermissions; } }

        public AllowCheckPermissionsAttribute()
        {
        }

        public AllowCheckPermissionsAttribute(bool allowCheckPermissions)
        {
            this.allowCheckPermissions = allowCheckPermissions;
        }
    }
}
