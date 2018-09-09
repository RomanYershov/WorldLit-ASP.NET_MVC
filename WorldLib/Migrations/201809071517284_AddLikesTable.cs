namespace WorldLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLikesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsLike = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                        CommentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CommentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "CommentId", "dbo.Comments");
            DropIndex("dbo.Likes", new[] { "CommentId" });
            DropIndex("dbo.Likes", new[] { "UserId" });
            DropTable("dbo.Likes");
        }
    }
}
