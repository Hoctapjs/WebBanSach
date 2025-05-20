namespace WebBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUserMapModel : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserMaps");
            AddColumn("dbo.UserMaps", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.UserMaps", "AppUserId", c => c.String());
            AddPrimaryKey("dbo.UserMaps", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserMaps");
            AlterColumn("dbo.UserMaps", "AppUserId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.UserMaps", "Id");
            AddPrimaryKey("dbo.UserMaps", "AppUserId");
        }
    }
}
