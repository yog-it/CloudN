using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace YogIT.Module.CloudN.Repository
{
    public class CloudNContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.CloudN> CloudN { get; set; }

        public CloudNContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.CloudN>().ToTable(ActiveDatabase.RewriteName("YogITCloudN"));
        }
    }
}
