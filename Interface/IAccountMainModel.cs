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
        
        Result Add(AccountMain accountMain, HttpPostedFileBase LogoImagePath, int createUserID, HttpPostedFileBase AndroidPathFile, HttpPostedFileBase AndroidSellPathFile,HttpPostedFileBase AppLogoImageFile);

        Result Edit_Permission(AccountMain accountMain, HttpPostedFileBase LogoImagePath, HttpPostedFileBase AndroidPathFile, HttpPostedFileBase AndroidSellPathFile, HttpPostedFileBase AppLogoImageFile, int loginSystemUserID = 0);

        Result Edit_ByAccountMain(AccountMain accountMain, HttpPostedFileBase LogoImagePath, HttpPostedFileBase AndroidPathFile, HttpPostedFileBase AndroidSellPathFile);

        Result Edit_ByAccountMain(AccountMain accountMain);

        /// <summary>
        /// 查询 随机编码是否与AccountMain匹配 true：匹配，false:错误
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <param name="RandomCode"></param>
        /// <returns></returns>
        bool CheckPropertyRandomCode(int AccountMainID, string RandomCode);

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

        AppVersionInfo CheckAppSellVersion(EnumClientSystemType type, int amid, string version);

        Result MicroSite_Add(AccountMain accountMain);

        /// <summary>
        /// 根据组织或集团账号，查找所有该集团下AccountMain
        /// </summary>
        /// <returns></returns>
        IQueryable<AccountMain> ListForOrganization(int organization_accountMainID);
    }
}
