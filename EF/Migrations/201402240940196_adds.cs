namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountMain", "IOSClientCertificate", c => c.String());
            AddColumn("dbo.AccountMain", "IOSSalestCertificate", c => c.String());
            AddColumn("dbo.ActivityInfoParticipator", "Email", c => c.String(maxLength: 30));
            AddColumn("dbo.AppAdvertorialOperation", "ForwardWeiXinCnt", c => c.Int(nullable: false));
            AddColumn("dbo.AppAdvertorialOperation", "ForwardWeiboCnt", c => c.Int(nullable: false));
            AddColumn("dbo.AppAdvertorialOperation", "ForwardFriendCnt", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppAdvertorialOperation", "ForwardFriendCnt");
            DropColumn("dbo.AppAdvertorialOperation", "ForwardWeiboCnt");
            DropColumn("dbo.AppAdvertorialOperation", "ForwardWeiXinCnt");
            DropColumn("dbo.ActivityInfoParticipator", "Email");
            DropColumn("dbo.AccountMain", "IOSSalestCertificate");
            DropColumn("dbo.AccountMain", "IOSClientCertificate");
        }
    }
}
