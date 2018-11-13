namespace WorldLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRecipeTable2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Recipes", "Category_Id", "dbo.FoodCategories");
            DropIndex("dbo.Recipes", new[] { "Category_Id" });
            RenameColumn(table: "dbo.Recipes", name: "Category_Id", newName: "FoodCategoryId");
            AlterColumn("dbo.Recipes", "FoodCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Recipes", "FoodCategoryId");
            AddForeignKey("dbo.Recipes", "FoodCategoryId", "dbo.FoodCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "FoodCategoryId", "dbo.FoodCategories");
            DropIndex("dbo.Recipes", new[] { "FoodCategoryId" });
            AlterColumn("dbo.Recipes", "FoodCategoryId", c => c.Int());
            RenameColumn(table: "dbo.Recipes", name: "FoodCategoryId", newName: "Category_Id");
            CreateIndex("dbo.Recipes", "Category_Id");
            AddForeignKey("dbo.Recipes", "Category_Id", "dbo.FoodCategories", "Id");
        }
    }
}
