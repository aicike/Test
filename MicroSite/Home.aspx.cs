using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Web.Security;
using Injection;
using Interface;
using Poco;

namespace MicroSite
{
    public partial class Home : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrentAccountMainID == 0)
                {
                    int accountMainID = 1;
                    var accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
                    var accountMain = accountMainModel.Get(accountMainID);
                    if (accountMain == null)
                    {
                    }
                    else
                    {
                        CurrentAccountMainID = accountMainID;
                    }

                }

                BaseMenu = EnumMenu.Home;
            }
        }
    }
}