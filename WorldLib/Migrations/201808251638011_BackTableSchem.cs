namespace WorldLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BackTableSchem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Discussion_Id", c => c.Int());
            AddColumn("dbo.Comments", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Discussions", "Category_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "NikName", c => c.String());
            CreateIndex("dbo.Comments", "Discussion_Id");
            CreateIndex("dbo.Comments", "User_Id");
            CreateIndex("dbo.Discussions", "Category_Id");
            AddForeignKey("dbo.Discussions", "Category_Id", "dbo.Categories", "Id");
            AddForeignKey("dbo.Comments", "Discussion_Id", "dbo.Discussions", "Id");
            AddForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Comments", "DiscussionId");
            DropColumn("dbo.Comments", "UserName");
            DropColumn("dbo.Discussions", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Discussions", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "UserName", c => c.String());
            AddColumn("dbo.Comments", "DiscussionId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Discussion_Id", "dbo.Discussions");
            DropForeignKey("dbo.Discussions", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Discussions", new[] { "Category_Id" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "Discussion_Id" });
            DropColumn("dbo.AspNetUsers", "NikName");
            DropColumn("dbo.Discussions", "Category_Id");
            DropColumn("dbo.Comments", "User_Id");
            DropColumn("dbo.Comments", "Discussion_Id");
        }
    }
}
