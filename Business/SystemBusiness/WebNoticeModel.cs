﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection;

namespace Business.SystemBusiness
{
    public class WebNoticeModel : BaseModel<WebNotice>, IWebNoticeModel
    {
        public Result Add(int menuID, int accountMainID)
        {
            Result result = new Result();
            var obj = GetByMenuID(menuID, accountMainID);
            if (obj == null)
            {
                obj = new WebNotice();
                obj.MenuID = menuID;
                obj.Count = 1;
                obj.AccountMainID = accountMainID;
                result = Add(obj);
            }
            else
            {
                obj.Count++;
                result = Edit(obj);
            }
            return result;
        }

        public WebNotice GetByMenuID(int menuID, int accountMainID)
        {
            return List().Where(a => a.MenuID == menuID && a.AccountMainID == accountMainID).FirstOrDefault();
        }

        public List<int> GetMenuIDByAccountMainID(int accountMainID)
        {
            return List().Where(a => a.AccountMainID == accountMainID && a.Count > 0).Select(a => a.MenuID.Value).ToList();
        }


        public void ClearWebNotice(int accountMainID,string menuToken)
        {
            var menuModel = Factory.Get<IMenuModel>(SystemConst.IOC_Model.MenuModel);
            var menuID = menuModel.GetMenuIDByToken(menuToken);

            string sql = "DELETE dbo.WebNotice WHERE AccountMainID=" + accountMainID + " AND MenuID=" + menuID;
            base.SqlExecute(sql);
        }


        public int GetMenuIDByToken(string token)
        {
            var menuModel = Factory.Get<IMenuModel>(SystemConst.IOC_Model.MenuModel);
            return menuModel.GetMenuIDByToken(token);
        }
    }
}
