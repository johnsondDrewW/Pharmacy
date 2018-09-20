namespace PharmDB.Migrations
{
    using PharmDB.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PharmDB.Models.PharmDBcontext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PharmDB.Models.PharmDBcontext context)
        {


            context.Categories.AddOrUpdate(p => p.ID,
                
                new Category{
                Name = "Pharmacy",
                Description = "All things used in a pharmacy",
                ParentCategory = "Main."
                },
                new Category{
                Name = "Drugs and Chemicals",
                Description = "All pharmasuticals that are a Drug or Chemical",
                ParentCategory = "Main,Pharmacy."
                },
                new Category{
                Name="Pills",
                Description ="All drugs and chemicals that come in pill form",
                ParentCategory = "Main,Pharmacy,Drugs and Chemicals."
                },
                new Category
                {
                 Name = "Recent1*DrewA",
                 Description = "No Recent Changes",
                 ParentCategory = "DBUtilities"
                },
                new Category
                {
                 Name = "Recent2*DrewA",
                 Description = "No Recent Changes",
                 ParentCategory = "DBUtilities"
                },
                new Category
                {
                 Name = "Recent3*DrewA",
                 Description = "No Recent Changes",
                 ParentCategory = "DBUtilities"

                }
                
                );
            context.Items.AddOrUpdate(p => p.ID,
                new Item
                {
                 Name = "Pharmacy Database",
                 Value = 0,
                 Qauntity = 1,
                 tags = "Main."
                }
                );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
