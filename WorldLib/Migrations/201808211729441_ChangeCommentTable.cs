namespace WorldLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCommentTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Discussion_Id", "dbo.Discussions");
            DropIndex("dbo.Comments", new[] { "Discussion_Id" });
            AddColumn("dbo.Comments", "DiscussionId", c => c.Int(nullable: false));
            DropColumn("dbo.Comments", "Discussion_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Discussion_Id", c => c.Int());
            DropColumn("dbo.Comments", "DiscussionId");
            CreateIndex("dbo.Comments", "Discussion_Id");
            AddForeignKey("dbo.Comments", "Discussion_Id", "dbo.Discussions", "Id");
        }
    }
}
