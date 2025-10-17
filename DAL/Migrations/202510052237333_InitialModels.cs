namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        Ingredients = c.String(nullable: false),
                        Instructions = c.String(nullable: false),
                        PrepTimeMinutes = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        AverageRating = c.Double(nullable: false),
                        TotalRatings = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        PasswordHash = c.String(nullable: false),
                        Role = c.String(nullable: false, maxLength: 10),
                        Status = c.String(nullable: false, maxLength: 20),
                        VerificationToken = c.String(),
                        PasswordResetToken = c.String(),
                        ResetTokenExpiry = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserFollows",
                c => new
                    {
                        FollowerId = c.Int(nullable: false),
                        FollowingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FollowerId, t.FollowingId })
                .ForeignKey("dbo.Users", t => t.FollowingId)
                .ForeignKey("dbo.Users", t => t.FollowerId)
                .Index(t => t.FollowerId)
                .Index(t => t.FollowingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.UserFollows", "FollowerId", "dbo.Users");
            DropForeignKey("dbo.UserFollows", "FollowingId", "dbo.Users");
            DropIndex("dbo.UserFollows", new[] { "FollowingId" });
            DropIndex("dbo.UserFollows", new[] { "FollowerId" });
            DropIndex("dbo.Recipes", new[] { "AuthorId" });
            DropTable("dbo.UserFollows");
            DropTable("dbo.Users");
            DropTable("dbo.Recipes");
        }
    }
}
