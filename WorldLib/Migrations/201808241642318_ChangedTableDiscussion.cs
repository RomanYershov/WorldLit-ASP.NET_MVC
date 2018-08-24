namespace WorldLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTableDiscussion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Discussions", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Discussions", new[] { "Category_Id" });
            AddColumn("dbo.Discussions", "CategoryId", c => c.Int(nullable: false));
            DropColumn("dbo.Discussions", "Category_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Discussions", "Category_Id", c => c.Int());
            DropColumn("dbo.Discussions", "CategoryId");
            CreateIndex("dbo.Discussions", "Category_Id");
            AddForeignKey("dbo.Discussions", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
