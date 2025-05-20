namespace WebBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class khoangoaiusermap : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserMaps", "AppUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserMaps", "AppUserId");
            AddForeignKey("dbo.UserMaps", "AppUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserMaps", "AppUserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserMaps", new[] { "AppUserId" });
            AlterColumn("dbo.UserMaps", "AppUserId", c => c.String());
        }
    }
}
