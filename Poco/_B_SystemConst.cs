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
        public static string MicroSiteHostName = System.Configuration.ConfigurationManager.AppSettings["HostName"];
        public static string IntegrationPathBase = System.Configuration.ConfigurationManager.AppSettings["IntegrationBase"];
        public static string IntegrationPathAccount = System.Configuration.ConfigurationManager.AppSettings["IntegrationAccount"];
        public static string IntegrationPathFileLibrary = System.Configuration.ConfigurationManager.AppSettings["IntegrationLibrary"];
        public static string IntegrationOrderTemporary = System.Configuration.ConfigurationManager.AppSettings["IntegrationOrderTemporary"];
        public static string PlatformName = System.Configuration.ConfigurationManager.AppSettings["PlatformName"];

        
        /// <summary>
        /// 微网站是否集成web项目
        /// </summary>
        public static bool IsIntegrationWebProject = System.Configuration.ConfigurationManager.AppSettings["IsIntegrationWebProject"] == "true" ? true : false;
        /// <summary>
        /// 微网站集成的主网站项目地址
        /// </summary>
        public static string IntegrationWebUrl = System.Configuration.ConfigurationManager.AppSettings["IntegrationWebUrl"];

        /// <summary>
        /// 判断项目是否是独立部署（true），还是云部署（false），
        /// </summary>
        public static bool IsIndependence = System.Configuration.ConfigurationManager.AppSettings["IsIndependence"] == "true" ? true : false;

        /// <summary>
        /// 如果是独立部署，判断是否以集团为单位（true），还是以项目为单位（false）
        /// </summary>
        public static bool IsOrganization = System.Configuration.ConfigurationManager.AppSettings["IsOrganization"] == "true" ? true : false;

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
            public const string PlatformFile = "~/File/PlatformFile/";
            public const string MerchantFile = "~/File/Merchant/{0}/";

        }

        public class Cache
        {
            private Cache() { }
            public const string SystemUserMenu = "SystemUserMenu";
            public const string Menu = "Menu";
            public const string RoleMenu = "RoleMenu";
            public const string RoleOption = "RoleOption";
            public const string MenuOption = "MenuOption";
            public const string LookupOption = "LookupOption";
            public const string AccountMainID = "AccountMainID";
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

            public const string LoginMerchant = "LoginMerchant";

            //public const string LoginAccountOrganization = "LoginAccountOrganization";


            public const string LoginUser = "LoginUser";

            public const string MicroSiteLoginAccount = "MicroSiteLoginUser";

            public const string MicroSiteAccountMain = "MicroSiteAccountMain";

            public const string MicroSiteMenu = "MicroSiteMenu";

            public const string CurrentAccountMainID = "MicroSiteCurrentAccountMainID";

            public const string IsMicroSiteSuperAdmin = "IsMicroSiteSuperAdmin";
        }

        public class Menu
        {
            private Menu() { }

            //平台
            public const string SystemUserHome = "SystemUserHome";
            public const string AccountMainManage = "AccountMainManage";
            public const string Role = "Role";
            public const string SystemUser = "SystemUser";
            public const string SystemUserRole = "SystemUserRole";
            public const string Tutor = "Tutor";
            public const string LifeSkill = "LifeSkill";
            public const string Recipes = "Recipes";
            public const string Unlock = "Unlock";
            public const string Move = "Move";
            public const string PipelineDredge = "PipelineDredge";




            //售楼部
            public const string Home = "Home";
            public const string UserManage = "UserManage";
            public const string Message = "Message";
            public const string LibraryImage = "LibraryImage";
            public const string LibraryVoice = "LibraryVoice";
            public const string LibraryVideo = "LibraryVideo";
            public const string LibraryImageText = "LibraryImageText";
            public const string Account = "Account";
            public const string HousesMange = "HousesMange";
            public const string Set = "Set";
            public const string Classify = "Classify";
            public const string Product = "Product";
            public const string OrderMType = "OrderMType";
            public const string Order = "Order";
            public const string Task = "Task";
            public const string Character = "Character";
            public const string VipInfo = "VipInfo";
            public const string CardInfo = "CardInfo";
            public const string InstantMes = "InstantMes";
            public const string Holiday = "Holiday";
            public const string HouseInfo = "HouseInfo";
            public const string HouseType = "HouseType";
            public const string HouseInfoDetail = "HouseInfoDetail";
            public const string AppWaitImg = "AppWaitImg";
            public const string KeywordMessage = "KeywordMessage";
            public const string BasisSet = "BasisSet";
            public const string SurveyMain = "SurveyMain";
            public const string AppAdvertorialAccount = "AppAdvertorialAccount";
            public const string AppAdvertorial = "AppAdvertorial";
            public const string SalesMessage = "SalesMessage";
            public const string ActivityInfo = "ActivityInfo";
            public const string Panorama = "Panorama";
            public const string MicroOrder = "MicroOrder";
            public const string AccountMain = "AccountMain";
            public const string AutoMessageReply = "AutoMessageReply";
            public const string LotteryDish = "LotteryDish";
            public const string PropertyHouse = "PropertyHouse";
            public const string HouseInfo_Property = "HouseInfo_Property";
            public const string MicroSiteSet = "MicroSiteSet";
            public const string PropertyFeeInfo = "PropertyFeeInfo";
            public const string RepairInfo = "RepairInfo";
            public const string Complaint = "Complaint";
            public const string Advertising = "Advertising";
            public const string Repairchargeso = "Repairchargeso";
            public const string RentalHouse = "RentalHouse";
            public const string ExpressCollection = "ExpressCollection";
            public const string ParkingFee = "ParkingFee";
            
            public const string AboutUS = "AboutUS";
            

            //Action
            public const string History = "History";
            public const string Index = "Index";
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
            public const string AccountRoleModel = "AccountRoleModel";
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
            public const string ProductImgModel = "ProductImgModel";
            public const string OrderMTypeModel = "OrderMTypeModel";
            public const string OrderUserInfoModel = "OrderUserInfoModel";
            public const string OrderModel = "OrderModel";
            public const string OrderMIntermediateModel = "OrderMIntermediateModel";
            public const string OrderDetailModel = "OrderDetailModel";
            public const string HolidayModel = "HolidayModel";

            public const string TaskRuleModel = "TaskRuleModel";
            public const string TaskModel = "TaskModel";
            public const string TaskDetailModel = "TaskDetailModel";

            public const string ConversationDetailedModel = "ConversationDetailedModel";
            public const string MessageGroupChatModel = "MessageGroupChatModel";

            public const string ReportFormPowerModel = "ReportFormPowerModel";
            public const string VipInfoModel = "VipInfoModel";
            public const string CardInfoModel = "CardInfoModel";
            public const string CardPrefixModel = "CardPrefixModel";
            public const string VIPInfoExpenseDetailModel = "VIPInfoExpenseDetailModel";
            public const string FeedbackModel = "FeedbackModel";

            public const string SurveyMainModel = "SurveyMainModel";
            public const string SurveyTroubleModel = "SurveyTroubleModel";
            public const string SurveyAnswerModel = "SurveyAnswerModel";
            public const string SurveyOptionModel = "SurveyOptionModel";
            public const string ActivityInfoModel = "ActivityInfoModel";
            public const string ActivityInfoParticipatorModel = "ActivityInfoParticipatorModel";
            public const string PanoramaModel = "PanoramaModel";
            public const string UserDeliveryAddressModel = "UserDeliveryAddressModel";
            public const string ServiceModel = "ServiceModel";
            public const string AccountMain_ServiceModel = "AccountMain_ServiceModel";

            public const string AppAdvertorialOperationModel = "AppAdvertorialOperationModel";
            public const string ActivityInfoSignInModel = "ActivityInfoSignInModel";

            public const string UserTagModel = "UserTagModel";
            public const string SurveyAnswerUserModel = "SurveyAnswerUserModel";
            public const string Lottery_dishModel = "Lottery_dishModel";
            public const string Lottery_dish_detailModel = "Lottery_dish_detailModel";
            public const string Lottery_UserModel = "Lottery_UserModel";
            public const string AppAdvertorialBrowseModel = "AppAdvertorialBrowseModel";
            public const string Property_HouseModel = "Property_HouseModel";
            public const string MicroSiteSetInfoModel = "MicroSiteSetInfoModel";
            public const string Property_UserModel = "Property_UserModel";
            public const string PropertyFeeInfoModel = "PropertyFeeInfoModel";
            public const string RepairInfoModel = "RepairInfoModel";
            public const string RepairOperationModel = "RepairOperationModel";
            public const string ComplaintModel = "ComplaintModel";
            public const string ComplaintReplyModel = "ComplaintReplyModel";
            public const string RepairchargesoModel = "RepairchargesoModel";
            public const string RentalHouseModel = "RentalHouseModel";
            public const string ExpressCollectionModel = "ExpressCollectionModel";
            public const string PropertyOrderModel = "PropertyOrderModel";
            public const string PropertyOrderDetailModel = "PropertyOrderDetailModel";
            public const string ParkingFeeModel = "ParkingFeeModel";

            public const string AboutUSModel = "AboutUSModel";


            public const string TutorModel = "TutorModel";
            public const string LifeSkillModel = "LifeSkillModel";
            public const string RecipesModel = "RecipesModel";

            public const string MerchantModel = "MerchantModel";

            public const string M_TakeOutModel = "M_TakeOutModel";
            public const string M_TakeOutDetailModel = "M_TakeOutDetailModel";
            public const string M_MoveModel = "M_MoveModel";
            public const string M_UnlockModel = "M_UnlockModel";
            public const string M_PipelineDredgeModel = "M_PipelineDredgeModel";
            

        }
    }
}
