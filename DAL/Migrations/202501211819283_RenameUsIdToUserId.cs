namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameUsIdToUserId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Recipes", name: "UsId", newName: "UserId");
            RenameIndex(table: "dbo.Recipes", name: "IX_UsId", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Recipes", name: "IX_UserId", newName: "IX_UsId");
            RenameColumn(table: "dbo.Recipes", name: "UserId", newName: "UsId");
        }
    }
}
