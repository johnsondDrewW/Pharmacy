using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using PharmDB.Models;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(PharmDB.Startup))]
namespace PharmDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Database.SetInitializer<PharmDBcontext>(new CreateDatabaseIfNotExists<PharmDBcontext>());
            //Database.SetInitializer<ApplicationDbContext>(new CreateDatabaseIfNotExists<ApplicationDbContext>());

            ConfigureAuth(app);
        }
      
    }
}
