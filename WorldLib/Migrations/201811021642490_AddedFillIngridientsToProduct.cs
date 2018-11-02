namespace WorldLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFillIngridientsToProduct : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Ingridients", "ProductId");
            AddForeignKey("dbo.Ingridients", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ingridients", "ProductId", "dbo.Products");
            DropIndex("dbo.Ingridients", new[] { "ProductId" });
        }
    }
}
