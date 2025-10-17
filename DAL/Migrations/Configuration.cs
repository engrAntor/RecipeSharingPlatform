namespace DAL.Migrations
{
    using DAL.EF.Models; 
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.EF.RecipeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.EF.RecipeContext context)
        {
            
            

            
            context.Categories.AddOrUpdate(
                c => c.Name, 
                new Category { Name = "Appetizer" },   
                new Category { Name = "Main Course" }, 
                new Category { Name = "Dessert" },     
                new Category { Name = "Salad" }        
            );

            
            context.Cuisines.AddOrUpdate(
                c => c.Name,
                new Cuisine { Name = "Italian" },      
                new Cuisine { Name = "Mexican" },      
                new Cuisine { Name = "Chinese" },      
                new Cuisine { Name = "Indian" }        
            );

            
        }
    }
}