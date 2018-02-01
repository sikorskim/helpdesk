namespace helpdesk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr0 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        AppUSerId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Visits = c.Int(nullable: false),
                        Credential_CredentialId = c.Int(),
                    })
                .PrimaryKey(t => t.AppUSerId)
                .ForeignKey("dbo.Credentials", t => t.Credential_CredentialId)
                .Index(t => t.Credential_CredentialId);
            
            CreateTable(
                "dbo.Credentials",
                c => new
                    {
                        CredentialId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CredentialId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.OrderComments",
                c => new
                    {
                        OrderCommentId = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Text = c.String(),
                        Username = c.String(),
                        OrderId = c.Int(nullable: false),
                        Status_StatusId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderCommentId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.Status_StatusId)
                .Index(t => t.OrderId)
                .Index(t => t.Status_StatusId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        TimeCreated = c.DateTime(nullable: false),
                        TimeClosed = c.DateTime(),
                        Urgent = c.Boolean(nullable: false),
                        Content = c.String(nullable: false),
                        Category_CategoryId = c.Int(),
                        Status_StatusId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId)
                .ForeignKey("dbo.Status", t => t.Status_StatusId)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.Status_StatusId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        StatusName = c.String(),
                    })
                .PrimaryKey(t => t.StatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderComments", "Status_StatusId", "dbo.Status");
            DropForeignKey("dbo.OrderComments", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Status_StatusId", "dbo.Status");
            DropForeignKey("dbo.Orders", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.AppUsers", "Credential_CredentialId", "dbo.Credentials");
            DropIndex("dbo.Orders", new[] { "Status_StatusId" });
            DropIndex("dbo.Orders", new[] { "Category_CategoryId" });
            DropIndex("dbo.OrderComments", new[] { "Status_StatusId" });
            DropIndex("dbo.OrderComments", new[] { "OrderId" });
            DropIndex("dbo.AppUsers", new[] { "Credential_CredentialId" });
            DropTable("dbo.Status");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderComments");
            DropTable("dbo.Categories");
            DropTable("dbo.Credentials");
            DropTable("dbo.AppUsers");
        }
    }
}
