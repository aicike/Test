using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Poco;

namespace MicroSite
{
    public class BasePage : System.Web.UI.Page
    {
        protected User LoginUser
        {
            get
            {
                var account = Session[SystemConst.Session.LoginUser] as User;
                return account;
            }
            set { Session[SystemConst.Session.LoginUser] = value; }
        }

        protected EnumMenu BaseMenu
        {
            get
            {
                var menu = Session[SystemConst.Session.MicroSiteMenu] == null ? EnumMenu.Home : (EnumMenu)Convert.ToInt32(Session[SystemConst.Session.MicroSiteMenu]);
                return menu;
            }
            set { Session[SystemConst.Session.MicroSiteMenu] = value; }
        }
    }

    public enum EnumMenu
    {
        Home=0,
        HouseInfo=1,
        Shop=2,
        Center=3,
        ShopCar=4
    }
}