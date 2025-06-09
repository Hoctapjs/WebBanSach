namespace WebBanSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nhaphang : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChiTietPhieuNhaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhieuNhapId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        SoLuong = c.Int(nullable: false),
                        DonGiaNhap = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.PhieuNhaps", t => t.PhieuNhapId, cascadeDelete: true)
                .Index(t => t.PhieuNhapId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.PhieuNhaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NgayNhap = c.DateTime(nullable: false),
                        NhaCungCapId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NhaCungCaps", t => t.NhaCungCapId, cascadeDelete: true)
                .Index(t => t.NhaCungCapId);
            
            CreateTable(
                "dbo.NhaCungCaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenNhaCungCap = c.String(),
                        DiaChi = c.String(),
                        SoDienThoai = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhieuNhaps", "NhaCungCapId", "dbo.NhaCungCaps");
            DropForeignKey("dbo.ChiTietPhieuNhaps", "PhieuNhapId", "dbo.PhieuNhaps");
            DropForeignKey("dbo.ChiTietPhieuNhaps", "BookId", "dbo.Books");
            DropIndex("dbo.PhieuNhaps", new[] { "NhaCungCapId" });
            DropIndex("dbo.ChiTietPhieuNhaps", new[] { "BookId" });
            DropIndex("dbo.ChiTietPhieuNhaps", new[] { "PhieuNhapId" });
            DropTable("dbo.NhaCungCaps");
            DropTable("dbo.PhieuNhaps");
            DropTable("dbo.ChiTietPhieuNhaps");
        }
    }
}
