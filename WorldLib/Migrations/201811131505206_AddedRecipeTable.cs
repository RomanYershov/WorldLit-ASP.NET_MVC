namespace WorldLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRecipeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FoodCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImageUrl = c.String(),
                        FoodCategoryId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FoodCategories", t => t.FoodCategoryId, cascadeDelete: true)
                .Index(t => t.FoodCategoryId);
            
            CreateTable(
                "dbo.RecipeComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        UserId = c.String(maxLength: 128),
                        Recipe_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipes", t => t.Recipe_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Recipe_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecipeComments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RecipeComments", "Recipe_Id", "dbo.Recipes");
            DropForeignKey("dbo.Recipes", "FoodCategoryId", "dbo.FoodCategories");
            DropIndex("dbo.RecipeComments", new[] { "Recipe_Id" });
            DropIndex("dbo.RecipeComments", new[] { "UserId" });
            DropIndex("dbo.Recipes", new[] { "FoodCategoryId" });
            DropTable("dbo.RecipeComments");
            DropTable("dbo.Recipes");
            DropTable("dbo.FoodCategories");
        }
    }
}
