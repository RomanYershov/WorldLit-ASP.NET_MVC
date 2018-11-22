namespace WorldLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFillInputTypeIngrTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ingridients", "InputType", c => c.String());
            DropColumn("dbo.Ingridients", "WeightType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ingridients", "WeightType", c => c.String());
            DropColumn("dbo.Ingridients", "InputType");
        }
    }
}
