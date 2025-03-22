namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        RecipeId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Ingredients = c.String(),
                        Instructions = c.String(),
                        UsId = c.Int(nullable: false),
                        Rating = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeId)
                .ForeignKey("dbo.Users", t => t.UsId, cascadeDelete: true)
                .Index(t => t.UsId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "UsId", "dbo.Users");
            DropIndex("dbo.Recipes", new[] { "UsId" });
            DropTable("dbo.Users");
            DropTable("dbo.Recipes");
        }
    }
}
