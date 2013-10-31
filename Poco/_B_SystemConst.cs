using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public static class SystemConst
    {
        public static string WebUrl = System.Configuration.ConfigurationManager.AppSettings["WebUrl"];
        public static string WebUrlIP = System.Configuration.ConfigurationManager.AppSettings["WebUrlIP"];
        public static string WebTitleRemark = System.Configuration.ConfigurationManager.AppSettings["WebTitle"];

        public class Business
        {
            private Business() { }
            public const string key = "abcdefgh";
            public const string iv = "12345678";
            public const string DefaultLogo = "DefaultLogo";
            public const string DefaultHeadImage = "~/Images/default_Avatar.png";
            public const string PathBase = "~/File/{0}/Base/";
            public const string PathAccount = "~/File/{0}/Account/";
            public const string PathFileLibrary = "~/File/{0}/FileLibrary/";
            public const string DefaultGroup = "未分组";
            public const string BlackListGroup = "黑名单";
            public const string AccountAdmin = "AccountAdmin";
            public const string ThumbnailImage_Width = "1000";
            public const string TimeFomatFull = "yyyy-MM-dd HH:mm:ss";
            public const string WebTitle = "{0} {1} {2}";
        }

        public class Cache
        {
            private Cache() { }
            public const string SystemUserMenu = "SystemUserMenu";
            public const string Menu = "Menu";
            public const string RoleMenu = "RoleMenu";
            public const string RoleOption = "RoleOption";
            public const string LookupOption = "LookupOption";
        }

        public class Notice
        {
            private Notice() { }
            public const string NotAuthorized = "无权操作。";
            public const string MultipleAccountMainAdminAccount = "只能创建一个有效的管理员账号，请先禁用其他管理员账号再添管理员账号加或选择其他角色创建账号。";
        }

        public class Session
        {
            private Session() { }

            public const string LoginSystemUser = "LoginSystemUser";

            public const string LoginAccount = "LoginAccount";
        }

        public class Menu {
            private Menu() { }

            //平台
            public const string SystemUserHome = "SystemUserHome";
            public const string AccountMainManage = "AccountMainManage";
            public const string Role = "Role";
            public const string SystemUser = "SystemUser";
            public const string SystemUserRole = "SystemUserRole";

            //售楼部
            public const string Home = "Home";
            public const string UserManage = "UserManage";
            public const string Message = "Message";
            public const string LibraryImage = "LibraryImage";
            public const string Account = "Account";
            public const string HousesMange = "HousesMange";
            public const string Set = "Set";
            public const string Classify = "Classify";
            public const string Product = "Product";
            public const string OrderMType = "OrderMType";
            
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
            public const string AccountMainHousesModel = "AccountMainHousesModel";
            public const string AccountMainHouseInfoModel = "AccountMainHouseInfoModel";
            public const string AccountMainHouseTypeModel = "AccountMainHouseTypeModel";
            public const string AccountMainHouseInfoDetailModel = "AccountMainHouseInfoDetailModel";
            public const string KeywordModel = "KeywordModel";
            public const string KeywordAutoMessageModel = "KeywordAutoMessageModel";
            public const string PendingMessagesModel = "PendingMessagesModel";
            public const string MessageModel = "MessageModel";
            public const string TemporayInstantMes = "TemporayInstantMesModel";
            public const string PushMsgDetailModel = "PushMsgDetailModel";
            public const string PushMsgModel = "PushMsgModel";
            public const string AutoMessage_UserModel = "AutoMessage_UserModel";
            public const string ConversationModel = "ConversationModel";

            public const string AppAdvertorialModel = "AppAdvertorialModel";
            public const string AppWaitImgModel = "AppWaitImgModel";
            public const string ClassifyModle = "ClassifyModel";
            public const string ProductModel = "ProductModel";
            public const string OrderMTypeModel = "OrderMTypeModel";
        }
    }
}
