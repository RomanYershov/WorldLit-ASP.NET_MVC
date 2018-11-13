namespace WorldLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedRecipeTatble : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Recipes", "FoodCategoryId", "dbo.FoodCategories");
            DropIndex("dbo.Recipes", new[] { "FoodCategoryId" });
            RenameColumn(table: "dbo.Recipes", name: "FoodCategoryId", newName: "Category_Id");
            AlterColumn("dbo.Recipes", "Category_Id", c => c.Int());
            CreateIndex("dbo.Recipes", "Category_Id");
            AddForeignKey("dbo.Recipes", "Category_Id", "dbo.FoodCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "Category_Id", "dbo.FoodCategories");
            DropIndex("dbo.Recipes", new[] { "Category_Id" });
            AlterColumn("dbo.Recipes", "Category_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Recipes", name: "Category_Id", newName: "FoodCategoryId");
            CreateIndex("dbo.Recipes", "FoodCategoryId");
            AddForeignKey("dbo.Recipes", "FoodCategoryId", "dbo.FoodCategories", "Id", cascadeDelete: true);
        }
    }
}
