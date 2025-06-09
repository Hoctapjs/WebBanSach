namespace WebBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserMapModel : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.UserMaps",
            //    c => new
            //    {
            //        Id = c.Int(nullable: false, identity: true),
            //        AppUserId = c.String(nullable: false, maxLength: 128),
            //        CSVUserId = c.Int(nullable: false),
            //    })
            //    .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.UserMaps");
        }
    }
}
