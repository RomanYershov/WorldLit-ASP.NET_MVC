// <auto-generated />
namespace WorldLib.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
    public sealed partial class AddUserIdToCommentsTable : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(AddUserIdToCommentsTable));
        
        string IMigrationMetadata.Id
        {
            get { return "201808260748258_AddUserIdToCommentsTable"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}