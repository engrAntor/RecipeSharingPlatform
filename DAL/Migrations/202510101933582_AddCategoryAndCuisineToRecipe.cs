namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryAndCuisineToRecipe : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.Categories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Cuisines",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                })
                .PrimaryKey(t => t.Id);

            
            Sql("INSERT INTO Categories (Name) VALUES ('General')");
            Sql("INSERT INTO Cuisines (Name) VALUES ('General')");

            
            AddColumn("dbo.Recipes", "CategoryId", c => c.Int(nullable: false, defaultValue: 1));
            AddColumn("dbo.Recipes", "CuisineId", c => c.Int(nullable: false, defaultValue: 1));

            
            CreateIndex("dbo.Recipes", "CategoryId");
            CreateIndex("dbo.Recipes", "CuisineId");
            AddForeignKey("dbo.Recipes", "CategoryId", "dbo.Categories", "Id");
            AddForeignKey("dbo.Recipes", "CuisineId", "dbo.Cuisines", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "CuisineId", "dbo.Cuisines");
            DropForeignKey("dbo.Recipes", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Recipes", new[] { "CuisineId" });
            DropIndex("dbo.Recipes", new[] { "CategoryId" });
            DropColumn("dbo.Recipes", "CuisineId");
            DropColumn("dbo.Recipes", "CategoryId");
            
        }
    }
}
