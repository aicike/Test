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
                        IsCanFindByUser = c.Boolean(),
                        Token = c.String(maxLength: 50),
                        AccountMainID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Role", t => t.ParentRoleID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.ParentRoleID)
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
                        AccountStatusID = c.Int(nullable: false),
                        FileLimit = c.Double(nullable: false),
                        SaleAddress = c.String(nullable: false, maxLength: 200),
                        SalePhone = c.String(),
                        SaleMapAddress = c.String(),
                        Lng = c.String(),
                        Lat = c.String(),
                        IOSDownloadPath = c.String(),
                        AndroidDownloadPath = c.String(),
                        AndroidVersion = c.String(),
                        IOSVersion = c.String(),
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
                        IsUpdate = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.Province", t => t.ProvinceID)
                .ForeignKey("dbo.City", t => t.CityID)
                .ForeignKey("dbo.District", t => t.DistrictID)
                .Index(t => t.AccountMainID)
                .Index(t => t.ProvinceID)
                .Index(t => t.CityID)
                .Index(t => t.DistrictID);
            
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
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.Order", t => t.OrderID)
                .ForeignKey("dbo.Product", t => t.ProductID)
                .Index(t => t.AccountMainID)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        imgFilePath = c.String(),
                        Name = c.String(nullable: false, maxLength: 30),
                        Specification = c.String(),
                        Price = c.Double(nullable: false),
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
                "dbo.Classify",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        ParentID = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        Sort = c.Int(nullable: false),
                        Subordinate = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
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
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        Name = c.String(maxLength: 10),
                        Phone = c.String(maxLength: 20),
                        AccountStatusID = c.Int(nullable: false),
                        IdentityCard = c.String(maxLength: 30),
                        AccountMainID = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
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
                        FindPwdCode = c.String(),
                        FindPwdTime = c.DateTime(),
                        FindPwdValidity = c.Boolean(),
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Account", t => t.FromAccountID)
                .ForeignKey("dbo.User", t => t.FromUserID)
                .ForeignKey("dbo.Account", t => t.ToAccountID)
                .ForeignKey("dbo.User", t => t.ToUserID)
                .ForeignKey("dbo.LibraryImageText", t => t.LibraryImageTextsID)
                .ForeignKey("dbo.Conversation", t => t.ConversationID)
                .Index(t => t.FromAccountID)
                .Index(t => t.FromUserID)
                .Index(t => t.ToAccountID)
                .Index(t => t.ToUserID)
                .Index(t => t.LibraryImageTextsID)
                .Index(t => t.ConversationID);
            
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
                .ForeignKey("dbo.LibraryImageText", t => t.LibraryImageTextParentID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.LibraryImageTextParentID)
                .Index(t => t.AccountMainID);
            
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Account", t => t.FromAccountID)
                .ForeignKey("dbo.User", t => t.FromUserID)
                .ForeignKey("dbo.Account", t => t.ToAccountID)
                .ForeignKey("dbo.User", t => t.ToUserID)
                .ForeignKey("dbo.Message", t => t.MessageID)
                .ForeignKey("dbo.LibraryImageText", t => t.LibraryImageTextsID)
                .ForeignKey("dbo.Conversation", t => t.ConversationID)
                .Index(t => t.FromAccountID)
                .Index(t => t.FromUserID)
                .Index(t => t.ToAccountID)
                .Index(t => t.ToUserID)
                .Index(t => t.MessageID)
                .Index(t => t.LibraryImageTextsID)
                .Index(t => t.ConversationID);
            
            CreateTable(
                "dbo.Conversation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.String(),
                        User1ID = c.String(),
                        User2ID = c.String(),
                        Ctype = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                .ForeignKey("dbo.User", t => t.UserID)
                .ForeignKey("dbo.Group", t => t.GroupID)
                .Index(t => t.AccountID)
                .Index(t => t.UserID)
                .Index(t => t.GroupID);
            
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
                .ForeignKey("dbo.LookupOption", t => t.EnumMessageTypeID)
                .ForeignKey("dbo.AutoMessage_Keyword", t => t.AutoMessage_KeywordID, cascadeDelete: true)
                .Index(t => t.EnumMessageTypeID)
                .Index(t => t.AutoMessage_KeywordID);
            
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
                        HOccupyArea = c.Double(nullable: false),
                        HBuildingArea = c.Double(nullable: false),
                        HGreeningArea = c.Double(nullable: false),
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
                        FileMp3Path = c.String(),
                        FileLength = c.String(),
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
                        FileLength = c.String(),
                        AccountMainID = c.Int(nullable: false),
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
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .Index(t => t.AccountMainID)
                .Index(t => t.AccountID);
            
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
                "dbo.AppAdvertorial",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemStatus = c.Int(nullable: false),
                        AccountMainID = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        stick = c.Int(nullable: false),
                        Sort = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        Depict = c.String(nullable: false),
                        IssueDate = c.DateTime(nullable: false),
                        MainImagPath = c.String(nullable: false),
                        AppShowImagePath = c.String(),
                        MinImagePath = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountMain", t => t.AccountMainID)
                .Index(t => t.AccountMainID);
            
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
            DropIndex("dbo.RoleOption", new[] { "MenuOptionID" });
            DropIndex("dbo.RoleOption", new[] { "RoleID" });
            DropIndex("dbo.MenuOption", new[] { "MenuID" });
            DropIndex("dbo.Menu", new[] { "ParentMenuID" });
            DropIndex("dbo.RoleMenu", new[] { "MenuID" });
            DropIndex("dbo.RoleMenu", new[] { "RoleID" });
            DropIndex("dbo.OrderMType", new[] { "AccountMainID" });
            DropIndex("dbo.AppWaitImg", new[] { "AccountMainID" });
            DropIndex("dbo.AppAdvertorial", new[] { "AccountMainID" });
            DropIndex("dbo.PushMsgDetail", new[] { "PushMsgID" });
            DropIndex("dbo.PushMsg", new[] { "AccountID" });
            DropIndex("dbo.PushMsg", new[] { "AccountMainID" });
            DropIndex("dbo.Account_AccountMain", new[] { "AccountMainID" });
            DropIndex("dbo.Account_AccountMain", new[] { "AccountID" });
            DropIndex("dbo.AutoMessage_Reply", new[] { "AccountMainID" });
            DropIndex("dbo.AutoMessage_Add", new[] { "AccountMainID" });
            DropIndex("dbo.LibraryImage", new[] { "AccountMainID" });
            DropIndex("dbo.LibraryVideo", new[] { "AccountMainID" });
            DropIndex("dbo.LibraryVoice", new[] { "AccountMainID" });
            DropIndex("dbo.LibraryText", new[] { "AccountMainID" });
            DropIndex("dbo.AppUpdate", new[] { "ID" });
            DropIndex("dbo.ClientInfo", new[] { "EnumClientUserTypeID" });
            DropIndex("dbo.ClientInfo", new[] { "EnumClientSystemTypeID" });
            DropIndex("dbo.AutoMessage_User", new[] { "AutoMessage_KeywordID" });
            DropIndex("dbo.AutoMessage_User", new[] { "AccountMainID" });
            DropIndex("dbo.Keyword", new[] { "AutoMessage_KeywordID" });
            DropIndex("dbo.AccountMainHouseType", new[] { "AccountMain_ID" });
            DropIndex("dbo.AccountMainHouseType", new[] { "AccountMainHousesID" });
            DropIndex("dbo.AccountMainHouseInfoDetail", new[] { "EnumSoldStateID" });
            DropIndex("dbo.AccountMainHouseInfoDetail", new[] { "AccountMainHouseTypeID" });
            DropIndex("dbo.AccountMainHouseInfoDetail", new[] { "AccountMainHouses_ID" });
            DropIndex("dbo.AccountMainHouseInfoDetail", new[] { "AccountMainHouseInfoID" });
            DropIndex("dbo.AccountMainHouseInfo", new[] { "AccountMainHousessID" });
            DropIndex("dbo.AccountMainHouses", new[] { "AccountMainID" });
            DropIndex("dbo.AutoMessage_Keyword", new[] { "ParentAutoMessage_KeywordID" });
            DropIndex("dbo.AutoMessage_Keyword", new[] { "AccountMainHousesID" });
            DropIndex("dbo.AutoMessage_Keyword", new[] { "AccountMainID" });
            DropIndex("dbo.KeywordAutoMessage", new[] { "AutoMessage_KeywordID" });
            DropIndex("dbo.KeywordAutoMessage", new[] { "EnumMessageTypeID" });
            DropIndex("dbo.SystemUserRole_SystemUserMenu", new[] { "SystemUserMenuID" });
            DropIndex("dbo.SystemUserRole_SystemUserMenu", new[] { "SystemUserRoleID" });
            DropIndex("dbo.SystemUserMenu", new[] { "ParentMenuID" });
            DropIndex("dbo.SystemUserMenuOption", new[] { "SystemUserMenuID" });
            DropIndex("dbo.SystemUserRole_Option", new[] { "SystemUserMenuOptionID" });
            DropIndex("dbo.SystemUserRole_Option", new[] { "SystemUserRoleID" });
            DropIndex("dbo.SystemUserRole", new[] { "ParentSystemUserRoleID" });
            DropIndex("dbo.SystemUser", new[] { "SystemUserRoleID" });
            DropIndex("dbo.SystemUser", new[] { "AccountStatusID" });
            DropIndex("dbo.Group", new[] { "AccountMainID" });
            DropIndex("dbo.Group", new[] { "AccountID" });
            DropIndex("dbo.Account_User", new[] { "GroupID" });
            DropIndex("dbo.Account_User", new[] { "UserID" });
            DropIndex("dbo.Account_User", new[] { "AccountID" });
            DropIndex("dbo.PendingMessages", new[] { "ConversationID" });
            DropIndex("dbo.PendingMessages", new[] { "LibraryImageTextsID" });
            DropIndex("dbo.PendingMessages", new[] { "MessageID" });
            DropIndex("dbo.PendingMessages", new[] { "ToUserID" });
            DropIndex("dbo.PendingMessages", new[] { "ToAccountID" });
            DropIndex("dbo.PendingMessages", new[] { "FromUserID" });
            DropIndex("dbo.PendingMessages", new[] { "FromAccountID" });
            DropIndex("dbo.LibraryImageText", new[] { "AccountMainID" });
            DropIndex("dbo.LibraryImageText", new[] { "LibraryImageTextParentID" });
            DropIndex("dbo.Message", new[] { "ConversationID" });
            DropIndex("dbo.Message", new[] { "LibraryImageTextsID" });
            DropIndex("dbo.Message", new[] { "ToUserID" });
            DropIndex("dbo.Message", new[] { "ToAccountID" });
            DropIndex("dbo.Message", new[] { "FromUserID" });
            DropIndex("dbo.Message", new[] { "FromAccountID" });
            DropIndex("dbo.SystemMessage", new[] { "UserID" });
            DropIndex("dbo.SystemMessage", new[] { "AccountID" });
            DropIndex("dbo.User", new[] { "UserLoginInfoID" });
            DropIndex("dbo.User", new[] { "AccountMainID" });
            DropIndex("dbo.User", new[] { "AccountStatusID" });
            DropIndex("dbo.LookupOption", new[] { "LookupID" });
            DropIndex("dbo.OrderMIntermediate", new[] { "OrderID" });
            DropIndex("dbo.Classify", new[] { "AccountMainID" });
            DropIndex("dbo.Product", new[] { "ClassifyID" });
            DropIndex("dbo.Product", new[] { "AccountMainID" });
            DropIndex("dbo.OrderDetail", new[] { "ProductID" });
            DropIndex("dbo.OrderDetail", new[] { "OrderID" });
            DropIndex("dbo.OrderDetail", new[] { "AccountMainID" });
            DropIndex("dbo.Order", new[] { "OrderUserInfoID" });
            DropIndex("dbo.Order", new[] { "AccountMainID" });
            DropIndex("dbo.OrderUserInfo", new[] { "DistrictID" });
            DropIndex("dbo.OrderUserInfo", new[] { "CityID" });
            DropIndex("dbo.OrderUserInfo", new[] { "ProvinceID" });
            DropIndex("dbo.OrderUserInfo", new[] { "AccountMainID" });
            DropIndex("dbo.District", new[] { "CityID" });
            DropIndex("dbo.City", new[] { "ProvinceID" });
            DropIndex("dbo.AccountMain", new[] { "SystemUserID" });
            DropIndex("dbo.AccountMain", new[] { "AccountStatusID" });
            DropIndex("dbo.AccountMain", new[] { "CityID" });
            DropIndex("dbo.AccountMain", new[] { "DistrictID" });
            DropIndex("dbo.AccountMain", new[] { "ProvinceID" });
            DropIndex("dbo.Role", new[] { "AccountMainID" });
            DropIndex("dbo.Role", new[] { "ParentRoleID" });
            DropIndex("dbo.Account", new[] { "AccountStatusID" });
            DropIndex("dbo.Account", new[] { "RoleID" });
            DropForeignKey("dbo.ActivateEmail", "AccountID", "dbo.Account");
            DropForeignKey("dbo.RoleOption", "MenuOptionID", "dbo.MenuOption");
            DropForeignKey("dbo.RoleOption", "RoleID", "dbo.Role");
            DropForeignKey("dbo.MenuOption", "MenuID", "dbo.Menu");
            DropForeignKey("dbo.Menu", "ParentMenuID", "dbo.Menu");
            DropForeignKey("dbo.RoleMenu", "MenuID", "dbo.Menu");
            DropForeignKey("dbo.RoleMenu", "RoleID", "dbo.Role");
            DropForeignKey("dbo.OrderMType", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.AppWaitImg", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.AppAdvertorial", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.PushMsgDetail", "PushMsgID", "dbo.PushMsg");
            DropForeignKey("dbo.PushMsg", "AccountID", "dbo.Account");
            DropForeignKey("dbo.PushMsg", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Account_AccountMain", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Account_AccountMain", "AccountID", "dbo.Account");
            DropForeignKey("dbo.AutoMessage_Reply", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.AutoMessage_Add", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.LibraryImage", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.LibraryVideo", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.LibraryVoice", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.LibraryText", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.AppUpdate", "ID", "dbo.AccountMain");
            DropForeignKey("dbo.ClientInfo", "EnumClientUserTypeID", "dbo.LookupOption");
            DropForeignKey("dbo.ClientInfo", "EnumClientSystemTypeID", "dbo.LookupOption");
            DropForeignKey("dbo.AutoMessage_User", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword");
            DropForeignKey("dbo.AutoMessage_User", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Keyword", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword");
            DropForeignKey("dbo.AccountMainHouseType", "AccountMain_ID", "dbo.AccountMain");
            DropForeignKey("dbo.AccountMainHouseType", "AccountMainHousesID", "dbo.AccountMainHouses");
            DropForeignKey("dbo.AccountMainHouseInfoDetail", "EnumSoldStateID", "dbo.LookupOption");
            DropForeignKey("dbo.AccountMainHouseInfoDetail", "AccountMainHouseTypeID", "dbo.AccountMainHouseType");
            DropForeignKey("dbo.AccountMainHouseInfoDetail", "AccountMainHouses_ID", "dbo.AccountMainHouses");
            DropForeignKey("dbo.AccountMainHouseInfoDetail", "AccountMainHouseInfoID", "dbo.AccountMainHouseInfo");
            DropForeignKey("dbo.AccountMainHouseInfo", "AccountMainHousessID", "dbo.AccountMainHouses");
            DropForeignKey("dbo.AccountMainHouses", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.AutoMessage_Keyword", "ParentAutoMessage_KeywordID", "dbo.AutoMessage_Keyword");
            DropForeignKey("dbo.AutoMessage_Keyword", "AccountMainHousesID", "dbo.AccountMainHouses");
            DropForeignKey("dbo.AutoMessage_Keyword", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.KeywordAutoMessage", "AutoMessage_KeywordID", "dbo.AutoMessage_Keyword");
            DropForeignKey("dbo.KeywordAutoMessage", "EnumMessageTypeID", "dbo.LookupOption");
            DropForeignKey("dbo.SystemUserRole_SystemUserMenu", "SystemUserMenuID", "dbo.SystemUserMenu");
            DropForeignKey("dbo.SystemUserRole_SystemUserMenu", "SystemUserRoleID", "dbo.SystemUserRole");
            DropForeignKey("dbo.SystemUserMenu", "ParentMenuID", "dbo.SystemUserMenu");
            DropForeignKey("dbo.SystemUserMenuOption", "SystemUserMenuID", "dbo.SystemUserMenu");
            DropForeignKey("dbo.SystemUserRole_Option", "SystemUserMenuOptionID", "dbo.SystemUserMenuOption");
            DropForeignKey("dbo.SystemUserRole_Option", "SystemUserRoleID", "dbo.SystemUserRole");
            DropForeignKey("dbo.SystemUserRole", "ParentSystemUserRoleID", "dbo.SystemUserRole");
            DropForeignKey("dbo.SystemUser", "SystemUserRoleID", "dbo.SystemUserRole");
            DropForeignKey("dbo.SystemUser", "AccountStatusID", "dbo.LookupOption");
            DropForeignKey("dbo.Group", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Group", "AccountID", "dbo.Account");
            DropForeignKey("dbo.Account_User", "GroupID", "dbo.Group");
            DropForeignKey("dbo.Account_User", "UserID", "dbo.User");
            DropForeignKey("dbo.Account_User", "AccountID", "dbo.Account");
            DropForeignKey("dbo.PendingMessages", "ConversationID", "dbo.Conversation");
            DropForeignKey("dbo.PendingMessages", "LibraryImageTextsID", "dbo.LibraryImageText");
            DropForeignKey("dbo.PendingMessages", "MessageID", "dbo.Message");
            DropForeignKey("dbo.PendingMessages", "ToUserID", "dbo.User");
            DropForeignKey("dbo.PendingMessages", "ToAccountID", "dbo.Account");
            DropForeignKey("dbo.PendingMessages", "FromUserID", "dbo.User");
            DropForeignKey("dbo.PendingMessages", "FromAccountID", "dbo.Account");
            DropForeignKey("dbo.LibraryImageText", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.LibraryImageText", "LibraryImageTextParentID", "dbo.LibraryImageText");
            DropForeignKey("dbo.Message", "ConversationID", "dbo.Conversation");
            DropForeignKey("dbo.Message", "LibraryImageTextsID", "dbo.LibraryImageText");
            DropForeignKey("dbo.Message", "ToUserID", "dbo.User");
            DropForeignKey("dbo.Message", "ToAccountID", "dbo.Account");
            DropForeignKey("dbo.Message", "FromUserID", "dbo.User");
            DropForeignKey("dbo.Message", "FromAccountID", "dbo.Account");
            DropForeignKey("dbo.SystemMessage", "UserID", "dbo.User");
            DropForeignKey("dbo.SystemMessage", "AccountID", "dbo.Account");
            DropForeignKey("dbo.User", "UserLoginInfoID", "dbo.UserLoginInfo");
            DropForeignKey("dbo.User", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.User", "AccountStatusID", "dbo.LookupOption");
            DropForeignKey("dbo.LookupOption", "LookupID", "dbo.Lookup");
            DropForeignKey("dbo.OrderMIntermediate", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Classify", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Product", "ClassifyID", "dbo.Classify");
            DropForeignKey("dbo.Product", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.OrderDetail", "ProductID", "dbo.Product");
            DropForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order");
            DropForeignKey("dbo.OrderDetail", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Order", "OrderUserInfoID", "dbo.OrderUserInfo");
            DropForeignKey("dbo.Order", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.OrderUserInfo", "DistrictID", "dbo.District");
            DropForeignKey("dbo.OrderUserInfo", "CityID", "dbo.City");
            DropForeignKey("dbo.OrderUserInfo", "ProvinceID", "dbo.Province");
            DropForeignKey("dbo.OrderUserInfo", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.District", "CityID", "dbo.City");
            DropForeignKey("dbo.City", "ProvinceID", "dbo.Province");
            DropForeignKey("dbo.AccountMain", "SystemUserID", "dbo.SystemUser");
            DropForeignKey("dbo.AccountMain", "AccountStatusID", "dbo.LookupOption");
            DropForeignKey("dbo.AccountMain", "CityID", "dbo.City");
            DropForeignKey("dbo.AccountMain", "DistrictID", "dbo.District");
            DropForeignKey("dbo.AccountMain", "ProvinceID", "dbo.Province");
            DropForeignKey("dbo.Role", "AccountMainID", "dbo.AccountMain");
            DropForeignKey("dbo.Role", "ParentRoleID", "dbo.Role");
            DropForeignKey("dbo.Account", "AccountStatusID", "dbo.LookupOption");
            DropForeignKey("dbo.Account", "RoleID", "dbo.Role");
            DropTable("dbo.ActivateEmail");
            DropTable("dbo.RoleOption");
            DropTable("dbo.MenuOption");
            DropTable("dbo.Menu");
            DropTable("dbo.RoleMenu");
            DropTable("dbo.OrderMType");
            DropTable("dbo.AppWaitImg");
            DropTable("dbo.AppAdvertorial");
            DropTable("dbo.PushMsgDetail");
            DropTable("dbo.PushMsg");
            DropTable("dbo.Account_AccountMain");
            DropTable("dbo.AutoMessage_Reply");
            DropTable("dbo.AutoMessage_Add");
            DropTable("dbo.LibraryImage");
            DropTable("dbo.LibraryVideo");
            DropTable("dbo.LibraryVoice");
            DropTable("dbo.LibraryText");
            DropTable("dbo.AppUpdate");
            DropTable("dbo.ClientInfo");
            DropTable("dbo.AutoMessage_User");
            DropTable("dbo.Keyword");
            DropTable("dbo.AccountMainHouseType");
            DropTable("dbo.AccountMainHouseInfoDetail");
            DropTable("dbo.AccountMainHouseInfo");
            DropTable("dbo.AccountMainHouses");
            DropTable("dbo.AutoMessage_Keyword");
            DropTable("dbo.KeywordAutoMessage");
            DropTable("dbo.SystemUserRole_SystemUserMenu");
            DropTable("dbo.SystemUserMenu");
            DropTable("dbo.SystemUserMenuOption");
            DropTable("dbo.SystemUserRole_Option");
            DropTable("dbo.SystemUserRole");
            DropTable("dbo.SystemUser");
            DropTable("dbo.Group");
            DropTable("dbo.Account_User");
            DropTable("dbo.Conversation");
            DropTable("dbo.PendingMessages");
            DropTable("dbo.LibraryImageText");
            DropTable("dbo.Message");
            DropTable("dbo.SystemMessage");
            DropTable("dbo.UserLoginInfo");
            DropTable("dbo.User");
            DropTable("dbo.Lookup");
            DropTable("dbo.LookupOption");
            DropTable("dbo.OrderMIntermediate");
            DropTable("dbo.Classify");
            DropTable("dbo.Product");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Order");
            DropTable("dbo.OrderUserInfo");
            DropTable("dbo.District");
            DropTable("dbo.City");
            DropTable("dbo.Province");
            DropTable("dbo.AccountMain");
            DropTable("dbo.Role");
            DropTable("dbo.Account");
        }
    }
}
