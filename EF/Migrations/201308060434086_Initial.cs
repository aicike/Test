namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                        Email = c.String(nullable: false, maxLength: 50),
                        RoleID = c.Int(nullable: false),
                        AccountStatusID = c.Int(nullable: false),
                        IsActivated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Role", t => t.RoleID)
                .ForeignKey("dbo.LookupOption", t => t.AccountStatusID)
                .Index(t => t.RoleID)
                .Index(t => t.AccountStatusID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        ParentRoleID = c.Int(),
                        IsCanDelete = c.Boolean(nullable: false),
                        Token = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Role", t => t.ParentRoleID)
                .Index(t => t.ParentRoleID);
            
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
                .ForeignKey("dbo.Role", t => t.RoleID)
                .ForeignKey("dbo.Menu", t => t.MenuID)
                .Index(t => t.RoleID)
                .Index(t => t.MenuID);
            
            CreateTable(
                "dbo.Menu",
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
                .ForeignKey("dbo.Menu", t => t.ParentMenuID)
                .Index(t => t.ParentMenuID);
            
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
                .ForeignKey("dbo.Role", t => t.RoleID)
                .ForeignKey("dbo.MenuOption", t => t.MenuOptionID)
                .Index(t => t.RoleID)
                .Index(t => t.MenuOptionID);
            
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
                        AccountStatusID = c.Int(nullable: false),
                        FileLimit = c.Double(nullable: false),
                        AccountMainExpandInfoID = c.Int(nullable: false),
                        AppUpdateID = c.Int(),
                        SystemUserID = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Province", t => t.ProvinceID)
                .ForeignKey("dbo.District", t => t.DistrictID)
                .ForeignKey("dbo.City", t => t.CityID)
                .ForeignKey("dbo.LookupOption", t => t.AccountStatusID)
                .ForeignKey("dbo.SystemUser", t => t.SystemUserID)
                .Index(t => t.ProvinceID)
                .Index(t => t.DistrictID)
                .Index(t => t.CityID)
                .Index(t => t.AccountStatusID)
                .Index(t => t.SystemUserID);
            
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
                "dbo.AccountMainExpandInfo",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        ProductAddress = c.String(nullable: false, maxLength: 200),
                        SaleAddress = c.String(nullable: false, maxLength: 200),
                        OpeningDate = c.DateTime(nullable: false),
                        CheckInDate = c.DateTime(nullable: false),
                        CompletedDate = c.DateTime(nullable: false),
                        BuildCompany = c.String(nullable: false, maxLength: 100),
                        Investor = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 4000),
                        ProjectSupport = c.String(maxLength: 2000),
                        TrafficInfo = c.String(maxLength: 2000),
                        BuildingMaterials = c.String(maxLength: 2000),
                        FloorInfo = c.String(maxLength: 2000),
                        StallInfo = c.String(maxLength: 2000),
                        OccupyArea = c.Double(),
                        BuildingArea = c.Double(),
                        ProjectSchedule = c.String(maxLength: 4000),
                        PropertyRight = c.Int(nullable: false),
                        HouseholdsCount = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.ID)
                .Index(t => t.ID);
            
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
                .ForeignKey("dbo.SystemUserRole", t => t.SystemUserRoleID)
                .ForeignKey("dbo.SystemUserMenu", t => t.SystemUserMenuID)
                .Index(t => t.SystemUserRoleID)
                .Index(t => t.SystemUserMenuID);
            
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
                "dbo.LibraryVoice",
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
                "dbo.LibraryVideo",
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
                "dbo.LibraryImageText",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        ImagePath = c.String(nullable: false, maxLength: 500),
                        Summary = c.String(nullable: false, maxLength: 200),
                        Content = c.String(nullable: false),
                        LibraryImageTextParentID = c.Int(),
                        AccountMainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LibraryImageText", t => t.LibraryImageTextParentID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.LibraryImageTextParentID)
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.AccountMainHouses", t => t.AccountMainHousesID)
                .ForeignKey("dbo.AutoMessage_Keyword", t => t.ParentAutoMessage_KeywordID)
                .Index(t => t.AccountMainID)
                .Index(t => t.AccountMainHousesID)
                .Index(t => t.ParentAutoMessage_KeywordID);
            
            CreateTable(
                "dbo.AccountMainHouses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        HName = c.String(nullable: false),
                        HIntroduce = c.String(maxLength: 2000),
                        HAddress = c.String(nullable: false, maxLength: 200),
                        HHouseCount = c.Int(nullable: false),
                        HHouseholdsCount = c.Int(),
                        HParkingCount = c.Int(),
                        HOpeningDate = c.DateTime(nullable: false),
                        HCheckInDate = c.DateTime(nullable: false),
                        HCompletedDate = c.DateTime(nullable: false),
                        HOccupyArea = c.Double(),
                        HBuildingArea = c.Double(),
                        HGreeningArea = c.Double(),
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
                "dbo.AccountMainHouseInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainHousessID = c.Int(nullable: false),
                        Building = c.String(),
                        Cell = c.String(),
                        NumberOfLayers = c.Int(nullable: false),
                        NumberOfTheElevator = c.Int(nullable: false),
                        NumberOfFamily = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMainHouses", t => t.AccountMainHousessID)
                .Index(t => t.AccountMainHousessID);
            
            CreateTable(
                "dbo.AccountMainHouseInfoDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainHouseInfoID = c.Int(nullable: false),
                        Layer = c.Int(nullable: false),
                        RoomNumber = c.String(nullable: false),
                        AccountMainHouseTypeID = c.Int(nullable: false),
                        BuildingArea = c.Double(nullable: false),
                        RealityArea = c.Double(nullable: false),
                        GongTan = c.Double(nullable: false),
                        EnumSoldStateID = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMainHouseInfo", t => t.AccountMainHouseInfoID)
                .ForeignKey("dbo.AccountMainHouseType", t => t.AccountMainHouseTypeID)
                .ForeignKey("dbo.LookupOption", t => t.EnumSoldStateID)
                .Index(t => t.AccountMainHouseInfoID)
                .Index(t => t.AccountMainHouseTypeID)
                .Index(t => t.EnumSoldStateID);
            
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
                "dbo.KeywordAutoMessage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        EnumMessageTypeID = c.Int(nullable: false),
                        Describe = c.String(nullable: false, maxLength: 100),
                        MessageID = c.Int(nullable: false),
                        AutoMessage_KeywordID = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LookupOption", t => t.EnumMessageTypeID)
                .ForeignKey("dbo.AutoMessage_Keyword", t => t.AutoMessage_KeywordID, cascadeDelete: true)
                .Index(t => t.EnumMessageTypeID)
                .Index(t => t.AutoMessage_KeywordID);
            
            CreateTable(
                "dbo.TextReply",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Content = c.String(nullable: false, maxLength: 4000),
                        AutoMessage_KeywordID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AutoMessage_Keyword", t => t.AutoMessage_KeywordID, cascadeDelete: true)
                .Index(t => t.AutoMessage_KeywordID);
            
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
                "dbo.Account_User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        GroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .ForeignKey("dbo.User", t => t.UserID)
                .ForeignKey("dbo.Group", t => t.GroupID)
                .Index(t => t.AccountID)
                .Index(t => t.UserID)
                .Index(t => t.GroupID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(maxLength: 10),
                        Address = c.String(maxLength: 50),
                        Phone = c.String(maxLength: 20),
                        HeadImagePath = c.String(maxLength: 500),
                        Email = c.String(maxLength: 50),
                        AccountStatusID = c.Int(nullable: false),
                        IdentityCard = c.String(maxLength: 30),
                        AccountMainID = c.Int(nullable: false),
                        UserLoginInfoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LookupOption", t => t.AccountStatusID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.UserLoginInfo", t => t.UserLoginInfoID)
                .Index(t => t.AccountStatusID)
                .Index(t => t.AccountMainID)
                .Index(t => t.UserLoginInfoID);
            
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
                    })
                .PrimaryKey(t => t.ID);
            
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
                "dbo.Message",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        TextContent = c.String(nullable: false, maxLength: 500),
                        EnumMessageSendDirectionID = c.Int(nullable: false),
                        EnumMessageTypeID = c.Int(nullable: false),
                        MessageObjID = c.Int(nullable: false),
                        FromAccountID = c.Int(),
                        FromUserID = c.Int(),
                        ToAccountID = c.Int(),
                        ToUserID = c.Int(),
                        SendTime = c.DateTime(nullable: false),
                        IsReceive = c.Boolean(nullable: false),
                        ReceiveTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LookupOption", t => t.EnumMessageSendDirectionID)
                .ForeignKey("dbo.LookupOption", t => t.EnumMessageTypeID)
                .ForeignKey("dbo.Account", t => t.FromAccountID)
                .ForeignKey("dbo.User", t => t.FromUserID)
                .ForeignKey("dbo.Account", t => t.ToAccountID)
                .ForeignKey("dbo.User", t => t.ToUserID)
                .Index(t => t.EnumMessageSendDirectionID)
                .Index(t => t.EnumMessageTypeID)
                .Index(t => t.FromAccountID)
                .Index(t => t.FromUserID)
                .Index(t => t.ToAccountID)
                .Index(t => t.ToUserID);
            
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

            var migrationDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\EF");
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
            DropIndex("dbo.ActivateEmail", new[] { "AccountID" });
            DropIndex("dbo.ClientInfo", new[] { "EnumClientUserTypeID" });
            DropIndex("dbo.ClientInfo", new[] { "EnumClientSystemTypeID" });
            DropIndex("dbo.Account_AccountMain", new[] { "AccountMainID" });
            DropIndex("dbo.Account_AccountMain", new[] { "AccountID" });
            DropIndex("dbo.Message", new[] { "ToUserID" });
            DropIndex("dbo.Message", new[] { "ToAccountID" });
            DropIndex("dbo.Message", new[] { "FromUserID" });
            DropIndex("dbo.Message", new[] { "FromAccountID" });
            DropIndex("dbo.Message", new[] { "EnumMessageTypeID" });
            DropIndex("dbo.Message", new[] { "EnumMessageSendDirectionID" });
            DropIndex("dbo.SystemMessage", new[] { "UserID" });
            DropIndex("dbo.SystemMessage", new[] { "AccountID" });
            DropIndex("dbo.User", new[] { "UserLoginInfoID" });
            DropIndex("dbo.User", new[] { "AccountMainID" });
            DropIndex("dbo.User", new[] { "AccountStatusID" });
            DropIndex("dbo.Account_User", new[] { "GroupID" });
            DropIndex("dbo.Account_User", new[] { "UserID" });
            DropIndex("dbo.Account_User", new[] { "AccountID" });
            DropIndex("dbo.Group", new[] { "AccountMainID" });
            DropIndex("dbo.Group", new[] { "AccountID" });
            DropIndex("dbo.TextReply", new[] { "AutoMessage_KeywordID" });
            DropIndex("dbo.KeywordAutoMessage", new[] { "AutoMessage_KeywordID" });
            DropIndex("dbo.KeywordAutoMessage", new[] { "EnumMessageTypeID" });
            DropIndex("dbo.Keyword", new[] { "AutoMessage_KeywordID" });
            DropIndex("dbo.AccountMainHouseType", new[] { "AccountMain_ID" });
            DropIndex("dbo.AccountMainHouseType", new[] { "AccountMainHousesID" });
            DropIndex("dbo.AccountMainHouseInfoDetail", new[] { "EnumSoldStateID" });
            DropIndex("dbo.AccountMainHouseInfoDetail", new[] { "AccountMainHouseTypeID" });
            DropIndex("dbo.AccountMainHouseInfoDetail", new[] { "AccountMainHouseInfoID" });
            DropIndex("dbo.AccountMainHouseInfo", new[] { "AccountMainHousessID" });
            DropIndex("dbo.AccountMainHouses", new[] { "AccountMainID" });
            DropIndex("dbo.AutoMessage_Keyword", new[] { "ParentAutoMessage_KeywordID" });
            DropIndex("dbo.AutoMessage_Keyword", new[] { "AccountMainHousesID" });
            DropIndex("dbo.AutoMessage_Keyword", new[] { "AccountMainID" });
            DropIndex("dbo.AutoMessage_Reply", new[] { "AccountMainID" });
            DropIndex("dbo.AutoMessage_Add", new[] { "AccountMainID" });
            DropIndex("dbo.LibraryImage", new[] { "AccountMainID" });
            DropIndex("dbo.LibraryImageText", new[] { "AccountMainID" });
            DropIndex("dbo.LibraryImageText", new[] { "LibraryImageTextParentID" });
            DropIndex("dbo.LibraryVideo", new[] { "AccountMainID" });
            DropIndex("dbo.LibraryVoice", new[] { "AccountMainID" });
            DropIndex("dbo.LibraryText", new[] { "AccountMainID" });
            DropIndex("dbo.SystemUserRole_SystemUserMenu", new[] { "SystemUserMenuID" });
            DropIndex("dbo.SystemUserRole_SystemUserMenu", new[] { "SystemUserRoleID" });
            DropIndex("dbo.SystemUserMenu", new[] { "ParentMenuID" });
            DropIndex("dbo.SystemUserMenuOption", new[] { "SystemUserMenuID" });
            DropIndex("dbo.SystemUserRole_Option", new[] { "SystemUserMenuOptionID" });
            DropIndex("dbo.SystemUserRole_Option", new[] { "SystemUserRoleID" });
            DropIndex("dbo.SystemUserRole", new[] { "ParentSystemUserRoleID" });
            DropIndex("dbo.SystemUser", new[] { "SystemUserRoleID" });
            DropIndex("dbo.SystemUser", new[] { "AccountStatusID" });
            DropIndex("dbo.AppUpdate", new[] { "ID" });
            DropIndex("dbo.AccountMainExpandInfo", new[] { "ID" });
            DropIndex("dbo.District", new[] { "CityID" });
            DropIndex("dbo.City", new[] { "ProvinceID" });
            DropIndex("dbo.AccountMain", new[] { "SystemUserID" });
            DropIndex("dbo.AccountMain", new[] { "AccountStatusID" });
            DropIndex("dbo.AccountMain", new[] { "CityID" });
            DropIndex("dbo.AccountMain", new[] { "DistrictID" });
            DropIndex("dbo.AccountMain", new[] { "ProvinceID" });
            DropIndex("dbo.LookupOption", new[] { "LookupID" });
            DropIndex("dbo.RoleOption", new[] { "MenuOptionID" });
            DropIndex("dbo.RoleOption", new[] { "RoleID" });
            DropIndex("dbo.MenuOption", new[] { "MenuID" });
            DropIndex("dbo.Menu", new[] { "ParentMenuID" });
            DropIndex("dbo.RoleMenu", new[] { "MenuID" });
            DropIndex("dbo.RoleMenu", new[] { "RoleID" });
            DropIndex("dbo.Role", new[] { "ParentRoleID" });
            DropIndex("dbo.Account", new[] { "AccountStatusID" });
            DropIndex("dbo.Account", new[] { "RoleID" });
            DropForeignKey("dbo.ActivateEmail", "AccountID", "dbo.Account");
            DropForeignKey("dbo.ClientInfo", "EnumClientUserTypeID", "dbo.LookupOption");
            DropForeignKey("dbo.ClientInfo", "EnumClientSystemTypeID", "dbo.LookupOption");
            DropForeignKey("dbo.Account_AccountMain", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Account_AccountMain", "AccountID", "dbo.Account");
            DropForeignKey("dbo.Message", "ToUserID", "dbo.User");
            DropForeignKey("dbo.Message", "ToAccountID", "dbo.Account");
            DropForeignKey("dbo.Message", "FromUserID", "dbo.User");
            DropForeignKey("dbo.Message", "FromAccountID", "dbo.Account");
            DropForeignKey("dbo.Message", "EnumMessageTypeID", "dbo.LookupOption");
            DropForeignKey("dbo.Message", "EnumMessageSendDirectionID", "dbo.LookupOption");
            DropForeignKey("dbo.SystemMessage", "UserID", "dbo.User");
            DropForeignKey("dbo.SystemMessage", "AccountID", "dbo.Account");
            DropForeignKey("dbo.User", "UserLoginInfoID", "dbo.UserLoginInfo");
            DropForeignKey("dbo.User", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.User", "AccountStatusID", "dbo.LookupOption");
            DropForeignKey("dbo.Account_User", "GroupID", "dbo.Group");
            DropForeignKey("dbo.Account_User", "UserID", "dbo.User");
            DropForeignKey("dbo.Account_User", "AccountID", "dbo.Account");
            DropForeignKey("dbo.Group", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Group", "AccountID", "dbo.Account");
            DropForeignKey("dbo.TextReply", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword");
            DropForeignKey("dbo.KeywordAutoMessage", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword");
            DropForeignKey("dbo.KeywordAutoMessage", "EnumMessageTypeID", "dbo.LookupOption");
            DropForeignKey("dbo.Keyword", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword");
            DropForeignKey("dbo.AccountMainHouseType", "AccountMain_ID", "dbo.AccountMain");
            DropForeignKey("dbo.AccountMainHouseType", "AccountMainHousesID", "dbo.AccountMainHouses");
            DropForeignKey("dbo.AccountMainHouseInfoDetail", "EnumSoldStateID", "dbo.LookupOption");
            DropForeignKey("dbo.AccountMainHouseInfoDetail", "AccountMainHouseTypeID", "dbo.AccountMainHouseType");
            DropForeignKey("dbo.AccountMainHouseInfoDetail", "AccountMainHouseInfoID", "dbo.AccountMainHouseInfo");
            DropForeignKey("dbo.AccountMainHouseInfo", "AccountMainHousessID", "dbo.AccountMainHouses");
            DropForeignKey("dbo.AccountMainHouses", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.AutoMessage_Keyword", "ParentAutoMessage_KeywordID", "dbo.AutoMessage_Keyword");
            DropForeignKey("dbo.AutoMessage_Keyword", "AccountMainHousesID", "dbo.AccountMainHouses");
            DropForeignKey("dbo.AutoMessage_Keyword", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.AutoMessage_Reply", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.AutoMessage_Add", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.LibraryImage", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.LibraryImageText", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.LibraryImageText", "LibraryImageTextParentID", "dbo.LibraryImageText");
            DropForeignKey("dbo.LibraryVideo", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.LibraryVoice", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.LibraryText", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.SystemUserRole_SystemUserMenu", "SystemUserMenuID", "dbo.SystemUserMenu");
            DropForeignKey("dbo.SystemUserRole_SystemUserMenu", "SystemUserRoleID", "dbo.SystemUserRole");
            DropForeignKey("dbo.SystemUserMenu", "ParentMenuID", "dbo.SystemUserMenu");
            DropForeignKey("dbo.SystemUserMenuOption", "SystemUserMenuID", "dbo.SystemUserMenu");
            DropForeignKey("dbo.SystemUserRole_Option", "SystemUserMenuOptionID", "dbo.SystemUserMenuOption");
            DropForeignKey("dbo.SystemUserRole_Option", "SystemUserRoleID", "dbo.SystemUserRole");
            DropForeignKey("dbo.SystemUserRole", "ParentSystemUserRoleID", "dbo.SystemUserRole");
            DropForeignKey("dbo.SystemUser", "SystemUserRoleID", "dbo.SystemUserRole");
            DropForeignKey("dbo.SystemUser", "AccountStatusID", "dbo.LookupOption");
            DropForeignKey("dbo.AppUpdate", "ID", "dbo.AccountMain");
            DropForeignKey("dbo.AccountMainExpandInfo", "ID", "dbo.AccountMain");
            DropForeignKey("dbo.District", "CityID", "dbo.City");
            DropForeignKey("dbo.City", "ProvinceID", "dbo.Province");
            DropForeignKey("dbo.AccountMain", "SystemUserID", "dbo.SystemUser");
            DropForeignKey("dbo.AccountMain", "AccountStatusID", "dbo.LookupOption");
            DropForeignKey("dbo.AccountMain", "CityID", "dbo.City");
            DropForeignKey("dbo.AccountMain", "DistrictID", "dbo.District");
            DropForeignKey("dbo.AccountMain", "ProvinceID", "dbo.Province");
            DropForeignKey("dbo.LookupOption", "LookupID", "dbo.Lookup");
            DropForeignKey("dbo.RoleOption", "MenuOptionID", "dbo.MenuOption");
            DropForeignKey("dbo.RoleOption", "RoleID", "dbo.Role");
            DropForeignKey("dbo.MenuOption", "MenuID", "dbo.Menu");
            DropForeignKey("dbo.Menu", "ParentMenuID", "dbo.Menu");
            DropForeignKey("dbo.RoleMenu", "MenuID", "dbo.Menu");
            DropForeignKey("dbo.RoleMenu", "RoleID", "dbo.Role");
            DropForeignKey("dbo.Role", "ParentRoleID", "dbo.Role");
            DropForeignKey("dbo.Account", "AccountStatusID", "dbo.LookupOption");
            DropForeignKey("dbo.Account", "RoleID", "dbo.Role");
            DropTable("dbo.ActivateEmail");
            DropTable("dbo.ClientInfo");
            DropTable("dbo.Account_AccountMain");
            DropTable("dbo.Message");
            DropTable("dbo.SystemMessage");
            DropTable("dbo.UserLoginInfo");
            DropTable("dbo.User");
            DropTable("dbo.Account_User");
            DropTable("dbo.Group");
            DropTable("dbo.TextReply");
            DropTable("dbo.KeywordAutoMessage");
            DropTable("dbo.Keyword");
            DropTable("dbo.AccountMainHouseType");
            DropTable("dbo.AccountMainHouseInfoDetail");
            DropTable("dbo.AccountMainHouseInfo");
            DropTable("dbo.AccountMainHouses");
            DropTable("dbo.AutoMessage_Keyword");
            DropTable("dbo.AutoMessage_Reply");
            DropTable("dbo.AutoMessage_Add");
            DropTable("dbo.LibraryImage");
            DropTable("dbo.LibraryImageText");
            DropTable("dbo.LibraryVideo");
            DropTable("dbo.LibraryVoice");
            DropTable("dbo.LibraryText");
            DropTable("dbo.SystemUserRole_SystemUserMenu");
            DropTable("dbo.SystemUserMenu");
            DropTable("dbo.SystemUserMenuOption");
            DropTable("dbo.SystemUserRole_Option");
            DropTable("dbo.SystemUserRole");
            DropTable("dbo.SystemUser");
            DropTable("dbo.AppUpdate");
            DropTable("dbo.AccountMainExpandInfo");
            DropTable("dbo.District");
            DropTable("dbo.City");
            DropTable("dbo.Province");
            DropTable("dbo.AccountMain");
            DropTable("dbo.Lookup");
            DropTable("dbo.LookupOption");
            DropTable("dbo.RoleOption");
            DropTable("dbo.MenuOption");
            DropTable("dbo.Menu");
            DropTable("dbo.RoleMenu");
            DropTable("dbo.Role");
            DropTable("dbo.Account");
        }
    }
}
