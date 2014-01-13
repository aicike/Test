using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MicroSite.Admin
{
    public partial class Login : BasePage_Admin
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string superAdminStr = System.Configuration.ConfigurationManager.AppSettings["SuperAdmin"];
            if (!string.IsNullOrEmpty(superAdminStr))
            {
                string l_loginName = superAdminStr.Split('|')[0];
                string l_loginPwd = superAdminStr.Split('|')[1];
                string loginName = txtLoginName.Text;
                string loginPwd = txtLoginPwd.Text;
                if (l_loginName == loginName && l_loginPwd == loginPwd)
                {
                    LoginAccount = new Poco.Account() { IsSuperAdmin =true};
                    Response.Redirect("/Admin/Home.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('用户名或密码错误')", true);
                }
            }
        }
    }
}