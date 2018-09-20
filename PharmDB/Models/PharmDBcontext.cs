using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PharmDB.Models
{
    public class PharmDBInitializer: IDatabaseInitializer<PharmDBcontext>
    {
        public void InitializeDatabase(PharmDBcontext context)
        {
            if(context.Database.Exists() == false)
            {
                context.Database.Create();
            }
        }
    }

    public class PharmDBcontext: DbContext
    {
        public PharmDBcontext(): base()
        {
            Database.SetInitializer(new PharmDBInitializer());
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>();
            modelBuilder.Entity<Item>();
        }
    }
}