namespace WorldLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedLikeTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Likes", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Likes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Likes", new[] { "UserId" });
            DropIndex("dbo.Likes", new[] { "CommentId" });
            AlterColumn("dbo.Likes", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Likes", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Likes", "CommentId");
            CreateIndex("dbo.Likes", "UserId");
            AddForeignKey("dbo.Likes", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Likes", "CommentId", "dbo.Comments", "Id", cascadeDelete: true);
        }
    }
}
