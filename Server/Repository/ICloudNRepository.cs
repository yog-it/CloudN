using System.Collections.Generic;
using System.Threading.Tasks;

namespace YogIT.Module.CloudN.Repository
{
    public interface ICloudNRepository
    {
        IEnumerable<Models.CloudN> GetCloudNs(int ModuleId);
        Models.CloudN GetCloudN(int CloudNId);
        Models.CloudN GetCloudN(int CloudNId, bool tracking);
        Models.CloudN AddCloudN(Models.CloudN CloudN);
        Models.CloudN UpdateCloudN(Models.CloudN CloudN);
        void DeleteCloudN(int CloudNId);

        Task<IEnumerable<Models.CloudN>> GetCloudNsAsync(int ModuleId);
        Task<Models.CloudN> GetCloudNAsync(int CloudNId);
        Task<Models.CloudN> GetCloudNAsync(int CloudNId, bool tracking);
        Task<Models.CloudN> AddCloudNAsync(Models.CloudN CloudN);
        Task<Models.CloudN> UpdateCloudNAsync(Models.CloudN CloudN);
        Task DeleteCloudNAsync(int CloudNId);
    }
}
