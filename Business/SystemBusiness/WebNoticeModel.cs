using System;
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

        public Result Add(string menuToken, int accountMainID)
        {
            var menuID = GetMenuIDByToken(menuToken);
            return Add(menuID, accountMainID);
        }

        public WebNotice GetByMenuID(int menuID, int accountMainID)
        {
            return List().Where(a => a.MenuID == menuID && a.AccountMainID == accountMainID).FirstOrDefault();
        }

        public List<int> GetMenuIDByAccountMainID(int accountMainID, int accountID)
        {
            var messageModel = Factory.Get<IMessageModel>(SystemConst.IOC_Model.MessageModel);


            var list = List().Where(a => a.AccountMainID == accountMainID && a.Count > 0).Select(a => a.MenuID.Value).ToList();
            if (accountID != 0)
            {
                int i = messageModel.List().Where(a => a.ToAccountID == accountID && a.IsReceive == false).Count();

                if (i > 0)
                {
                    var menuID = GetMenuIDByToken("Token_User_Msg");
                    list.Add(menuID);
                }
            }

            return list;
        }


        public void ClearWebNotice(int accountMainID, string menuToken)
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
