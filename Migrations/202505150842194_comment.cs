namespace WebBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RatingRows", "Comment", c => c.String());
            AddColumn("dbo.RatingRows", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RatingRows", "DateCreated");
            DropColumn("dbo.RatingRows", "Comment");
        }
    }
}
