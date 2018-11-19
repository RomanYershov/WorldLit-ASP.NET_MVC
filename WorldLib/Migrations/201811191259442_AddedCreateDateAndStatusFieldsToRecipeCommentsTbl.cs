namespace WorldLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCreateDateAndStatusFieldsToRecipeCommentsTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecipeComments", "CreateDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecipeComments", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecipeComments", "Status");
            DropColumn("dbo.RecipeComments", "CreateDateTime");
        }
    }
}
