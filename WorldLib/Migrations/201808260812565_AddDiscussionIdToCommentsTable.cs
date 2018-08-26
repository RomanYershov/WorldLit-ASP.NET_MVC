namespace WorldLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDiscussionIdToCommentsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Discussion_Id", "dbo.Discussions");
            DropIndex("dbo.Comments", new[] { "Discussion_Id" });
            RenameColumn(table: "dbo.Comments", name: "Discussion_Id", newName: "DiscussionId");
            AlterColumn("dbo.Comments", "DiscussionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "DiscussionId");
            AddForeignKey("dbo.Comments", "DiscussionId", "dbo.Discussions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "DiscussionId", "dbo.Discussions");
            DropIndex("dbo.Comments", new[] { "DiscussionId" });
            AlterColumn("dbo.Comments", "DiscussionId", c => c.Int());
            RenameColumn(table: "dbo.Comments", name: "DiscussionId", newName: "Discussion_Id");
            CreateIndex("dbo.Comments", "Discussion_Id");
            AddForeignKey("dbo.Comments", "Discussion_Id", "dbo.Discussions", "Id");
        }
    }
}
