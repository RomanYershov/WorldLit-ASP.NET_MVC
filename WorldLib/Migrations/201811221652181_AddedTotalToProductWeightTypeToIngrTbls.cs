namespace WorldLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTotalToProductWeightTypeToIngrTbls : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ingridients", "WeightType", c => c.String());
            AddColumn("dbo.Products", "Total", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Total");
            DropColumn("dbo.Ingridients", "WeightType");
        }
    }
}
