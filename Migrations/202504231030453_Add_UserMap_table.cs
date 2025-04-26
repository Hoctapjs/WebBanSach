namespace WebBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UserMap_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserMaps",
                c => new
                    {
                        AppUserId = c.String(nullable: false, maxLength: 128),
                        CSVUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AppUserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserMaps");
        }
    }
}
