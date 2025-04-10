namespace WebBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        PasswordHash = c.String(),
                        Email = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Orders", "UserId");
            AddForeignKey("dbo.Orders", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
