using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace YogIT.LargeAzFileUpload.Repository
{
    public class LargeAzFileUploadContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.LargeAzFileUpload> LargeAzFileUpload { get; set; }

        public LargeAzFileUploadContext(ITenantManager tenantManager, IHttpContextAccessor accessor) : base(tenantManager, accessor)
        {
            // ContextBase handles multi-tenant database connections
        }
    }
}
