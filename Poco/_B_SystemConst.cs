using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public static class SystemConst
    {
        public class Business
        {
            private Business() { }
            public const string key = "abcdefgh";
            public const string iv = "12345678";
            public const string DefaultLogo = "DefaultLogo";
            public const string DefaultHeadImage = "DefaultHeadImage";
            public const string PathBase = "~\\File\\{0}\\Base\\";
            public const string PathAccount = "~\\File\\{0}\\Account\\";
            public const string DefaultGroup = "未分组";
        }

        public class Cache
        {
            private Cache() { }
            public const string SystemUserMenu = "SystemUserMenu";
            public const string Menu = "Menu";
        }

        public class Notice
        {
            private Notice() { }
            public const string NotAuthorized = "无权操作。";
        }

        public class Session
        {
            private Session() { }

            public const string LoginSystemUser = "LoginSystemUser";

            public const string LoginAccount = "LoginAccount";
        }

        public class IOC_Model
        {
            private IOC_Model() { }

            public const string CommonModel = "CommonModel";
            public const string AccountMainModel = "AccountMainModel";
            public const string AccountMainExpandInfoModel = "AccountMainExpandInfoModel";
            public const string SystemUserModel = "SystemUserModel";
            public const string ProvinceModel = "ProvinceModel";
            public const string CityModel = "CityModel";
            public const string DistrictModel = "DistrictModel";
            public const string LookupOptionModel = "LookupOptionModel";
            public const string RoleModel = "RoleModel";
            public const string Account_AccountMainModel = "Account_AccountMainModel";
            public const string AccountModel = "AccountModel";
            public const string ActivateEmailModel = "ActivateEmailModel";
            public const string AutoMessage_AddModel = "AutoMessage_AddModel";
            public const string AutoMessage_KeywordModel = "AutoMessage_KeywordModel";
            public const string AutoMessage_ReplyModel = "AutoMessage_ReplyModel";
            public const string GroupModel = "GroupModel";
            public const string LibraryImageModel = "LibraryImageModel";
            public const string LibraryImageTextModel = "LibraryImageTextModel";
            public const string LibraryTextModel = "LibraryTextModel";
            public const string LibraryVideoModel = "LibraryVideoModel";
            public const string LibraryVoiceModel = "LibraryVoiceModel";
            public const string SystemMessageModel = "SystemMessageModel";
            public const string SystemUserMenuModel = "SystemUserMenuModel";
            public const string SystemUserRoleModel = "SystemUserRoleModel";
            public const string SystemUserMenuOptionModel = "SystemUserMenuOptionModel";
            public const string SystemUserRole_SystemUserMenuModel = "SystemUserRole_SystemUserMenuModel";
            public const string SystemUserRole_OptionModel = "SystemUserRole_OptionModel";
            public const string RoleOptionModel = "RoleOptionModel";
            public const string RoleMenuModel = "RoleMenuModel";
            public const string MenuModel = "MenuModel";
            public const string MenuOptionModel = "MenuOptionModel";
            public const string UserModel = "UserModel";
            public const string ClientInfoModel = "ClientInfoModel";
            public const string Account_UserModel = "Account_UserModel";
            public const string UserLoginInfoModel = "UserLoginInfoModel";
            
            




        }
    }
}
