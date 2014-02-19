namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 10),
                        LoginPwd = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 30),
                        HeadImagePath = c.String(maxLength: 200),
                        Email = c.String(maxLength: 50),
                        AccountStatusID = c.Int(nullable: false),
                        IsActivated = c.Boolean(nullable: false),
                        ParentAccountID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Account", t => t.ParentAccountID)
                .ForeignKey("dbo.LookupOption", t => t.AccountStatusID)
                .Index(t => t.ParentAccountID)
                .Index(t => t.AccountStatusID);
            
            CreateTable(
                "dbo.Account_AccountMain",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.AccountMain",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        HostName = c.String(nullable: false, maxLength: 50),
                        ProvinceID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        DistrictID = c.Int(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 30),
                        LogoImageThumbnailPath = c.String(nullable: false, maxLength: 500),
                        LogoImagePath = c.String(nullable: false, maxLength: 500),
                        AppLogoImagePath = c.String(),
                        AccountStatusID = c.Int(nullable: false),
                        FileLimit = c.Double(nullable: false),
                        SaleAddress = c.String(nullable: false, maxLength: 200),
                        SalePhone = c.String(),
                        SaleMapAddress = c.String(),
                        Lng = c.String(),
                        Lat = c.String(),
                        IOSDownloadPath = c.String(),
                        IOSVersion = c.String(),
                        IOSSellDownloadPath = c.String(),
                        IOSSellVersion = c.String(),
                        AndroidDownloadPath = c.String(),
                        AndroidVersion = c.String(),
                        AndroidSellDownloadPath = c.String(),
                        AndroidSellVersion = c.String(),
                        RandomCode = c.String(nullable: false, maxLength: 50),
                        AppUpdateID = c.Int(),
                        SystemUserID = c.Int(),
                        CreateTime = c.DateTime(nullable: false),
                        IsOrganization = c.Boolean(),
                        ParentAccountMainID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LookupOption", t => t.AccountStatusID)
                .ForeignKey("dbo.SystemUser", t => t.SystemUserID)
                .ForeignKey("dbo.City", t => t.CityID)
                .ForeignKey("dbo.District", t => t.DistrictID)
                .ForeignKey("dbo.Province", t => t.ProvinceID)
                .ForeignKey("dbo.AccountMain", t => t.ParentAccountMainID)
                .Index(t => t.AccountStatusID)
                .Index(t => t.SystemUserID)
                .Index(t => t.CityID)
                .Index(t => t.DistrictID)
                .Index(t => t.ProvinceID)
                .Index(t => t.ParentAccountMainID);
            
            CreateTable(
                "dbo.AccountMain_Service",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        ServiceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.Service", t => t.ServiceID)
                .Index(t => t.AccountMainID)
                .Index(t => t.ServiceID);
            
            CreateTable(
                "dbo.Service",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        IsIndependentSite = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        ShowName = c.String(nullable: false, maxLength: 20),
                        Area = c.String(maxLength: 50),
                        Controller = c.String(maxLength: 50),
                        Action = c.String(maxLength: 50),
                        Order = c.Int(nullable: false),
                        ParentMenuID = c.Int(),
                        IsAppMenu = c.Boolean(nullable: false),
                        Level = c.Int(nullable: false),
                        ServiceID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Menu", t => t.ParentMenuID)
                .ForeignKey("dbo.Service", t => t.ServiceID)
                .Index(t => t.ParentMenuID)
                .Index(t => t.ServiceID);
            
            CreateTable(
                "dbo.MenuOption",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        MenuID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Action = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Menu", t => t.MenuID)
                .Index(t => t.MenuID);
            
            CreateTable(
                "dbo.RoleOption",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                        MenuOptionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MenuOption", t => t.MenuOptionID)
                .ForeignKey("dbo.Role", t => t.RoleID)
                .Index(t => t.MenuOptionID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        ParentRoleID = c.Int(),
                        IsCanDelete = c.Boolean(nullable: false),
                        IsCanFindByUser = c.Boolean(),
                        Token = c.String(maxLength: 50),
                        AccountMainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.Role", t => t.ParentRoleID)
                .Index(t => t.AccountMainID)
                .Index(t => t.ParentRoleID);
            
            CreateTable(
                "dbo.Account_Role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .ForeignKey("dbo.Role", t => t.RoleID)
                .Index(t => t.AccountID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.RoleMenu",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                        MenuID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Menu", t => t.MenuID)
                .ForeignKey("dbo.Role", t => t.RoleID)
                .Index(t => t.MenuID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.AccountMainHouses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        HName = c.String(nullable: false),
                        HIntroduce = c.String(nullable: false, maxLength: 2000),
                        HAddress = c.String(nullable: false, maxLength: 200),
                        HHouseCount = c.Int(nullable: false),
                        HHouseholdsCount = c.Int(nullable: false),
                        HParkingCount = c.Int(nullable: false),
                        HOpeningDate = c.DateTime(nullable: false),
                        HCheckInDate = c.DateTime(nullable: false),
                        HCompletedDate = c.DateTime(nullable: false),
                        HOccupyArea = c.String(),
                        HBuildingArea = c.String(),
                        HGreeningArea = c.String(),
                        PropertyRight = c.Int(nullable: false),
                        HBuildCompany = c.String(nullable: false, maxLength: 200),
                        HInvestor = c.String(nullable: false, maxLength: 200),
                        BuildingType = c.String(nullable: false),
                        Decoration = c.String(nullable: false),
                        Traffic = c.String(nullable: false, maxLength: 1000),
                        ProPertyCom = c.String(nullable: false, maxLength: 200),
                        PropetyFee = c.String(nullable: false, maxLength: 200),
                        PreSalePermit = c.String(nullable: false, maxLength: 2000),
                        SalesState = c.Boolean(nullable: false),
                        TelSales = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.AccountMainHouseInfoDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainHouseInfoID = c.Int(nullable: false),
                        AccountMainHouseID = c.Int(nullable: false),
                        Layer = c.Int(nullable: false),
                        RoomNumber = c.String(nullable: false),
                        AccountMainHouseTypeID = c.Int(nullable: false),
                        BuildingArea = c.Double(nullable: false),
                        RealityArea = c.Double(nullable: false),
                        GongTan = c.Double(nullable: false),
                        EnumSoldStateID = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccountMainHouses_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMainHouseInfo", t => t.AccountMainHouseInfoID)
                .ForeignKey("dbo.AccountMainHouses", t => t.AccountMainHouses_ID)
                .ForeignKey("dbo.AccountMainHouseType", t => t.AccountMainHouseTypeID)
                .ForeignKey("dbo.LookupOption", t => t.EnumSoldStateID)
                .Index(t => t.AccountMainHouseInfoID)
                .Index(t => t.AccountMainHouses_ID)
                .Index(t => t.AccountMainHouseTypeID)
                .Index(t => t.EnumSoldStateID);
            
            CreateTable(
                "dbo.AccountMainHouseInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainHousessID = c.Int(nullable: false),
                        Building = c.String(nullable: false),
                        Cell = c.String(nullable: false),
                        NumberOfLayers = c.Int(nullable: false),
                        NumberOfTheElevator = c.Int(nullable: false),
                        NumberOfFamily = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMainHouses", t => t.AccountMainHousessID)
                .Index(t => t.AccountMainHousessID);
            
            CreateTable(
                "dbo.AccountMainHouseType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        HName = c.String(nullable: false),
                        HouseTypeImagePath = c.String(nullable: false, maxLength: 500),
                        HouseTypeDescription = c.String(nullable: false, maxLength: 200),
                        AccountMainHousesID = c.Int(nullable: false),
                        AccountMain_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMainHouses", t => t.AccountMainHousesID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMain_ID)
                .Index(t => t.AccountMainHousesID)
                .Index(t => t.AccountMain_ID);
            
            CreateTable(
                "dbo.LookupOption",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        LookupID = c.Int(nullable: false),
                        Token = c.String(nullable: false, maxLength: 50),
                        Value = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Lookup", t => t.LookupID)
                .Index(t => t.LookupID);
            
            CreateTable(
                "dbo.ClientInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        IMEI = c.String(maxLength: 16),
                        EnumClientSystemTypeID = c.Int(),
                        SetupTiem = c.DateTime(),
                        EnumClientUserTypeID = c.Int(),
                        Tag = c.String(maxLength: 50),
                        ClientID = c.String(maxLength: 50),
                        EntityID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LookupOption", t => t.EnumClientSystemTypeID)
                .ForeignKey("dbo.LookupOption", t => t.EnumClientUserTypeID)
                .Index(t => t.EnumClientSystemTypeID)
                .Index(t => t.EnumClientUserTypeID);
            
            CreateTable(
                "dbo.KeywordAutoMessage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        EnumMessageTypeID = c.Int(nullable: false),
                        TextReply = c.String(maxLength: 4000),
                        MessageID = c.Int(nullable: false),
                        AutoMessage_KeywordID = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AutoMessage_Keyword", t => t.AutoMessage_KeywordID, cascadeDelete: true)
                .ForeignKey("dbo.LookupOption", t => t.EnumMessageTypeID)
                .Index(t => t.AutoMessage_KeywordID)
                .Index(t => t.EnumMessageTypeID);
            
            CreateTable(
                "dbo.AutoMessage_Keyword",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        RuleName = c.String(nullable: false, maxLength: 30),
                        RuleNo = c.Int(nullable: false),
                        FullRuleNo = c.String(nullable: false, maxLength: 50),
                        AccountMainID = c.Int(nullable: false),
                        ParentAutoMessage_KeywordID = c.Int(),
                        AccountMainHousesID = c.Int(nullable: false),
                        IsFistAutoMessage = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.AccountMainHouses", t => t.AccountMainHousesID)
                .ForeignKey("dbo.AutoMessage_Keyword", t => t.ParentAutoMessage_KeywordID)
                .Index(t => t.AccountMainID)
                .Index(t => t.AccountMainHousesID)
                .Index(t => t.ParentAutoMessage_KeywordID);
            
            CreateTable(
                "dbo.AutoMessage_User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        SendTime = c.DateTime(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        AutoMessage_KeywordID = c.Int(),
                        Question = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.AutoMessage_Keyword", t => t.AutoMessage_KeywordID)
                .Index(t => t.AccountMainID)
                .Index(t => t.AutoMessage_KeywordID);
            
            CreateTable(
                "dbo.Keyword",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AutoMessage_KeywordID = c.Int(nullable: false),
                        Token = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AutoMessage_Keyword", t => t.AutoMessage_KeywordID, cascadeDelete: true)
                .Index(t => t.AutoMessage_KeywordID);
            
            CreateTable(
                "dbo.Lookup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Token = c.String(nullable: false, maxLength: 50),
                        Value = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SystemUser",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 10),
                        Email = c.String(nullable: false, maxLength: 50),
                        LoginPwd = c.String(nullable: false, maxLength: 100),
                        HeadImage = c.String(maxLength: 500),
                        Phone = c.String(maxLength: 20),
                        AccountStatusID = c.Int(nullable: false),
                        SystemUserRoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LookupOption", t => t.AccountStatusID)
                .ForeignKey("dbo.SystemUserRole", t => t.SystemUserRoleID)
                .Index(t => t.AccountStatusID)
                .Index(t => t.SystemUserRoleID);
            
            CreateTable(
                "dbo.SystemUserRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        ParentSystemUserRoleID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SystemUserRole", t => t.ParentSystemUserRoleID)
                .Index(t => t.ParentSystemUserRoleID);
            
            CreateTable(
                "dbo.SystemUserRole_Option",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        SystemUserRoleID = c.Int(nullable: false),
                        SystemUserMenuOptionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SystemUserRole", t => t.SystemUserRoleID)
                .ForeignKey("dbo.SystemUserMenuOption", t => t.SystemUserMenuOptionID)
                .Index(t => t.SystemUserRoleID)
                .Index(t => t.SystemUserMenuOptionID);
            
            CreateTable(
                "dbo.SystemUserMenuOption",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        SystemUserMenuID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Action = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SystemUserMenu", t => t.SystemUserMenuID)
                .Index(t => t.SystemUserMenuID);
            
            CreateTable(
                "dbo.SystemUserMenu",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Area = c.String(maxLength: 50),
                        Controller = c.String(nullable: false, maxLength: 50),
                        Action = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                        ParentMenuID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SystemUserMenu", t => t.ParentMenuID)
                .Index(t => t.ParentMenuID);
            
            CreateTable(
                "dbo.SystemUserRole_SystemUserMenu",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        SystemUserRoleID = c.Int(nullable: false),
                        SystemUserMenuID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SystemUserMenu", t => t.SystemUserMenuID)
                .ForeignKey("dbo.SystemUserRole", t => t.SystemUserRoleID)
                .Index(t => t.SystemUserMenuID)
                .Index(t => t.SystemUserRoleID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(maxLength: 10),
                        Phone = c.String(maxLength: 20),
                        SEX = c.Int(),
                        Age = c.Int(),
                        IDCard = c.String(maxLength: 18),
                        Address = c.String(maxLength: 200),
                        AccountStatusID = c.Int(nullable: false),
                        IdentityCard = c.String(maxLength: 30),
                        AccountMainID = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UserLoginInfoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.LookupOption", t => t.AccountStatusID)
                .ForeignKey("dbo.UserLoginInfo", t => t.UserLoginInfoID)
                .Index(t => t.AccountMainID)
                .Index(t => t.AccountStatusID)
                .Index(t => t.UserLoginInfoID);
            
            CreateTable(
                "dbo.Account_User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        GroupID = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .ForeignKey("dbo.Group", t => t.GroupID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.AccountID)
                .Index(t => t.GroupID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        GroupName = c.String(nullable: false, maxLength: 20),
                        AccountID = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        IsDefaultGroup = c.Boolean(nullable: false),
                        IsCanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        TextContent = c.String(nullable: false),
                        EnumMessageSendDirectionID = c.Int(nullable: false),
                        EnumMessageTypeID = c.Int(nullable: false),
                        FromAccountID = c.Int(),
                        FromUserID = c.Int(),
                        ToAccountID = c.Int(),
                        ToUserID = c.Int(),
                        SendTime = c.DateTime(nullable: false),
                        IsReceive = c.Boolean(nullable: false),
                        ReceiveTime = c.DateTime(nullable: false),
                        FileUrl = c.String(),
                        voiceMP3Url = c.String(),
                        FileLength = c.String(),
                        LibraryImageTextsID = c.Int(),
                        ConversationID = c.Int(nullable: false),
                        CType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Conversation", t => t.ConversationID)
                .ForeignKey("dbo.LibraryImageText", t => t.LibraryImageTextsID)
                .ForeignKey("dbo.Account", t => t.FromAccountID)
                .ForeignKey("dbo.User", t => t.FromUserID)
                .ForeignKey("dbo.Account", t => t.ToAccountID)
                .ForeignKey("dbo.User", t => t.ToUserID)
                .Index(t => t.ConversationID)
                .Index(t => t.LibraryImageTextsID)
                .Index(t => t.FromAccountID)
                .Index(t => t.FromUserID)
                .Index(t => t.ToAccountID)
                .Index(t => t.ToUserID);
            
            CreateTable(
                "dbo.Conversation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        CType = c.Int(nullable: false),
                        Cname = c.String(),
                        Cimg = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.ConversationDetailed",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        ConversationID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        UserType = c.Int(nullable: false),
                        CType = c.Int(nullable: false),
                        SetDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.Conversation", t => t.ConversationID)
                .Index(t => t.AccountMainID)
                .Index(t => t.ConversationID);
            
            CreateTable(
                "dbo.PendingMessages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        MSD = c.String(),
                        EnumMessageTypeID = c.Int(nullable: false),
                        FromAccountID = c.Int(),
                        FromUserID = c.Int(),
                        ToAccountID = c.Int(),
                        ToUserID = c.Int(),
                        SendTime = c.DateTime(nullable: false),
                        Content = c.String(),
                        FileUrl = c.String(),
                        voiceMP3Url = c.String(),
                        FileLength = c.String(),
                        MessageID = c.Int(nullable: false),
                        LibraryImageTextsID = c.Int(),
                        ConversationID = c.Int(nullable: false),
                        CType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Conversation", t => t.ConversationID)
                .ForeignKey("dbo.Account", t => t.FromAccountID)
                .ForeignKey("dbo.User", t => t.FromUserID)
                .ForeignKey("dbo.LibraryImageText", t => t.LibraryImageTextsID)
                .ForeignKey("dbo.Message", t => t.MessageID)
                .ForeignKey("dbo.Account", t => t.ToAccountID)
                .ForeignKey("dbo.User", t => t.ToUserID)
                .Index(t => t.ConversationID)
                .Index(t => t.FromAccountID)
                .Index(t => t.FromUserID)
                .Index(t => t.LibraryImageTextsID)
                .Index(t => t.MessageID)
                .Index(t => t.ToAccountID)
                .Index(t => t.ToUserID);
            
            CreateTable(
                "dbo.LibraryImageText",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        ImagePath = c.String(nullable: false, maxLength: 500),
                        Summary = c.String(maxLength: 200),
                        Content = c.String(nullable: false),
                        LibraryImageTextParentID = c.Int(),
                        AccountMainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.LibraryImageText", t => t.LibraryImageTextParentID)
                .Index(t => t.AccountMainID)
                .Index(t => t.LibraryImageTextParentID);
            
            CreateTable(
                "dbo.MessageGroupChat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        MessageID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        UserType = c.Int(nullable: false),
                        SID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Message", t => t.MessageID)
                .Index(t => t.MessageID);
            
            CreateTable(
                "dbo.SystemMessage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Message = c.String(nullable: false, maxLength: 500),
                        SendDate = c.DateTime(nullable: false),
                        AccountID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.AccountID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.UserDeliveryAddress",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        ProvinceID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        DistrictID = c.Int(nullable: false),
                        Address = c.String(maxLength: 300),
                        Receiver = c.String(maxLength: 10),
                        RPhone = c.String(),
                        TelePhone = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.City", t => t.CityID)
                .ForeignKey("dbo.District", t => t.DistrictID)
                .ForeignKey("dbo.Province", t => t.ProvinceID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.AccountMainID)
                .Index(t => t.CityID)
                .Index(t => t.DistrictID)
                .Index(t => t.ProvinceID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        ProvinceID = c.Int(nullable: false),
                        ZipCode = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Province", t => t.ProvinceID)
                .Index(t => t.ProvinceID);
            
            CreateTable(
                "dbo.District",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        CityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.City", t => t.CityID)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.OrderUserInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        ProvinceID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        DistrictID = c.Int(nullable: false),
                        Address = c.String(),
                        Receiver = c.String(),
                        RPhone = c.String(),
                        TelePhone = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.City", t => t.CityID)
                .ForeignKey("dbo.District", t => t.DistrictID)
                .ForeignKey("dbo.Province", t => t.ProvinceID)
                .Index(t => t.AccountMainID)
                .Index(t => t.CityID)
                .Index(t => t.DistrictID)
                .Index(t => t.ProvinceID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        OrderNum = c.String(),
                        OrderUserID = c.Int(nullable: false),
                        OrderUserType = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        Payment = c.DateTime(),
                        BeginDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        OrderUserInfoID = c.Int(nullable: false),
                        Remark = c.String(),
                        Price = c.Double(nullable: false),
                        status = c.Int(nullable: false),
                        DeliveryType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.OrderUserInfo", t => t.OrderUserInfoID)
                .Index(t => t.AccountMainID)
                .Index(t => t.OrderUserInfoID);
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        ProductName = c.String(),
                        ProductImg = c.String(),
                        ProductType = c.String(),
                        Price = c.Double(nullable: false),
                        Count = c.Int(nullable: false),
                        Specification = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.Order", t => t.OrderID)
                .ForeignKey("dbo.Product", t => t.ProductID)
                .Index(t => t.AccountMainID)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.OrderMIntermediate",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        OrderID = c.Int(nullable: false),
                        MTypeName = c.String(),
                        MTypeDateCnt = c.Int(nullable: false),
                        MTypeCount = c.Int(nullable: false),
                        surplusDay = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Order", t => t.OrderID)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.Province",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserLoginInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 10),
                        Address = c.String(maxLength: 50),
                        Phone = c.String(maxLength: 20),
                        HeadImagePath = c.String(maxLength: 500),
                        IdentityCard = c.String(maxLength: 30),
                        Email = c.String(maxLength: 50),
                        LoginPwd = c.String(nullable: false, maxLength: 100),
                        FindPwdCode = c.String(),
                        FindPwdTime = c.DateTime(),
                        FindPwdValidity = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.VIPInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        UserID = c.Int(),
                        CardInfoID = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        VIPType = c.Int(),
                        score = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.CardInfo", t => t.CardInfoID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.AccountMainID)
                .Index(t => t.CardInfoID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.CardInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        CardPrefixID = c.Int(nullable: false),
                        CardNum = c.String(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreateDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.CardPrefix", t => t.CardPrefixID)
                .Index(t => t.AccountMainID)
                .Index(t => t.CardPrefixID);
            
            CreateTable(
                "dbo.CardPrefix",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        PrefixName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.VIPInfoExpenseDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        ExpenseDate = c.DateTime(nullable: false),
                        ExpensePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VIPInfoID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        EnumVIPOperate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .ForeignKey("dbo.User", t => t.UserID)
                .ForeignKey("dbo.VIPInfo", t => t.VIPInfoID)
                .Index(t => t.AccountID)
                .Index(t => t.UserID)
                .Index(t => t.VIPInfoID);
            
            CreateTable(
                "dbo.ActivityInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        Remarks = c.String(nullable: false),
                        AccountID = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        EnrollEndDate = c.DateTime(nullable: false),
                        ActivityStratDate = c.String(nullable: false),
                        MaxCount = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.ActivityInfoParticipator",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        ActivityInfoID = c.Int(nullable: false),
                        EnumAdvertorialUType = c.Int(),
                        UserID = c.Int(),
                        Phone = c.String(nullable: false, maxLength: 15),
                        Name = c.String(maxLength: 30),
                        JoinDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ActivityInfo", t => t.ActivityInfoID)
                .Index(t => t.ActivityInfoID);
            
            CreateTable(
                "dbo.AppAdvertorial",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        stick = c.Int(nullable: false),
                        Sort = c.Int(nullable: false),
                        Content = c.String(),
                        Depict = c.String(nullable: false, maxLength: 500),
                        IssueDate = c.DateTime(nullable: false),
                        MainImagPath = c.String(nullable: false),
                        AppShowImagePath = c.String(),
                        MinImagePath = c.String(),
                        EnumAdvertorialUType = c.Int(nullable: false),
                        ContentURL = c.String(),
                        EnumAdverTorialType = c.Int(nullable: false),
                        EnumAdverURLType = c.Int(),
                        BrowseCnt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.AppAdvertorialOperation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AppAdvertorialID = c.Int(nullable: false),
                        EnumAdvertorialUType = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        ForwardCnt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AppAdvertorial", t => t.AppAdvertorialID)
                .Index(t => t.AppAdvertorialID);
            
            CreateTable(
                "dbo.AppUpdate",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.AppWaitImg",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        ImgPath = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.AutoMessage_Add",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Content = c.String(maxLength: 500),
                        AccountMainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.AutoMessage_Reply",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Content = c.String(nullable: false, maxLength: 4000),
                        AccountMainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.Classify",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Depict = c.String(maxLength: 20),
                        ImgPath = c.String(),
                        ParentID = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        Sort = c.Int(nullable: false),
                        Subordinate = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Specification = c.String(),
                        Price = c.Double(nullable: false),
                        Freight = c.Double(nullable: false),
                        Introduction = c.String(),
                        LastSetDate = c.String(),
                        Status = c.Int(nullable: false),
                        ClassifyID = c.Int(nullable: false),
                        file1 = c.String(),
                        file2 = c.String(),
                        file3 = c.String(),
                        file4 = c.String(),
                        file5 = c.String(),
                        file6 = c.String(),
                        file7 = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.Classify", t => t.ClassifyID)
                .Index(t => t.AccountMainID)
                .Index(t => t.ClassifyID);
            
            CreateTable(
                "dbo.ProductImg",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        PImgOriginal = c.String(),
                        PImgMini = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.ProductID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Feedback",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Content = c.String(),
                        contact = c.String(),
                        client = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.Holiday",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        HoliDayValue = c.DateTime(nullable: false),
                        Remark = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.LibraryImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        FileName = c.String(nullable: false, maxLength: 50),
                        FilePath = c.String(nullable: false, maxLength: 500),
                        AccountMainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.LibraryText",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 10),
                        Content = c.String(nullable: false, maxLength: 4000),
                        AccountMainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.LibraryVideo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        FileName = c.String(nullable: false, maxLength: 50),
                        FilePath = c.String(nullable: false, maxLength: 500),
                        FileImgPath = c.String(),
                        FileLength = c.String(),
                        AccountMainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.LibraryVoice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        FileName = c.String(nullable: false, maxLength: 50),
                        FilePath = c.String(nullable: false, maxLength: 500),
                        FileMp3Path = c.String(),
                        FileLength = c.String(),
                        AccountMainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.OrderMType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(maxLength: 30),
                        DateCnt = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.Panorama",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        FullImage = c.String(maxLength: 500),
                        ImageRight = c.String(maxLength: 500),
                        ImageLeft = c.String(maxLength: 500),
                        ImageTop = c.String(maxLength: 500),
                        ImageBottom = c.String(maxLength: 500),
                        ImageBlack = c.String(maxLength: 500),
                        ImageFront = c.String(maxLength: 500),
                        AccountMainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.PushMsg",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Text = c.String(),
                        EnumMessageType = c.Int(nullable: false),
                        LibraryID = c.Int(),
                        PushTime = c.DateTime(nullable: false),
                        PushStatus = c.Int(nullable: false),
                        EnumPushType = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.PushMsgDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        PushMsgID = c.Int(nullable: false),
                        EnumClientUserType = c.Int(nullable: false),
                        ReceiveID = c.Int(nullable: false),
                        ReceiveTime = c.DateTime(),
                        EnumReceiveStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PushMsg", t => t.PushMsgID)
                .Index(t => t.PushMsgID);
            
            CreateTable(
                "dbo.ReportFormPower",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        EunmReportID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.SurveyMain",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        SurveyTitle = c.String(nullable: false, maxLength: 50),
                        SurveyRemarks = c.String(nullable: false, maxLength: 500),
                        AccountID = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        EnumSurveyMainType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountID)
                .Index(t => t.AccountMainID);
            
            CreateTable(
                "dbo.SurveyTrouble",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        SurveyMainID = c.Int(nullable: false),
                        TroubleNumber = c.Int(nullable: false),
                        TroubleName = c.String(nullable: false, maxLength: 50),
                        EnumTroubleType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SurveyMain", t => t.SurveyMainID)
                .Index(t => t.SurveyMainID);
            
            CreateTable(
                "dbo.SurveyAnswer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        SurveyTroubleID = c.Int(nullable: false),
                        SurveyOptionID = c.Int(),
                        Content = c.String(maxLength: 500),
                        CreateDate = c.DateTime(nullable: false),
                        UserName = c.String(),
                        UserID = c.Int(),
                        UserType = c.Int(),
                        UserCode = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SurveyOption", t => t.SurveyOptionID)
                .ForeignKey("dbo.SurveyTrouble", t => t.SurveyTroubleID)
                .Index(t => t.SurveyOptionID)
                .Index(t => t.SurveyTroubleID);
            
            CreateTable(
                "dbo.SurveyOption",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        SurveyTroubleID = c.Int(nullable: false),
                        Option = c.String(nullable: false, maxLength: 50),
                        Fraction = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SurveyTrouble", t => t.SurveyTroubleID)
                .Index(t => t.SurveyTroubleID);
            
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        CreateAccountID = c.Int(nullable: false),
                        ReceiverAccountID = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        EnumTaskStatus = c.Int(nullable: false),
                        TaskSpecification = c.String(maxLength: 4000),
                        Quantity = c.Double(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.Account", t => t.CreateAccountID)
                .ForeignKey("dbo.Account", t => t.ReceiverAccountID)
                .Index(t => t.AccountMainID)
                .Index(t => t.CreateAccountID)
                .Index(t => t.ReceiverAccountID);
            
            CreateTable(
                "dbo.TaskDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        TaskID = c.Int(nullable: false),
                        Content = c.String(maxLength: 4000),
                        CreateDate = c.DateTime(nullable: false),
                        Quantity = c.Double(),
                        AccountID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .ForeignKey("dbo.Task", t => t.TaskID)
                .Index(t => t.AccountID)
                .Index(t => t.TaskID);
            
            CreateTable(
                "dbo.TaskAccount",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        TaskDetailID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .ForeignKey("dbo.TaskDetail", t => t.TaskDetailID)
                .Index(t => t.AccountID)
                .Index(t => t.TaskDetailID);
            
            CreateTable(
                "dbo.ActivateEmail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 50),
                        SendDate = c.DateTime(nullable: false),
                        AccountID = c.Int(nullable: false),
                        Token = c.String(nullable: false, maxLength: 50),
                        IsUsed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .Index(t => t.AccountID);
            var migrationDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\");
            var ddlSqlFiles = new string[] { "InitialProvince.sql", "Initial.sql" };
            foreach (var file in ddlSqlFiles)
            {
                string scriptText = System.IO.File.ReadAllText(System.IO.Path.Combine(migrationDir, file));
                var commandTexts = scriptText.Split(new string[] { "\r\nGO\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var commandText in commandTexts)
                {
                    if (!String.IsNullOrWhiteSpace(commandText))
                        Sql(commandText);
                }
            }
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActivateEmail", "AccountID", "dbo.Account");
            DropForeignKey("dbo.Account", "AccountStatusID", "dbo.LookupOption");
            DropForeignKey("dbo.Account", "ParentAccountID", "dbo.Account");
            DropForeignKey("dbo.TaskAccount", "TaskDetailID", "dbo.TaskDetail");
            DropForeignKey("dbo.TaskAccount", "AccountID", "dbo.Account");
            DropForeignKey("dbo.TaskDetail", "TaskID", "dbo.Task");
            DropForeignKey("dbo.TaskDetail", "AccountID", "dbo.Account");
            DropForeignKey("dbo.Task", "ReceiverAccountID", "dbo.Account");
            DropForeignKey("dbo.Task", "CreateAccountID", "dbo.Account");
            DropForeignKey("dbo.Task", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.SurveyTrouble", "SurveyMainID", "dbo.SurveyMain");
            DropForeignKey("dbo.SurveyAnswer", "SurveyTroubleID", "dbo.SurveyTrouble");
            DropForeignKey("dbo.SurveyOption", "SurveyTroubleID", "dbo.SurveyTrouble");
            DropForeignKey("dbo.SurveyAnswer", "SurveyOptionID", "dbo.SurveyOption");
            DropForeignKey("dbo.SurveyMain", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.SurveyMain", "AccountID", "dbo.Account");
            DropForeignKey("dbo.ReportFormPower", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.PushMsgDetail", "PushMsgID", "dbo.PushMsg");
            DropForeignKey("dbo.PushMsg", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.PushMsg", "AccountID", "dbo.Account");
            DropForeignKey("dbo.Panorama", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.OrderMType", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.LibraryVoice", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.LibraryVideo", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.LibraryText", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.LibraryImage", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Holiday", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Feedback", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.ProductImg", "ProductID", "dbo.Product");
            DropForeignKey("dbo.OrderDetail", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Product", "ClassifyID", "dbo.Classify");
            DropForeignKey("dbo.Product", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Classify", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.AutoMessage_Reply", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.AutoMessage_Add", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.AppWaitImg", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.AppUpdate", "ID", "dbo.AccountMain");
            DropForeignKey("dbo.AppAdvertorialOperation", "AppAdvertorialID", "dbo.AppAdvertorial");
            DropForeignKey("dbo.AppAdvertorial", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.ActivityInfoParticipator", "ActivityInfoID", "dbo.ActivityInfo");
            DropForeignKey("dbo.ActivityInfo", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.ActivityInfo", "AccountID", "dbo.Account");
            DropForeignKey("dbo.AccountMain", "ParentAccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.AccountMainHouseType", "AccountMain_ID", "dbo.AccountMain");
            DropForeignKey("dbo.VIPInfo", "UserID", "dbo.User");
            DropForeignKey("dbo.VIPInfoExpenseDetail", "VIPInfoID", "dbo.VIPInfo");
            DropForeignKey("dbo.VIPInfoExpenseDetail", "UserID", "dbo.User");
            DropForeignKey("dbo.VIPInfoExpenseDetail", "AccountID", "dbo.Account");
            DropForeignKey("dbo.VIPInfo", "CardInfoID", "dbo.CardInfo");
            DropForeignKey("dbo.CardInfo", "CardPrefixID", "dbo.CardPrefix");
            DropForeignKey("dbo.CardPrefix", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.CardInfo", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.VIPInfo", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.User", "UserLoginInfoID", "dbo.UserLoginInfo");
            DropForeignKey("dbo.UserDeliveryAddress", "UserID", "dbo.User");
            DropForeignKey("dbo.UserDeliveryAddress", "ProvinceID", "dbo.Province");
            DropForeignKey("dbo.UserDeliveryAddress", "DistrictID", "dbo.District");
            DropForeignKey("dbo.UserDeliveryAddress", "CityID", "dbo.City");
            DropForeignKey("dbo.OrderUserInfo", "ProvinceID", "dbo.Province");
            DropForeignKey("dbo.City", "ProvinceID", "dbo.Province");
            DropForeignKey("dbo.AccountMain", "ProvinceID", "dbo.Province");
            DropForeignKey("dbo.Order", "OrderUserInfoID", "dbo.OrderUserInfo");
            DropForeignKey("dbo.OrderMIntermediate", "OrderID", "dbo.Order");
            DropForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order");
            DropForeignKey("dbo.OrderDetail", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Order", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.OrderUserInfo", "DistrictID", "dbo.District");
            DropForeignKey("dbo.OrderUserInfo", "CityID", "dbo.City");
            DropForeignKey("dbo.OrderUserInfo", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.District", "CityID", "dbo.City");
            DropForeignKey("dbo.AccountMain", "DistrictID", "dbo.District");
            DropForeignKey("dbo.AccountMain", "CityID", "dbo.City");
            DropForeignKey("dbo.UserDeliveryAddress", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.SystemMessage", "UserID", "dbo.User");
            DropForeignKey("dbo.SystemMessage", "AccountID", "dbo.Account");
            DropForeignKey("dbo.Message", "ToUserID", "dbo.User");
            DropForeignKey("dbo.Message", "ToAccountID", "dbo.Account");
            DropForeignKey("dbo.MessageGroupChat", "MessageID", "dbo.Message");
            DropForeignKey("dbo.Message", "FromUserID", "dbo.User");
            DropForeignKey("dbo.Message", "FromAccountID", "dbo.Account");
            DropForeignKey("dbo.PendingMessages", "ToUserID", "dbo.User");
            DropForeignKey("dbo.PendingMessages", "ToAccountID", "dbo.Account");
            DropForeignKey("dbo.PendingMessages", "MessageID", "dbo.Message");
            DropForeignKey("dbo.PendingMessages", "LibraryImageTextsID", "dbo.LibraryImageText");
            DropForeignKey("dbo.Message", "LibraryImageTextsID", "dbo.LibraryImageText");
            DropForeignKey("dbo.LibraryImageText", "LibraryImageTextParentID", "dbo.LibraryImageText");
            DropForeignKey("dbo.LibraryImageText", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.PendingMessages", "FromUserID", "dbo.User");
            DropForeignKey("dbo.PendingMessages", "FromAccountID", "dbo.Account");
            DropForeignKey("dbo.PendingMessages", "ConversationID", "dbo.Conversation");
            DropForeignKey("dbo.Message", "ConversationID", "dbo.Conversation");
            DropForeignKey("dbo.ConversationDetailed", "ConversationID", "dbo.Conversation");
            DropForeignKey("dbo.ConversationDetailed", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Conversation", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.User", "AccountStatusID", "dbo.LookupOption");
            DropForeignKey("dbo.User", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Account_User", "UserID", "dbo.User");
            DropForeignKey("dbo.Group", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Account_User", "GroupID", "dbo.Group");
            DropForeignKey("dbo.Group", "AccountID", "dbo.Account");
            DropForeignKey("dbo.Account_User", "AccountID", "dbo.Account");
            DropForeignKey("dbo.SystemUser", "SystemUserRoleID", "dbo.SystemUserRole");
            DropForeignKey("dbo.SystemUserRole_Option", "SystemUserMenuOptionID", "dbo.SystemUserMenuOption");
            DropForeignKey("dbo.SystemUserRole_SystemUserMenu", "SystemUserRoleID", "dbo.SystemUserRole");
            DropForeignKey("dbo.SystemUserRole_SystemUserMenu", "SystemUserMenuID", "dbo.SystemUserMenu");
            DropForeignKey("dbo.SystemUserMenuOption", "SystemUserMenuID", "dbo.SystemUserMenu");
            DropForeignKey("dbo.SystemUserMenu", "ParentMenuID", "dbo.SystemUserMenu");
            DropForeignKey("dbo.SystemUserRole_Option", "SystemUserRoleID", "dbo.SystemUserRole");
            DropForeignKey("dbo.SystemUserRole", "ParentSystemUserRoleID", "dbo.SystemUserRole");
            DropForeignKey("dbo.SystemUser", "AccountStatusID", "dbo.LookupOption");
            DropForeignKey("dbo.AccountMain", "SystemUserID", "dbo.SystemUser");
            DropForeignKey("dbo.LookupOption", "LookupID", "dbo.Lookup");
            DropForeignKey("dbo.KeywordAutoMessage", "EnumMessageTypeID", "dbo.LookupOption");
            DropForeignKey("dbo.KeywordAutoMessage", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword");
            DropForeignKey("dbo.AutoMessage_Keyword", "ParentAutoMessage_KeywordID", "dbo.AutoMessage_Keyword");
            DropForeignKey("dbo.Keyword", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword");
            DropForeignKey("dbo.AutoMessage_User", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword");
            DropForeignKey("dbo.AutoMessage_User", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.AutoMessage_Keyword", "AccountMainHousesID", "dbo.AccountMainHouses");
            DropForeignKey("dbo.AutoMessage_Keyword", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.ClientInfo", "EnumClientUserTypeID", "dbo.LookupOption");
            DropForeignKey("dbo.ClientInfo", "EnumClientSystemTypeID", "dbo.LookupOption");
            DropForeignKey("dbo.AccountMain", "AccountStatusID", "dbo.LookupOption");
            DropForeignKey("dbo.AccountMainHouseInfoDetail", "EnumSoldStateID", "dbo.LookupOption");
            DropForeignKey("dbo.AccountMainHouseType", "AccountMainHousesID", "dbo.AccountMainHouses");
            DropForeignKey("dbo.AccountMainHouseInfoDetail", "AccountMainHouseTypeID", "dbo.AccountMainHouseType");
            DropForeignKey("dbo.AccountMainHouseInfoDetail", "AccountMainHouses_ID", "dbo.AccountMainHouses");
            DropForeignKey("dbo.AccountMainHouseInfo", "AccountMainHousessID", "dbo.AccountMainHouses");
            DropForeignKey("dbo.AccountMainHouseInfoDetail", "AccountMainHouseInfoID", "dbo.AccountMainHouseInfo");
            DropForeignKey("dbo.AccountMainHouses", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Menu", "ServiceID", "dbo.Service");
            DropForeignKey("dbo.Menu", "ParentMenuID", "dbo.Menu");
            DropForeignKey("dbo.RoleOption", "RoleID", "dbo.Role");
            DropForeignKey("dbo.RoleMenu", "RoleID", "dbo.Role");
            DropForeignKey("dbo.RoleMenu", "MenuID", "dbo.Menu");
            DropForeignKey("dbo.Role", "ParentRoleID", "dbo.Role");
            DropForeignKey("dbo.Role", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Account_Role", "RoleID", "dbo.Role");
            DropForeignKey("dbo.Account_Role", "AccountID", "dbo.Account");
            DropForeignKey("dbo.RoleOption", "MenuOptionID", "dbo.MenuOption");
            DropForeignKey("dbo.MenuOption", "MenuID", "dbo.Menu");
            DropForeignKey("dbo.AccountMain_Service", "ServiceID", "dbo.Service");
            DropForeignKey("dbo.AccountMain_Service", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Account_AccountMain", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Account_AccountMain", "AccountID", "dbo.Account");
            DropIndex("dbo.ActivateEmail", new[] { "AccountID" });
            DropIndex("dbo.Account", new[] { "AccountStatusID" });
            DropIndex("dbo.Account", new[] { "ParentAccountID" });
            DropIndex("dbo.TaskAccount", new[] { "TaskDetailID" });
            DropIndex("dbo.TaskAccount", new[] { "AccountID" });
            DropIndex("dbo.TaskDetail", new[] { "TaskID" });
            DropIndex("dbo.TaskDetail", new[] { "AccountID" });
            DropIndex("dbo.Task", new[] { "ReceiverAccountID" });
            DropIndex("dbo.Task", new[] { "CreateAccountID" });
            DropIndex("dbo.Task", new[] { "AccountMainID" });
            DropIndex("dbo.SurveyTrouble", new[] { "SurveyMainID" });
            DropIndex("dbo.SurveyAnswer", new[] { "SurveyTroubleID" });
            DropIndex("dbo.SurveyOption", new[] { "SurveyTroubleID" });
            DropIndex("dbo.SurveyAnswer", new[] { "SurveyOptionID" });
            DropIndex("dbo.SurveyMain", new[] { "AccountMainID" });
            DropIndex("dbo.SurveyMain", new[] { "AccountID" });
            DropIndex("dbo.ReportFormPower", new[] { "AccountMainID" });
            DropIndex("dbo.PushMsgDetail", new[] { "PushMsgID" });
            DropIndex("dbo.PushMsg", new[] { "AccountMainID" });
            DropIndex("dbo.PushMsg", new[] { "AccountID" });
            DropIndex("dbo.Panorama", new[] { "AccountMainID" });
            DropIndex("dbo.OrderMType", new[] { "AccountMainID" });
            DropIndex("dbo.LibraryVoice", new[] { "AccountMainID" });
            DropIndex("dbo.LibraryVideo", new[] { "AccountMainID" });
            DropIndex("dbo.LibraryText", new[] { "AccountMainID" });
            DropIndex("dbo.LibraryImage", new[] { "AccountMainID" });
            DropIndex("dbo.Holiday", new[] { "AccountMainID" });
            DropIndex("dbo.Feedback", new[] { "AccountMainID" });
            DropIndex("dbo.ProductImg", new[] { "ProductID" });
            DropIndex("dbo.OrderDetail", new[] { "ProductID" });
            DropIndex("dbo.Product", new[] { "ClassifyID" });
            DropIndex("dbo.Product", new[] { "AccountMainID" });
            DropIndex("dbo.Classify", new[] { "AccountMainID" });
            DropIndex("dbo.AutoMessage_Reply", new[] { "AccountMainID" });
            DropIndex("dbo.AutoMessage_Add", new[] { "AccountMainID" });
            DropIndex("dbo.AppWaitImg", new[] { "AccountMainID" });
            DropIndex("dbo.AppUpdate", new[] { "ID" });
            DropIndex("dbo.AppAdvertorialOperation", new[] { "AppAdvertorialID" });
            DropIndex("dbo.AppAdvertorial", new[] { "AccountMainID" });
            DropIndex("dbo.ActivityInfoParticipator", new[] { "ActivityInfoID" });
            DropIndex("dbo.ActivityInfo", new[] { "AccountMainID" });
            DropIndex("dbo.ActivityInfo", new[] { "AccountID" });
            DropIndex("dbo.AccountMain", new[] { "ParentAccountMainID" });
            DropIndex("dbo.AccountMainHouseType", new[] { "AccountMain_ID" });
            DropIndex("dbo.VIPInfo", new[] { "UserID" });
            DropIndex("dbo.VIPInfoExpenseDetail", new[] { "VIPInfoID" });
            DropIndex("dbo.VIPInfoExpenseDetail", new[] { "UserID" });
            DropIndex("dbo.VIPInfoExpenseDetail", new[] { "AccountID" });
            DropIndex("dbo.VIPInfo", new[] { "CardInfoID" });
            DropIndex("dbo.CardInfo", new[] { "CardPrefixID" });
            DropIndex("dbo.CardPrefix", new[] { "AccountMainID" });
            DropIndex("dbo.CardInfo", new[] { "AccountMainID" });
            DropIndex("dbo.VIPInfo", new[] { "AccountMainID" });
            DropIndex("dbo.User", new[] { "UserLoginInfoID" });
            DropIndex("dbo.UserDeliveryAddress", new[] { "UserID" });
            DropIndex("dbo.UserDeliveryAddress", new[] { "ProvinceID" });
            DropIndex("dbo.UserDeliveryAddress", new[] { "DistrictID" });
            DropIndex("dbo.UserDeliveryAddress", new[] { "CityID" });
            DropIndex("dbo.OrderUserInfo", new[] { "ProvinceID" });
            DropIndex("dbo.City", new[] { "ProvinceID" });
            DropIndex("dbo.AccountMain", new[] { "ProvinceID" });
            DropIndex("dbo.Order", new[] { "OrderUserInfoID" });
            DropIndex("dbo.OrderMIntermediate", new[] { "OrderID" });
            DropIndex("dbo.OrderDetail", new[] { "OrderID" });
            DropIndex("dbo.OrderDetail", new[] { "AccountMainID" });
            DropIndex("dbo.Order", new[] { "AccountMainID" });
            DropIndex("dbo.OrderUserInfo", new[] { "DistrictID" });
            DropIndex("dbo.OrderUserInfo", new[] { "CityID" });
            DropIndex("dbo.OrderUserInfo", new[] { "AccountMainID" });
            DropIndex("dbo.District", new[] { "CityID" });
            DropIndex("dbo.AccountMain", new[] { "DistrictID" });
            DropIndex("dbo.AccountMain", new[] { "CityID" });
            DropIndex("dbo.UserDeliveryAddress", new[] { "AccountMainID" });
            DropIndex("dbo.SystemMessage", new[] { "UserID" });
            DropIndex("dbo.SystemMessage", new[] { "AccountID" });
            DropIndex("dbo.Message", new[] { "ToUserID" });
            DropIndex("dbo.Message", new[] { "ToAccountID" });
            DropIndex("dbo.MessageGroupChat", new[] { "MessageID" });
            DropIndex("dbo.Message", new[] { "FromUserID" });
            DropIndex("dbo.Message", new[] { "FromAccountID" });
            DropIndex("dbo.PendingMessages", new[] { "ToUserID" });
            DropIndex("dbo.PendingMessages", new[] { "ToAccountID" });
            DropIndex("dbo.PendingMessages", new[] { "MessageID" });
            DropIndex("dbo.PendingMessages", new[] { "LibraryImageTextsID" });
            DropIndex("dbo.Message", new[] { "LibraryImageTextsID" });
            DropIndex("dbo.LibraryImageText", new[] { "LibraryImageTextParentID" });
            DropIndex("dbo.LibraryImageText", new[] { "AccountMainID" });
            DropIndex("dbo.PendingMessages", new[] { "FromUserID" });
            DropIndex("dbo.PendingMessages", new[] { "FromAccountID" });
            DropIndex("dbo.PendingMessages", new[] { "ConversationID" });
            DropIndex("dbo.Message", new[] { "ConversationID" });
            DropIndex("dbo.ConversationDetailed", new[] { "ConversationID" });
            DropIndex("dbo.ConversationDetailed", new[] { "AccountMainID" });
            DropIndex("dbo.Conversation", new[] { "AccountMainID" });
            DropIndex("dbo.User", new[] { "AccountStatusID" });
            DropIndex("dbo.User", new[] { "AccountMainID" });
            DropIndex("dbo.Account_User", new[] { "UserID" });
            DropIndex("dbo.Group", new[] { "AccountMainID" });
            DropIndex("dbo.Account_User", new[] { "GroupID" });
            DropIndex("dbo.Group", new[] { "AccountID" });
            DropIndex("dbo.Account_User", new[] { "AccountID" });
            DropIndex("dbo.SystemUser", new[] { "SystemUserRoleID" });
            DropIndex("dbo.SystemUserRole_Option", new[] { "SystemUserMenuOptionID" });
            DropIndex("dbo.SystemUserRole_SystemUserMenu", new[] { "SystemUserRoleID" });
            DropIndex("dbo.SystemUserRole_SystemUserMenu", new[] { "SystemUserMenuID" });
            DropIndex("dbo.SystemUserMenuOption", new[] { "SystemUserMenuID" });
            DropIndex("dbo.SystemUserMenu", new[] { "ParentMenuID" });
            DropIndex("dbo.SystemUserRole_Option", new[] { "SystemUserRoleID" });
            DropIndex("dbo.SystemUserRole", new[] { "ParentSystemUserRoleID" });
            DropIndex("dbo.SystemUser", new[] { "AccountStatusID" });
            DropIndex("dbo.AccountMain", new[] { "SystemUserID" });
            DropIndex("dbo.LookupOption", new[] { "LookupID" });
            DropIndex("dbo.KeywordAutoMessage", new[] { "EnumMessageTypeID" });
            DropIndex("dbo.KeywordAutoMessage", new[] { "AutoMessage_KeywordID" });
            DropIndex("dbo.AutoMessage_Keyword", new[] { "ParentAutoMessage_KeywordID" });
            DropIndex("dbo.Keyword", new[] { "AutoMessage_KeywordID" });
            DropIndex("dbo.AutoMessage_User", new[] { "AutoMessage_KeywordID" });
            DropIndex("dbo.AutoMessage_User", new[] { "AccountMainID" });
            DropIndex("dbo.AutoMessage_Keyword", new[] { "AccountMainHousesID" });
            DropIndex("dbo.AutoMessage_Keyword", new[] { "AccountMainID" });
            DropIndex("dbo.ClientInfo", new[] { "EnumClientUserTypeID" });
            DropIndex("dbo.ClientInfo", new[] { "EnumClientSystemTypeID" });
            DropIndex("dbo.AccountMain", new[] { "AccountStatusID" });
            DropIndex("dbo.AccountMainHouseInfoDetail", new[] { "EnumSoldStateID" });
            DropIndex("dbo.AccountMainHouseType", new[] { "AccountMainHousesID" });
            DropIndex("dbo.AccountMainHouseInfoDetail", new[] { "AccountMainHouseTypeID" });
            DropIndex("dbo.AccountMainHouseInfoDetail", new[] { "AccountMainHouses_ID" });
            DropIndex("dbo.AccountMainHouseInfo", new[] { "AccountMainHousessID" });
            DropIndex("dbo.AccountMainHouseInfoDetail", new[] { "AccountMainHouseInfoID" });
            DropIndex("dbo.AccountMainHouses", new[] { "AccountMainID" });
            DropIndex("dbo.Menu", new[] { "ServiceID" });
            DropIndex("dbo.Menu", new[] { "ParentMenuID" });
            DropIndex("dbo.RoleOption", new[] { "RoleID" });
            DropIndex("dbo.RoleMenu", new[] { "RoleID" });
            DropIndex("dbo.RoleMenu", new[] { "MenuID" });
            DropIndex("dbo.Role", new[] { "ParentRoleID" });
            DropIndex("dbo.Role", new[] { "AccountMainID" });
            DropIndex("dbo.Account_Role", new[] { "RoleID" });
            DropIndex("dbo.Account_Role", new[] { "AccountID" });
            DropIndex("dbo.RoleOption", new[] { "MenuOptionID" });
            DropIndex("dbo.MenuOption", new[] { "MenuID" });
            DropIndex("dbo.AccountMain_Service", new[] { "ServiceID" });
            DropIndex("dbo.AccountMain_Service", new[] { "AccountMainID" });
            DropIndex("dbo.Account_AccountMain", new[] { "AccountMainID" });
            DropIndex("dbo.Account_AccountMain", new[] { "AccountID" });
            DropTable("dbo.ActivateEmail");
            DropTable("dbo.TaskAccount");
            DropTable("dbo.TaskDetail");
            DropTable("dbo.Task");
            DropTable("dbo.SurveyOption");
            DropTable("dbo.SurveyAnswer");
            DropTable("dbo.SurveyTrouble");
            DropTable("dbo.SurveyMain");
            DropTable("dbo.ReportFormPower");
            DropTable("dbo.PushMsgDetail");
            DropTable("dbo.PushMsg");
            DropTable("dbo.Panorama");
            DropTable("dbo.OrderMType");
            DropTable("dbo.LibraryVoice");
            DropTable("dbo.LibraryVideo");
            DropTable("dbo.LibraryText");
            DropTable("dbo.LibraryImage");
            DropTable("dbo.Holiday");
            DropTable("dbo.Feedback");
            DropTable("dbo.ProductImg");
            DropTable("dbo.Product");
            DropTable("dbo.Classify");
            DropTable("dbo.AutoMessage_Reply");
            DropTable("dbo.AutoMessage_Add");
            DropTable("dbo.AppWaitImg");
            DropTable("dbo.AppUpdate");
            DropTable("dbo.AppAdvertorialOperation");
            DropTable("dbo.AppAdvertorial");
            DropTable("dbo.ActivityInfoParticipator");
            DropTable("dbo.ActivityInfo");
            DropTable("dbo.VIPInfoExpenseDetail");
            DropTable("dbo.CardPrefix");
            DropTable("dbo.CardInfo");
            DropTable("dbo.VIPInfo");
            DropTable("dbo.UserLoginInfo");
            DropTable("dbo.Province");
            DropTable("dbo.OrderMIntermediate");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Order");
            DropTable("dbo.OrderUserInfo");
            DropTable("dbo.District");
            DropTable("dbo.City");
            DropTable("dbo.UserDeliveryAddress");
            DropTable("dbo.SystemMessage");
            DropTable("dbo.MessageGroupChat");
            DropTable("dbo.LibraryImageText");
            DropTable("dbo.PendingMessages");
            DropTable("dbo.ConversationDetailed");
            DropTable("dbo.Conversation");
            DropTable("dbo.Message");
            DropTable("dbo.Group");
            DropTable("dbo.Account_User");
            DropTable("dbo.User");
            DropTable("dbo.SystemUserRole_SystemUserMenu");
            DropTable("dbo.SystemUserMenu");
            DropTable("dbo.SystemUserMenuOption");
            DropTable("dbo.SystemUserRole_Option");
            DropTable("dbo.SystemUserRole");
            DropTable("dbo.SystemUser");
            DropTable("dbo.Lookup");
            DropTable("dbo.Keyword");
            DropTable("dbo.AutoMessage_User");
            DropTable("dbo.AutoMessage_Keyword");
            DropTable("dbo.KeywordAutoMessage");
            DropTable("dbo.ClientInfo");
            DropTable("dbo.LookupOption");
            DropTable("dbo.AccountMainHouseType");
            DropTable("dbo.AccountMainHouseInfo");
            DropTable("dbo.AccountMainHouseInfoDetail");
            DropTable("dbo.AccountMainHouses");
            DropTable("dbo.RoleMenu");
            DropTable("dbo.Account_Role");
            DropTable("dbo.Role");
            DropTable("dbo.RoleOption");
            DropTable("dbo.MenuOption");
            DropTable("dbo.Menu");
            DropTable("dbo.Service");
            DropTable("dbo.AccountMain_Service");
            DropTable("dbo.AccountMain");
            DropTable("dbo.Account_AccountMain");
            DropTable("dbo.Account");
        }
    }
}
