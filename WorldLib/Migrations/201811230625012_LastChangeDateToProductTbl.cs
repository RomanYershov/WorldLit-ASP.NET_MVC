namespace WorldLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LastChangeDateToProductTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "LastChangeDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "LastChangeDate");
        }
    }
}
