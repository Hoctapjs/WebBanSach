namespace WebBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RatingRows", "UserID", c => c.String());
            AlterColumn("dbo.UserMaps", "CSVUserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserMaps", "CSVUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.RatingRows", "UserID", c => c.Int(nullable: false));
        }
    }
}
