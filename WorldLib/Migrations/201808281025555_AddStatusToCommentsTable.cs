namespace WorldLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusToCommentsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Status");
        }
    }
}
