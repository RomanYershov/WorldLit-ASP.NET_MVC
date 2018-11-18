namespace WorldLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChandeRecipeCommentTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RecipeComments", "Recipe_Id", "dbo.Recipes");
            DropIndex("dbo.RecipeComments", new[] { "Recipe_Id" });
            RenameColumn(table: "dbo.RecipeComments", name: "Recipe_Id", newName: "RecipeId");
            AlterColumn("dbo.RecipeComments", "RecipeId", c => c.Int(nullable: false));
            CreateIndex("dbo.RecipeComments", "RecipeId");
            AddForeignKey("dbo.RecipeComments", "RecipeId", "dbo.Recipes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecipeComments", "RecipeId", "dbo.Recipes");
            DropIndex("dbo.RecipeComments", new[] { "RecipeId" });
            AlterColumn("dbo.RecipeComments", "RecipeId", c => c.Int());
            RenameColumn(table: "dbo.RecipeComments", name: "RecipeId", newName: "Recipe_Id");
            CreateIndex("dbo.RecipeComments", "Recipe_Id");
            AddForeignKey("dbo.RecipeComments", "Recipe_Id", "dbo.Recipes", "Id");
        }
    }
}
