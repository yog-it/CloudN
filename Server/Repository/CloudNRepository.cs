using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using System.Threading.Tasks;

namespace YogIT.Module.CloudN.Repository
{
    public class CloudNRepository : ICloudNRepository, ITransientService
    {
        private readonly IDbContextFactory<CloudNContext> _factory;

        public CloudNRepository(IDbContextFactory<CloudNContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.CloudN> GetCloudNs(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return db.CloudN.Where(item => item.ModuleId == ModuleId).ToList();
        }

        public Models.CloudN GetCloudN(int CloudNId)
        {
            return GetCloudN(CloudNId, true);
        }

        public Models.CloudN GetCloudN(int CloudNId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.CloudN.Find(CloudNId);
            }
            else
            {
                return db.CloudN.AsNoTracking().FirstOrDefault(item => item.CloudNId == CloudNId);
            }
        }

        public Models.CloudN AddCloudN(Models.CloudN CloudN)
        {
            using var db = _factory.CreateDbContext();
            db.CloudN.Add(CloudN);
            db.SaveChanges();
            return CloudN;
        }

        public Models.CloudN UpdateCloudN(Models.CloudN CloudN)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(CloudN).State = EntityState.Modified;
            db.SaveChanges();
            return CloudN;
        }

        public void DeleteCloudN(int CloudNId)
        {
            using var db = _factory.CreateDbContext();
            Models.CloudN CloudN = db.CloudN.Find(CloudNId);
            db.CloudN.Remove(CloudN);
            db.SaveChanges();
        }


        public async Task<IEnumerable<Models.CloudN>> GetCloudNsAsync(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return await db.CloudN.Where(item => item.ModuleId == ModuleId).ToListAsync();
        }

        public async Task<Models.CloudN> GetCloudNAsync(int CloudNId)
        {
            return await GetCloudNAsync(CloudNId, true);
        }

        public async Task<Models.CloudN> GetCloudNAsync(int CloudNId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return await db.CloudN.FindAsync(CloudNId);
            }
            else
            {
                return await db.CloudN.AsNoTracking().FirstOrDefaultAsync(item => item.CloudNId == CloudNId);
            }
        }

        public async Task<Models.CloudN> AddCloudNAsync(Models.CloudN CloudN)
        {
            using var db = _factory.CreateDbContext();
            db.CloudN.Add(CloudN);
            await db.SaveChangesAsync();
            return CloudN;
        }

        public async Task<Models.CloudN> UpdateCloudNAsync(Models.CloudN CloudN)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(CloudN).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return CloudN;
        }

        public async Task DeleteCloudNAsync(int CloudNId)
        {
            using var db = _factory.CreateDbContext();
            Models.CloudN CloudN = db.CloudN.Find(CloudNId);
            db.CloudN.Remove(CloudN);
            await db.SaveChangesAsync();
        }
    }
}
