namespace WorldLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCrudFlagToIngrTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ingridients", "ProcessFlag", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ingridients", "ProcessFlag");
        }
    }
}
