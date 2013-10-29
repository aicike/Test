using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web;
using Poco.Enum;
using Poco.WebAPI_Poco;

namespace Interface
{
    public interface IAccountMainModel : IBaseModel<AccountMain>
    {
        IQueryable<AccountMain> List_Permission(int loginSystemUserID);

        AccountMain Get_Permission(int id, int loginSystemUserID);

        Result Delete_Permission(int id, int loginSystemUserID);

        Result Add(AccountMain accountMain, HttpPostedFileBase LogoImagePath, int createUserID, HttpPostedFileBase AndroidPathFile);

        Result Edit_Permission(AccountMain accountMain, HttpPostedFileBase LogoImagePath, HttpPostedFileBase AndroidPathFile, int loginSystemUserID);

        Result Edit_ByAccountMain(AccountMain accountMain, HttpPostedFileBase LogoImagePath, HttpPostedFileBase AndroidPathFile);

        Result ChangeStatus_Permission(int accountMainID, EnumAccountStatus status, int loginSystemUserID);

        /// <summary>
        /// 严查是否有权限超过数据
        /// </summary>
        /// <param name="accountMainID">数据ID</param>
        /// <param name="loginSystemUserID">授权用户ID（0：为不检查）</param>
        /// <returns>ture:有权 false:无权</returns>
        bool CheckHasPermissions(int loginSystemUserID, params int[] accountMainID);

        double GetFileLimitUseInfo(int accountMainID);

        AccountMainLibraryInfo GetAccountMainLibraryInfo(int accountMainID);

        AppVersionInfo CheckAppVersion(EnumClientSystemType type, int amid, string version);
    }
}
