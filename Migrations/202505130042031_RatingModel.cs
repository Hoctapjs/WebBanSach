namespace WebBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RatingRows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        ISBN = c.String(),
                        BookRating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RatingRows");
        }
    }
}
